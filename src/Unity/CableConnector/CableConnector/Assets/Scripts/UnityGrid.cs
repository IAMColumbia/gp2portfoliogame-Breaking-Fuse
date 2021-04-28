using CableConnector.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityGrid : MonoBehaviour
{
    public GameObject StraightCablePrefab;
    public GameObject CurvedCablePrefab;
    public GameObject FourWayCablePrefab;
    public enum GridStates { Unsolved, Solved }
    public virtual GameObject[,] CableTileGrid { get; set; }
    public virtual GameObject LastConnectedTile { get; set; }
    public GridStates State = GridStates.Unsolved;
    public int columnSize = 6;
    public int rowSize = 6;
    public float xScreenOffset = -2.7f;
    public float yScreenOffset = -3.8f;
    public float padding = 1.5f;

    private void Start()
    {
        CableTileGrid = new GameObject[this.columnSize, this.rowSize];
        FillWCableTiles();
        ShuffleGrid(CableTileGrid);
    }

    private void Update()
    {
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
                pos = new Vector3((i * padding) + xScreenOffset, (j * padding) + yScreenOffset, -1);
                if (i == 0)
                    //Instantiate at least one row of Straight Cables
                    CableTileGrid[i, j] = Instantiate(StraightCablePrefab, pos, Quaternion.identity, this.transform) as GameObject;
                else if (i == 1)
                    //Instantiate at least one row of Curved Cables
                    CableTileGrid[i, j] = Instantiate(CurvedCablePrefab, pos, Quaternion.identity, this.transform) as GameObject;
                else
                {
                    int type = UnityEngine.Random.Range(0, Enum.GetNames(typeof(CableTile.CableTypes)).Length);
                    switch (type)
                    {
                        case 0:
                            CableTileGrid[i, j] = Instantiate(StraightCablePrefab, pos, Quaternion.identity, this.transform) as GameObject;
                            break;
                        case 1:
                            CableTileGrid[i, j] = Instantiate(CurvedCablePrefab, pos, Quaternion.identity, this.transform) as GameObject;
                            break;
                        case 2:
                            CableTileGrid[i, j] = Instantiate(FourWayCablePrefab, pos, Quaternion.identity, this.transform) as GameObject;
                            break;
                        default:
                            CableTileGrid[i, j] = Instantiate(StraightCablePrefab, pos, Quaternion.identity, this.transform) as GameObject;
                            break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Shuffle this Grid's CableTiles
    /// </summary>
    /// <param name="values"></param>
    protected virtual void ShuffleGrid(GameObject[,] values)
    {
        int numOfRows = values.GetUpperBound(0) + 1;
        int numOfColumns = values.GetUpperBound(1) + 1;
        int numOfTiles = numOfRows * numOfColumns;

        for (int i = 0; i < numOfTiles - 1; i++)
        {
            // Pick a random cell between i and the end of the array.
            int j = UnityEngine.Random.Range(i, numOfTiles);

            // Convert to row/column indexes.
            int row_i = i / numOfColumns;
            int col_i = i % numOfColumns;
            int row_j = j / numOfColumns;
            int col_j = j % numOfColumns;

            // Swap cells i and j.
            GameObject temp = values[row_i, col_i]; //temp = tile1
            temp.transform.position = values[row_i, col_i].transform.position;

            values[row_i, col_i] = values[row_j, col_j]; // tile1 = tile2
            values[row_i, col_i].transform.position = values[row_j, col_j].transform.position;

            values[row_j, col_j] = temp; //tile2 = temp(tile1)
            values[row_j, col_j].transform.position = temp.transform.position;
        }

        this.LastConnectedTile = CableTileGrid[0, 0]; //Designate new StartTile
    }

    public void Draw()
    {
        Vector3 pos;
        for (int i = 0; i < this.rowSize; i++)
        {
            for (int j = 0; j < this.columnSize; j++)
            {
                //set position
                pos = new Vector3((i * padding) + xScreenOffset, (j * padding) + yScreenOffset, -1);
                CableTileGrid[i, j].transform.position = pos;

                //set rotation - Hardcoded Hack
                if (Input.anyKeyDown)
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                    if (hit.collider.gameObject.tag == "cableTile") //Null Reference Exception?
                    {
                        if (Input.GetMouseButtonDown(0))
                        {

                            CableTileGrid[i, j].GetComponent<UnityCableTile>().cableTile.Cable.Nodes = CableRotator.Instance.RotateLeft(CableTileGrid[i, j].GetComponent<UnityCableTile>().cableTile.Cable.Nodes);
                            CableTileGrid[i, j].transform.Rotate(new Vector3(0, 0, 90));
                        }
                        else if (Input.GetMouseButtonDown(1))
                        {
                            CableTileGrid[i, j].GetComponent<UnityCableTile>().cableTile.Cable.Nodes = CableRotator.Instance.RotateRight(CableTileGrid[i, j].GetComponent<UnityCableTile>().cableTile.Cable.Nodes);
                            CableTileGrid[i, j].transform.Rotate(new Vector3(0, 0, -90));
                        }

                    }

                }
            }
        }
    }

}
