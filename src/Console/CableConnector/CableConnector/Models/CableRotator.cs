using System;
using System.Collections.Generic;
using System.Text;

namespace CableConnector
{
    /// <summary>
    /// Singleton Rotator designed to rotate the Nodes within a Cable (Modify which Nodes are valid)).
    /// </summary>
    public class CableRotator
    {
        //Private Static instance
        private static CableRotator instance;

        //Private Constructor
        private CableRotator() { }

        //Accessible Public instance
        public static CableRotator Instance
        {
            get
            {
                if (instance == null) 
                    instance = new CableRotator();
                return instance;
            }
        }
        
        public Node[] RotateLeft(Node[] nodes)
        {
            Node[] newNodes = nodes;
            //[0]123
            bool temp = nodes[0].IsValid;
            //123[null]
            Array.Copy(nodes, 1, newNodes, 0, nodes.Length - 1);
            //123[0]
            newNodes[nodes.Length - 1] = new Node(Node.Positions.Down, temp);
            return newNodes;
        }
        public Node[] RotateRight(Node[] nodes)
        {
            Node[] newNodes = nodes;
            //012[3]
            bool temp = nodes[nodes.Length - 1].IsValid;
            //[null]012
            Array.Copy(nodes, 0, newNodes, 1, nodes.Length - 1);
            //[3]012
            newNodes[0] = new Node(Node.Positions.Left, temp);
            return newNodes;
        }

        /// <summary>
        /// Randomly Rotates a given set of nodes a few times clockwise.
        /// </summary>
        /// <param name="nodes">A set of nodes from a Cable Object</param>
        /// <returns></returns>
        public Node[] GetRandomRotation(Node[] nodes)
        {
            Random rand = new Random();
            for (int i = 0; i < rand.Next(4); i++)
            {
                nodes = RotateLeft(nodes);
            }
            return nodes;
        }

    }
}
