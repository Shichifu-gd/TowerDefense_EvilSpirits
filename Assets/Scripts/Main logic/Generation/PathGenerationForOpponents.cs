﻿using System.Collections;
using UnityEngine;

public class PathGenerationForOpponents : MonoBehaviour
{
    private ComponentsForPathGenerationForOpponents components;
    private DeterminantVectorCurrentPosition directions;
    public EnemySpawnManager enemySpawnManager;
    public CellManager cellManager;
    public UIManager uIManager;

    public GameObject VectorDeterminant;
    private GameObject DirectionOfVerificationNorth;
    private GameObject DirectionOfVerificationSouth;
    private GameObject DirectionOfVerificationEast;

    private int YPosition = 0;
    private int NumberOfCellsTraversedEast;
    public int NumberForMaximumRandomNumber = 10;

    private string CurrentForbiddenDirection;
    private string OldForbiddenDirection = "none";

    private bool WorkflowSwitching = true;
    private bool CompleteShutdown;
    private bool FirstCell;

    private void Awake()
    {
        components = GetComponent<ComponentsForPathGenerationForOpponents>();
        directions = VectorDeterminant.GetComponent<DeterminantVectorCurrentPosition>();
    }

    private void FixedUpdate()
    {
        if (CompleteShutdown == false && WorkflowSwitching == true && VectorDeterminant) StartCoroutine(TrailGeneration());
    }

    public IEnumerator TrailGeneration()
    {
        WorkflowSwitching = false;
        MovingVectorDeterminant();
        VectorDeterminant.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        PavingTrail();
        RefreshCells();
        AddTerritoryForTowers();
        VectorDeterminant.SetActive(false);
        WorkflowSwitching = true;
    }

    private void MovingVectorDeterminant()
    {
        Vector2 vector = GetVector();
        CheckForSpawnDirectionPoints();
        VectorDeterminant.transform.position = new Vector2(vector.x, vector.y);
    }

    private Vector2 GetVector()
    {
        Vector2 vector = directions.East.transform.position;
        int randomNumber;
        if (NumberOfCellsTraversedEast == 0 && FirstCell == true)
        {
            randomNumber = Random.Range(0, 3);
            if (YPosition <= -2 && randomNumber != 2)
            {
                if (CurrentForbiddenDirection == "east") randomNumber = 0;
                else randomNumber = 2;
            }
            if (YPosition >= 2 && randomNumber != 2)
            {
                if (CurrentForbiddenDirection == "east") randomNumber = 1;
                else randomNumber = 2;
            }
            if (randomNumber == 0 && CurrentForbiddenDirection == "south" || randomNumber == 1 && CurrentForbiddenDirection == "north") randomNumber = 2;
            if (randomNumber == 0)
            {
                vector = directions.North.transform.position;
                CurrentForbiddenDirection = "north";
                NumberOfCellsTraversedEast = 0;
            }
            if (randomNumber == 1)
            {
                vector = directions.South.transform.position;
                CurrentForbiddenDirection = "south";
                NumberOfCellsTraversedEast = 0;
            }
            if (randomNumber == 2)
            {
                vector = directions.East.transform.position;
                CurrentForbiddenDirection = "east";
                NumberOfCellsTraversedEast++;
            }
            HeightCount(randomNumber);
        }
        else
        {
            vector = directions.East.transform.position;
            NumberOfCellsTraversedEast = 0;
            FirstCell = true;
        }
        return vector;
    }

    private void CheckForSpawnDirectionPoints()
    {
        if (OldForbiddenDirection != "none")
        {
            if (OldForbiddenDirection != CurrentForbiddenDirection) SpawnAdditionalComponents(components.PreDirectionPoint, "DirectionPoints");
            OldForbiddenDirection = CurrentForbiddenDirection;
        }
        else OldForbiddenDirection = CurrentForbiddenDirection;
    }

    private void HeightCount(int numSide)
    {
        if (numSide == 0) YPosition++;
        if (numSide == 1) YPosition--;
    }

    private void PavingTrail()
    {
        if (directions.CurrentCell.GetComponent<CellInformation>().EndOfRoad == false) ChangeCell(directions.CurrentCell, components.Trail, "trail");
        else NextProcess();
    }

    private void NextProcess()
    {
        CompleteShutdown = true;
        SpawnAdditionalComponents(components.PreFinish, "DirectionPoints");
        ChangeCell(directions.CurrentCell, null, "finish");
        VectorDeterminant.SetActive(false);
        cellManager.ChangeCellTag("tower", "CellForTower");
        cellManager.ArrangingDecor();
        enemySpawnManager.StartEnemySpawn();
        uIManager.OnPanel();
    }

    private void SpawnAdditionalComponents(GameObject prefab, string assignedView)
    {
        GameObject spawnObject = Instantiate(prefab, VectorDeterminant.transform.position, Quaternion.identity);
        spawnObject.transform.parent = GameObject.FindGameObjectWithTag(assignedView).transform;
    }

    private void RefreshCells()
    {
        DirectionOfVerificationNorth = directions.North.GetComponent<GettingCellReference>().CellPosition;
        DirectionOfVerificationSouth = directions.South.GetComponent<GettingCellReference>().CellPosition;
        DirectionOfVerificationEast = directions.East.GetComponent<GettingCellReference>().CellPosition;
    }

    private void AddTerritoryForTowers()
    {
        if (DirectionOfVerificationNorth.GetComponent<CellInformation>().VarietyCell == "standart" && DetermineWhetherPossibleChangeTypeCell()) ChangeCell(DirectionOfVerificationNorth, components.ZoneForTower, "tower");
        else if (DirectionOfVerificationNorth.GetComponent<CellInformation>().VarietyCell == "standart") ChangeCell(DirectionOfVerificationNorth, null, "none");
        if (DirectionOfVerificationSouth.GetComponent<CellInformation>().VarietyCell == "standart" && DetermineWhetherPossibleChangeTypeCell()) ChangeCell(DirectionOfVerificationSouth, components.ZoneForTower, "tower");
        else if (DirectionOfVerificationSouth.GetComponent<CellInformation>().VarietyCell == "standart") ChangeCell(DirectionOfVerificationSouth, null, "none");
        if (DirectionOfVerificationEast.GetComponent<CellInformation>().VarietyCell == "standart" && DetermineWhetherPossibleChangeTypeCell()) ChangeCell(DirectionOfVerificationEast, components.ZoneForTower, "tower");
        else if (DirectionOfVerificationEast.GetComponent<CellInformation>().VarietyCell == "standart") ChangeCell(DirectionOfVerificationEast, null, "none");
    }

    private bool DetermineWhetherPossibleChangeTypeCell()
    {
        int randomNumber;
        randomNumber = Random.Range(0, NumberForMaximumRandomNumber);
        if (randomNumber != 0 && randomNumber != 4 && randomNumber != 6) return true;
        else return false;
    }

    private void ChangeCell(GameObject cellReferenceReceived, Sprite newSprite, string assignedView)
    {
        GameObject cell = cellReferenceReceived;
        if (newSprite) cell.GetComponent<SpriteRenderer>().sprite = newSprite;
        if (cell.GetComponent<CellInformation>().VarietyCell != "trail") cell.GetComponent<CellInformation>().VarietyCell = assignedView;
    }
}