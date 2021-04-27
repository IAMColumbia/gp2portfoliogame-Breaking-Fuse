using System;
using System.Collections.Generic;
using System.Text;

namespace CableConnector.Models
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
            //Nodes = CableRotator.Instance.GetRandomRotation(Nodes); //force a random rotation upon Instantiation
        }
    }
}
