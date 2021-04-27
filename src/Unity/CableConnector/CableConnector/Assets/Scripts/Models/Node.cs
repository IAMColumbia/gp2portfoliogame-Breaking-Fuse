using System;
using System.Collections.Generic;
using System.Text;

namespace CableConnector.Models
{
    public class Node
    {
        //Designated Position of this node in a given space - "←↑→↓" | "0123"
        public enum Positions { Left, Up, Right, Down }

        public virtual Positions Position { get; set; }

        public virtual bool IsValid { get; set; }

        public Node(Positions position, bool isValid)
        {
            this.Position = position;
            this.IsValid = isValid;
        }
    }
}