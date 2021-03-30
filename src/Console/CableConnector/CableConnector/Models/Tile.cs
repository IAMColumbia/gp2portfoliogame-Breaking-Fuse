using System;
using System.Collections.Generic;
using System.Text;

namespace CableConnector
{
    public class Tile
    {
        //TODO Tiles only active when Revealed
        protected virtual bool IsRevealed { get; set; }

        public Tile()
        {

        }
    }
}