using System;
using System.Collections.Generic;
using System.Text;
using CableConnector.Models;

namespace CableConnector.ViewModels
{
    public class ConsoleGrid
    {
        #region Fields/Props
        public Grid GridModel { get; set; }
        public int RowColumnSize { get; set; }
        Evaluator eval = Evaluator.Instance;
        #endregion

        #region Constructors
        public ConsoleGrid() : this(6) { }
        public ConsoleGrid(int RowColumnSize)
        {
            this.RowColumnSize = RowColumnSize;
            GridModel = new Grid(RowColumnSize);
            eval.PrepareStartTile(GridModel.CableTileGrid[0, 0]);
            eval.PrepareEndTile(GridModel.CableTileGrid[GridModel.CableTileGrid.GetLength(0) - 1, 
                                                        GridModel.CableTileGrid.GetLength(1) - 1]);
        }
        #endregion


        /// <summary>
        /// Call on the evaluator class to evaluate this Grid's Model.
        /// </summary>
        public void Evaluate()
        {
            eval.Evaluate(GridModel);
        }

        /// <summary>
        /// Draw this Grid's CableTiles and perform an appropriate Drawing to the Console.
        /// </summary>
        public void Draw()
        {
            if (this.GridModel.IsSolved)
            {
                Console.WriteLine("\nThis grid is Solved!");
            }
            else
            {
                Console.Clear();
                for (int i = 0; i < this.RowColumnSize; i++)
                {
                    for (int j = 0; j < this.RowColumnSize; j++)
                    {
                        if (GridModel.CableTileGrid[i, j].IsConnected)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        switch (GridModel.CableTileGrid[i, j].Type)
                        {
                            case CableTile.CableTypes.Straight:
                                DrawStraightCable((StraightCable)GridModel.CableTileGrid[i, j].Cable);
                                break;
                            case CableTile.CableTypes.Curved:
                                DrawCurvedCable((CurvedCable)GridModel.CableTileGrid[i, j].Cable);
                                break;
                            case CableTile.CableTypes.FourWay:
                                DrawFourWayCable((FourWayCable)GridModel.CableTileGrid[i, j].Cable);
                                break;
                            default:
                                DrawDefaultCable();
                                break;
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(Environment.NewLine);
                }
            }
        }

        #region Draw Methods
        /// <summary>
        /// Draw the default cable.
        /// </summary>
        private void DrawDefaultCable()
        {
            Console.Write("X");
        }

        /// <summary>
        /// Draw a horizontal or vertical straight cable depending on its valid Nodes.
        /// </summary>
        private void DrawStraightCable(StraightCable cable)
        {
            //If Left and Right are the Valid Nodes
            if (cable.Nodes[0].IsValid && cable.Nodes[2].IsValid)
                Console.Write("─");
            //If Up and Down are the Valid Nodes
            else if (cable.Nodes[1].IsValid && cable.Nodes[3].IsValid)
                Console.Write("│");
            //Error
            else
            {
                DrawDefaultCable();
            }
        }

        /// <summary>
        /// Draw an appropriate curved cable depending on its valid Nodes.
        /// </summary>
        private void DrawCurvedCable(CurvedCable cable)
        {
            if (cable.Nodes[0].IsValid && cable.Nodes[1].IsValid)
                Console.Write("┘");
            else if (cable.Nodes[0].IsValid && cable.Nodes[3].IsValid)
                Console.Write("┐");
            else if (cable.Nodes[1].IsValid && cable.Nodes[2].IsValid)
                Console.Write("└");
            else if (cable.Nodes[2].IsValid && cable.Nodes[3].IsValid)
                Console.Write("┌");
            //Error
            else
            {
                DrawDefaultCable();
            }
        }

        /// <summary>
        /// Draw a Cross/Four-way Cable.
        /// </summary>
        private void DrawFourWayCable(FourWayCable cable)
        {
            Console.Write("┼");
        }
        #endregion

    }
}
