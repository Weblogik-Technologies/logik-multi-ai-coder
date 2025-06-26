using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Logik.MultiAiCoder.VisualStudio.UI
{
    [Guid("3441D20C-98FB-4E54-AD7C-12C9F586D9CE")]
    public class LogikMultiAiToolWindow : ToolWindowPane
    {
        public LogikMultiAiToolWindow() : base(null)
        {
            this.Caption = "Logik Multi AI Coder";
            //this.Content = new System.Windows.Controls.TextBlock { Text = "Hello from Logik" };
            this.Content = new LogikMultiAiControl();
            Trace.WriteLine("[LogikMultiAiToolWindow] ToolWindow built.");
        }
    }
}
