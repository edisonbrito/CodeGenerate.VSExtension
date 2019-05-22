using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util;
using System.IO;
using System.Reflection;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Base.Frontend
{
    public class InterfaceHelper
    {
        private readonly Assembly _assembly;
        private readonly string _resourceNameTemplates;                
        private string basePath;
        private string nomeEntidade;

        public InterfaceHelper()
        {
            _assembly = Assembly.GetExecutingAssembly();
            _resourceNameTemplates = "Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Templates.";            
        }

        public void CriarArquivos(string nomeEntidade, string urlProjeto)
        {
            basePath = urlProjeto + @"\src\app\core";
            this.nomeEntidade = nomeEntidade;
            Diretorio.CriarSeNaoExistirDiretorio(basePath + @"\interfaces\repositories\");
            Diretorio.CriarSeNaoExistirDiretorio(basePath + @"\interfaces\usecases\");
            Diretorio.CriarSeNaoExistirDiretorio(basePath + @"\interfaces\services\");
            Diretorio.CriarSeNaoExistirDiretorio(basePath + @"\interfaces\validations\");

            CriarIRepository();
            CriarIService();
            CriarIUseCase();
            CriarIValidator();
        }        

        private void CriarIRepository()
        {
            var resourceName = _resourceNameTemplates + "Frontend.Core.Interfaces.nameIRepository.txt";            
            var path = basePath + @"\interfaces\repositories\";
            var fileDestino = $"{path}I{nomeEntidade}Repository.ts";

            EscreverConteudo(fileDestino, resourceName);
        }

        private void CriarIService()
        {
            var resourceName = _resourceNameTemplates + "Frontend.Core.Interfaces.nameIService.txt";
            var path = basePath + @"\interfaces\services\";
            var fileDestino = $"{path}I{nomeEntidade}Service.ts";            

            EscreverConteudo(fileDestino, resourceName);
        }

        private void CriarIUseCase()
        {
            var resourceName = _resourceNameTemplates + "Frontend.Core.Interfaces.nameIUseCase.txt";            
            var path = basePath + @"\interfaces\usecases\";
            var fileDestino = $"{path}I{nomeEntidade}UseCase.ts";

            EscreverConteudo(fileDestino, resourceName);
        }

        private void CriarIValidator()
        {
            var resourceName = _resourceNameTemplates + "Frontend.Core.Interfaces.nameIValidator.txt";
            var path = basePath + @"\interfaces\validations\";
            var fileDestino = $"{path}I{nomeEntidade}Validator.ts";

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
                    .Replace("{{nameCamelCase}}", nomeEntidade.ToCamelCase());

                File.WriteAllText(fileDestino, textoTratado);
            }
        }
    }
}