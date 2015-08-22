using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HST.Core.Models;
using System.Collections.Generic;

namespace HST.Tests
{
    [TestClass]
    public class HSTWorkoutTests
    {
        HSTWorkout workout;

        [TestInitialize]
        public void Setup()
        {
            workout = new HSTWorkout("workout", new List<ExerciseConfiguration> 
            {
                new ExerciseConfiguration
                {
                    Exercise = new Exercise("Squat"),
                    RMs = new Dictionary<RepEnum,double>
                    {
                        {RepEnum.RM15, 30},
                        {RepEnum.RM10, 40},
                        {RepEnum.RM5, 50}
                    }
                }
            });
        }

        [TestMethod]
        public void ShouldConfigureCyclesAccordingToRMWeights()
        {
            ExerciseConfiguration squatConfiguration = workout["Squat"];

            Cycle firstCycle = workout.Cycle1;
            double rm15SquatWeight = squatConfiguration[RepEnum.RM15];

            double[] cycleWeights15 = new double[] 
            {
                rm15SquatWeight * .5,
                rm15SquatWeight * .6,
                rm15SquatWeight * .7,
                rm15SquatWeight * .8,
                rm15SquatWeight * .9,
                rm15SquatWeight * 1.0
            };

            for (int i = 0; i < cycleWeights15.Length; ++i)
            {
                Workday workday = firstCycle[i];
                double weightUsed = workday["Squat"].Weight;

                Assert.AreEqual(cycleWeights15[i], weightUsed);
            }

            Cycle secondCycle = workout.Cycle2;
            double rm10SquatWeight = squatConfiguration[RepEnum.RM10];
            double[] cycleWeights10 = new double[] 
            {
                rm10SquatWeight * .5,
                rm10SquatWeight * .6,
                rm10SquatWeight * .7,
                rm10SquatWeight * .8,
                rm10SquatWeight * .9,
                rm10SquatWeight * 1.0
            };

            for (int i = 0; i < cycleWeights10.Length; ++i)
            {
                Workday workday = secondCycle[i];
                double weightUsed = workday["Squat"].Weight;

                Assert.AreEqual(cycleWeights10[i], weightUsed);
            }

            Cycle thirdCycle = workout.Cycle3;
            double rm5SquatWeight = squatConfiguration[RepEnum.RM5];
            double[] cycleWeights5 = new double[] 
            {
                rm5SquatWeight * .5,
                rm5SquatWeight * .6,
                rm5SquatWeight * .7,
                rm5SquatWeight * .8,
                rm5SquatWeight * .9,
                rm5SquatWeight * 1.0
            };

            for (int i = 0; i < cycleWeights5.Length; ++i)
            {
                Workday workday = thirdCycle[i];
                double weightUsed = workday["Squat"].Weight;

                Assert.AreEqual(cycleWeights5[i], weightUsed);
            }
        }

        [TestMethod]
        public void ShouldGenerateNewWorkoutFromOldOne()
        {
            double percentageRaised = .05;
            var newWorkout = workout.Continue("continuation", percentageRaised);
            ExerciseConfiguration squatConfiguration = newWorkout["Squat"];
            ExerciseConfiguration originaSquatConfiguration = workout["Squat"];

            Assert.AreEqual(originaSquatConfiguration.RMs[RepEnum.RM15] +
                           (originaSquatConfiguration.RMs[RepEnum.RM15] *
                            percentageRaised),
                            squatConfiguration.RMs[RepEnum.RM15]);

            Assert.AreEqual(originaSquatConfiguration.RMs[RepEnum.RM10] +
                           (originaSquatConfiguration.RMs[RepEnum.RM10] *
                            percentageRaised),
                            squatConfiguration.RMs[RepEnum.RM10]);

            Assert.AreEqual(originaSquatConfiguration.RMs[RepEnum.RM5] +
                           (originaSquatConfiguration.RMs[RepEnum.RM5] *
                            percentageRaised),
                            squatConfiguration.RMs[RepEnum.RM5]);

            Assert.AreEqual("continuation", newWorkout.Name);
        }
    }
}
