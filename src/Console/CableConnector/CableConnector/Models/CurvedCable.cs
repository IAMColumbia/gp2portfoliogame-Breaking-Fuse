using System;
using System.Collections.Generic;
using System.Text;

namespace CableConnector
{
    /// <summary>
    /// A Curved Cable - a cable that has two valid, adjacent nodes (Default: Left/Up) 
    /// Console Drawing: " ┘ ┐ └ ┌ "
    /// </summary>
    public class CurvedCable : Cable
    {
        public CurvedCable() : base()
        {
            Nodes = new Node[]
            {
                new Node(Node.Positions.Left, true),
                new Node(Node.Positions.Up, true),
                new Node(Node.Positions.Right, false),
                new Node(Node.Positions.Down, false)
            };
            Nodes = CableRotator.Instance.GetRandomRotation(Nodes); // Forced One clockwise turn
        }

        /// <summary>
        /// Console App Only - Draw this cable
        /// </summary>
        /// <returns>Returns a specific drawing of curved cable depending on its valid Nodes.</returns>
        public override string Draw()
        {
            if (this.Nodes[0].IsValid && this.Nodes[1].IsValid)
                return "┘";
            else if (this.Nodes[0].IsValid && this.Nodes[3].IsValid)
            {
                return "┐";
            }
            else if (this.Nodes[1].IsValid && this.Nodes[2].IsValid)
            {
                return "└";
            }
            else if (this.Nodes[2].IsValid && this.Nodes[3].IsValid)
            {
                return "┌";
            }
            //Error
            else
            {
                return "X";
            }
        }
    }
}
