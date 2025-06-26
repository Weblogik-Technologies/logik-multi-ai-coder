using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using Task = System.Threading.Tasks.Task;

namespace Logik.MultiAiCoder.VisualStudio
{
    internal sealed class MyCommands
    {
        public const int CommandId = 0x0100;
        public static readonly Guid CommandSet = new Guid("1D7B1437-2B97-43D1-BAA4-5D1E01CF6310");
        private readonly AsyncPackage package;

        private MyCommands(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to main thread to use services
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            new MyCommands(package, commandService);
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            package.JoinableTaskFactory.RunAsync(async () =>
            {
                try
                {
                    var window = await package.ShowToolWindowAsync(typeof(UI.LogikMultiAiToolWindow), 0, true, package.DisposalToken);

                    if (window == null || window.Frame == null)
                    {
                        throw new NotSupportedException("Cannot create tool window.");
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"Error creating Logik Multi AI Coder tool window: {ex.Message}");
                    throw;
                }
            });
        }
    }
}
