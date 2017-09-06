using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtext103.CFET2.Things.NiAiLib
{
    public class ArrayIsFullEventArgs: EventArgs
    {
        private double[] data;

        private int no;

        /// <summary>
        /// 数据
        /// </summary>
        public double[] Data
        {
            get
            {
                return data;
            }
        }

        /// <summary>
        /// 唯一标识符
        /// </summary>
        public int No
        {
            get
            {
                return no;
            }

        }

        public ArrayIsFullEventArgs(double[] data, int no)
        {
            this.data = data;
            this.no = no;
        }
    }
}
