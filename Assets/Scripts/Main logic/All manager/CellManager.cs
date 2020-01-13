using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public List<GameObject> ListCell;

    public void ChangeCellTag(string varietyCell, string newTag)
    {
        for (int index = 0; index < ListCell.Count; index++)
        {
            if (ListCell[index].GetComponent<CellInformation>().VarietyCell == varietyCell) ListCell[index].tag = newTag;
        }
    }
}