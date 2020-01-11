using System.Collections;
using UnityEngine;

public class PathGenerationForOpponents : MonoBehaviour
{
    private ComponentsForPathGenerationForOpponents components;
    private DeterminantVectorCurrentPosition directions;
    public EnemySpawnManager enemySpawnManager;

    public GameObject VectorDeterminant;

    private bool WorkflowSwitching = true;
    private bool CompleteShutdown;
    private bool FirstCell;

    private int YPosition = 0;
    private int NumberOfCellsTraversedEast;

    private string CurrentForbiddenDirection;
    private string OldForbiddenDirection = "none";

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
        yield return new WaitForSeconds(0.001f);
        ChangeCell();
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
            if (OldForbiddenDirection != CurrentForbiddenDirection) SpawnDirectionPoints();
            OldForbiddenDirection = CurrentForbiddenDirection;
        }
        else OldForbiddenDirection = CurrentForbiddenDirection;
    }

    private void SpawnDirectionPoints()
    {
        GameObject point_DP = Instantiate(components.PreDirectionPoint, VectorDeterminant.transform.position, Quaternion.identity);
        point_DP.transform.parent = GameObject.FindGameObjectWithTag("DirectionPoints").transform;
    }

    private void HeightCount(int numSide)
    {
        if (numSide == 0) YPosition++;
        if (numSide == 1) YPosition--;
    }

    private void ChangeCell()
    {
        GameObject cell = VectorDeterminant.GetComponent<DeterminantVectorCurrentPosition>().CurrentCell;
        if (cell.GetComponent<CellInformation>().EndOfRoad == false)
        {
            cell.GetComponent<SpriteRenderer>().sprite = components.Trail;
            cell.GetComponent<CellInformation>().VarietyCell = "trail";
            cell = null;
        }
        else
        {
            CompleteShutdown = true;
            SpawnFinishPoint();
            VectorDeterminant.SetActive(false);
        }
    }

    private void SpawnFinishPoint()
    {
        GameObject point_F = Instantiate(components.PreFinish, VectorDeterminant.transform.position, Quaternion.identity);
        point_F.transform.parent = GameObject.FindGameObjectWithTag("DirectionPoints").transform;
    }
}