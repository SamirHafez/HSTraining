using System.Collections.Generic;
using System.Linq;

namespace HST.Core.Models
{
    public class HSTWorkout
    {

        public string Name { get; private set; }
        public List<ExerciseConfiguration> Exercises { get; private set; }

        double[] rmPercentages;

        public Cycle Cycle1 { get; set; }
        public Cycle Cycle2 { get; set; }
        public Cycle Cycle3 { get; set; }
        public Cycle Cycle4 { get; set; }

        public ExerciseConfiguration this[string name]
        {
            get
            {
                return Exercises.First(ec => ec.Exercise.Name == name);
            }
        }

        public HSTWorkout(string name, List<ExerciseConfiguration> exercises)
        {
            Name = name;
            Exercises = exercises;

            rmPercentages = new double[] { .75, .80, .85, .90, .95, 1 };

            Cycle1 = new Cycle(rmPercentages.Select(rmPer => CreateWorkday(Exercises, RepEnum.RM15, rmPer)).ToList());
            Cycle2 = new Cycle(rmPercentages.Select(rmPer => CreateWorkday(Exercises, RepEnum.RM10, rmPer)).ToList());
            Cycle3 = new Cycle(rmPercentages.Select(rmPer => CreateWorkday(Exercises, RepEnum.RM5, rmPer)).ToList());
            Cycle4 = new Cycle(rmPercentages.Select(rmPer => CreateWorkday(Exercises, RepEnum.RM5, rmPer)).ToList());
        }

        private Workday CreateWorkday(List<ExerciseConfiguration> exercises, RepEnum rep, double maxPercentage)
        {
            var exerciseWorkDays = from exercise in exercises
                                   select new ExerciseWorkday(exercise.Exercise, exercise.RMs[rep] * maxPercentage);

            return new Workday(exerciseWorkDays.ToList());
        }

        public HSTWorkout Continue(string name, double percentageRaised)
        {
            var exercises = Exercises.Select(ec =>
            {
                var rms = new Dictionary<RepEnum, double>();
                foreach (var rm in ec.RMs)
                    rms.Add(rm.Key, rm.Value + (rm.Value * percentageRaised));

                return new ExerciseConfiguration
                {
                    Exercise = ec.Exercise,
                    RMs = rms
                };
            });

            return new HSTWorkout(name, exercises.ToList());
        }
    }
}