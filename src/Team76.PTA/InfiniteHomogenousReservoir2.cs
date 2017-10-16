using System;
using CuttingEdge.Conditions;
using MathNet.Numerics.Differentiation;
using Team76.PTA.SpecialFunctions;

namespace Team76.PTA
{
    /// <summary>
    /// Infinite Homogenous Reservoir
    /// </summary>
    public static class InfiniteHomogenousReservoir2
    {
        /// <summary>
        ///     The calculation Of the dimensionless wellbore pressure drop  with skin for homogeneous reservoir.
        /// </summary>
        /// <param name="td">dimensionless time</param>
        /// <param name="cd">dimensionless wellbore storage</param>
        /// <param name="skinFactor">skin factor</param>
        /// <returns></returns>
        public static double PwdR(double td, double cd, double skinFactor)
        {
            Condition.Requires(td, nameof(td)).IsGreaterOrEqual(0.0);
            Condition.Requires(cd, nameof(cd)).IsGreaterOrEqual(0.0);

            return Laplace.InverseTransform((x) => PwdRinLaplaceSpace(x, cd, skinFactor), td);
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

        private static double PwdRinLaplaceSpace(double s, double cd, double skinFactor)
        {
            var rz = Math.Sqrt(s);
            var p1 = MathNet.Numerics.SpecialFunctions.BesselK0(rz) + skinFactor * rz * MathNet.Numerics.SpecialFunctions.BesselK1(rz);
            var p2 = s * (rz * MathNet.Numerics.SpecialFunctions.BesselK1(rz) + cd * s * p1);
            return p1 / p2;
        }
    }
}
