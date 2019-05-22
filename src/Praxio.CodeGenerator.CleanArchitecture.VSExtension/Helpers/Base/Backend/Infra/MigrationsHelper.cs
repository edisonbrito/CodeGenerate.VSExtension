using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Base.Backend.Infra
{
    public class MigrationsHelper : BaseHelper
    {
        private readonly string _nomeProjeto;
        private readonly JsonHelper _jsonHelper;
        private readonly string _diretorioProjeto;
        private readonly string _diretorioApi;
        IEnumerable<PropriedadeRegerar> _propriedadesRegerar;

        public MigrationsHelper(JsonHelper jsonHelper) : base(eProjeto.Migrations)
        {
            _jsonHelper = jsonHelper;                        
            _nomeProjeto = InfoProjeto.Nome;
            _diretorioProjeto = InfoProjeto.Diretorio;

            var infoProjectApi = ProjectHelper.ProjectDetails(eProjeto.Api);
            _diretorioApi = infoProjectApi.Diretorio;
        }

        public void CriarArquivos(Entidade entidade, ConfiguracaoEntidade configuracao)
        {
            if (entidade.Regerar)
            {
                _propriedadesRegerar = _jsonHelper.RetornarAlteracoesRegerar(entidade);
                if (!_propriedadesRegerar.Any(x => !x.Propriedade.IsCollection))
                    return;
            }

            if (configuracao.Baseline)
            {
                var arquivoBaseline = $"{_diretorioProjeto}\\Baseline.cs";
                if (File.Exists(arquivoBaseline))
                    AlterarBaseline(entidade, arquivoBaseline);
                else
                    CriarBaseline(entidade, arquivoBaseline);
            }
            else
            {
                var numeroMigration = configuracao.NumeroMigration.ToString().PadLeft(5, '0');
                CriarMigration(entidade, numeroMigration);
            }            
                       
            if (configuracao.UpdateBase)
            {
                LinhaDeComando.Executar($"dotnet build {ProjectHelper.GetSolutionName()}");                
                var migration = string.Format(@"dotnet fm migrate --connection ""{0}"" --processor SqlServer2016 --assembly {1}", BuscarConnectionString(), RetornaCaminhoDll());                
                LinhaDeComando.Executar(migration);
            }
        }

        private void CriarMigration(Entidade entidade, string numeroMigration)
        {
            var resourceName = ResourceNameTemplates + "Backend.Infra.Migrations.migration.txt";
            var fileDestino = $@"{_diretorioProjeto}\Migration_{numeroMigration}.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();
                var textoTratado = TratarConteudo(entidade, textoTemplate);

                textoTratado = textoTratado
                   .Replace("{{namespace}}", _nomeProjeto)
                   .Replace("{{numeroMigration}}", numeroMigration);

                File.WriteAllText(fileDestino, textoTratado);
            }
        }

        private void CriarBaseline(Entidade entidade, string arquivoFinal)
        {
            var resourceName = ResourceNameTemplates + "Backend.Infra.Migrations.baseline.txt";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();
                var textoTratado = TratarConteudo(entidade, textoTemplate);

                textoTratado = textoTratado
                   .Replace("{{namespace}}", _nomeProjeto);

                File.WriteAllText(arquivoFinal, textoTratado);
            }
        }

        private void AlterarBaseline(Entidade entidade, string arquivoFinal)
        {
            var conteudo = new List<string>(File.ReadAllLines(arquivoFinal));

            var comandosCriacao = RetornarComandoCriarTabela(entidade);
            int indiceFinalUp = RetornarIndiceArquivo(conteudo, "}");
            conteudo.Insert(indiceFinalUp, $"\n\t\t\t{comandosCriacao}");

            var comandosExclusao = RetornarComandoExcluirTabela(entidade.Nome);
            int indiceInicioDown = RetornarIndiceArquivo(conteudo, "Down");
            conteudo.Insert(indiceInicioDown + 2, $"\t\t\t{comandosExclusao }");

            File.WriteAllLines(arquivoFinal, conteudo);
        }

        private string TratarConteudo(Entidade entidade, string textoAtual)
        {
            if (entidade.Regerar)
                return TratarConteudoRegerar(entidade, textoAtual);

            var conteudo = RetornarUp(entidade, textoAtual);
            return RetornarDown(entidade.Nome, conteudo);
        }

        private string RetornarUp(Entidade entidade, string textoAtual)
        {
            var conteudoUp = RetornarComandoCriarTabela(entidade);
            return textoAtual.Replace("{{up}}", conteudoUp.ToString());
        }

        private string RetornarDown(string nomeEntidade, string textoAtual)
        {
            string conteudoDown = RetornarComandoExcluirTabela(nomeEntidade);
            return textoAtual.Replace("{{down}}", conteudoDown);
        }

        private string RetornarComandoCriarTabela(Entidade entidade)
        {
            var comando = new StringBuilder();
            var propriedades = entidade.Propriedades.Where(x => !x.IsCollection).ToList();

            comando.Append($"Create.Table(\"{entidade.Nome}\")\n\t\t\t\t");
            comando.Append($".WithColumn(\"Id\").AsInt32().PrimaryKey().NotNullable()\n\t\t\t\t");

            //comando.Append($".WithColumn(\"Id\").AsInt32().PrimaryKey().Identity().NotNullable()\n\t\t\t\t");

            for (int i = 0; i < propriedades.Count; i++)
            {
                var ultimoItem = i == propriedades.Count - 1;
                var finalDaLinha = ultimoItem ? ";" : "\n\t\t\t\t";

                comando
                    .Append($".WithColumn(\"{RetornarNome(propriedades[i])}\").")
                    .Append(RetornarTipo(propriedades[i]))
                    .Append(RetornarNullable(propriedades[i]))
                    .Append(finalDaLinha);
            }

            comando.Append($"\n\t\t this.IfDatabase(\"Oracle\").Create.Sequence(\"SEQ_{entidade.Nome}\");\n\t\t\t\t");

            var referencias = propriedades.Where(w => w.Tipo == eTipoPropriedade.Reference && !w.IsCollection).ToList();
            if (referencias.Any())
                comando.Append(RetornarComandoInserirFK(entidade.Nome, referencias));

            return comando.ToString();
        }

        private string RetornarComandoExcluirTabela(string nomeEntidade)
        {
            return $"Delete.Table(\"{nomeEntidade}\");";
        }

        private string TratarConteudoRegerar(Entidade entidade, string textoAtual)
        {
            var conteudo = RetornarUpRegerar(entidade.Nome, textoAtual);
            return RetornarDownRegerar(entidade.Nome, conteudo);
        }

        private string RetornarUpRegerar(string nomeEntidade, string textoAtual)
        {
            var comando = RetornarConteudoRegerar(nomeEntidade, true);
            return textoAtual.Replace("{{up}}", comando.ToString());
        }

        private string RetornarDownRegerar(string nomeEntidade, string textoAtual)
        {
            var comando = RetornarConteudoRegerar(nomeEntidade, false);
            return textoAtual.Replace("{{down}}", comando.ToString());
        }

        private string RetornarConteudoRegerar(string nomeEntidade, bool up)
        {
            var comando = new StringBuilder();
            var propriedades = up ?
                _propriedadesRegerar.ToList() :
                _propriedadesRegerar.Reverse().ToList();

            var referencias = propriedades.Where(w => w.Propriedade.Tipo == eTipoPropriedade.Reference && !w.Propriedade.IsCollection).ToList();
            var possuiReferencias = referencias.Any();

            if (possuiReferencias)
                comando.Append(TratarFKsParaExclusao(nomeEntidade, referencias, up));

            for (int i = 0; i < propriedades.Count; i++)
            {
                var ultimoItem = i == propriedades.Count - 1;
                var finalDaLinha = ultimoItem ? ";" : ";\n\t\t\t";

                comando
                    .Append(RetornarComandoRegerar(nomeEntidade, propriedades[i], up))
                    .Append(finalDaLinha);
            }

            if (possuiReferencias)
                comando.Append(TratarFKsParaInsercao(nomeEntidade, referencias, up));

            return comando.ToString();
        }

        private string RetornarComandoRegerar(string nomeEntidade, PropriedadeRegerar propriedadeRegerar, bool up)
        {
            string comando;

            switch (propriedadeRegerar.Status)
            {
                case eStatusPropriedade.Inserir:
                    comando = up ?
                        RetornarComandoInserirColuna(nomeEntidade, propriedadeRegerar.Propriedade) :
                        RetornarComandoExcluirColuna(nomeEntidade, propriedadeRegerar.Propriedade);
                    break;

                case eStatusPropriedade.Alterar:
                    comando = RetornarComandoAlterarColuna(nomeEntidade, propriedadeRegerar.Propriedade);
                    break;

                default:
                    comando = up ?
                        RetornarComandoExcluirColuna(nomeEntidade, propriedadeRegerar.Propriedade) :
                        RetornarComandoInserirColuna(nomeEntidade, propriedadeRegerar.Propriedade);
                    break;
            }

            return comando;
        }

        private string TratarFKsParaExclusao(string nomeEntidade, IList<PropriedadeRegerar> propriedadesRegerar, bool up)
        {
            eStatusPropriedade statusFiltro = up ?
                eStatusPropriedade.Excluir :
                eStatusPropriedade.Inserir;

            var propriedades = propriedadesRegerar.Where(w => w.Status == statusFiltro).Select(s => s.Propriedade).ToList();
            return RetornarComandoExcluirFK(nomeEntidade, propriedades);
        }

        private string TratarFKsParaInsercao(string nomeEntidade, IList<PropriedadeRegerar> propriedadesRegerar, bool up)
        {
            eStatusPropriedade status = up ?
                eStatusPropriedade.Inserir :
                eStatusPropriedade.Excluir;

            var propriedades = propriedadesRegerar.Where(w => w.Status == status).Select(s => s.Propriedade).ToList();
            return RetornarComandoInserirFK(nomeEntidade, propriedades);
        }

        private string RetornarComandoInserirFK(string nomeEntidade, IList<Propriedade> propriedades)
        {
            var comando = new StringBuilder();
            var resourceName = ResourceNameTemplates + "Backend.Infra.Migrations.foreignKey.txt";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();

                foreach (var propriedade in propriedades)
                {
                    var fk = textoTemplate
                       .Replace("{{from}}", nomeEntidade)
                       .Replace("{{to}}", propriedade.Nome);

                    comando
                        .Append($"\n\n")
                        .Append(fk);
                }
            }

            return comando.ToString();
        }

        private string RetornarComandoExcluirFK(string nomeEntidade, IList<Propriedade> propriedades)
        {
            var comando = new StringBuilder();

            for (int i = 0; i < propriedades.Count; i++)
            {
                var ultimoItem = i == propriedades.Count - 1;
                var finalDaLinha = ultimoItem ? ";" : ";\n\t\t\t";
                var toTable = propriedades[i].Nome;

                comando
                    .Append($"Delete.ForeignKey(\"FK_{nomeEntidade}_{toTable}\").OnTable(\"{nomeEntidade}\")")
                    .Append(finalDaLinha);

                if (ultimoItem)
                    comando.Append("\n\t\t\t");
            }

            return comando.ToString();
        }

        private string RetornarComandoInserirColuna(string nomeEntidade, Propriedade propriedade)
        {
            return $"Create.Column(\"{RetornarNome(propriedade)}\").OnTable(\"{nomeEntidade}\")." +
                    RetornarTipo(propriedade) +
                    RetornarNullable(propriedade);
        }

        private string RetornarComandoAlterarColuna(string nomeEntidade, Propriedade propriedade)
        {
            return $"Alter.Column(\"{RetornarNome(propriedade)}\").OnTable(\"{nomeEntidade}\")." +
                    RetornarTipo(propriedade) +
                    RetornarNullable(propriedade);
        }

        private string RetornarComandoExcluirColuna(string nomeEntidade, Propriedade propriedade)
        {
            return $"Delete.Column(\"{RetornarNome(propriedade)}\").FromTable(\"{nomeEntidade}\")";
        }

        private string RetornarNome(Propriedade propriedade)
        {
            if (propriedade.Tipo == eTipoPropriedade.Reference)
                return "Id" + propriedade.Nome;

            return propriedade.Nome;
        }

        private string RetornarTipo(Propriedade propriedade)
        {
            return propriedade.Tipo.GetEnumMigrationColumn().Replace("{{max-length}}", propriedade.Max.ToString());
        }

        private string RetornarNullable(Propriedade propriedade)
        {
            return propriedade.Nullable ?
                ".Nullable()" :
                ".NotNullable()";
        }

        private string RetornaCaminhoDll()
        {
            var caminhoDLL = _diretorioProjeto.Replace("\\", @"\") + @"\bin\Debug\netcoreapp2.2\" + _nomeProjeto + ".dll";

            return caminhoDLL;
        }

        private string BuscarConnectionString()
        {
            var path = Path.Combine(_diretorioApi, "appsettings.json");
            var definition = new { ConnectionStrings = new { DefaultConnection = "" } };
            var connection = JsonConvert.DeserializeAnonymousType(File.ReadAllText(path), definition);

            return connection.ConnectionStrings.DefaultConnection;
        }
    }
}