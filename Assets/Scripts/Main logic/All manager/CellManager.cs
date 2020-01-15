using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    private SpritesDecor spritesDecor;
    public List<GameObject> ListCell;
    public GameObject PreDecor;

    public void Awake()
    {
        spritesDecor = GetComponent<SpritesDecor>();
    }

    public void ChangeCellTag(string varietyCell, string newTag)
    {
        for (int index = 0; index < ListCell.Count; index++)
        {
            if (ListCell[index].GetComponent<CellInformation>().VarietyCell == varietyCell) ListCell[index].tag = newTag;
        }
    }

    #region For spawn decor
    public void ArrangingDecor()
    {
        int random;
        for (int index = 0; index < ListCell.Count; index++)
        {
            if (ListCell[index].GetComponent<CellInformation>().VarietyCell == "standart")
            {
                random = Random.Range(0, 11);
                if (random == 5 || random == 8 || random == 2) ChoiceDecor(index);
            }
        }
    }

    public void ChoiceDecor(int index)
    {
        int randomVarieties = Random.Range(0, 5);
        int randomDecor;
        if (randomVarieties == 0)
        {
            randomDecor = Random.Range(0, spritesDecor.DecorStone.Length);
            SpawnDecor(spritesDecor.DecorStone[randomDecor], index);
        }
        if (randomVarieties == 1)
        {
            randomDecor = Random.Range(0, spritesDecor.DecorTree.Length);
            SpawnDecor(spritesDecor.DecorTree[randomDecor], index);
        }
        if (randomVarieties == 2 || randomVarieties == 3 || randomVarieties == 4)
        {
            randomDecor = Random.Range(0, spritesDecor.DecorGrass.Length);
            SpawnDecor(spritesDecor.DecorGrass[randomDecor], index);
        }
    }

    public void SpawnDecor(Sprite sprite, int index)
    {
        GameObject newDecor = Instantiate(PreDecor, ListCell[index].transform.position, Quaternion.identity);
        newDecor.transform.parent = GameObject.FindGameObjectWithTag("DecorSlot").transform;
        newDecor.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    #endregion
}