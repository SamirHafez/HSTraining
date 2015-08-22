using System.Collections.Generic;
using System.Linq;

namespace HST.Core.Models
{
    public enum RepEnum
    {
        RM15,
        RM10,
        RM5
    }

    public class ExerciseConfiguration
    {
        public Exercise Exercise { get; set; }

        public Dictionary<RepEnum, double> RMs { get; set; }

        public double this[int index]
        {
            get
            {
                return RMs.ElementAt(index).Value;
            }
        }

        public double this[RepEnum rm]
        {
            get
            {
                return RMs[rm];
            }
        }
    }
}
