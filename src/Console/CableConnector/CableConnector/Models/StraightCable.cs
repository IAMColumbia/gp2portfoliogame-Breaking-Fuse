using System;
using System.Collections.Generic;
using System.Text;

namespace CableConnector
{
    /// <summary>
    /// A Straight Cable - a cable with two valid nodes on opposite sides (Default: Left/Right)
    /// Console Drawing: " ─ | "
    /// </summary>
    public class StraightCable : Cable
    {
        public StraightCable() : base()
        {
            Nodes = new Node[]
            {
                new Node(Node.Positions.Left, true),
                new Node(Node.Positions.Up, false),
                new Node(Node.Positions.Right, true),
                new Node(Node.Positions.Down, false)
            };
            Nodes = CableRotator.Instance.GetRandomRotation(Nodes); // Forced One clockwise turn
        }

        /// <summary>
        /// Console App Only - Draw this cable
        /// </summary>
        /// <returns>Returns a horizontal or vertical drawing of a straight cable depending on its valid Nodes.</returns>
        public override string Draw()
        {
            //If Left and Right are the Valid Nodes
            if (this.Nodes[0].IsValid && this.Nodes[2].IsValid)
                return "─";
            //If Up and Down are the Valid Nodes
            else if (this.Nodes[1].IsValid && this.Nodes[3].IsValid)
                return "|";
            //Error
            else
            {
                return "X";
            }
        }
    }
}
