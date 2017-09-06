using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TestAILib
{
    public partial class ShowFileReadData : Form
    {
        public ShowFileReadData(IEnumerable<double> data, string channelName)
        {
            InitializeComponent();
            initSplineChart(chart1, "samples", "value", channelName);
            updateSplineChart(chart1, data);
        }

        /// <summary>
        /// 初始化曲线
        /// </summary>
        /// <param name="targetChart"></param>
        /// <param name="xName"></param>
        /// <param name="yName"></param>
        /// <param name="seriesName">一般按顺序设为各个通道名</param>
        private void initSplineChart(Chart targetChart, string xName, string yName, string seriesName)
        {
            //设置刻度
            ChartArea TempChartArea = targetChart.ChartAreas[0];
            //X轴
            TempChartArea.AxisX.IsMarginVisible = true;
            TempChartArea.AxisX.Title = xName;
            TempChartArea.AxisX.TitleFont = new Font("微软雅黑", 16);
            TempChartArea.AxisX.LineWidth = 1;
            TempChartArea.AxisX.MajorGrid.LineWidth = 1;
            TempChartArea.AxisX.MajorGrid.LineColor = Color.Gray;
            TempChartArea.AxisX.MajorGrid.Enabled = true;
            TempChartArea.AxisX.MajorTickMark.Enabled = true;
            TempChartArea.AxisX.IntervalOffset = 0;
            //设置x轴刻度类型及间隔
            TempChartArea.AxisX.IntervalType = DateTimeIntervalType.Auto;
            TempChartArea.AxisX.Enabled = AxisEnabled.True;

            //Y轴
            TempChartArea.AxisY.IsMarginVisible = true;
            TempChartArea.AxisY.Title = yName;
            TempChartArea.AxisY.TitleFont = new Font("微软雅黑", 16);
            TempChartArea.AxisY.LineWidth = 1;
            TempChartArea.AxisY.MajorGrid.LineWidth = 1;
            TempChartArea.AxisY.MajorGrid.LineColor = Color.Gray;
            TempChartArea.AxisY.MajorGrid.Enabled = true;
            TempChartArea.AxisY.MajorTickMark.Enabled = true;
            TempChartArea.AxisY.Enabled = AxisEnabled.True;

            //设置系列
            targetChart.Series.Clear();
            targetChart.Series.Add(seriesName);
            foreach (Series tempSeries in targetChart.Series)
            {
                tempSeries.ChartType = SeriesChartType.Spline;
                tempSeries.IsXValueIndexed = true;
                tempSeries.BorderWidth = 2;
                //系列上点的大小
                tempSeries.MarkerSize = 2;
                //系列上点的形状
                tempSeries.MarkerStyle = MarkerStyle.Circle;
                // Set point labels
                tempSeries.IsValueShownAsLabel = false;
                //系列的X值的类型
                tempSeries.XValueType = ChartValueType.Auto;
                //系列的Y值的类型
                tempSeries.YValueType = ChartValueType.Auto;
                //系列的刻度值
                tempSeries.Legend = targetChart.Legends[0].Name;
                //显示图例
                targetChart.Legends[0].Enabled = true;
                //系列的图标区域
                tempSeries.ChartArea = targetChart.ChartAreas[0].Name;
            }

        }

        /// <summary>
        /// 将数据加入chart
        /// </summary>
        /// <param name="targetChart"></param>
        /// <param name="data">第0维代表了通道数，第1维对应各个通道的数据</param>
        private void updateSplineChart(Chart targetChart, IEnumerable<double> data)
        {
            targetChart.Series[0].Points.DataBindY(data);
        }
    }

}
