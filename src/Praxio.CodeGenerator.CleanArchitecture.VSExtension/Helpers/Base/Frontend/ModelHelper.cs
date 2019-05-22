using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Base.Frontend
{
    public class ModelHelper
    {
        private readonly Assembly _assembly;
        private readonly string _resourceNameTemplates;
        private string basePath;

        public ModelHelper()
        {
            _assembly = Assembly.GetExecutingAssembly();
            _resourceNameTemplates = "Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Templates.";
        }

        public void CriarArquivos(Entidade entidade, string urlProjeto)
        {
            basePath = urlProjeto + @"\src\app\core";
            Diretorio.CriarSeNaoExistirDiretorio(basePath + @"\Domain\Entities\");
            string nomeCamelCase = entidade.Nome.ToCamelCase();            
            CriarModelGenereted(entidade, nomeCamelCase);

            if (!entidade.Regerar)
                CriarModel(entidade, nomeCamelCase);
        }

        private void CriarModel(Entidade entidade, string nomeCamelCase)
        {
            var resourceName = _resourceNameTemplates + "Frontend.Core.Domain.nameModel.txt";
            var path = basePath + @"\domain\entities\";
            var fileDestino = $"{path}{nomeCamelCase}.model.ts";

            using (Stream stream = _assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();                
                var textoTratado = textoTemplate
                    .Replace("{{name}}", entidade.Nome)
                    .Replace("{{nameCamelCase}}", nomeCamelCase);

                File.WriteAllText(fileDestino, textoTratado);
            }
        }

        private void CriarModelGenereted(Entidade entidade, string nomeCamelCase)
        {
            var resourceName = _resourceNameTemplates + "Frontend.Core.Domain.nameModelGenerated.txt";
            var path = basePath + @"\domain\entities\";
            var fileDestino = $"{path}{nomeCamelCase}.model.generated.ts";

            using (Stream stream = _assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();
                var textoTratado = TratarPropriedade(entidade.Propriedades, textoTemplate);

                textoTratado = textoTratado
                    .Replace("{{name}}", entidade.Nome)
                    .Replace("{{nameCamelCase}}", nomeCamelCase);

                File.WriteAllText(fileDestino, textoTratado);
            }
        }       

        private string TratarPropriedade(IList<Propriedade> propriedades, string textoTemplate)
        {
            var sbPropriedades = new StringBuilder();
            
            for (int i = 0; i < propriedades.Count; i++)
            {
                var propriedade = propriedades[i];
                var ultimoItem = i == propriedades.Count - 1;
                var finalDaLinha = ultimoItem ? "" : " \n  ";

                sbPropriedades
                    .Append($"{RetornarNomePropriedade(propriedade)}: ")
                    .Append($"{RetornarTipoPropriedade(propriedade)};{finalDaLinha}");
            }            

            return textoTemplate.Replace("{{property}}", sbPropriedades.ToString());            
        }

        public string RetornarNomePropriedade(Propriedade propriedade)
        {
            if (propriedade.IsCollection)
                return "ids" + propriedade.Nome;

            if (propriedade.Tipo == eTipoPropriedade.Reference)
                return "id" + propriedade.Nome;

            return propriedade.Nome.ToCamelCase();
        }

        public string RetornarTipoPropriedade(Propriedade propriedade)
        {
            if (propriedade.IsCollection)
                return "Array<number>";

            if (propriedade.Tipo == eTipoPropriedade.Reference)
                return "number";            

            return propriedade.Tipo.GetFrontType();
        }
    }
}
