using NUnit.Framework;
using Should;
using Team76.PTA.SpecialFunctions;

namespace Team76.PTA.Tests.SpecialFunctions
{
    [TestFixture]
    public class ExponentialIntegralTests
    {
        [Test]
        public void EvaluateTest()
        {
            var error = 1e-5;
            ExponentialIntegral.Evaluate(-100).ShouldEqual(-3.68359776168203e-46, error);
            ExponentialIntegral.Evaluate(-10).ShouldEqual(-4.15696892968532e-6, error);
            ExponentialIntegral.Evaluate(-5).ShouldEqual(-0.00114829559127532, error);
            ExponentialIntegral.Evaluate(-1).ShouldEqual(-0.21938393439552, error);
            ExponentialIntegral.Evaluate(-0.1).ShouldEqual(-1.82292, error);
            ExponentialIntegral.Evaluate(-0.01).ShouldEqual(-4.03793, error);
            ExponentialIntegral.Evaluate(-0.001).ShouldEqual(-6.33154, error);
            ExponentialIntegral.Evaluate(0.001).ShouldEqual(-6.32954, error);
            ExponentialIntegral.Evaluate(0.1).ShouldEqual(-1.62281, error);
            ExponentialIntegral.Evaluate(1).ShouldEqual(1.89511781635593, error);
            ExponentialIntegral.Evaluate(2).ShouldEqual(4.954234, error);
            ExponentialIntegral.Evaluate(10).ShouldEqual(2492.22897624187, error);
            (1e-41* ExponentialIntegral.Evaluate(100)).ShouldEqual(2.71555274485388, error);
        }
    }
}
