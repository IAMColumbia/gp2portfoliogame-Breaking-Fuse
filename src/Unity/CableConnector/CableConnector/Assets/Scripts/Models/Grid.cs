using System;
using System.Collections.Generic;
using System.Text;

namespace CableConnector.Models
{
    public class Grid
    {
        public enum GridStates { Unsolved, Solved}

        #region Fields/Props
        public virtual CableTile[,] CableTileGrid { get; set; }
        public virtual CableTile LastConnectedTile { get; set; }
        public virtual GridStates State { get; set; }
        protected virtual int columnSize { get; set; }
        protected virtual int rowSize { get; set; }
        #endregion

        #region Constructors
        public Grid() : this(6,6) { }
        public Grid(int gridColumnAndRowSize) : this(gridColumnAndRowSize, gridColumnAndRowSize) { }
        public Grid(int gridColumnSize, int gridRowSize)
        {
            this.State = GridStates.Unsolved;
            this.columnSize = gridColumnSize;
            this.rowSize = gridRowSize;
            CableTileGrid = new CableTile[this.columnSize, this.rowSize];
            FillWCableTiles();
            ShuffleGrid(CableTileGrid);
        }
        #endregion


        /// <summary>
        /// Fills this Grid with random CableTiles and guarantees at least one row of StraightCables and Curved Cables
        /// </summary>
        protected virtual void FillWCableTiles()
        {
            Random rand = new Random();

            for (int i = 0; i < this.rowSize; i++)
            {
                for(int j = 0; j < this.columnSize; j++)
                {
                    if (i == 0)
                        //Instantiate at least one row of Straight Cables
                        CableTileGrid[i, j] = new CableTile(CableTile.CableTypes.Straight);
                    else if (i == 1)
                        //Instantiate at least one row of Curved Cables
                        CableTileGrid[i, j] = new CableTile(CableTile.CableTypes.Curved);

                    else
                    {
                        //Instantiate a random type of Cable at this CableTile
                        CableTileGrid[i, j] = new CableTile((CableTile.CableTypes) rand.Next(0, Enum.GetNames(typeof(CableTile.CableTypes)).Length));
                    }

                }
            }
        }

        /// <summary>
        /// Shuffle this Grid's CableTiles
        /// </summary>
        /// <param name="values"></param>
        protected virtual void ShuffleGrid(CableTile[,] values)
        {
            int numOfRows = values.GetUpperBound(0) + 1;
            int numOfColumns = values.GetUpperBound(1) + 1;
            int numOfTiles = numOfRows * numOfColumns;

            Random rand = new Random();
            for (int i = 0; i < numOfTiles - 1; i++)
            {
                // Pick a random cell between i and the end of the array.
                int j = rand.Next(i, numOfTiles);

                // Convert to row/column indexes.
                int row_i = i / numOfColumns;
                int col_i = i % numOfColumns;
                int row_j = j / numOfColumns;
                int col_j = j % numOfColumns;

                // Swap cells i and j.
                CableTile temp = values[row_i, col_i];
                values[row_i, col_i] = values[row_j, col_j];
                values[row_j, col_j] = temp;
            }
            
            this.LastConnectedTile = CableTileGrid[0, 0]; //Designate new StartTile
        }


    }
}