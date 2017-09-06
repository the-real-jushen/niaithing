using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtext103.CFET2.Things.NiAiLib
{
    /// <summary>
    /// 任务状态
    /// </summary>
    public enum Status
    {
        Idle = 0,

        Ready = 1,

        Running = 2,

        Error = 255
    }
}
