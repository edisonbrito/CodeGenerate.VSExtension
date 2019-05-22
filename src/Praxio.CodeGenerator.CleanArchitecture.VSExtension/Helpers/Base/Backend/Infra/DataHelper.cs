using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Base.Backend.Infra
{
    public class DataHelper : BaseHelper
    {
        private readonly string _nomeProjeto;
        private readonly string _pastaOrm;
        private readonly eTipoOrm _tipoOrm;
        private readonly string _diretorioProjeto;

        public DataHelper() : base(eProjeto.Data)
        {
            _nomeProjeto = InfoProjeto.Nome;
            _diretorioProjeto = InfoProjeto.Diretorio;
            _tipoOrm = OrmUtilizado();
            _pastaOrm = _tipoOrm.GetEnumDescription();
        }

        public eTipoOrm OrmUtilizado()
        {
            var diretorioEntity = $@"{InfoProjeto.Diretorio}\{eTipoOrm.NHibernate.GetEnumDescription()}";
            return Directory.Exists(diretorioEntity) ?
                eTipoOrm.NHibernate :
                eTipoOrm.EntityFramework;
        }

        public void CriarArquivos(Entidade entidade)
        {
            Diretorio.CriarSeNaoExistirDiretorio($@"{_diretorioProjeto}\{_pastaOrm}\DataModels\");
            Diretorio.CriarSeNaoExistirDiretorio($@"{_diretorioProjeto}\{_pastaOrm}\Repositories\");

            CriarDataGenerated(entidade);
            if (_tipoOrm == eTipoOrm.NHibernate)
                Mappings(entidade);

            if (!entidade.Regerar)
            {
                CriarData(entidade);                
                CriarRepository(entidade.Nome);
                VincularPartialClass(InfoProjeto, $"{entidade.Nome}Data", $@"{_pastaOrm}\DataModels\{entidade.Nome}Data");

                if (_tipoOrm == eTipoOrm.EntityFramework)
                    AlterarContext(entidade.Nome);                
            }
        }

        public void CriarDataGenerated(Entidade entidade)
        {
            var nomeEntidade = entidade.Nome;
            var resourceName = ResourceNameTemplates + "Backend.Infra.Data.nameDataGenerated.txt";
            var fileDestino = $@"{_diretorioProjeto}\{_pastaOrm}\DataModels\{nomeEntidade}Data.Generated.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();
                
                var textoTratado = TratarConteudo(entidade.Propriedades, textoTemplate, eTratarConteudo.Data, _tipoOrm == eTipoOrm.NHibernate);
                textoTratado = textoTratado
                    .Replace("{{name}}", nomeEntidade)
                    .Replace("{{namespace}}", _nomeProjeto)
                    .Replace("{{pastaOrm}}", _pastaOrm);
                File.WriteAllText(fileDestino, textoTratado);
            }
        }
        
        public void CriarData(Entidade entidade)
        {
            var nomeEntidade = entidade.Nome;           
            var resourceName = ResourceNameTemplates + "Backend.Infra.Data.nameData.txt";
            var fileDestino = $@"{_diretorioProjeto}\{_pastaOrm}\DataModels\{nomeEntidade}Data.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();
                
                var textoTratado = TratarConteudo(entidade.Propriedades, textoTemplate, eTratarConteudo.Data);
                textoTratado = textoTratado
                    .Replace("{{name}}", nomeEntidade)
                    .Replace("{{namespace}}", _nomeProjeto)
                    .Replace("{{pastaOrm}}", _pastaOrm);
                File.WriteAllText(fileDestino, textoTratado);
            }
        }

        public void CriarRepository(string nomeEntidade)
        {
            var namespaceApplication = ProjectHelper.ProjectDetails(eProjeto.Application).Nome;
            var namespaceDomain = ProjectHelper.ProjectDetails(eProjeto.Domain).Nome;
            var resourceName = ResourceNameTemplates + "Backend.Infra.Data." + _pastaOrm + ".nameRepository.txt";
            var fileDestino = $@"{_diretorioProjeto}\{_pastaOrm}\Repositories\{nomeEntidade}Repository.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();              

                var textoTratado = textoTemplate
                    .Replace("{{name}}", nomeEntidade)
                    .Replace("{{namespace}}", _nomeProjeto)
                    .Replace("{{namespaceApplication}}", namespaceApplication)
                    .Replace("{{namespaceDomain}}", namespaceDomain);
                File.WriteAllText(fileDestino, textoTratado);
            }
        }

        public void AlterarContext(string nomeEntidade)
        {
            var arquivoFinal = $@"{_diretorioProjeto}\EntityFrameworkDataAccess\Context.cs";
            var conteudo = new List<string>(File.ReadAllLines(arquivoFinal));
            var debSet = "\t\tpublic DbSet<" + nomeEntidade + "Data> " + nomeEntidade + " { get; set; }";
            int indiceDbSet = RetornarIndiceArquivo(conteudo, "DbSet<");

            conteudo.Insert(indiceDbSet + 1, debSet);
            File.WriteAllLines(arquivoFinal, conteudo);
        }

        public void Mappings(Entidade entidade)
        {
            var nomeEntidade = entidade.Nome;
            var pastaEntidade = $@"{_diretorioProjeto}\{_pastaOrm}\Mappings\{nomeEntidade}";
            Diretorio.CriarSeNaoExistirDiretorio(pastaEntidade);
            
            var resourceName = ResourceNameTemplates + "Backend.Infra.Data.NHibernateDataAccess.mapGenerated.txt";            
            var fileDestino = $@"{pastaEntidade}\{nomeEntidade}MapGenerated.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();

                var textoTratado = TratarConteudoMapping(entidade.Propriedades, textoTemplate);
                textoTratado = textoTratado
                    .Replace("{{name}}", nomeEntidade)
                    .Replace("{{namespace}}", _nomeProjeto);
                File.WriteAllText(fileDestino, textoTratado);
            }            
        }

        private string TratarConteudoMapping(IList<Propriedade> propriedades, string textoAtual)
        {
            var comando = new StringBuilder();
            var propriedadesMap = propriedades.Where(w => w.Tipo != eTipoPropriedade.Reference).ToList();
            var referencias = propriedades.Where(w => w.Tipo == eTipoPropriedade.Reference).ToList();

            for (int i = 0; i < propriedadesMap.Count; i++)
            {
                var ultimoItem = i == propriedadesMap.Count - 1;
                var finalDaLinha = ultimoItem ? ";" : ";\n\t\t\t";

                comando.Append($"Map(m => m.{propriedadesMap[i].Nome}, \"{propriedadesMap[i].Nome}\"){finalDaLinha}");                
            }

            for (int i = 0; i < referencias.Count; i++)
            {
                if (i == 0)
                    comando.Append($"\n\t\t\t");

                var ultimoItem = i == referencias.Count - 1;
                var finalDaLinha = ultimoItem ? ";" : ";\n\t\t\t";

                if (referencias[i].IsCollection)
                    comando.Append($"HasMany(h => h.{referencias[i].NomePlural}).Cascade.SaveUpdate().Inverse(){finalDaLinha}");
                else
                    comando.Append($"References(r => r.{referencias[i].Nome}, \"Id{referencias[i].Nome}\"){finalDaLinha}");
            }

            return textoAtual.Replace("{{property}}", comando.ToString());
        }
    }
}