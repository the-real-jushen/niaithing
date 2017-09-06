using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtext103.CFET2.Things.NiAiLib
{
    public class DataArrivalEventArgs: EventArgs
    {
        private double[,] newData;

        /// <summary>
        /// List中每个数组代表一个通道的数据
        /// </summary>
        public double[,] NewData
        {
            get
            {
                return newData;
            }
        }

        public DataArrivalEventArgs(double[,] data)
        {
            newData = data;

            //data.CopyTo(NewData, 0);
        }
    }
}
