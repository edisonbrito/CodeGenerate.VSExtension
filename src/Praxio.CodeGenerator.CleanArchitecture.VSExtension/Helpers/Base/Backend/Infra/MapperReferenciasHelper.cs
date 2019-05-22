using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Base.Backend.Infra
{
    public class MapperReferenciasHelper : BaseHelper
    {
        private readonly string _nomeProjeto;
        private readonly string _diretorioProjeto;
        private readonly string _namespaceApplication;
        private readonly string _namespaceDomain;
        private IEnumerable<Propriedade> propriedadesReferencias;

        public MapperReferenciasHelper() : base(eProjeto.CrossCutting)
        {
            _nomeProjeto = InfoProjeto.Nome;
            _diretorioProjeto = InfoProjeto.Diretorio;
            _namespaceApplication = ProjectHelper.ProjectDetails(eProjeto.Application).Nome;
            _namespaceDomain = ProjectHelper.ProjectDetails(eProjeto.Domain).Nome;
        }

        public void CriarArquivos(Entidade entidade)
        {
            var nomeEntidade = entidade.Nome;
            var diretorio = $@"{_diretorioProjeto}\AutoMapper\Generated\";
            var arquivoFinal = $@"{diretorio}\{nomeEntidade}Profile.cs";
            propriedadesReferencias = entidade.Propriedades.Where(a => a.Tipo == eTipoPropriedade.Reference && !a.IsCollection);
            
            if (!propriedadesReferencias.Any())
            {
                if (File.Exists(arquivoFinal))
                    File.Delete(arquivoFinal);

                return;
            }

            Diretorio.CriarSeNaoExistirDiretorio(diretorio);
            CriarProfile(nomeEntidade, arquivoFinal);
        }

        private void CriarProfile(string nomeEntidade, string arquivoFinal)
        {
            var resourceName = ResourceNameTemplates + "Backend.Infra.Mapper.profileGenerated.txt";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();
                var maps = RetornarMaps(nomeEntidade, propriedadesReferencias);

                var textoTratado = textoTemplate
                    .Replace("{{map}}", maps)
                    .Replace("{{name}}", nomeEntidade)
                    .Replace("{{namespace}}", _nomeProjeto)                    
                    .Replace("{{namespaceApplication}}", _namespaceApplication)
                    .Replace("{{namespaceDomain}}", _namespaceDomain);

                File.WriteAllText(arquivoFinal, textoTratado);
            }
        }

        private string RetornarMaps(string nomeEntidade, IEnumerable<Propriedade> referencias)
        {
            var comando = new StringBuilder();

            comando
                .Append($"CreateMap<{nomeEntidade}Model, {nomeEntidade}Response>()")                
                .Append(RetornarForMember(referencias))
                .Append($"\n\n\t\t\t")
                .Append($"CreateMap<Inserir{nomeEntidade}Request, {nomeEntidade}Model>()")                
                .Append(RetornarForPath(referencias))
                .Append($"\n\n\t\t\t")
                .Append($"CreateMap<Alterar{nomeEntidade}Request, {nomeEntidade}Model>()")                
                .Append(RetornarForPath(referencias));

            return comando.ToString();
        }
        
        private string RetornarForMember(IEnumerable<Propriedade> referencias)
        {
            var comando = new StringBuilder();

            foreach (var referencia in referencias)
            {
                comando
                    .Append($"\n\t\t\t\t")
                    .Append($".ForMember(d => d.Id{referencia.Nome}, ")
                    .Append($"opt => opt.MapFrom(src => src.{referencia.Nome}.Id))");
            }

            comando.Append(";");
            return comando.ToString();
        }

        private string RetornarForPath(IEnumerable<Propriedade> referencias)
        {
            var comando = new StringBuilder();

            foreach (var referencia in referencias)
            {
                comando
                    .Append($"\n\t\t\t\t")
                    .Append($".ForPath(d => d.{referencia.Nome}.Id, ")
                    .Append($"opt => opt.MapFrom(src => src.Id{referencia.Nome}))");
            }

            comando.Append(";");
            return comando.ToString();
        }
    }
}