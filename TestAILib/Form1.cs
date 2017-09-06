using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jtext103.CFET2.Things.NiAiLib;
using System.Collections;
using System.Collections.Generic;

namespace TestAILib
{
    public partial class Form1 : Form
    {
        private AIThing aiThing;
        
        public Form1()
        {
            InitializeComponent();
            aiThing = new AIThing();
            //aiThing.InitAIThing(@"d:\111");
            aiThing.InitAIThing(@"D:\123", @"D:\123\config.txt");
        }

        private void armButton_Click(object sender, EventArgs e)
        {
            aiThing.TryArm();
            timer1.Enabled = true;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            aiThing.TryStopTask();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            aiStatusTextBox.Text = aiThing.AIState.ToString();
            lastShotTimeTextBox.Text = aiThing.LastShotTime.ToLocalTime().ToString("yyyy-MMM-d HH:mm:ss");
            sampleRateTextBox.Text = aiThing.SampleRate.ToString();
            dataCountTextBox.Text = aiThing.TotalSampleCountPerChannel.ToString();
            //dataCountTextBox.Text = aiThing.GetLatestSample(0).ToString();
            //刷新列表
            //aiChannelListBox.Items.Clear();
            //foreach (string channel in aiThing.AllActiveChannels)
            //{
            //    aiChannelListBox.Items.Add(channel);
            //}
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            int shotNo;
            int channelNo;
            if (int.TryParse(shotNoTextBox.Text, out shotNo))
            {
                if (int.TryParse(channelNoTextBox.Text, out channelNo))
                {
                    string channelName = channelNoTextBox.Text;
                    double[] data = aiThing.LoadShot(shotNo, channelNo, 50000000, 2000);
                    ShowFileReadData showFileReadDataForm = new ShowFileReadData(data, channelName);
                    showFileReadDataForm.ShowDialog(this);
                    showFileReadDataForm.Dispose();
                }
                else
                {
                    MessageBox.Show("通道号只能为数字！");
                }
            }
            else
            {
                MessageBox.Show("炮号只能为数字！");
            }
            
        }

        private void showWaveButton_Click(object sender, EventArgs e)
        {
            //ShowRTDataForm showDataForm = new ShowRTDataForm(aiThing.AllActiveChannels);
            //showDataForm.Owner = this;
            ////或者showDataForm.ShowDialog(this);
            //showDataForm.ShowDialog();

            ////showDialog的窗口关闭时不会自动释放资源
            //showDataForm.Dispose();
        }
    }
}
