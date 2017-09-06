using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtext103.CFET2.Things.NiAiLib
{
    public class DataLoader
    {
        private string dataFilePath;

        private string configFilePath;

        public DataLoader(string fileDirectory)
        {
            dataFilePath = fileDirectory + "\\data";
            configFilePath = fileDirectory + "\\config.txt";
        }

        /// <summary>
        /// 从文件中读出指定通道的数据片段
        /// </summary>
        /// <param name="channelNo">指定的通道，从0开始</param>
        /// <param name="start">大于等于0</param>
        /// <param name="length">如果小于0则代表从start开始，读余下的所有点；大于等于零则代表读length个数的点</param>
        /// <returns></returns>
        public double[] LoadDataFromFile(int channelNo, int start, long length)
        {
            StaticConfig config = new StaticConfig(configFilePath);
            int totalChannelNo = config.ChannelNames.Count();
            long totalDataLength = config.TotalSampleLengthPerChannel;
            //真正读数据的长度
            long readDataCount;
            if (start >= 0)
            {
                if (channelNo > totalChannelNo)
                {
                    throw new Exception("通道号错误！");
                }
                if (length < 0)
                {
                    readDataCount = totalDataLength - start;
                }
                else
                {
                    readDataCount = length;
                    if (start + readDataCount > totalDataLength)
                    {
                        throw new Exception("读取请求位置和长度不匹配！");
                    }
                }
            }
            else
            {
                throw new Exception("读取请求数据起始位置应大于等于0！");
            }

            double[] result = new double[readDataCount];            
            int doubleSize = sizeof(double);
            
            FileStream dataFileStream = new FileStream(dataFilePath, FileMode.Open);
            BinaryReader fileBinaryReader = new BinaryReader(dataFileStream);

            for (long i = 0; i < readDataCount; i++)
            {
                //找到对应double的位置
                fileBinaryReader.BaseStream.Seek((start * totalChannelNo + channelNo + i * totalChannelNo) * doubleSize, SeekOrigin.Begin);
                var a = fileBinaryReader.BaseStream.Position;
                result[i] = fileBinaryReader.ReadDouble();
            }
            fileBinaryReader.Close();

            return result;
        }
    }
}
