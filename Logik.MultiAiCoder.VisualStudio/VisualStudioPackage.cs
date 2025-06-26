using Logik.MultiAiCoder.Engine;
using Logik.MultiAiCoder.VisualStudio.UI;
using Logik.MultiAiCoder.VisualStudio.Helpers;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO.Packaging;
using System.Linq;
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
    [InstalledProductRegistration("Logik Multi AI Coder", "Multi AI Coder from different AI platforms side-by-side", "0.1")]
    [ProvideToolWindow(typeof(LogikMultiAiToolWindow))]
    [ProvideMenuResource("1000", 1)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class VisualStudioPackage : AsyncPackage
    {
        /// <summary>
        /// Logik.MultiAiCoder.VisualStudioPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "48d80075-29a9-44c8-befe-fe6a28559979";

        public VisualStudioPackage()
        {
            System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.DefaultTraceListener());
        }

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
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            // Obtenir la fenêtre Output
            IVsOutputWindow outWindow = await GetServiceAsync(typeof(SVsOutputWindow)) as IVsOutputWindow;
            if (outWindow != null)
            {
                Guid paneGuid = new Guid("C243AF0F-2E2C-4DC4-8E90-86337705E4A6"); // GUID personnalisé (choisi par toi)
                string paneTitle = "Logik AI Coder";

                // Créer le pane s'il n'existe pas encore
                outWindow.CreatePane(ref paneGuid, paneTitle, fInitVisible: 1, fClearWithSolution: 1);
                outWindow.GetPane(ref paneGuid, out IVsOutputWindowPane pane);

                if (pane != null)
                {
                    pane.Activate(); // Affiche le pane dans Visual Studio
                    Trace.Listeners.Add(new OutputWindowTraceListener(pane));
                    Trace.WriteLine("✅ Logik Multi AI Coder Output pane ready.");
                }
            }

            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            await MyCommands.InitializeAsync(this);

            PromptConfigurationStore.SetDefaultAiProviders();
           
            if (false)
            {
                JoinableTaskFactory.RunAsync(async () =>
                {
                    try
                    {
                        var window = await this.ShowToolWindowAsync(typeof(UI.LogikMultiAiToolWindow), 0, true, this.DisposalToken);

                        if (window == null || window.Frame == null)
                        {
                            throw new NotSupportedException("Cannot create tool window.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine($"Error creating Logik Multi AI Coder tool window: {ex.Message}");
                        //throw;
                    }
                });
            }

            //var window = await this.ShowToolWindowAsync(typeof(LogikMultiAiToolWindow), 0, true, cancellationToken);

            //if ((null == window) || (null == window.Frame))
            //{
            //    throw new NotSupportedException("Cannot create Logik Multi AI Coder tool window.");
            //}

            Trace.WriteLine("Logik Multi AI Coder package initialized.");
        }

        #endregion
    }
}
