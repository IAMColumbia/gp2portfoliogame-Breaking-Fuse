using System;
using System.Collections.Generic;
using System.Text;

namespace CableConnector.Models
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
            //Nodes = CableRotator.Instance.GetRandomRotation(Nodes); //force a random rotation upon Instantiation
        }

    }
}
