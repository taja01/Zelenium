using System;

namespace ZeleniumFramework.Utils
{
    public static class CssCalcUtil
    {
        public static double MatrixToDegree(string matrix)
        {
            if (matrix == "none")
            {
                return 0; // if there is no rotation it returns 0
            }

            double valueInDegree;

            try
            {
                var matches = matrix.Split('(')[1].Split(')')[0].Split(',');
                var a = double.Parse(matches[0]);
                var b = double.Parse(matches[1]);

                valueInDegree = Math.Round(Math.Atan2(b, a) * (180 / Math.PI));
            }
            catch (IndexOutOfRangeException e)
            {
                throw new IndexOutOfRangeException($"{e.Message}: {matrix}");
            }
            catch (FormatException e)
            {
                throw new FormatException($"{e.Message}: {matrix}");
            }

            return valueInDegree;
        }
    }
}
