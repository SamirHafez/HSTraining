using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HST.Core.Models
{
    public class ExerciseWorkday
    {
        public Exercise Exercise { get; set; }

        public double Weight { get; set; }

        public bool Done { get; set; }

        public ExerciseWorkday(Exercise exercise, double weight, bool done = false)
        {
            Exercise = exercise;
            Weight = weight;
            Done = done;
        }
    }
}
