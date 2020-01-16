using System;
using Microsoft.SPOT;
using System.Threading;

namespace Demo.TestControls
{
    class ThreadHelper
    {
        public static void Run(ThreadStart threadStart)
        {
            var thread = new Thread(threadStart);
            thread.Start();
        }
    }
}
