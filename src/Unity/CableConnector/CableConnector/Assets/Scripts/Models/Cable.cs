using System;
using System.Collections.Generic;
using System.Text;

namespace CableConnector.Models
{
    /// <summary>
    /// A Cable with 4 Nodes. Specific Nodes are valid based on the type of Cable.
    /// </summary>
    public abstract class Cable
    {
        public virtual Node[] Nodes { get; set; }

    }
}