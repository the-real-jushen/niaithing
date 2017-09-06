using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jtext103.CFET2.Things.NiAiLib
{
    /// <summary>
    /// 暂时保存采集到的的数据，并将数据存入文件
    /// </summary>
    public class DataWriter
    {
        /// <summary>
        /// 数据
        /// </summary>
        private Queue<double[,]> dataQueue;

        private bool shouldFinish { get; set; }

        private bool isFinished { get; set; }

        private DataFileWriter dataFileWriter;
        
        /// <summary>
        /// 每通道写入数据总数
        /// </summary>
        public long TotalSampleCount { get; private set; }

        /// <summary>
        /// 总通道数
        /// </summary>
        private int totalChannelCount;

        /// <summary>
        /// 每个通道的最新数据
        /// </summary>
        private double[] rtDataArray;

        /// <summary>
        /// 队列中数据个数
        /// 即待写入文件的List个数
        /// </summary>
        public int DataToWrite
        {
            get
            {
                return dataQueue.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileDirectory">保存数据和配置文件的文件夹</param>
        /// <param name="channelCount">通道数</param>
        public DataWriter(string fileDirectory, int channelCount)
        {
            shouldFinish = false;
            isFinished = false;
            dataQueue = new Queue<double[,]>();
            TotalSampleCount = 0;
            totalChannelCount = channelCount;
            rtDataArray = new double[totalChannelCount];

            dataFileWriter = new DataFileWriter(fileDirectory);

            OperateFile();
        }

        /// <summary>
        /// 获得指定通道最新数据
        /// </summary>
        /// <param name="channelNo"></param>
        /// <returns></returns>
        public double GetRtData(int channelNo)
        {
            return rtDataArray[channelNo];
        }

        /// <summary>
        /// 接收新数据
        /// 将新数据放入队列中
        /// </summary>
        /// <param name="data"></param>
        public void AcceptNewData(double[,] data)
        {
            //只需把引用传到queue中，保证queue中引用不会被外界修改
            double[,] cloneData = data;
            //int dataLength = data.GetLength(0) * data.GetLength(1);
            //Array.Copy(data, cloneData, dataLength);

            //入列时更新最新数据
            for (int i = 0; i < totalChannelCount; i++)
            {
                rtDataArray[i] = data[i, data.GetLength(0)];
            }
            //入列
            dataQueue.Enqueue(cloneData);
        }

        /// <summary>
        /// 采集停止，将不再传入新数据
        /// 一直等待队列中数据写完
        /// </summary>
        public void FinishWrite()
        {
            shouldFinish = true;
            waitUntilFinish();
        }

        /// <summary>
        /// 一直等待，直到队列中数据写完
        /// </summary>
        private void waitUntilFinish()
        {
            while(isFinished == false)
            {
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 操作数据文件
        /// 1.建立数据文件，打开对应的流
        /// 2.（异步）不断将数据写入流
        /// 3.完成后，关闭流，保存文件，IsFinished = true
        /// </summary>
        /// <returns></returns>
        private async Task OperateFile()
        {            
            //建立数据文件，并打开流，准备写数据
            dataFileWriter.CreateDataFileAndBinaryWriter();
            //异步将数据写入流
            await Task.Run(() => { sendDataToBinaryWriter(); });
            //写完后关闭流，保存文件
            dataFileWriter.CloseBinaryWriter();
            //保存放电时间
            dataFileWriter.SaveShotTimeToFile();

            isFinished = true;
        }

        //private async Task sendDataToBinaryWriter(BinaryWriter fileBinaryWriter)
        //{
        //    await Task.Run(() =>
        //    {
        //        while (dataQueue.Count > 0 || (shouldFinish == false))
        //        {
        //            if (dataQueue.Count > 0)
        //            {
        //                List<double[]> data = dataQueue.Dequeue();
        //                SaveLoadFile.AppendDataToMemoryBuffer(fileBinaryWriter, data);
        //            }
        //            else
        //            {
        //                Thread.Sleep(100);
        //            }
        //        }
        //    }
        //    );
        //}
        
        /// <summary>
        /// 数据出队列，并写入流中
        /// 只要queue中有数据，就要一直写
        /// </summary>
        /// <param name="fileBinaryWriter"></param>
        private void sendDataToBinaryWriter()
        {
            while (dataQueue.Count > 0 || (shouldFinish == false))
            {
                //队列中还有数据，将数据读出并写文件
                //只有dataQueue累计到一定数量或已经采集完毕后，才开始向文件中写数据
                if ((dataQueue.Count > 0 && shouldFinish) || (dataQueue.Count > 10 && shouldFinish == false))
                {
                    double[,] data = dataQueue.Dequeue();
                    dataFileWriter.AppendDataToBinaryWriter(data);
                    TotalSampleCount += data.GetLength(1);
                }
                //队列中没数据，等待数据到来
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}
