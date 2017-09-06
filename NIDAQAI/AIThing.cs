using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtext103.CFET2.Things.NiAiLib
{
    public class AIThing
    {
        private AI ai;

        /// <summary>
        /// cfet status及部分字段均从该config中读取
        /// </summary>
        private StaticConfig staticConfig;

        /// <summary>
        /// 
        /// </summary>
        private DataWriter dataWriter;

        #region cfet status

        /// <summary>
        /// AI任务状态
        /// </summary>
        public Status AIState { get; private set; }

        /// <summary>
        /// 最新一炮开始时间
        /// </summary>
        public DateTime LastShotTime
        {
            get
            {
                return ai.LastShotTime;
            }
        }

        /// <summary>
        /// 触发方式
        /// </summary>
        public TriggerType AITriggerType
        {
            get
            {
                return ai.AITriggerType;
            }
        }

        /// <summary>
        /// 配置终端输入方式（差分、单端）
        /// </summary>
        public TerminalConfiguration TerminalConfig
        {
            get
            {
                return ai.TerminalConfig;
            }
        }

        /// <summary>
        /// 输入电压最小值
        /// </summary>
        public double MinimumVolt
        {
            get
            {
                return ai.MinimumVolt;
            }
        }

        /// <summary>
        /// 输入电压最大值
        /// </summary>
        public double MaximumVolt
        {
            get
            {
                return ai.MaximumVolt;
            }
        }

        /// <summary>
        /// 采样率
        /// </summary>
        public int SampleRate
        {
            get
            {
                return ai.SampleRate;
            }
        }

        /// <summary>
        /// 触发通道（仅用于外部触发）
        /// </summary>
        public string TriggerSource
        {
            get
            {
                return ai.TriggerSource;
            }
        }

        /// <summary>
        /// 触发边沿
        /// </summary>
        public Edge AITriggerEdge
        {
            get
            {
                return ai.AITriggerEdge;
            }
        }

        /// <summary>
        /// 触发电平（仅用于外部模拟触发）
        /// </summary>
        public double AnalogTriggerLevel
        {
            get
            {
                return ai.AnalogTriggerLevel;
            }
        }

        /// <summary>
        /// 时钟源（使用内部时钟时，为空字符串）
        /// </summary>
        public string ClkSource
        {
            get
            {
                return ai.ClkSource;
            }
        }

        /// <summary>
        /// 时钟边沿
        /// </summary>
        public Edge ClkActiveEdge
        {
            get
            {
                return ai.ClkActiveEdge;
            }
        }

        /// <summary>
        /// 采样方式
        /// </summary>
        public SamplesMode AISamplesMode
        {
            get
            {
                return ai.AISamplesMode;
            }
        }

        /// <summary>
        /// 每通道采样数（仅用于有限采样）
        /// </summary>
        public int SamplesPerChannel
        {
            get
            {
                return ai.SamplesPerChannel;
            }
        }

        /// <summary>
        /// 每次读取数据个数
        /// </summary>
        public int ReadSamplePerTime
        {
            get
            {
                return ai.ReadSamplePerTime;
            }
        }

        /// <summary>
        /// 指示是否自动重新ARM
        /// 只对外部触发的有限采样有效
        /// </summary>
        public bool AutoReArm
        {
            get
            {
                return ai.AutoReArm;
            }
        }

        /// <summary>
        /// 使用的AI通道
        /// </summary>
        public List<string> AllActiveChannels
        {
            get
            {
                return ai.AllActiveChannels;
            }
        }

        /// <summary>
        /// 实时每通道采样点总数
        /// 任务结束后不再变化
        /// </summary>
        public long TotalSampleCountPerChannel
        {
            get
            {
                try
                {
                    return dataWriter.TotalSampleCount;
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 存储当前炮数据及配置文件的文件夹路径
        /// </summary>
        public string CurrentFileDirectiory { get; private set; }
        #endregion

        #region cfet config
        /// <summary>
        /// 存放所有数据文件的根文件夹路径
        /// </summary>
        public string DataFileParentDirectory { get; set; }

        #endregion

        #region cfet method
        /// <summary>
        /// 启动AI采集任务
        /// </summary>
        public void TryArm()
        {
            ai.TryArm();
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        /// <returns></returns>
        public bool TryStopTask()
        {
            return ai.StopTask();
        }
        #endregion

        public AIThing()
        {
            ai = new AI();
        }

        /// <summary>
        /// 不使用配置文件初始化AIThing
        /// 有可能配置出错
        /// </summary>
        /// <param name="dataFileParentDirectory">存放所有数据文件的根文件夹路径</param>
        [Obsolete("为避免错误，最好使用配置文件配置AIThing")]
        public void InitAIThing(string dataFileParentDirectory)
        {
            //使用默认值初始化AI
            StaticConfig staticConfig = new StaticConfig();

            subInit(dataFileParentDirectory, staticConfig);
        }

        /// <summary>
        /// 使用配置文件初始化AIThing
        /// </summary>
        /// <param name="dataFileParentDirectory">存放所有数据文件的根文件夹路径</param>
        /// <param name="configFilePath">配置文件完整路径</param>
        public void InitAIThing(string dataFileParentDirectory, string configFilePath)
        {
            //用读到的配置文件初始化AI
            staticConfig = new StaticConfig(configFilePath);

            subInit(dataFileParentDirectory, staticConfig);

        }

        private void subInit(string dataFileParentDirectory, StaticConfig staticConfig)
        {
            DataFileParentDirectory = dataFileParentDirectory;

            ai.InitAI(staticConfig);

            //订阅AI的event
            subscribeEvent();
            //如果AutoReArm为真，则自动开始任务
            if (AutoReArm == true)
            {
                TryArm();
            }
        }

        private void subscribeEvent()
        {
            ai.RaiseDataArrivalEvent += DataArrivalHandler;
            ai.RaiseStatusChangeEvent += StatusChangedHandler;
            ai.RaiseStopEvent += AIStopHandler;
        }

        /// <summary>
        /// 数据到达事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataArrivalHandler(object sender, DataArrivalEventArgs e)
        {
            dataWriter.AcceptNewData(e.NewData);
        }

        /// <summary>
        /// AI任务状态变化事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusChangedHandler(object sender, EventArgs e)
        {
            //sender其实就是ai
            AI thisAI = (AI)sender;
            switch (thisAI.AIState)
            {
                case Status.Idle:
                    AIState = Status.Idle;
                    break;
                case Status.Ready:
                    //创建下一个文件夹
                    CurrentFileDirectiory = DataFileWriter.CreateNextDirectory(DataFileParentDirectory);
                    //new CurrentData，缓存当前炮数据及将数据写入文件
                    dataWriter = new DataWriter(CurrentFileDirectiory, AllActiveChannels.Count);
                    AIState = Status.Ready;
                    break;
                case Status.Running:
                    AIState = Status.Running;
                    break;
                case Status.Error:
                    thisAI.StopTask();
                    AIState = Status.Error;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// AI任务结束
        /// 保存时间文件及配置文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AIStopHandler(object sender, EventArgs e)
        {         
            //不再读数据，把数据文件写完
            dataWriter.FinishWrite();
            //保存配置文件
            staticConfig.TotalSampleLengthPerChannel = dataWriter.TotalSampleCount;
            staticConfig.ChannelNames = AllActiveChannels;
            staticConfig.Save(CurrentFileDirectiory);

            //停止任务
            TryStopTask();

            //自动重新开始任务
            if (AutoReArm)
            {
                TryArm();
            }
        }

        /// <summary>
        /// 从文件中读取指定炮、指定通道、指定数目的数据
        /// </summary>
        /// <param name="shotNo"></param>
        /// <param name="channelNo"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public double[] LoadShot(int shotNo, int channelNo, int start, long length)
        {
            string fileDirectory = DataFileParentDirectory + "\\" + shotNo.ToString();
            DataLoader dataLoader = new DataLoader(fileDirectory);
            double[] data = dataLoader.LoadDataFromFile(channelNo, start, length);
            return data;
        }
    }
}
