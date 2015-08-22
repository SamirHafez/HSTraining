using System.Collections.Generic;

namespace HST.Core.Utils
{
    public enum WeightEnum
    {
        KG,
        LBS
    }

    public static class WeightConverter
    {
        static readonly double[] KgWeights = new double[]
        {
            20, 15, 10, 5, 2.5, 1.25
        };

        static readonly double[] LbsWeights = new double[]
        {
            20, 15, 10, 5, 2.5, 1.25 // TODO - correct this to lbs weights
        };

        public static string Convert(double weight, WeightEnum type)
        {
            return type == WeightEnum.KG ?
                Convert(weight, KgWeights) : Convert(weight, LbsWeights);
        }

        static string Convert(double weight, double[] weights)
        {
            //Divide by 2 because this will provide plates for each side of a barbell
            weight = weight / 2;

            var builder = new List<string>();

            for (int index = 0; index < weights.Length; ++index)
                while (weight / weights[index] >= 1)
                {
                    builder.Add(weights[index].ToString());
                    weight -= weights[index];
                }

            if (weight > 0)
            {
                if (builder[builder.Count - 1] == weights[weights.Length - 1].ToString())
                {
                    builder.RemoveAt(builder.Count - 1);
                    builder.Add(weights[weights.Length - 2].ToString());
                }
                else
                    builder.Add(weights[weights.Length - 1].ToString());
            }

            return string.Join(" + ", builder.ToArray());
        }
    }
}
