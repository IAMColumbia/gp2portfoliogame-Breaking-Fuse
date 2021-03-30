using System;
using System.Collections.Generic;
using System.Text;

namespace CableConnector
{
    /// <summary>
    /// A FourWay Cable - a cable with all four nodes being valid 
    /// Console Drawing: " ┼ "
    /// </summary>
    public class FourWayCable : Cable
    {
        public FourWayCable() : base()
        {
            Nodes = new Node[]
            {
                new Node(Node.Positions.Left, true),
                new Node(Node.Positions.Up, true),
                new Node(Node.Positions.Right, true),
                new Node(Node.Positions.Down, true)
            };
        }

        /// <summary>
        /// Console App Only - Draw this cable
        /// </summary>
        /// <returns>Returns a drawing of a Cross/Four-way Cable</returns>
        public override string Draw()
        {
            return "┼";
        }
    }
}
