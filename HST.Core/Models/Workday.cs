using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HST.Core.Models
{
    public class Workday
    {
        public List<ExerciseWorkday> Exercises { get; set; }

        public bool Done
        {
            get
            {
                return Exercises.All(e => e.Done);
            }
        }

        public ExerciseWorkday this[string name]
        {
            get
            {
                return Exercises.First(ew => ew.Exercise.Name == name);
            }
        }

        public Workday(List<ExerciseWorkday> exercises)
        {
            Exercises = exercises;
        }
    }
}
