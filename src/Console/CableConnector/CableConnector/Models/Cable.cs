using System;
using System.Collections.Generic;
using System.Text;

namespace CableConnector
{
    /// <summary>
    /// A Cable with 4 Nodes. Specific Nodes are valid based on the type of Cable.
    /// </summary>
    public abstract class Cable
    {
        protected virtual Node[] Nodes { get; set; }


        /// <summary>
        /// Console App Only - Draw this cable
        /// </summary>
        /// <returns>Returns an "X" showing if this was not overwritten.</returns>
        public virtual string Draw()
        {
            return "X";
        }
    }
}