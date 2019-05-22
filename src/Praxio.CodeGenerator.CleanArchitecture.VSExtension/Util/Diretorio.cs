using System.IO;
using System.Linq;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util
{
    public static class Diretorio
    {
        public static void CriarSeNaoExistirDiretorio(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static bool ExisteArquivos(string path, string nome)
        {
            if (Directory.Exists(path))
                return Directory.GetFiles(path).Count() > 0;

            return false;
        }
    }
}
