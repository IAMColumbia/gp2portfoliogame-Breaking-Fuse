using System;
using System.Collections.Generic;
using System.Text;

namespace CableConnector
{
    public class Grid
    {
        #region Fields/Props
        protected virtual CableTile[,] grid { get; set; }
        protected virtual int GridColumnSize { get; set; }
        protected virtual int GridRowSize { get; set; }
        #endregion

        #region Constructors
        public Grid() : this(6,6) { }
        public Grid(int gridColumnAndRowSize) : this(gridColumnAndRowSize, gridColumnAndRowSize) { }
        public Grid(int gridColumnSize, int gridRowSize)
        {
            this.GridColumnSize = gridColumnSize;
            this.GridRowSize = gridRowSize;
            grid = new CableTile[this.GridColumnSize, this.GridRowSize];
            FillWCableTiles();
            ShuffleGrid(grid);
        }
        #endregion


        /// <summary>
        /// Fills this Grid with random CableTiles and guarantees at least one row of StraightCables and Curved Cables
        /// </summary>
        protected virtual void FillWCableTiles()
        {
            Random rand = new Random();

            for (int i = 0; i < this.GridRowSize; i++)
            {
                for(int j = 0; j < this.GridColumnSize; j++)
                {
                    if (i == 0)
                        //Instantiate at least one row of Straight Cables
                        grid[i, j] = new CableTile(CableTile.CableTypes.Straight);
                    else if (i == 1)
                        //Instantiate at least one row of Curved Cables
                        grid[i, j] = new CableTile(CableTile.CableTypes.Curved);

                    else
                    {
                        //Instantiate a random type of Cable at this CableTile
                        grid[i, j] = new CableTile((CableTile.CableTypes) rand.Next(0, Enum.GetNames(typeof(CableTile.CableTypes)).Length));
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
        }

        /// <summary>
        /// Console App Only - Loop through this Grid's CableTiles and perform each Draw().
        /// </summary>
        public virtual void Draw()
        {
            Console.Clear();
            for (int i = 0; i < this.GridRowSize; i++)
            {
                for (int j = 0; j < this.GridColumnSize; j++)
                {
                    Console.Write(string.Format("{0}" +
                        "", grid[i, j].Draw()));
                }
                Console.Write(Environment.NewLine);
            }
        }


    }
}