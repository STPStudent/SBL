using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitComponent : HealthControl, IEnumerable<UnitComponent>
{
    [SerializeField] private UnitType type;
    [SerializeField] public int PlayerIndex;
    internal UnitComponent previousComponent;
    internal UnitComponent nextComponent;
    private float acceleration = 3;
    private Rigidbody2D rigidBodyComponent;
    private bool used;
    internal Vector2 finishPosition = Vector2.zero;
    private new SpriteRenderer renderer;
    

    void Start()
    {
        //При создании юнита  ищем компонент 
        //Rigidbody, который отвечает за физику обьекта
        //SpriteRenderer который отвечает за спрайт обьекта
        //Устанавливаем начальное здорвье на максимальное значение
        //В зависимости от того какому игроку пренадлежит юнит
        //добавляет его в соответсвующии разделы
        rigidBodyComponent = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        this.SetHealth();
        if(PlayerIndex == 0)
            UnitControl.AddUnit(this);
        else
            EvilBrain.AddUnit(this);
    }

    public override void DestroyObject()
    {
        //Когда погибает юнит уберает ссылки на себя
        //у других членов связаного списка
        //А потом destoy уничтожает игровой обьект(себя)
        if(previousComponent != null)
            previousComponent.nextComponent = nextComponent;
        if(nextComponent != null)
            nextComponent.previousComponent = previousComponent;
        Destroy(this.gameObject);
    }

    public IEnumerator<UnitComponent> GetEnumerator()
    {
        //Позволяет бегать по связаному списку
        yield return this;
        var pathItem = previousComponent;
            while (pathItem != null)
            {
                yield return pathItem;
                pathItem = pathItem.previousComponent;
            }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public void Deselect()
    {
        //Отменяет выделение юнита
        //И возращает старый цвет
        used = false;
        renderer.material.color = Color.white;
    }

    public void Select()
    {
        //Выделяет юнита 
        //и меняет ему цвет на более зеленый
        used = true;
        renderer.material.color = Color.green;
    }
    
    void Update()
    {
        //находит вектор направления из текущей точки в указаную
        var startPosition = rigidBodyComponent.position;
        var a = finishPosition - startPosition;

        if (a.x < 0)
            renderer.flipX = false;
        else
            renderer.flipX = true;


        if(a.magnitude < 0.1
        || finishPosition == Vector2.zero)
        {
            rigidBodyComponent.velocity = Vector2.zero;
            return;
        }
        rigidBodyComponent.velocity = a.normalized * acceleration;
    }

    void OnMouseOver()
    {
        CursorControl.SetAttackCursor();
    }
    void OnMouseExit()
    {
        CursorControl.SetNormalCursor();
    }

}
