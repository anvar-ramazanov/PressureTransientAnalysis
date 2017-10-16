using CuttingEdge.Conditions;
using MathNet.Numerics.Differentiation;
using Team76.PTA.SpecialFunctions;

namespace Team76.PTA
{
    /// <summary>
    /// 
    /// </summary>
    public static class OuterBoundaries
    {
        /// <summary>
        ///     Dimensionless bottom hole pressure affected by outer boundary. Boundary type -  Linear Sealing Fault. Dimensionless line source solution pressure of infinite homogeneous reservoir, which is a function in the principle of superposition.
        /// </summary>
        /// <param name="td">dimensionless time</param>
        /// <param name="Ld">dimensionless distance to fault</param>
        /// <returns>System.Double.</returns>
        public static double PwDbLinearSealingFault(double td, double Ld)
        {

            Condition.Requires(td, nameof(td)).IsGreaterOrEqual(0.0);
            Condition.Requires(Ld, nameof(Ld)).IsGreaterThan(0.0);

            return -0.5 * ExponentialIntegral.Evaluate(-Ld * Ld / td);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="td"></param>
        /// <param name="Ld"></param>
        /// <returns></returns>
        public static double PwDbLinearSealingFaultDerivative(double td, double Ld)
        {
            Condition.Requires(td, nameof(td)).IsGreaterOrEqual(0.0);
            Condition.Requires(Ld, nameof(Ld)).IsGreaterThan(0.0);

            var nd = new NumericalDerivative();
            var d = nd.EvaluateDerivative(c => PwDbLinearSealingFault(c, Ld), td, 1);
            return d * td;
        }

        /// <summary>
        ///      Dimensionless bottom hole pressure affected by outer boundary. Boundary type -  Linear Constant Pressure. Dimensionless line source solution pressure of infinite homogeneous reservoir, which is a function in the principle of superposition.
        /// </summary>
        /// <param name="td">dimensionless time</param>
        /// <param name="Ld">dimensionless distance to fault</param>
        /// <returns>System.Double.</returns>
        public static double PwDbLinearConstantPressure(double td, double Ld)
        {
            Condition.Requires(td, nameof(td)).IsGreaterOrEqual(0.0);
            Condition.Requires(Ld, nameof(Ld)).IsGreaterThan(0.0);

            return 0.5 * ExponentialIntegral.Evaluate(-Ld * Ld / td);
        }

        /// <summary>
        ///     Dimensionless bottom hole pressure affected by outer boundary. Boundary type -  Perpendicular Sealing Faults. Dimensionless line source solution pressure of infinite homogeneous reservoir, which is a function in the principle of superposition.
        /// </summary>
        /// <param name="td">dimensionless time</param>
        /// <param name="Ld1">dimensionless distance to fault 1</param>
        /// <param name="Ld2">dimensionless distance to fault 2</param>
        /// <returns>System.Double.</returns>
        public static double PwDbPerpendicularSealingFaults(double td, double Ld1, double Ld2)
        {
            Condition.Requires(td, nameof(td)).IsGreaterOrEqual(0.0);
            Condition.Requires(Ld1, nameof(Ld1)).IsGreaterThan(0.0);
            Condition.Requires(Ld2, nameof(Ld2)).IsGreaterThan(0.0);


            return -0.5 * (
                       + ExponentialIntegral.Evaluate(-Ld1 * Ld1 / td)
                       + ExponentialIntegral.Evaluate(-Ld2 * Ld2 / td)
                       + ExponentialIntegral.Evaluate(-(Ld1 * Ld1 + Ld2 * Ld2) / td)
                   );
        }

        /// <summary>
        ///     Dimensionless bottom hole pressure affected by outer boundary. Boundary type -  Perpendicular Constant Pressures. Dimensionless line source solution pressure of infinite homogeneous reservoir, which is a function in the principle of superposition.
        /// </summary>
        /// <param name="td">dimensionless time</param>
        /// <param name="Ld1">dimensionless distance to fault 1</param>
        /// <param name="Ld2">dimensionless distance to fault 2</param>
        /// <returns>System.Double.</returns>
        public static double PwDbPerpendicularConstantPressures(double td, double Ld1, double Ld2)
        {
            Condition.Requires(td, nameof(td)).IsGreaterOrEqual(0.0);
            Condition.Requires(Ld1, nameof(Ld1)).IsGreaterThan(0.0);
            Condition.Requires(Ld2, nameof(Ld2)).IsGreaterThan(0.0);

            return 0.5 * (
                       + ExponentialIntegral.Evaluate(-Ld1 * Ld1 / td)
                       + ExponentialIntegral.Evaluate(-Ld2 * Ld2 / td)
                       + ExponentialIntegral.Evaluate(-(Ld1 * Ld1 + Ld2 * Ld2) / td)
                   );
        }

        /// <summary>
        ///  Dimensionless bottom hole pressure affected by outer boundary.
        ///  Boundary type - Perpendicular Mixed Boundaries (Sealing Fault and Constant Pressure).
        /// </summary>
        /// <param name="td">dimensionless time</param>
        /// <param name="Ld1">dimensionless distance to fault 1</param>
        /// <param name="Ld2">dimensionless distance to fault 2</param>
        /// <returns>System.Double.</returns>
        public static double PwDbPerpendicularMixedBoundaries(double td, double Ld1, double Ld2)
        {
            Condition.Requires(td, nameof(td)).IsGreaterOrEqual(0.0);
            Condition.Requires(Ld1, nameof(Ld1)).IsGreaterThan(0.0);
            Condition.Requires(Ld2, nameof(Ld2)).IsGreaterThan(0.0);

            return -0.5 * (
                       + ExponentialIntegral.Evaluate(-Ld1 * Ld1 / td)
                       - ExponentialIntegral.Evaluate(-Ld2 * Ld2 / td)
                       - ExponentialIntegral.Evaluate(-(Ld1 * Ld1 + Ld2 * Ld2) / td)
                   );
        }
    }
}
