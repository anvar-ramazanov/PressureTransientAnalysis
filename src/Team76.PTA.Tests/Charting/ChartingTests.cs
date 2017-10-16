using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Team76.PTA.Tests.Charting
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ChartingTests
    {

        [Test]
        public void InfHomPressureDropPlots()
        {
            if (!Debugger.IsAttached) return;

            var skinFactor = 0.0;
            var cd = 0.0;

            Func<double, double> f1 = (x) => InfiniteHomogenousReservoir2.PwdR(x, cd, skinFactor);
            var dp1 = GetXValues().Select(c => new ScatterPoint(c, f1(c)));
            var ls1 = new ScatterSeries() { Title = "PwdR2" };
            ls1.Points.AddRange(dp1);

            Func<double, double> f2 = (x) => InfiniteHomogenousReservoir.PwdR(x, cd, skinFactor);
            var dp2 = GetXValues().Select(c => new DataPoint(c, f2(c)));
            var ls2 = new LineSeries() { Title = "PwdR" };
            ls2.Points.AddRange(dp2);

            Func<double, double> f3 = (x) => InfiniteHomogenousReservoir.Pd(x, 1.0);
            var dp3 = GetXValues().Select(c => new DataPoint(c, f3(c)));
            var ls3 = new LineSeries() { Title = "Pd rd = 1.0" };
            ls3.Points.AddRange(dp3);


            var pm = new PlotModel() { Title = nameof(InfHomPressureDropPlots) }; ;
            pm.Axes.Add(new LogarithmicAxis() { Position = AxisPosition.Bottom, AbsoluteMinimum = 0.001 });
            pm.Axes.Add(new LogarithmicAxis() { Position = AxisPosition.Left, AbsoluteMinimum = 0.001 });
            pm.LegendPlacement = LegendPlacement.Outside;

            pm.Series.Add(ls1);
            pm.Series.Add(ls2);
            pm.Series.Add(ls3);

            ShowPlot(pm);
        }


        [Test]
        public void PwdR2_SkinSensitivity()
        {
            if (!Debugger.IsAttached) return;

            var pm = new PlotModel() { Title = nameof(PwdR2_SkinSensitivity) }; ;
            pm.Axes.Add(new LogarithmicAxis() { Position = AxisPosition.Bottom, AbsoluteMinimum = 0.001 });
            pm.Axes.Add(new LogarithmicAxis() { Position = AxisPosition.Left, AbsoluteMinimum = 0.001 });
            pm.LegendPlacement = LegendPlacement.Outside;

            var cd = 10;

            for (int i = -4; i < 5; i++)
            {
                pm.Series.Add(GetPwdR2Series(i, cd));
                pm.Series.Add(GetDirPwdR2Series(i * 10, cd));
            }
            ShowPlot(pm);

        }

        [Test]
        public void PwdR2_CdSensitivity()
        {
            if (!Debugger.IsAttached) return;

            var pm = new PlotModel() { Title = nameof(PwdR2_CdSensitivity) }; ;
            pm.Axes.Add(new LogarithmicAxis() { Position = AxisPosition.Bottom, AbsoluteMinimum = 0.001 });
            pm.Axes.Add(new LogarithmicAxis() { Position = AxisPosition.Left, AbsoluteMinimum = 0.001 });
            pm.LegendPlacement = LegendPlacement.Outside;


            for (int i = 0; i < 5; i++)
            {
                pm.Series.Add(GetPwdR2Series(0, i * 10));
                pm.Series.Add(GetDirPwdR2Series(0, i * 10));
            }
            ShowPlot(pm);
        }


        [Test]
        public void PwdR2_and_DirPwdR2()
        {
            if (!Debugger.IsAttached) return;

            var pm = new PlotModel() { Title = nameof(PwdR2_and_DirPwdR2) }; ;
            pm.Axes.Add(new LogarithmicAxis() { Position = AxisPosition.Bottom, AbsoluteMinimum = 0.001 });
            pm.Axes.Add(new LogarithmicAxis() { Position = AxisPosition.Left, AbsoluteMinimum = 0.001 });
            pm.LegendPlacement = LegendPlacement.Outside;

            var cd = 10;
            var skin = 2;

            Func<double, double> f1 = (x) => InfiniteHomogenousReservoir2.PwdR(x, cd, skin);
            var dp1 = GetXValues().Select(c => new DataPoint(c / cd, f1(c) / cd));
            var ls1 = new LineSeries() { Title = $"PwdR2 Skin={skin} CD={cd}" };
            ls1.Points.AddRange(dp1);
            pm.Series.Add(ls1);


            Func<double, double> f2 = (x) => InfiniteHomogenousReservoir2.PwdRDerivative(x, cd, 0);
            var dp2 = GetXValues().Select(c => new DataPoint(c / cd, f2(c) / cd));
            var ls2 = new LineSeries() { Title = $"DirPwdR2 Skin={skin} CD={cd}" };
            ls2.Points.AddRange(dp2);

            pm.Series.Add(ls2);

            ShowPlot(pm);

        }


        [Test]
        public void PwdR2_and_DirPwdR2_PwDbLinearSealingFault()
        {
            if (!Debugger.IsAttached) return;

            var pm = new PlotModel() { Title = nameof(PwdR2_and_DirPwdR2_PwDbLinearSealingFault) }; ;
            pm.Axes.Add(new LogarithmicAxis() { Position = AxisPosition.Bottom, AbsoluteMinimum = 0.001 });
            pm.Axes.Add(new LogarithmicAxis() { Position = AxisPosition.Left, AbsoluteMinimum = 0.001 });
            pm.LegendPlacement = LegendPlacement.Outside;

            var cd = 50;
            var skin =5;
            var Ld = 500;

            Func<double, double> f1 = (x) => InfiniteHomogenousReservoir2.PwdR(x, cd, skin) +
                OuterBoundaries.PwDbLinearSealingFault(x, Ld);
            var dp1 = GetXValues().Select(c => new DataPoint(c / cd, f1(c) / cd));
            var ls1 = new LineSeries() { Title = $"PwdR2 Skin={skin} CD={cd}" };
            ls1.Points.AddRange(dp1);
            pm.Series.Add(ls1);


            Func<double, double> f2 = (x) => InfiniteHomogenousReservoir2.PwdRDerivative(x, cd, 0)
                + OuterBoundaries.PwDbLinearSealingFaultDerivative(x, Ld);

            var dp2 = GetXValues().Select(c => new DataPoint(c / cd, f2(c) / cd));
            var ls2 = new LineSeries() { Title = $"PwdRDerivative Skin={skin} CD={cd}" };
            ls2.Points.AddRange(dp2);

            pm.Series.Add(ls2);

            ShowPlot(pm);

        }


        private void ShowPlot(PlotModel model)
        {
            var vm = new MainViewModel();
            vm.PlotModel = model;

            var view = new MainView();
            view.DataContext = vm;

            view.ShowDialog();
        }

        private List<double> GetXValues()
        {
            var xMin = -3.0;
            var xMax = 10.0;

            var xValues = new List<double>();

            var xi = xMin;
            while (xi <= xMax)
            {
                xValues.Add(xi);
                xi = xi + 0.1;
            }

            return xValues.Select(c => Math.Pow(10, c)).ToList();
        }

        private LineSeries GetPwdR2Series(double skin, double cd)
        {
            Func<double, double> f = (x) => InfiniteHomogenousReservoir2.PwdR(x, cd, skin);
            var dp = GetXValues().Select(c => new DataPoint(c, f(c)));
            var ls = new LineSeries() { Title = $"PwdR2 Skin={skin} CD={cd}" };
            ls.Points.AddRange(dp);
            return ls;
        }

        private LineSeries GetDirPwdR2Series(double skin, double cd)
        {
            Func<double, double> f = (x) => InfiniteHomogenousReservoir2.PwdRDerivative(x, cd, skin);
            var dp = GetXValues().Select(c => new DataPoint(c, f(c)));
            var ls = new LineSeries() { Title = $"DirPwdR2 Skin={skin} CD={cd}" };
            ls.Points.AddRange(dp);
            return ls;
        }
    }
}
