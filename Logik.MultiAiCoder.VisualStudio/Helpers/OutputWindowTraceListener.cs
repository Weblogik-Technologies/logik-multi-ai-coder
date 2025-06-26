using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logik.MultiAiCoder.VisualStudio.Helpers
{
    public class OutputWindowTraceListener : TraceListener
    {
        private readonly IVsOutputWindowPane _pane;

        public OutputWindowTraceListener(IVsOutputWindowPane pane)
        {
            _pane = pane;
        }

        public override void Write(string message)
        {
            _pane?.OutputString(message);
        }

        public override void WriteLine(string message)
        {
            _pane?.OutputString(message + Environment.NewLine);
        }
    }
}