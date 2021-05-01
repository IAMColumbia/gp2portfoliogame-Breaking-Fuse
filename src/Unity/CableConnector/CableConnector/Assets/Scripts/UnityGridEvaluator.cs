using System;
using CableConnector.Models;
using UnityEngine;


namespace Assets.Scripts
{
    /// <summary>
    /// Singleton Grid-Evaluator/Solver that can prepare a grid for solving and connect the Last-known ConnectedTile
    /// </summary>
    public class UnityGridEvaluator
    {
        //Private Static instance
        private static UnityGridEvaluator instance;

        //Private Constructor
        private UnityGridEvaluator() { }

        //Accessible Public Instance
        public static UnityGridEvaluator Instance
        {
            get
            {
                if (instance == null)
                    instance = new UnityGridEvaluator();
                return instance;
            }
        }

        /// <summary>
        /// Evaluate the grid's tiles and update for a new valid connection if possible.
        /// </summary>
        /// <param name="grid">The grid to be evaluated.</param>
        public void Evaluate(UnityGrid grid)
        {
            if (grid.State == UnityGrid.GridStates.Unsolved)
            {
                //Check for valid starting tile before checking the grid and connect it if necessary
                if (grid.CableTileGrid[0,0].GetComponent<UnityCableTile>().cableTile.Cable.Nodes[0].IsValid)
                {
                    if (grid.CableTileGrid[0, 0].GetComponent<UnityCableTile>().CableState != CableTile.CableStates.Connected)
                    {
                        grid.CableTileGrid[0, 0].GetComponent<UnityCableTile>().CableState = CableTile.CableStates.Connected;
                        grid.LastConnectedTile = grid.CableTileGrid[0, 0];
                    }
                    else
                    {
                        UnityCableTile lastConnectedTile = grid.LastConnectedTile.GetComponent<UnityCableTile>();

                        //i - vertical, j - horizontal
                        for (int i = 0; i < grid.CableTileGrid.GetLength(0); i++)
                        {
                            for (int j = 0; j < grid.CableTileGrid.GetLength(1); j++)
                            {

                                if (grid.CableTileGrid[i, j].GetComponent<UnityCableTile>() == lastConnectedTile)
                                {
                                    //Check if this is the EndTile and mark Solved IF it has a valid Right
                                    if (grid.CableTileGrid[i, j] == grid.CableTileGrid[grid.CableTileGrid.GetLength(0) - 1, grid.CableTileGrid.GetLength(1) - 1])
                                    {
                                        grid.State = UnityGrid.GridStates.Solved;
                                        Debug.Log("Solved!");
                                    }

                                    //Check Tile to Right
                                    else if (CanConnectRight(i, j))
                                    {
                                        grid.CableTileGrid[i, j + 1].GetComponent<UnityCableTile>().CableState = CableTile.CableStates.Connected;
                                        grid.LastConnectedTile = grid.CableTileGrid[i, j + 1];
                                    }
                                    //Check Tile to Bottom
                                    else if (CanConnectBottom(i, j))
                                    {
                                        grid.CableTileGrid[i + 1, j].GetComponent<UnityCableTile>().CableState = CableTile.CableStates.Connected;
                                        grid.LastConnectedTile = grid.CableTileGrid[i + 1, j];
                                    }
                                    //Check Tile to Top
                                    else if (CanConnectTop(i, j))
                                    {
                                        grid.CableTileGrid[i - 1, j].GetComponent<UnityCableTile>().CableState = CableTile.CableStates.Connected;
                                        grid.LastConnectedTile = grid.CableTileGrid[i - 1, j];
                                    }
                                    //Check Tile to Left
                                    else if (CanConnectLeft(i, j))
                                    {
                                        grid.CableTileGrid[i, j - 1].GetComponent<UnityCableTile>().CableState = CableTile.CableStates.Connected;
                                        grid.LastConnectedTile = grid.CableTileGrid[i, j - 1];
                                    }
                                }

                            }
                        }

                    }
                }

            }

            #region Surrounding-Tile Checkers
            bool CanConnectRight(int i, int j)
            {
                //Return false Immediately if it goes beyond grid bounds.
                if (j + 1 > grid.CableTileGrid.GetLength(1) - 1)
                {
                    return false;
                }
                if (grid.CableTileGrid[i, j].GetComponent<UnityCableTile>().cableTile.Cable.Nodes[2].IsValid &&
                    grid.CableTileGrid[i, j + 1].GetComponent<UnityCableTile>().cableTile.Cable.Nodes[0].IsValid &&
                    grid.CableTileGrid[i, j + 1].GetComponent<UnityCableTile>().CableState == CableTile.CableStates.Revealed)
                    return true;

                return false;
            }
            bool CanConnectBottom(int i, int j)
            {
                //Return false Immediately if it goes beyond grid bounds.
                if (i + 1 > grid.CableTileGrid.GetLength(0) - 1)
                {
                    return false;
                }

                if (grid.CableTileGrid[i, j].GetComponent<UnityCableTile>().cableTile.Cable.Nodes[3].IsValid &&
                    grid.CableTileGrid[i + 1, j].GetComponent<UnityCableTile>().cableTile.Cable.Nodes[1].IsValid &&
                    grid.CableTileGrid[i + 1, j].GetComponent<UnityCableTile>().CableState == CableTile.CableStates.Revealed)
                    return true;

                return false;
            }
            bool CanConnectTop(int i, int j)
            {
                if (i - 1 < 0)
                {
                    return false;
                }

                if (grid.CableTileGrid[i, j].GetComponent<UnityCableTile>().cableTile.Cable.Nodes[1].IsValid &&
                    grid.CableTileGrid[i - 1, j].GetComponent<UnityCableTile>().cableTile.Cable.Nodes[3].IsValid &&
                    grid.CableTileGrid[i - 1, j].GetComponent<UnityCableTile>().CableState == CableTile.CableStates.Revealed)
                    return true;

                return false;
            }
            bool CanConnectLeft(int i, int j)
            {
                if (j - 1 < 0)
                {
                    return false;
                }

                if (grid.CableTileGrid[i, j].GetComponent<UnityCableTile>().cableTile.Cable.Nodes[0].IsValid &&
                    grid.CableTileGrid[i, j - 1].GetComponent<UnityCableTile>().cableTile.Cable.Nodes[2].IsValid &&
                    grid.CableTileGrid[i, j - 1].GetComponent<UnityCableTile>().CableState == CableTile.CableStates.Revealed)
                    return true;

                return false;
            }
            #endregion

        }

    }
}
