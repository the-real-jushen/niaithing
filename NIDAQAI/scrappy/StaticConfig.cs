using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtext103.CFET2.Things.NiAiLib
{
    public class StaticConfig
    {
        /// <summary>
        /// 触发方式
        /// </summary>
        public TriggerType AITriggerType { get; set; }

        /// <summary>
        /// 配置终端输入方式（差分、单端）
        /// </summary>
        public TerminalConfiguration TerminalConfig { get; set; }

        /// <summary>
        /// 输入电压最小值
        /// </summary>
        public double MinimumVolt { get; set; }

        /// <summary>
        /// 输入电压最大值
        /// </summary>
        public double MaximumVolt { get; set; }

        /// <summary>
        /// 采样率
        /// </summary>
        public int SampleRate { get; set; }

        /// <summary>
        /// 触发通道（仅用于外部触发）
        /// </summary>
        public string TriggerSource { get; set; }

        /// <summary>
        /// 触发边沿
        /// </summary>
        public Edge AITriggerEdge { get; set; }

        /// <summary>
        /// 触发电平（仅用于外部模拟触发）
        /// </summary>
        public double AnalogTriggerLevel { get; set; }

        /// <summary>
        /// 时钟源（使用内部时钟时，为空字符串）
        /// </summary>
        public string ClkSource { get; set; }

        /// <summary>
        /// 时钟边沿
        /// </summary>
        public Edge ClkActiveEdge { get; set; }

        /// <summary>
        /// 采样方式
        /// </summary>
        public SamplesMode AISamplesMode { get; set; }

        /// <summary>
        /// 每通道采样数（仅用于有限采样）
        /// </summary>
        public int SamplesPerChannel { get; set; }

        /// <summary>
        /// 每次读取数据个数
        /// </summary>
        public int ReadSamplePerTime { get; set; }

        /// <summary>
        /// 使用的AI通道
        /// 使用的AI通道,创建任务时使用
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// 指示是否自动重新ARM
        /// 只对有限采样生效
        /// </summary>
        public bool AutoReArm { get; set; }

        /// <summary>
        /// 每个通道的名称（用于读取文件）
        /// 与Channel对应
        /// 任务结束后为该属性赋值
        /// </summary>
        public List<string> ChannelNames { get; set; }

        /// <summary>
        /// 该炮每通道总采样点数（用于读取文件）
        /// 任务结束后为该属性赋值
        /// </summary>
        public long TotalSampleLengthPerChannel { get; set; }

        /// <summary>
        /// 为属性赋默认值，但有可能无法创建任务
        /// </summary>
        public StaticConfig()
        {
            AITriggerType = TriggerType.SoftTrigger;
            TerminalConfig = TerminalConfiguration.Differential;
            MinimumVolt = 0;
            MaximumVolt = 10;
            SampleRate = 3000;
            TriggerSource = "";
            AITriggerEdge = Edge.Rising;
            AnalogTriggerLevel = 1;
            ClkSource = "";
            ClkActiveEdge = Edge.Rising;
            AISamplesMode = SamplesMode.ContinuousSamples;
            //AISamplesMode = SamplesMode.FiniteSamples;
            SamplesPerChannel = 3100;
            ReadSamplePerTime = 1000;
            //这个要根据实际的卡来写
            Channel = "PXI2Slot3/ai0";
            AutoReArm = false;
        }

        /// <summary>
        /// 读取配置文件，构造新实例
        /// </summary>
        /// <param name="filePath">配置文件完整路径</param>
        public StaticConfig(string filePath)
        {
            //读取配置文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
            StreamReader fileStreamReader = new StreamReader(fs);
            string configJson = fileStreamReader.ReadToEnd();
            fileStreamReader.Close();
            //反序列化后为属性赋值
            StaticConfig readConfig = JsonConvert.DeserializeObject<StaticConfig>(configJson);
            AITriggerType = readConfig.AITriggerType;
            TerminalConfig = readConfig.TerminalConfig;
            MinimumVolt = readConfig.MinimumVolt;
            MaximumVolt = readConfig.MaximumVolt;
            SampleRate = readConfig.SampleRate;
            TriggerSource = readConfig.TriggerSource;
            AITriggerEdge = readConfig.AITriggerEdge;
            AnalogTriggerLevel = readConfig.AnalogTriggerLevel;
            ClkSource = readConfig.ClkSource;
            ClkActiveEdge = readConfig.ClkActiveEdge;
            AISamplesMode = readConfig.AISamplesMode;
            SamplesPerChannel = readConfig.SamplesPerChannel;
            ReadSamplePerTime = readConfig.ReadSamplePerTime;
            Channel = readConfig.Channel;
            AutoReArm = readConfig.AutoReArm;
            ChannelNames = readConfig.ChannelNames;
            TotalSampleLengthPerChannel = readConfig.TotalSampleLengthPerChannel;
        }
        
        /// <summary>
        /// 保存该配置至指定目录
        /// </summary>
        /// <param name="fileDirectory"></param>
        public void Save(string fileDirectory)
        {
            FileStream fs = new FileStream(fileDirectory + "\\config.txt", FileMode.Create);
            StreamWriter stringWriter = new StreamWriter(fs);
            //将配置序列化为Json后保存
            stringWriter.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
            stringWriter.Close();
            //对应的stream也被关闭了，所以不用再关
        }
    }
}
