using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Logik.MultiAiCoder.VisualStudio.VSUI
{
    [Guid("3441D20C-98FB-4E54-AD7C-12C9F586D9CE")]
    public class LogikMultiAiToolWindow : ToolWindowPane
    {
        public LogikMultiAiToolWindow() : base(null)
        {
            this.Caption = "Logik Multi AI Coder";
            this.Content = new LogikMultiAiControl();
        }
    }
}
