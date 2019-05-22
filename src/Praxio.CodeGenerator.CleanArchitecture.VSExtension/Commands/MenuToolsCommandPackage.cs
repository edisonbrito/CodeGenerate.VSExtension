using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Commands
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#3110", "#3112", "1.0", IconResourceID = 3400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(MenuToolsCommandPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class MenuToolsCommandPackage : AsyncPackage
    {
        public DTE2 Dte;
        public static MenuToolsCommandPackage Instance;

        public const string PackageGuidString = "afe9cd1c-b4fd-4f27-bd29-d3083c21ac5e";

        public MenuToolsCommandPackage()
        {
        }

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await MenuToolsCommand.InitializeAsync(this);
            await CriarCrudCommand.InitializeAsync(this);
            await RegerarCrudCommand.InitializeAsync(this);

            Dte = (DTE2)ServiceProvider.GlobalProvider.GetService(typeof(DTE));
            Instance = this;
        }
    }
}
