using System;
using System.Collections.Generic;
using System.Text;

namespace CableConnector.Models
{
    public class CableTile
    {
        //For the different types of cables
        public enum CableTypes { Straight, Curved, FourWay }
        //For the status of this tile. "Revealed" = Not Connected, but the cable sprite is revealed
        public enum CableStates { Hidden, Revealed, Connected}
        public CableTypes Type { get; set; }
        public CableStates State { get; set; }
        public Cable Cable { get; set; }

        public CableTile() : this(CableTypes.Straight) { }
        public CableTile(CableTypes type)
        {
            this.Type = type;
            InstantiateCable();
        }

        /// <summary>
        /// Instantiate a specific Cable for this CableTile
        /// </summary>
        /// <param name="type">Desired CableType</param>
        private void InstantiateCable()
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
    }
}