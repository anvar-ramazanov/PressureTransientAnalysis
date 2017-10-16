using System;
using System.Linq;

namespace Team76.PTA
{
    public class DataAnalysis
    {
        readonly double[] _x;
        readonly double[] _y;

        /// <param name="x">Sample points (N), sorted ascending</param>
        /// <param name="sy">Samples values (N) of each segment starting at the corresponding sample point.</param>
        public DataAnalysis(double[] x, double[] sy)
        {
            if (x.Length != sy.Length)
            {
                throw new ArgumentException("Ammount of of x and sy should be the same");
            }

            if (x.Length < 1)
            {
                throw new ArgumentException("Number of sample points should be more than 1", nameof(x));
            }

            _x = x;
            _y = sy;
        }

        /// <summary>
        /// Differentiate at point t.
        /// </summary>
        /// <param name="t">Point t to interpolate at.</param>
        /// <param name="l">Minimum distanse between abscissa of the points and that of point t.</param>
        /// <returns></returns>
        public double Differentiate(double t, double l)
        {
            int index = Array.BinarySearch(_x, t);
            double xLeft, xRight, dx1, dx2, dp1, dp2, diff;
            int indexLeft, indexRight;


            if (index == 0)
            {
                xRight = _x.Where(c => c <= t + l).Max();
                indexRight = Array.BinarySearch(_x, xRight);

                if (indexRight == index && index < _x.Length - 2)
                {
                    indexRight = index + 1;
                    xRight = _x[indexRight];
                }

                dx2 = xRight - t;
                dp2 = _y[indexRight] - _y[index];
                diff = dp2 / dx2;
                return diff;

            }

            if (index == _x.Length - 1)
            {
                xLeft = _x.Where(c => c >= t - l).Min();
                indexLeft = Array.BinarySearch(_x, xLeft);
                if (indexLeft == index && index > 0)
                {
                    indexLeft = index - 1;
                    xLeft = _x[indexLeft];
                }


                dx1 = t - xLeft;
                dp1 = _y[index] - _y[indexLeft];
                diff = dp1 / dx1 ;
                return diff;

            }

            xLeft = _x.Where(c => c >= t - l).Min();
            indexLeft = Array.BinarySearch(_x, xLeft);
            if (indexLeft == index)
            {
                indexLeft = index - 1;
                xLeft = _x[indexLeft];
            }

            xRight = _x.Where(c => c <= t + l).Max();
            indexRight = Array.BinarySearch(_x, xRight);

            if (indexRight == index)
            {
                indexRight = index + 1;
                xRight = _x[indexRight];
            }

            dx1 = t - xLeft;
            dx2 = xRight - t;

            dp1 = _y[index] - _y[indexLeft];
            dp2 = _y[indexRight] - _y[index];

            diff = (dp1 / dx1 * dx2 + dp2 / dx2 * dx1) / (dx1 + dx2);

            return diff;
        }
    }
}