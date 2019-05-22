using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Base.Frontend
{
    public class ValidationHelper
    {
        private readonly Assembly _assembly;
        private readonly string _resourceNameTemplates;
        private string basePath;
        private Entidade entidade;

        public ValidationHelper()
        {
            _assembly = Assembly.GetExecutingAssembly();
            _resourceNameTemplates = "Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Templates.";
        }

        public void CriarArquivos(Entidade entidade, string urlProjeto)
        {
            this.entidade = entidade;
            basePath = $@"{urlProjeto}\src\app\core\usecases\{entidade.Nome.ToCamelCase()}\base\validations\";            
            Diretorio.CriarSeNaoExistirDiretorio(basePath);
            CriarValidatorGenerated();            

            if (!entidade.Regerar)
                CriarValidator();
        }

        private void CriarValidator()
        {
            var resourceName = _resourceNameTemplates + "Frontend.Core.Validation.nameValidator.txt";
            var fileDestino = $"{basePath}{entidade.Nome}Validator.ts";

            using (Stream stream = _assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();

                var textoTratado = textoTemplate
                    .Replace("{{name}}", entidade.Nome)
                    .Replace("{{nameCamelCase}}", entidade.Nome.ToCamelCase());

                File.WriteAllText(fileDestino, textoTratado);
            }
        }

        private void CriarValidatorGenerated()
        {
            var resourceName = _resourceNameTemplates + "Frontend.Core.Validation.nameValidatorGenerated.txt";
            var fileDestino = $"{basePath}{entidade.Nome}Validator.generated.ts";

            EscreverValidacoes(fileDestino, resourceName);
        }

        private void EscreverValidacoes(string fileDestino, string resourceName)
        {
            using (Stream stream = _assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();
                var textoValidations = TratarValidations(textoTemplate);

                var textoTratado = textoValidations
                    .Replace("{{name}}", entidade.Nome)
                    .Replace("{{nameCamelCase}}", entidade.Nome.ToCamelCase());

                File.WriteAllText(fileDestino, textoTratado);
            }
        }

        private string TratarValidations(string textoTemplate)
        {
            string conteudo = string.Empty;
            var propriedades = entidade.PropriedadesAValidar().ToList();            
            if (propriedades.Any())
                conteudo = PercorrerPropriedades(propriedades);
            
            return textoTemplate.Replace("{{validations}}", conteudo);
        }
        
        private string PercorrerPropriedades(IList<Propriedade> propriedades)
        {
            var sbCorpo = new StringBuilder();

            for (int i = 0; i < propriedades.Count; i++)
            {
                var propriedade = propriedades[i];
                var nomeCamelCase = propriedade.Nome.ToCamelCase();                

                if (!propriedade.Nullable)
                    sbCorpo.Append(RetornarCampoObrigatorio(propriedade, nomeCamelCase));

                bool tipoString = propriedade.Tipo == eTipoPropriedade.String;
                if (propriedade.Min == 0 && propriedade.Max > 0)
                    sbCorpo.Append(RetornarTamanhoOuValorMaximo(nomeCamelCase, propriedade, tipoString));

                if (propriedade.Min > 0 && propriedade.Max > 0)
                {
                    if (tipoString)
                        sbCorpo.Append(RetornarIntervaloString(nomeCamelCase, propriedade.Min, propriedade.Max));
                    else
                    {
                        sbCorpo.Append(RetornarValorMinimo(nomeCamelCase, propriedade.Min));
                        sbCorpo.Append(RetornarValorMaximo(nomeCamelCase, propriedade.Max));
                    }
                }
            }

            return sbCorpo.ToString();
        }
        
        private string RetornarTamanhoOuValorMaximo(string nomeCamelCase, Propriedade propriedade, bool tipoString)
        {
            return tipoString ?
                RetornarTamanhoMaximoString(nomeCamelCase, propriedade.Max) :
                RetornarValorMaximo(nomeCamelCase, propriedade.Max);
        }

        private string RetornarCampoObrigatorio(Propriedade propriedade, string nomeCamelCase)
        {
            string msgObrigatorio = "this.iValidatorMensagem.obrigatorio";

            if (propriedade.Tipo == eTipoPropriedade.String)
                return $"\n\t\t.NotEmpty(m => m.{nomeCamelCase}, {msgObrigatorio}('{nomeCamelCase}').value)";

            if (propriedade.IsCollection)
                return $"\n\t\t.NotNull(m => m.ids{propriedade.Nome}, {msgObrigatorio}('{propriedade.NomePlural.ToCamelCase()}').value)";

            if (propriedade.Tipo == eTipoPropriedade.Reference)
                return $"\n\t\t.NotNull(m => m.id{propriedade.Nome}, {msgObrigatorio}('{nomeCamelCase}').value)";

            var validacao = $"\n\t\t.NotNull(m => m.{nomeCamelCase}, {msgObrigatorio}('{nomeCamelCase}').value)";
            if (propriedade.Tipo.GetFrontType() == Constantes.TipoNumericoFront)
                validacao += $"\n\t\t.IsNumberNotEqual(m => m.{nomeCamelCase}, 0, {msgObrigatorio}('{nomeCamelCase}').value)";

            return validacao;
        }

        private string RetornarTamanhoMaximoString(string nomeCamelCase, int max)
        {
            return $"\n\t\t.Length(m => m.{nomeCamelCase}, 0, {max}, this.iValidatorMensagem.tamanhoMaximo('{nomeCamelCase}', '{max}').value)";
        }

        private string RetornarValorMaximo(string nomeCamelCase, int max)
        {
            return $"\n\t\t.IsNumberLessThanOrEqual(m => m.{nomeCamelCase}, {max}, this.iValidatorMensagem.valorMaximo('{nomeCamelCase}', '{max}').value)";
        }

        private string RetornarValorMinimo(string nomeCamelCase, int min)
        {
            return $"\n\t\t.IsNumberGreaterThanOrEqual(m => m.{nomeCamelCase}, {min}, this.iValidatorMensagem.valorMinimo('{nomeCamelCase}', '{min}').value)";
        }

        private string RetornarIntervaloString(string nomeCamelCase, int min, int max)
        {
            return $"\n\t\t.Length(m => m.{nomeCamelCase}, {min}, {max}, this.iValidatorMensagem.intervaloCaracteres('{nomeCamelCase}', {min}, {max}).value)";
        }
    }
}