using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtext103.CFET2.Things.NiAiLib
{
    public enum SamplesMode
    {
        //
        // 摘要:
        //     Acquire or generate samples until you stop the task.
        ContinuousSamples = 1,
        //
        // 摘要:
        //     Acquire or generate a finite number of samples.
        FiniteSamples = 2,
        //
        // 摘要:
        //     Acquire or generate samples continuously using hardware timing without a buffer.
        //     javascript:launchSharedHelp('mxcncpts.chm::/HWTSPSampleMode.html'); sample mode
        //     is supported only for the sample clock and change detection timing types.
        HardwareTimedSinglePoint = 3
    }
}
