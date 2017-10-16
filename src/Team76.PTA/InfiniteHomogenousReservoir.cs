using System;
using CuttingEdge.Conditions;
using MathNet.Numerics.Differentiation;
using Team76.PTA.SpecialFunctions;

namespace Team76.PTA
{
    /// <summary>
    /// Infinite Homogenous Reservoir
    /// </summary>
    public static class InfiniteHomogenousReservoir
    {
        /// <summary>
        ///     The expression of dimensionless line source solution pressure of infinite homogeneous reservoir.
        /// </summary>
        /// <param name="td">dimensionless time</param>
        /// <param name="rd">dimensionless radius</param>
        /// <returns>System.Double.</returns>
        public static double Pd(double td, double rd)
        {
            Condition.Requires(td, nameof(td)).IsGreaterOrEqual(0.0);
            Condition.Requires(rd, nameof(rd)).IsGreaterOrEqual(0.0);

            return -0.5 * ExponentialIntegral.Evaluate(-rd * rd / (4 * td));
        }

        /// <summary>
        ///     The calculation Of the dimensionless wellbore pressure drop  with skin for homogeneous reservoir.
        /// </summary>
        /// <param name="td">dimensionless time</param>
        /// <param name="cd">dimensionless wellbore storage</param>
        /// <param name="skinFactor">skin factor</param>
        /// <returns>System.Double.</returns>
        public static double PwdR(double td, double cd, double skinFactor)
        {

            Condition.Requires(td, nameof(td)).IsGreaterOrEqual(0.0);
            Condition.Requires(cd, nameof(cd)).IsGreaterOrEqual(0.0);

            var n = 14;

            double pwdR = 0;

            for (int i = 1; i <= n; i++)
            {
                double u = i * Math.Log(2.0) / td;

                double sru = Math.Sqrt(u);

                double p1 = MathNet.Numerics.SpecialFunctions.BesselK0(sru)
                            + skinFactor * sru * MathNet.Numerics.SpecialFunctions.BesselK1(sru);

                double p2 = u * sru * MathNet.Numerics.SpecialFunctions.BesselK1(sru)
                            + cd * Math.Pow(u, 2) * MathNet.Numerics.SpecialFunctions.BesselK0(sru)
                            + cd * Math.Pow(u, 2) * skinFactor * MathNet.Numerics.SpecialFunctions.BesselK1(sru) * sru;

                pwdR = StehfestCoefficients.Vi(i, n) * p1 / p2 + pwdR;
            }
            pwdR = (Math.Log(2) / td) * pwdR;

            return pwdR;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="td"></param>
        /// <param name="cd"></param>
        /// <param name="skinFactor"></param>
        /// <returns></returns>
        public static double PwdRDerivative(double td, double cd, double skinFactor)
        {
            Condition.Requires(td, nameof(td)).IsGreaterOrEqual(0.0);
            Condition.Requires(cd, nameof(cd)).IsGreaterOrEqual(0.0);

            var nd = new NumericalDerivative();
            var d = nd.EvaluateDerivative(c => PwdR(c, cd, skinFactor), td, 1);
            return d * td;
        }
    }
}
