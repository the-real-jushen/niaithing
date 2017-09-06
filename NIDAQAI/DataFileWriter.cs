using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtext103.CFET2.Things.NiAiLib
{
    public class DataFileWriter
    {
        /// <summary>
        /// 用于写二进制数据文件
        /// </summary>
        private BinaryWriter fileBinaryWriter;

        /// <summary>
        /// 保存数据文件和配置文件的文件夹
        /// </summary>
        private string fileDirectory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileDirectory"></param>
        public DataFileWriter(string fileDirectory)
        {
            this.fileDirectory = fileDirectory;
        }

        /// <summary>
        /// 读取上层目录中所有文件夹名，并按从小到大数字规则新建下一炮的文件夹
        /// </summary>
        /// <param name="parentDirectory">上层目录路径</param>
        /// <returns>新建的文件夹名称</returns>
        public static string CreateNextDirectory(string parentDirectory)
        {
            //新建的目录完整路径
            string nextDirectoryPath;
            //新建的目录名
            string nextDirectoryName;
            //其他目录完整路径
            string[] otherDirectories = Directory.GetDirectories(parentDirectory);
            //其他目录名
            List<int> otherDirectoryNames = new List<int>();
            foreach (string otherDirectory in otherDirectories)
            {
                int name;

                if (int.TryParse(Path.GetFileName(otherDirectory), out name))
                {
                    otherDirectoryNames.Add(name);
                }
            }
            //当已经存在多个文件夹时，找到最大数并+1后设为目录名
            //否则将目录名设为1
            if (otherDirectoryNames.Count > 0)
            {
                //顺序排序
                otherDirectoryNames.Sort();
                nextDirectoryName = (otherDirectoryNames.Last() + 1).ToString();
            }
            else
            {
                nextDirectoryName = "1";

            }
            nextDirectoryPath = parentDirectory + "\\" + nextDirectoryName;
            Directory.CreateDirectory(nextDirectoryPath);
            return nextDirectoryPath;
        }

        /// <summary>
        /// 创建以当前时间命名的空文件，用于保存放电时间
        /// </summary>
        /// <param name="fileDirectory"></param>
        public void SaveShotTimeToFile()
        {
            string fileName = DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-dd@HH-mm-ss");
            FileStream fs = new FileStream(fileDirectory + "\\" + fileName, FileMode.Create);
            fs.Close();
        }

        /// <summary>
        /// 创建保存数据二进制文件
        /// 该方法必须与CloseBinaryWriter成对使用
        /// </summary>
        /// <param name="fileDirectory">文件对应的BinaryWriter，供之后写数据用</param>
        public void CreateDataFileAndBinaryWriter()
        {
            //FileStream fs = new FileStream(fileDirectory + "\\data", FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
            //fs.WriteAsync();
            FileStream fs = new FileStream(fileDirectory + "\\data", FileMode.Create);
            fileBinaryWriter = new BinaryWriter(fs);
        }

        /// <summary>
        /// 关闭BinaryWriter
        /// </summary>
        /// <param name="fileBinaryWriter"></param>
        public void CloseBinaryWriter()
        {
            fileBinaryWriter.Close();
        }

        int testNo = 0;

        /// <summary>
        /// 将数据写入BinaryWriter中
        /// 存储格式如下：ch0[0],ch1[0],ch2[0]...ch0[1],ch1[1],ch2[1]...
        /// 数据均为double格式
        /// </summary>
        /// <param name="data"></param>
        public void AppendDataToBinaryWriter(double[,] data)
        {
            int channelCount = data.GetLength(0);
            int dataLengthPerChannel = data.GetLength(1);
            //double类型字节长度，理论上是8
            int doubleSize = sizeof(double);
            //double[,] 转成byte[]，再将该byte[] 写文件，理论上能加快速度
            byte[] buffer = new byte[channelCount * dataLengthPerChannel * doubleSize];
            //先找到末尾位置，从末尾开始写数据
            fileBinaryWriter.BaseStream.Seek(0, SeekOrigin.End);

            System.Diagnostics.Debug.WriteLine("转byte开始 " + testNo + " " + DateTime.Now.ToString("mm-ss.fffffff"));
            //写入数据(double)
            for (int j = 0; j < dataLengthPerChannel; j++)
            {
                for (int i = 0; i < channelCount; i++)
                {
                    //将double转成byte[]，再将该byte[]复制到buffer中
                    BitConverter.GetBytes(data[i, j]).CopyTo(buffer, (i + j * channelCount) * doubleSize);
                    //fileBinaryWriter.Write(data[i, j]);
                }
            }
            System.Diagnostics.Debug.WriteLine("转byte结束 " + testNo + " " + DateTime.Now.ToString("mm-ss.fffffff"));

            System.Diagnostics.Debug.WriteLine("写文件开始 " + testNo + " " + DateTime.Now.ToString("mm-ss.fffffff"));
            fileBinaryWriter.Write(buffer);
            System.Diagnostics.Debug.WriteLine("写文件结束 " + testNo + " " + DateTime.Now.ToString("mm-ss.fffffff"));
            testNo++;
        }

    }
}