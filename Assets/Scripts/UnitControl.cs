using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitControl : MonoBehaviour
{
    [SerializeField] private int maxUnits = 100;
    [SerializeField] private Image mainRect;
    private Rect rect;
    private Vector2 startPosition, endPosition;
    private Color original, clear, curColor;
    private bool canDraw;
	private static UnitComponent units;
	private static List<UnitComponent> unitSelected;
	private static int unitCount = 0;
	
	public static void AddUnit(UnitComponent comp)
	{
		//Добовляет юнита в связаный список
		Debug.Log(units);
		units.nextComponent = comp;
		comp.previousComponent = units;
		units = comp;
		unitCount++;
	}
    void Awake()
    {
		//Запускается до начала прогрузки обьктов
		//устанавливает начальные значения
		unitCount = 0;
		units = new UnitComponent();
		unitSelected = new List<UnitComponent>();
		original = mainRect.color;
		clear = original;
		clear.a = 0;
		curColor = clear;
		mainRect.color = clear;
    }

    void Draw()
	{
		//Рисует прямоугольник по координатам верторов start position и endposition
		endPosition = Input.mousePosition;
		if(startPosition == endPosition || !canDraw) 
            return;

		curColor = original;

		rect = new Rect(Mathf.Min(endPosition.x, startPosition.x),
			Screen.height - Mathf.Max(endPosition.y, startPosition.y),
			Mathf.Max(endPosition.x, startPosition.x) - Mathf.Min(endPosition.x, startPosition.x),
			Mathf.Max(endPosition.y, startPosition.y) - Mathf.Min(endPosition.y, startPosition.y)
		);

		mainRect.rectTransform.sizeDelta = new Vector2(rect.width, rect.height);
		var sizeDelta = mainRect.rectTransform.sizeDelta;

		mainRect.rectTransform.anchoredPosition = new Vector2(rect.x + sizeDelta.x/2, 
			Mathf.Max(endPosition.y, startPosition.y) - sizeDelta.y/2);
	}

	void SetSelect()
	{
		//Ищет какие юниты находятся внутри прямоугольника и выделяет их
		foreach(var unit in units)
		{
			if(unit != null)
			{
				var position = Camera.main.WorldToScreenPoint(unit.transform.position);
				if(rect.Contains(new Vector2(position.x, Screen.height - position.y)))
				{
					unit.Select();
					unitSelected.Add(unit);
				}
			}
		}
	}

	public static void SetDeselect()
	{
		//Отменяет выделение юнитов
		foreach(var unit in unitSelected)
			if(unit != null)
				unit.Deselect();
		unitSelected = new List<UnitComponent>();
	}

	void Update()
    {
		//Когда нажимают левую кнопку мыши отменяет выделеине
		//и говорит что мы можем рисовать прямоугольник
        if(Input.GetMouseButtonDown(0))
        {
            rect = new Rect();
            startPosition = Input.mousePosition;
            canDraw = true;
			SetDeselect();
        }
		//Когда отпускают левую кнопку мыши
		//убирает цвет нарисованому прямоугольнику
        if (Input.GetMouseButtonUp(0))
        {
            curColor = clear;
            canDraw = false;
			SetSelect();
        }
		//Когда нажимают правую кнопку мыши для каждого выбраного юника меняет финишную позицию
		if(Input.GetMouseButtonDown(1))
		{
			foreach (var comp in unitSelected)
            	comp.finishPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}

        Draw();

        mainRect.color = Color.Lerp(mainRect.color, curColor, 10 * Time.deltaTime);
    }
}
