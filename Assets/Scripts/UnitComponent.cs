using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitComponent : HealthControl, IEnumerable<UnitComponent>
{
    [SerializeField] public int PlayerIndex;
    internal UnitComponent previousComponent;
    internal UnitComponent nextComponent;
    [SerializeField] private float acceleration = 3;
    private Rigidbody2D rigidBodyComponent;
    private bool isChoose;
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
        SetHealth();
        if (PlayerIndex == 0)
            UnitControl.AddUnit(this);
        else
            EvilBrain.AddUnit(this);
    }

    public override void DestroyObject()
    {
        //Когда погибает юнит уберает ссылки на себя
        //у других членов связаного списка
        //А потом destoy уничтожает игровой обьект(себя)
        if (previousComponent != null)
            previousComponent.nextComponent = nextComponent;
        if (nextComponent != null)
            nextComponent.previousComponent = previousComponent;
        Destroy(gameObject);

        if (PlayerIndex == 0)
            UnitControl.unitCount--;
        else
            EvilBrain.unitCount--;
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
        //Отменяет выделение юнита и возращает старый цвет
        isChoose = false;
        renderer.material.color = Color.white;
    }

    public void Select()
    {
        //Выделяет юнита и меняет ему цвет на зеленый
        isChoose = true;
        renderer.material.color = Color.green;
    }

    void Update()
    {
        var k = new Vector2(
            Mathf.Clamp(transform.position.x, MainCamera.leftX, MainCamera.rightX),
            Mathf.Clamp(transform.position.y, MainCamera.leftY, MainCamera.rightY)
        );
        transform.position = k;
        //находит вектор направления из текущей точки в указаную
        var startPosition = rigidBodyComponent.position;
        var vectorDifference = finishPosition - startPosition;

        //Отражает спрайт по горизонтали во время смены движения
        renderer.flipX = !(vectorDifference.x < 0);

        if (vectorDifference.magnitude < 2
            || finishPosition == Vector2.zero)
        {
            rigidBodyComponent.velocity = Vector2.zero;
            return;
        }

        rigidBodyComponent.velocity = vectorDifference.normalized * acceleration;
    }

    // private void OnMouseOver()
    // {
    //     CursorControl.SetAttackCursor();
    // }
    //
    // private void OnMouseExit()
    // {
    //     CursorControl.SetNormalCursor();
    // }

    void OnTriggerStay2D(Collider2D other)
    {
        transform.position += (transform.position - other.transform.position)
            .normalized;
        if(other.gameObject.name.Contains("Bomb"))
            Destroy(other.gameObject);
    }
}