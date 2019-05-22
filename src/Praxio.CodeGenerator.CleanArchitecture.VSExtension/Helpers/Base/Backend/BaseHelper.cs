using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Base.Backend
{
    public abstract class BaseHelper
    {
        protected const string Id = "ID";
        protected Assembly Assembly { get; set; }
        protected InfoProjeto InfoProjeto { get; set; }
        protected string ResourceNameTemplates { get; set; }        

        public BaseHelper(eProjeto projeto)
        {
            InfoProjeto = ProjectHelper.ProjectDetails(projeto);
            Assembly = Assembly.GetExecutingAssembly();
            ResourceNameTemplates = "Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Templates.";
        }

        public string TratarConteudo(IList<Propriedade> propriedades, string textoAtual, eTratarConteudo tratarConteudo)
        {
            return TratarConteudo(propriedades, textoAtual, tratarConteudo, false);
        }

        public string TratarConteudo(IList<Propriedade> propriedades, string textoAtual, eTratarConteudo tratarConteudo, bool definirVirtual)
        {
            return TratarPropriedades(propriedades, textoAtual, tratarConteudo, definirVirtual, false);
        }

        public string TratarPropriedades(IList<Propriedade> propriedades, string textoAtual, eTratarConteudo tratarConteudo)
        {
            return TratarPropriedades(propriedades, textoAtual, tratarConteudo, false, false);
        }

        public string TratarPropriedades(IList<Propriedade> propriedades, string textoAtual, eTratarConteudo tratarConteudo, bool annotation)
        {
            return TratarPropriedades(propriedades, textoAtual, tratarConteudo, false, annotation);
        }

        public string TratarPropriedades(IList<Propriedade> propriedades, string textoAtual, eTratarConteudo tratarConteudo, 
            bool definirVirtual, bool annotation)
        {
            var sbPropriedades = new StringBuilder();
            string modificadorAcesso = definirVirtual ? "public virtual " : "public ";

            for (int i = 0; i < propriedades.Count; i++)
            {
                var propriedade = propriedades[i];
                var ultimoItem = i == propriedades.Count - 1;
                var finalDaLinha = ultimoItem ? " { get; set; }" : " { get; set; }\n\n\t\t";

                if (annotation && !propriedade.Nullable)
                    sbPropriedades.Append("[Required]\n\t\t");

                sbPropriedades
                    .Append($"{modificadorAcesso}{RetornarTipoPropriedade(propriedade, tratarConteudo)} ")
                    .Append($"{RetornarNomePropriedade(propriedade, tratarConteudo)}{finalDaLinha}");
            }

            var teste0 = textoAtual;
            var teste = sbPropriedades;

            return textoAtual.Replace("{{property}}", sbPropriedades.ToString());
        }

        public string TratarConstrutor(IList<Propriedade> propriedades, string textoAtual, eTratarConteudo tratarConteudo)
        {
            var sbParametros = new StringBuilder();
            var sbCorpo = new StringBuilder();

            for (int i = 0; i < propriedades.Count; i++)
            {
                sbParametros.Append(TratarParametroConstrutor(propriedades[i], i == 0, tratarConteudo));
                sbCorpo.Append(TratarCorpoConstrutor(propriedades[i], i == propriedades.Count - 1, tratarConteudo));
            }

            var textoTratado = textoAtual.Replace("{{params}}", sbParametros.ToString());
            return textoTratado.Replace("{{constructorBody}}", sbCorpo.ToString());
        }

        public string TratarParametroConstrutor(Propriedade propriedade, bool primeiroItem, eTratarConteudo tratarConteudo)
        {
            var separador = primeiroItem ? "" : ", ";
            return $"{separador}{RetornarTipoPropriedade(propriedade, tratarConteudo)} {RetornarNomePropriedadeCamelCase(propriedade, tratarConteudo)}";
        }

        public string TratarCorpoConstrutor(Propriedade propriedade, bool ultimoItem, eTratarConteudo tratarConteudo)
        {
            var fimDaLinha = ultimoItem ? ";" : ";\n\t\t\t";
            return RetornarNomePropriedade(propriedade, tratarConteudo) + " = " + RetornarNomePropriedadeCamelCase(propriedade, tratarConteudo) + fimDaLinha;
        }

        public string RetornarTipoPropriedade(Propriedade propriedade, eTratarConteudo tratarConteudo)
        {
            if (propriedade.Tipo == eTipoPropriedade.Reference && (tratarConteudo == eTratarConteudo.Request || tratarConteudo == eTratarConteudo.Response))
            {
                if (propriedade.IsCollection)
                    return "IEnumerable<int>";
                
                return "int" + (propriedade.Nullable ? "?" : "");                
            }

            if (propriedade.Tipo == eTipoPropriedade.String)
                return "string";

            if (propriedade.IsCollection)
                return "IEnumerable<" + propriedade.Nome + tratarConteudo.GetEnumDescription() + ">";

            if (propriedade.Tipo == eTipoPropriedade.Reference)
                return propriedade.Nome + tratarConteudo.GetEnumDescription();

            return propriedade.Tipo.GetEnumDescription() + (propriedade.Nullable ? "?" : "");
        }

        public string RetornarNomePropriedadeCamelCase(Propriedade propriedade, eTratarConteudo tratarConteudo)
        {
            return RetornarNomePropriedade(propriedade, tratarConteudo).ToCamelCase();            
        }

        public string RetornarNomePropriedade(Propriedade propriedade, eTratarConteudo tratarConteudo)
        {
            if (propriedade.Tipo == eTipoPropriedade.Reference && (tratarConteudo == eTratarConteudo.Request || tratarConteudo == eTratarConteudo.Response))
            {
                if (propriedade.IsCollection)
                    return "Ids" + propriedade.Nome;
                
                return "Id" + propriedade.Nome;
            }

            if (propriedade.IsCollection)
                return propriedade.NomePlural;
            
            return propriedade.Nome;
        }

        public int RetornarIndiceArquivo(IList<string> conteudoArquivo, string texto)
        {
            int indice = 0;

            for (int i = 0; i < conteudoArquivo.Count; i++)
            {
                if (conteudoArquivo[i].Contains(texto))
                {
                    indice = i;
                    break;
                }
            }

            return indice;
        }

        public void VincularPartialClass(InfoProjeto infoProjeto, string nomeArquivo, string diretorioArquivo)
        {
            var resourceName = ResourceNameTemplates + "Project.itemGroup.txt";
            string textoTratado;

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                textoTratado = reader.ReadToEnd()
                    .Replace("{{name}}", nomeArquivo)
                    .Replace("{{path}}", diretorioArquivo);
            }

            var arquivoProjeto = $@"{infoProjeto.Diretorio}\{infoProjeto.Nome}.csproj";
            var conteudoArquivo = new List<string>(File.ReadAllLines(arquivoProjeto));
            int indice = RetornarIndiceArquivo(conteudoArquivo, "</PropertyGroup>");

            conteudoArquivo.Insert(indice + 1, textoTratado);
            File.WriteAllLines(arquivoProjeto, conteudoArquivo);
        }

        public string TabbedString(int count)
        {
            return count > 0 ? new string('\t', count) : string.Empty;
        }
    }
}
