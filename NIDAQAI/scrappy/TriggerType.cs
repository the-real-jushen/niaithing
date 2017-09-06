using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtext103.CFET2.Things.NiAiLib
{
    /// <summary>
    /// 触发方式
    /// </summary>
    public enum TriggerType
    {
        //SoftTrigger开始任务，直接运行
        SoftTrigger = 0,
        DigitalTrigger = 1,
        AnalogTrigger = 2
    }
}
