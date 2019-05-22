using System;
using System.Diagnostics;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util
{
    public static class LinhaDeComando
    {
        public static string Executar(string command)
        {
            return Executar(command, string.Empty);
        }

        public static string Executar(string command, string workingDirectory)
        {
            using (Process processo = new Process())
            {
                processo.StartInfo = new ProcessStartInfo
                {
                    FileName = Environment.GetEnvironmentVariable("comspec"),
                    Arguments = string.Format("/c {0}", command),
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WorkingDirectory = workingDirectory
                };

                processo.Start();
                processo.WaitForExit();

                var saida = processo.StandardOutput.ReadToEnd();
                return saida;
            }
        }
    }
}