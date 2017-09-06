using AutoMapper;
using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtext103.CFET2.Things.NiAiLib
{
    public class AI
    {
        #region public AI status
        /// <summary>
        /// AI任务状态
        /// </summary>
        public Status AIState { get; private set; }

        /// <summary>
        /// 最新一炮开始时间
        /// </summary>
        public DateTime LastShotTime { get; private set; }

        /// <summary>
        /// 触发方式
        /// </summary>
        public TriggerType AITriggerType { get; private set; }

        /// <summary>
        /// 配置终端输入方式（差分、单端）
        /// </summary>
        public TerminalConfiguration TerminalConfig { get; private set; }
        /// <summary>
        /// 输入电压最小值
        /// </summary>
        public double MinimumVolt { get; private set; }

        /// <summary>
        /// 输入电压最大值
        /// </summary>
        public double MaximumVolt { get; private set; }

        /// <summary>
        /// 采样率
        /// </summary>
        public int SampleRate { get; private set; }

        /// <summary>
        /// 触发通道（仅用于外部触发）
        /// </summary>
        public string TriggerSource { get; private set; }

        /// <summary>
        /// 触发边沿
        /// </summary>
        public Edge AITriggerEdge { get; private set; }

        /// <summary>
        /// 触发电平（仅用于外部模拟触发）
        /// </summary>
        public double AnalogTriggerLevel { get; private set; }

        /// <summary>
        /// 时钟源（使用内部时钟时，为空字符串）
        /// </summary>
        public string ClkSource { get; private set; }

        /// <summary>
        /// 时钟边沿
        /// </summary>
        public Edge ClkActiveEdge { get; private set; }

        /// <summary>
        /// 采样方式
        /// </summary>
        public SamplesMode AISamplesMode { get; private set; }

        /// <summary>
        /// 每通道采样数（仅用于有限采样）
        /// </summary>
        public int SamplesPerChannel { get; private set; }

        /// <summary>
        /// 每次读取数据个数
        /// </summary>
        public int ReadSamplePerTime { get; private set; }

        /// <summary>
        /// 指示是否自动重新ARM
        /// 只对外部触发的有限采样有效
        /// </summary>
        public bool AutoReArm { get; private set; }

        /// <summary>
        /// 使用的AI通道
        /// </summary>
        public List<string> AllActiveChannels { get; private set; }

        #endregion

        public event EventHandler RaiseStopEvent;

        public event EventHandler RaiseStatusChangeEvent;

        public event EventHandler<DataArrivalEventArgs> RaiseDataArrivalEvent;

        private string channel { get; set; }

        /// <summary>
        /// AI任务
        /// </summary>
        private NationalInstruments.DAQmx.Task aiTask;        

        /// <summary>
        /// 用于NI采集卡AI读数据
        /// </summary>
        private AnalogMultiChannelReader reader;

        /// <summary>
        /// reader读数据时回调
        /// </summary>
        private AsyncCallback aiReaderCallback;
        
        /// <summary>
        /// 使用默认参数构造AI
        /// </summary>
        public AI()
        {
            AIState = Status.Idle;
            LastShotTime = DateTime.UtcNow;            
        }

        public void InitAI(StaticConfig config)
        {
            AITriggerType = config.AITriggerType;
            TerminalConfig = config.TerminalConfig;
            MinimumVolt = config.MinimumVolt;
            MaximumVolt = config.MaximumVolt;
            SampleRate = config.SampleRate;
            TriggerSource = config.TriggerSource;
            AITriggerEdge = config.AITriggerEdge;
            AnalogTriggerLevel = config.AnalogTriggerLevel;
            ClkSource = config.ClkSource;
            ClkActiveEdge = config.ClkActiveEdge;
            AISamplesMode = config.AISamplesMode;
            SamplesPerChannel = config.SamplesPerChannel;
            ReadSamplePerTime = config.ReadSamplePerTime;
            channel = config.Channel;
            AutoReArm = config.AutoReArm;
        }

        /// <summary>
        /// 启动AI采集任务
        /// </summary>
        public void TryArm()
        {
            if (AIState != Status.Idle)
            {
                throw new Exception("If you want to arm, the AI state must be 'Idle'!");
            }
            else
            {
                if (aiTask == null)
                {
                    try
                    {
                        //autoMapper
                        //createMap时，输出enum直接是输入enum的值，所以不对
                        //不createMap时，使用name匹配，是我们需要的，输出enum是对应name的值，但是必须保证输入和输出enum的name完全对应
                        var config = new MapperConfiguration(cfg => { });
                        IMapper mapper = new Mapper(config);

                        AITerminalConfiguration niTerminalConfig = mapper.Map<AITerminalConfiguration>(TerminalConfig);

                        //Create a task that will be disposed after it has been used
                        aiTask = new NationalInstruments.DAQmx.Task();

                        //Create a virtual channel
                        aiTask.AIChannels.CreateVoltageChannel(channel, "", niTerminalConfig, MinimumVolt, MaximumVolt, AIVoltageUnits.Volts);

                        //Trigger type
                        switch (AITriggerType)
                        {
                            //softTrigger = start directly
                            case TriggerType.SoftTrigger:
                                break;
                            case TriggerType.DigitalTrigger:
                                var digitalTriggerEdge = mapper.Map<DigitalEdgeStartTriggerEdge>(AITriggerEdge);
                                aiTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(TriggerSource, digitalTriggerEdge);
                                break;
                            case TriggerType.AnalogTrigger:
                                var analogTriggerEdge = mapper.Map<AnalogEdgeStartTriggerSlope>(AITriggerEdge);
                                aiTask.Triggers.StartTrigger.ConfigureAnalogEdgeTrigger(TriggerSource, analogTriggerEdge, AnalogTriggerLevel);
                                break;
                            default:
                                break;
                        }

                        ////Timing
                        SampleQuantityMode sampleQuantityMode = mapper.Map<SampleQuantityMode>(AISamplesMode);

                        var sampleClockActiveEdge = mapper.Map<SampleClockActiveEdge>(ClkActiveEdge);

                        //经过测试，对于aiTask.Timing.ConfigureSampleClock
                        //有限采样时，采样SamplesPerChannel 个点
                        //连续采样时，一直采集，直到手动停止
                        aiTask.Timing.ConfigureSampleClock(ClkSource, SampleRate, sampleClockActiveEdge, sampleQuantityMode, SamplesPerChannel);

                        //Verify the Task
                        aiTask.Control(TaskAction.Verify);                        

                        //将所有通道名赋给AllActiveChannels
                        AllActiveChannels = new List<string>();
                        var channelCollection = aiTask.AIChannels;
                        int numOfChannels = channelCollection.Count;
                        for (int currentChannelIndex = 0; currentChannelIndex < numOfChannels; currentChannelIndex++)
                        {
                            AllActiveChannels.Add(channelCollection[currentChannelIndex].PhysicalName);
                        }

                        AIState = Status.Ready;
                        //idle -> ready
                        OnStatusChanged();

                        //SoftTrigger means start the task directly
                        //if (AITriggerType == TriggerType.SoftTrigger)
                        //{
                        //    AIState = Status.Running;
                        //}

                        //read stream
                        reader = new AnalogMultiChannelReader(aiTask.Stream);
                        reader.SynchronizeCallbacks = false;
                        aiReaderCallback = new AsyncCallback(aiCallback);
                        reader.BeginReadMultiSample(ReadSamplePerTime, aiReaderCallback, aiTask);

                    }
                    catch (DaqException ex)
                    {
                        //ex.Message
                        goError();
                    }
                }
            }
        }

        /// <summary>
        /// 停止任务，回到idle状态
        /// </summary>
        /// <returns></returns>
        public bool StopTask()
        {
            if (aiTask != null)
            {
                aiTask.Stop();
                aiTask.Dispose();
                aiTask = null;

                AIState = Status.Idle;
                OnStatusChanged();

                return true;
            }
            else
            {
                return false;
            }
        }

        private int testNo = 0;
        
        /// <summary>
        /// 用于读AI数据的回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void aiCallback(IAsyncResult ar)
        {
            try
            {
                if (aiTask == ar.AsyncState)
                {
                    if (AIState == Status.Ready)
                    {
                        AIState = Status.Running;
                        //ready -> running
                        OnStatusChanged();
                    }

                    if (aiTask.IsDone)
                    {
                        System.Diagnostics.Debug.WriteLine("任务结束，开始 "+ DateTime.Now.ToString("mm-ss.fffffff"));
                        //任务结束，把数据读完
                        double[,] readData = reader.EndReadMultiSample(ar);
                        OnDataArrival(readData);
                        System.Diagnostics.Debug.WriteLine("任务结束，结束 " + DateTime.Now.ToString("mm-ss.fffffff"));
                        //产生任务结束事件
                        //在该事件处理函数中结束当前任务
                        OnStopped();
                    }
                    else
                    {
                        //任务未结束
                        //read data
                        //EndReadMultiSample返回值中第0维代表了通道数，第1维对应各个通道的数据

                        System.Diagnostics.Debug.WriteLine("读取开始 " + testNo + " " + DateTime.Now.ToString("mm-ss.fffffff"));
                        double[,] readData = reader.EndReadMultiSample(ar);
                        System.Diagnostics.Debug.WriteLine("读取结束 " + testNo + " " + DateTime.Now.ToString("mm-ss.fffffff"));
                        //OnDataArrival(new double[, ] { { 1 }, { 1 }, { 1 }, { 1 }, { 1 }, { 1 }, { 1 }, { 1 } , { 1 }, { 1 }, { 1 }, { 1 }, { 1 }, { 1 }, { 1 }, { 1 } });
                        OnDataArrival(readData);
                        System.Diagnostics.Debug.WriteLine("事件结束 " + testNo + " " + DateTime.Now.ToString("mm-ss.fffffff"));

                        testNo++;

                        //iteration
                        reader.BeginReadMultiSample(ReadSamplePerTime, aiReaderCallback, aiTask);
                    }
                }
            }
            catch (DaqException ex)
            {
                //ex.Message
                goError();
                throw ex;

            }
        }

        /// <summary>
        /// invoke RaiseStopEvent
        /// </summary>
        protected virtual void OnStopped()
        {
            LastShotTime = DateTime.UtcNow;
            RaiseStopEvent?.Invoke(this, new EventArgs());
            //相当于下面的
            //if (StopEventHandler != null)
            //{
            //    StopEventHandler(this, new EventArgs());
            //}
        }

        /// <summary>
        /// invoke RaiseStatusChangeEvent
        /// </summary>
        protected virtual void OnStatusChanged()
        {
            RaiseStatusChangeEvent?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// invoke RaiseDataArrivelEvent
        /// </summary>
        protected virtual void OnDataArrival(double[,] data)
        {
            //如果传过来了数据，产生事件
            if (data.GetLength(1) > 0)
            {
                RaiseDataArrivalEvent?.Invoke(this, new DataArrivalEventArgs(data));
            }
        }

        /// <summary>
        /// 进入ERROR状态
        /// 状态改为ERROR并停止任务
        /// </summary>
        private void goError()
        {
            AIState = Status.Error;
            OnStatusChanged();
        }
    }
}
