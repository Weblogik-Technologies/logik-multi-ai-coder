using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace Logik.MultiAiCoder.VS2022
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("Multi AI Coder", "", "0.1")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid("1D450CDF-90D7-4B36-9A29-78630D5DD9E2")]
    public sealed class VsPackage : AsyncPackage
    {
        protected override System.Threading.Tasks.Task InitializeAsync(System.Threading.CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
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
            return base.InitializeAsync(cancellationToken, progress);
        }
    }
}
