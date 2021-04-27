using System;
using System.Collections.Generic;
using System.Text;

namespace CableConnector.Models
{
    /// <summary>
    /// Singleton Grid-Evaluator/Solver that can prepare a grid for solving and connect the Last-known ConnectedTile
    /// </summary>
    public class Evaluator
    {
        //Private Static instance
        private static Evaluator instance;

        //Private Constructor
        private Evaluator() { }

        //Accessible Public Instance
        public static Evaluator Instance
        {
            get
            {
                if (instance == null)
                    instance = new Evaluator();
                return instance;
            }
        }

        /// <summary>
        /// Evaluate the grid's tiles and update for a new valid connection if possible.
        /// </summary>
        /// <param name="grid">The grid to be evaluated.</param>
        public void Evaluate(Grid grid)
        {
            CableTile lastConnectedTile = grid.LastConnectedTile;

            if (grid.State == Grid.GridStates.Unsolved)
            {
                //i - vertical, j - horizontal
                for (int i = 0; i < grid.CableTileGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.CableTileGrid.GetLength(1); j++)
                    {

                        if (grid.CableTileGrid[i, j] == lastConnectedTile)
                        {
                            //Check if this is the EndTile and mark Solved IF it has a valid Right
                            if (grid.CableTileGrid[i, j] == grid.CableTileGrid[grid.CableTileGrid.GetLength(0) - 1, grid.CableTileGrid.GetLength(1) - 1])
                                grid.State = Grid.GridStates.Solved;

                            //Check Tile to Right
                            else if (CanConnectRight(i, j))
                            {
                                grid.CableTileGrid[i, j + 1].State = CableTile.CableStates.Connected;
                                grid.LastConnectedTile = grid.CableTileGrid[i, j + 1];
                            }
                            //Check Tile to Bottom
                            else if (CanConnectBottom(i, j))
                            {
                                grid.CableTileGrid[i + 1, j].State = CableTile.CableStates.Connected;
                                grid.LastConnectedTile = grid.CableTileGrid[i + 1, j];
                            }
                            //Check Tile to Top
                            else if (CanConnectTop(i, j))
                            {
                                grid.CableTileGrid[i - 1, j].State = CableTile.CableStates.Connected;
                                grid.LastConnectedTile = grid.CableTileGrid[i - 1, j];
                            }
                            //Check Tile to Left
                            else if (CanConnectLeft(i, j))
                            {
                                grid.CableTileGrid[i, j - 1].State = CableTile.CableStates.Connected;
                                grid.LastConnectedTile = grid.CableTileGrid[i, j - 1];
                            }
                        }

                    }
                }

            }

            #region Surrounding-Tile Checkers
            bool CanConnectRight(int i, int j)
            {
                //Return false Immediately if it goes beyond grid bounds.
                if (j+1 > grid.CableTileGrid.GetLength(1) - 1)
                    return false; 

                if (grid.CableTileGrid[i, j].Cable.Nodes[2].IsValid && 
                    grid.CableTileGrid[i, j + 1].Cable.Nodes[0].IsValid &&
                    grid.CableTileGrid[i, j + 1].State == CableTile.CableStates.Revealed)
                    return true;

                return false;
            }
            bool CanConnectBottom(int i, int j)
            {
                //Return false Immediately if it goes beyond grid bounds.
                if (i + 1 > grid.CableTileGrid.GetLength(0) - 1)
                    return false;

                if (grid.CableTileGrid[i, j].Cable.Nodes[3].IsValid &&
                    grid.CableTileGrid[i + 1, j].Cable.Nodes[1].IsValid &&
                    grid.CableTileGrid[i + 1, j].State == CableTile.CableStates.Revealed)
                    return true;

                return false;
            }
            bool CanConnectTop(int i, int j)
            {
                if (i - 1 < 0)
                    return false;

                if (grid.CableTileGrid[i, j].Cable.Nodes[1].IsValid &&
                    grid.CableTileGrid[i - 1, j].Cable.Nodes[3].IsValid &&
                    grid.CableTileGrid[i - 1, j].State == CableTile.CableStates.Revealed)
                    return true;

                return false;
            }
            bool CanConnectLeft(int i, int j)
            {
                if (j - 1 < 0)
                    return false;

                if (grid.CableTileGrid[i, j].Cable.Nodes[0].IsValid &&
                    grid.CableTileGrid[i, j - 1].Cable.Nodes[2].IsValid &&
                    grid.CableTileGrid[i, j - 1].State == CableTile.CableStates.Revealed)
                    return true;

                return false;
            }
            #endregion

        }

        /// <summary>
        /// Force the first tile of a Grid to have a valid Left + (Bottom or Right) Node by rotating the tile.
        /// </summary>
        /// <param name="startTile">The first CableTile in a CableTiles Matrix of a Grid Object.</param>
        public void PrepareStartTile(CableTile startTile)
        {
            bool tileIsPrepared = false;
            while(!tileIsPrepared)
            {
                if (NodesAreReady(startTile.Cable.Nodes[0]))
                    tileIsPrepared = true;
            }
            startTile.State = CableTile.CableStates.Connected;
            

            //Get Valid Left Node first then get a valid Output Node.
            bool NodesAreReady(Node n)
            {
                //Check for a valid Left Node with the cable's current rotation
                if (n.IsValid)
                    return OutputNodeIsReady(startTile.Cable.Nodes[2], startTile.Cable.Nodes[3]);
                else if (!n.IsValid)
                {
                    startTile.Cable.Nodes = CableRotator.Instance.RotateRight(startTile.Cable.Nodes);
                }
                return false;
            }

            //Get a Valid Output Node. If none available. Force another Rotation
            bool OutputNodeIsReady(Node right_n, Node bottom_n)
            {
                if (right_n.IsValid || bottom_n.IsValid)
                    return true;
                else
                {
                    startTile.Cable.Nodes = CableRotator.Instance.RotateRight(startTile.Cable.Nodes);
                    return false;
                }
            }

        }

        /// <summary>
        /// Force the last tile of a Grid to have a valid Right + (Left or Up) Node by rotating the tile.
        /// </summary>
        /// <param name="endTile">The last CableTile in a CableTiles Matrix of a Grid Object.</param>
        public void PrepareEndTile(CableTile endTile)
        {
            bool tileIsPrepared = false;
            while (!tileIsPrepared)
            {
                if (NodesAreReady(endTile.Cable.Nodes[2]))
                    tileIsPrepared = true;
            }
            

            //Get Valid Left Node first then get a valid Output Node.
            bool NodesAreReady(Node n)
            {
                //Check for a valid Left Node with the cable's current rotation
                if (n.IsValid)
                    return OutputNodeIsReady(endTile.Cable.Nodes[0], endTile.Cable.Nodes[1]);
                else if (!n.IsValid)
                {
                    endTile.Cable.Nodes = CableRotator.Instance.RotateRight(endTile.Cable.Nodes);
                }
                return false;
            }

            //Get a Valid Output Node. If none available. Force another Rotation
            bool OutputNodeIsReady(Node left_n, Node top_n)
            {
                if (left_n.IsValid || top_n.IsValid)
                    return true;
                else
                {
                    endTile.Cable.Nodes = CableRotator.Instance.RotateRight(endTile.Cable.Nodes);
                    return false;
                }
            }

        }

    }
}
