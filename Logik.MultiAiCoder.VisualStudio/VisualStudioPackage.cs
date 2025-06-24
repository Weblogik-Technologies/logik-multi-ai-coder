using Logik.MultiAiCoder.VisualStudio.VSUI;
using Logik.MultiAiCoder.Engine;
using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace Logik.MultiAiCoder.VisualStudio
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(VisualStudioPackage.PackageGuidString)]
    [InstalledProductRegistration("Logik Multi AI Coder", "", "0.1")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(LogikMultiAiToolWindow))]
    public sealed class VisualStudioPackage : AsyncPackage
    {
         /// <summary>
        /// Logik.MultiAiCoder.VisualStudioPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "48d80075-29a9-44c8-befe-fe6a28559979";

    #region Package Members

    /// <summary>
    /// Initialization of the package; this method is called right after the package is sited, so this is the place
    /// where you can put all the initialization code that rely on services provided by VisualStudio.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
    /// <param name="progress">A provider for progress updates.</param>
    /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
    protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
    {
        // When initialized asynchronously, the current thread may be a background thread at this point.
        // Do any initialization that requires the UI thread after switching to the UI thread.
        await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            var configs = PromptConfigurationStore.Load();
            if (configs.Count == 0)
            {
                configs.AddRange(new[]
                {
                    new Engine.PromptConfiguration { Provider = "OpenAI", Model = "gpt-4o", Order = 0 },
                    new Engine.PromptConfiguration { Provider = "Claude", Model = "3-opus", Order = 1 },
                    new Engine.PromptConfiguration { Provider = "Gemini", Model = "1.5-pro", Order = 2 },
                    new Engine.PromptConfiguration { Provider = "AzureOpenAI", Model = "gpt-4o", Order = 3 }
                });
                PromptConfigurationStore.Save(configs);
            }

            var window = await this.ShowToolWindowAsync(typeof(LogikMultiAiToolWindow), 0, true, cancellationToken);

            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException("Cannot create Logik Multi AI Coder tool window.");
            }
        }

    #endregion
}
}
