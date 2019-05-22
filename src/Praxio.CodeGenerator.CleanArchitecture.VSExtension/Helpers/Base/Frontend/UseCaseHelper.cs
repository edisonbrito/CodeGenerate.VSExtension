using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util;
using System.IO;
using System.Reflection;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Base.Frontend
{
    public class UseCaseHelper
    {
        private readonly Assembly _assembly;
        private readonly string _resourceNameTemplates;
        private string basePath;
        private string nomeEntidade;
        private string nomeEntidadeCamelCase;

        public UseCaseHelper()
        {
            _assembly = Assembly.GetExecutingAssembly();
            _resourceNameTemplates = "Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Templates.";
        }

        public void CriarArquivos(string nomeEntidade, string urlProjeto)
        {
            basePath = urlProjeto + @"\src\app\core";
            this.nomeEntidade = nomeEntidade;
            nomeEntidadeCamelCase = nomeEntidade.ToCamelCase();
            Diretorio.CriarSeNaoExistirDiretorio(basePath + @"\usecases\" + nomeEntidadeCamelCase);
            Diretorio.CriarSeNaoExistirDiretorio(basePath + @"\usecases\" + nomeEntidadeCamelCase + @"\base\");

            CriarUseCase();
        }

        private void CriarUseCase()
        {
            var resourceName = _resourceNameTemplates + "Frontend.Core.UseCase.nameUseCase.txt";
            var path = basePath + $@"\usecases\{nomeEntidadeCamelCase}\base\";
            var fileDestino = $"{path}{nomeEntidade}UseCase.ts";

            EscreverConteudo(fileDestino, resourceName);
        }
        
        private void EscreverConteudo(string fileDestino, string resourceName)
        {
            using (Stream stream = _assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();
                var textoTratado = textoTemplate
                    .Replace("{{name}}", nomeEntidade)
                    .Replace("{{nameCamelCase}}", nomeEntidadeCamelCase);

                File.WriteAllText(fileDestino, textoTratado);
            }
        }
    }
}
