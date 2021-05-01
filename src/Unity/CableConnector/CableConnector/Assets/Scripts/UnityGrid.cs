using CableConnector.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class UnityGrid : MonoBehaviour
{
    public enum GridStates { None, Unsolved, Solved }
    public virtual GameObject[,] CableTileGrid { get; set; }
    public virtual GameObject LastConnectedTile { get; set; }

    public GameObject TimedEvaluator;

    //Store the tile that was last selected
    private GameObject lastSelectedTile;

    //Parent Object for Instantiated Cables
    private GameObject cableObjectParent;

    private int wins;
    private int winStreak;

    #region UnityCableTile Prefabs
    public GameObject StraightCablePrefab;
    public GameObject CurvedCablePrefab;
    public GameObject FourWayCablePrefab;

    #endregion

    #region GridSetup
    public GridStates State = GridStates.None;
    public int columnSize = 6;
    public int rowSize = 6;
    public float xScreenOffset = -2.7f;
    public float yScreenOffset = -3.8f;
    public float padding = 1.5f;
    #endregion

    private void Update()
    {
        if (State != GridStates.None)
            Draw();
    }

    /// <summary>
    /// Fills this Grid with random CableTiles and guarantees at least one row of StraightCables and Curved Cables
    /// </summary>
    protected virtual void FillWCableTiles()
    {
        Vector3 pos;
        for (int i = 0; i < this.rowSize; i++)
        {
            for (int j = 0; j < this.columnSize; j++)
            {
                pos = new Vector3((j * padding) + xScreenOffset, ((i * padding) * -1) + yScreenOffset, -1);

                if (i == 0)
                    //Instantiate at least one row of Straight Cables
                    CableTileGrid[i, j] = Instantiate(StraightCablePrefab, pos, Quaternion.identity, cableObjectParent.transform) as GameObject;
                else if (i == 1)
                    //Instantiate at least one row of Curved Cables
                    CableTileGrid[i, j] = Instantiate(CurvedCablePrefab, pos, Quaternion.identity, cableObjectParent.transform) as GameObject;
                else
                {
                    int type = UnityEngine.Random.Range(0, Enum.GetNames(typeof(CableTile.CableTypes)).Length);
                    switch (type)
                    {
                        case 0:
                            CableTileGrid[i, j] = Instantiate(StraightCablePrefab, pos, Quaternion.identity, cableObjectParent.transform) as GameObject;
                            break;
                        case 1:
                            CableTileGrid[i, j] = Instantiate(CurvedCablePrefab, pos, Quaternion.identity, cableObjectParent.transform) as GameObject;
                            break;
                        case 2:
                            CableTileGrid[i, j] = Instantiate(FourWayCablePrefab, pos, Quaternion.identity, cableObjectParent.transform) as GameObject;
                            break;
                        default:
                            CableTileGrid[i, j] = Instantiate(StraightCablePrefab, pos, Quaternion.identity, cableObjectParent.transform) as GameObject;
                            break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Shuffle this Grid's CableTiles.
    /// </summary>
    /// <param name="values"></param>
    protected virtual void ShuffleGrid(GameObject[,] values)
    {
        int numOfRows = values.GetUpperBound(0) + 1;
        int numOfColumns = values.GetUpperBound(1) + 1;
        int numOfTiles = numOfRows * numOfColumns;

        for (int i = 0; i < numOfTiles - 1; i++)
        {
            // Pick a random tile between i and the end of the array.
            int j = UnityEngine.Random.Range(i, numOfTiles);

            // Convert to row/column indexes.
            int row_i = i / numOfColumns;
            int col_i = i % numOfColumns;
            int row_j = j / numOfColumns;
            int col_j = j % numOfColumns;

            // Swap tiles i and j.
            GameObject temp = values[row_i, col_i]; //temp = tile1
            values[row_i, col_i] = values[row_j, col_j]; //tile1 = tile2
            values[row_j, col_j] = temp; //tile2 = temp(tile1)
        }
    }

    /// <summary>
    /// Draw the CableTileGrid at their appropriate positions.
    /// </summary>
    protected virtual void Draw()
    {
        Vector3 pos;
        for (int i = 0; i < this.rowSize; i++)
        {
            for (int j = 0; j < this.columnSize; j++)
            {
                pos = new Vector3((j * padding) + xScreenOffset, ((i * padding) * -1) + yScreenOffset, -1);
                CableTileGrid[i, j].transform.position = pos;
            }
        }
    }

    /// <summary>
    /// Receive an update from a clicked cableTile and handle input.
    /// </summary>
    /// <param name="cableTile"></param>
    /// <param name="key"></param>
    public void CableTileUpdate(UnityCableTile cableTile, KeyCode key)
    {
        if (lastSelectedTile != null)
        {
            if (cableTile.gameObject == lastSelectedTile)
            {
                //Reveal / RotateLeft on Mouse 0
                if (key == KeyCode.Mouse0)
                {
                    if (cableTile.CableState == CableTile.CableStates.Hidden)
                        cableTile.Reveal();
                    else if (cableTile.CableState == CableTile.CableStates.Revealed)
                        cableTile.Rotate(UnityCableTile.TransformRotationDirections.Left);
                }
                //RotateRight on Mouse 1
                else if (key == KeyCode.Mouse1)
                    cableTile.Rotate(UnityCableTile.TransformRotationDirections.Right);
            }
            else
            {
                //Select the Tile on Mouse 0 then Reveal if it is Hidden
                if (key == KeyCode.Mouse0)
                    SelectTile();
                //Swap with the last selected tile on Mouse 1
                else if (key == KeyCode.Mouse1)
                {
                    //SwapTiles(cableTile.gameObject, lastSelectedTile);
                }
            }
        }
        else
        {
            //Select the Tile on Mouse 0 then Reveal if it is Hidden
            if (key == KeyCode.Mouse0)
                SelectTile();
        }

        void SelectTile()
        {
            cableTile.Select();
            if (lastSelectedTile != null) lastSelectedTile.GetComponent<UnityCableTile>().Deselect();
            lastSelectedTile = cableTile.gameObject;

            if (cableTile.CableState == CableTile.CableStates.Hidden)
                cableTile.Reveal();
        }
    }


    public void Reset()
    {
        Destroy(cableObjectParent);
        DestroyAllCableTiles();

        this.State = GridStates.Unsolved;
        cableObjectParent = new GameObject("Cables");
        CableTileGrid = new GameObject[this.columnSize, this.rowSize];
        FillWCableTiles();
        ShuffleGrid(CableTileGrid);
        TimedEvaluator.GetComponent<CountdownTimer>().Reset();
    }

    /// <summary>
    /// Evaluate this grid.
    /// </summary>
    public void RunEvaluation()
    {
        for (int i = 0; i < (this.rowSize * this.columnSize); i++)
        {
            UnityGridEvaluator.Instance.Evaluate(this);
        }

        if (this.State == GridStates.Solved)
        {
            GameManager.Win();
            Debug.Log("Win");
        }
        else
        {
            GameManager.Lose();
            Debug.Log("Lose");
            this.State = GridStates.None;
        }


    }


    /// <summary>
    /// If the grid is not already null, nullify it.
    /// </summary>
    private void DestroyAllCableTiles()
    {
        if (CableTileGrid != null)
        {
            for (int i = 0; i < this.rowSize; i++)
            {
                for (int j = 0; j < this.columnSize; j++)
                {
                    Destroy(CableTileGrid[i, j]);
                }
            }
        }

    }
}
