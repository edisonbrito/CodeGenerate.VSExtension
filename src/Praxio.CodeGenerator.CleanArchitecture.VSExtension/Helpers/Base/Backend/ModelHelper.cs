using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Base.Backend
{
    public class ModelHelper : BaseHelper
    {
        private readonly string _nomeProjeto;
        private readonly string _diretorioProjeto;

        public ModelHelper() : base(eProjeto.Domain)
        {
            _nomeProjeto = InfoProjeto.Nome;
            _diretorioProjeto = InfoProjeto.Diretorio + $@"\Entities\";
        }

        public void CriarArquivos(Entidade entidade)
        {
            Diretorio.CriarSeNaoExistirDiretorio(_diretorioProjeto);

            if (entidade.Regerar)
                CriarArquivoGenerated(entidade);
            else
            {
                CriarArquivo(entidade);
                CriarArquivoGenerated(entidade);
                VincularPartialClass(InfoProjeto, $"{entidade.Nome}Model", $@"Entities\{entidade.Nome}Model");
            }            
        }            
        
        private void CriarArquivoGenerated(Entidade entidade)
        {
            var nomeEntidade = entidade.Nome;
            var resourceNameG = ResourceNameTemplates + "Backend.Domain.nameModelGenerated.txt";
            var fileGenerated = $"{_diretorioProjeto + nomeEntidade}Model.Generated.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceNameG))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();
                
                var textoTratado = TratarConteudo(entidade.Propriedades, textoTemplate, eTratarConteudo.Model);
                textoTratado = textoTratado
                   .Replace("{{name}}", nomeEntidade)
                   .Replace("{{namespace}}", _nomeProjeto);

                File.WriteAllText(fileGenerated, textoTratado);
            }
        }

        private void CriarArquivo(Entidade entidade)
        {
            var nomeEntidade = entidade.Nome;
            var resourceName = ResourceNameTemplates + "Backend.Domain.nameModel.txt";
            var fileDestino = $"{_diretorioProjeto + nomeEntidade}Model.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();
                var textoTratado = TratarConteudo(entidade.Propriedades, textoTemplate, eTratarConteudo.Model);

                textoTratado = textoTratado
                   .Replace("{{name}}", nomeEntidade)
                   .Replace("{{namespace}}", _nomeProjeto);

                File.WriteAllText(fileDestino, textoTratado);
            }
        }

        public List<string> ListarArquivos()
        {
            var diretorio = new DirectoryInfo(_diretorioProjeto);
            var arquivos = diretorio.GetFiles("*.cs");
            var lstArquivos = new List<string>();

            foreach (var item in arquivos)
                lstArquivos.Add(item.Name);

            return (from p in lstArquivos where !p.Contains("Generated") select p).ToList();            
        }
    }
}