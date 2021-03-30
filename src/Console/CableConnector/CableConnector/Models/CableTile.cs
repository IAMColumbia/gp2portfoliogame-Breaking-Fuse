using System;
using System.Collections.Generic;
using System.Text;

namespace CableConnector
{
    public class CableTile : Tile
    {
        public enum CableTypes { Straight, Curved, FourWay }

        public CableTypes Type { get; set; }
        public Cable Cable { get; set; }
        public bool IsConnected { get; set; }

        public CableTile() : this(CableTypes.Straight) { }
        public CableTile(CableTypes type) : base()
        {
            this.Type = type;
            InstantiateCable(Type);
        }

        /// <summary>
        /// Instantiate a specific Cable for this CableTile
        /// </summary>
        /// <param name="type">Desired CableType</param>
        private void InstantiateCable(CableTypes type)
        {
            switch (this.Type)
            {
                case CableTypes.Straight:
                    Cable = new StraightCable();
                    break;
                case CableTypes.Curved:
                    Cable = new CurvedCable();
                    break;
                case CableTypes.FourWay:
                    Cable = new FourWayCable();
                    break;
            }
        }

        /// <summary>
        /// Call this CableTile's Cable.Draw().
        /// </summary>
        /// <returns>Returns the drawing appropriate for the Cable's rotation</returns>
        public string Draw()
        {
            return Cable.Draw();
        }


    }
}