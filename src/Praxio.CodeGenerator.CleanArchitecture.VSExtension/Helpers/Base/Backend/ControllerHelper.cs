using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util;
using System.IO;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Base.Backend
{
    public class ControllerHelper : BaseHelper
    {
        public ControllerHelper() : base(eProjeto.Api)
        {
            
        }

        public void CriarArquivos(Entidade entidade)
        {
            var nomeEntidade = entidade.Nome;            
            var projectName = InfoProjeto.Nome;
            var pathDestino = InfoProjeto.Diretorio;
            var resourceName = ResourceNameTemplates + "Backend.Api.nameController.txt";
            pathDestino += $@"\Controllers\";
            var fileDestino = $"{pathDestino + nomeEntidade}Controller.cs";

            Diretorio.CriarSeNaoExistirDiretorio(pathDestino);

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var namespaceApplication = ProjectHelper.ProjectDetails(eProjeto.Application).Nome;
                var textoTemplate = reader.ReadToEnd();               

                var textoTratado = textoTemplate
                    .Replace("{{name}}", nomeEntidade)
                    .Replace("{{nameCamelCase}}", nomeEntidade.ToCamelCase())
                    .Replace("{{namespace}}", projectName)
                    .Replace("{{namespaceApplication}}", namespaceApplication);

                File.WriteAllText(fileDestino, textoTratado);
            }
        }
    }
}