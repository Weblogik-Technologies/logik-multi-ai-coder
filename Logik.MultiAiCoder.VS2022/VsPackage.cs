using Logik.MultiAiCoder.VS2022.ToolWindow;
using Logik.MultiAiCoder.VS2022.UI;
using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace Logik.MultiAiCoder.VS2022
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("Logik Multi AI Coder", "", "0.1")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid("1D450CDF-90D7-4B36-9A29-78630D5DD9E2")]
    [ProvideToolWindow(typeof(LogikMultiAiToolWindow))]
    public sealed class VsPackage : AsyncPackage
    {
        protected override async System.Threading.Tasks.Task InitializeAsync(System.Threading.CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            var configs = Engine.PromptConfigurationStore.Load();
            if (configs.Count == 0)
            {
                configs.AddRange(new[]
                {
                    new Engine.PromptConfiguration { Provider = "OpenAI", Model = "gpt-4o", Order = 0 },
                    new Engine.PromptConfiguration { Provider = "Claude", Model = "3-opus", Order = 1 },
                    new Engine.PromptConfiguration { Provider = "Gemini", Model = "1.5-pro", Order = 2 },
                    new Engine.PromptConfiguration { Provider = "AzureOpenAI", Model = "gpt-4o", Order = 3 }
                });
                Engine.PromptConfigurationStore.Save(configs);
            }

            var window = await this.ShowToolWindowAsync(typeof(LogikMultiAiToolWindow), 0, true, cancellationToken);

            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException("Cannot create Logik Multi AI Coder tool window.");
            }

        }
    }
}
