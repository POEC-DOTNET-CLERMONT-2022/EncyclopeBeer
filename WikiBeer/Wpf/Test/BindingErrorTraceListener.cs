using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Wpf.Test
{
    public class BindingErrorTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            Trace.WriteLine(string.Format("==[Write]{0}==", message));
        }

        public override void WriteLine(string message)
        {
            Trace.WriteLine(string.Format("==[WriteLine]{0}==", message));
        }
    }
}
