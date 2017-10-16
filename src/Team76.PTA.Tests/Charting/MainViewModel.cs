using System;
using OxyPlot;
using OxyPlot.Series;

namespace Team76.PTA.Tests.Charting
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            this.PlotModel = new PlotModel { Title = "Example 1" };
            this.PlotModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
        }

        public PlotModel PlotModel { get; set; }
    }
}
