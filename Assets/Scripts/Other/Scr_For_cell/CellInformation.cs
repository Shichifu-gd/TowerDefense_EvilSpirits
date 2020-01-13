using UnityEngine;

public class CellInformation : MonoBehaviour
{
    public string VarietyCell { get; set; } = "standart";
    public bool EndOfRoad;
    public Color MainColor;
    public Color AdditionalColor;

    private void OnMouseEnter()
    {
        if (VarietyCell == "tower") GetComponent<SpriteRenderer>().color = AdditionalColor;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = MainColor;
    }
}