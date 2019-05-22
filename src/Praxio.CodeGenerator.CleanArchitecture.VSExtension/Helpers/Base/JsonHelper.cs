using Newtonsoft.Json;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers
{
    public class JsonHelper
    {
        private readonly string toolsFolder;

        public JsonHelper()
        {
            toolsFolder = $@"{ProjectHelper.GetSolutionPath()}\Tools\";
        }

        public void CriarJsonHistorico(Entidade entidade)
        {
            var nomeEntidade = entidade.Nome;
            var fileDestino = $"{toolsFolder + nomeEntidade}.json";

            var file = new FileInfo(toolsFolder);
            file.Directory.Create();

            using (StreamWriter fileWriter = File.CreateText(fileDestino))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(fileWriter, entidade);
            }
        }

        public void CriarJsonConfigGit(GitConfig gitConfig)
        {
            var diretorioConfig = $@"{toolsFolder}Config\";
            Diretorio.CriarSeNaoExistirDiretorio(diretorioConfig);
            var fileDestino = $@"{diretorioConfig}\git.json";                        

            using (StreamWriter fileWriter = File.CreateText(fileDestino))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(fileWriter, gitConfig);
            }
        }

        public IEnumerable<FileInfo> ListarJsonHistorico()
        {
            var dir = new DirectoryInfo(toolsFolder);

            if (!dir.Exists)
                return null;

            return dir.GetFiles("*.json", SearchOption.TopDirectoryOnly)
                .OrderBy(x => x.Name).ToList();
        }

        public IEnumerable<PropriedadeRegerar> RetornarAlteracoesRegerar(Entidade entidade)
        {
            var entidadeHistorico = RetornarEntidadeHistorico(entidade.Nome);

            var paraExcluir = entidadeHistorico.Propriedades
                .Where(w => !entidade.Propriedades.Any(a => a.Nome == w.Nome))
                .Select(s => new PropriedadeRegerar
                {
                    Propriedade = s,
                    Status = eStatusPropriedade.Excluir
                });

            var propriedadesRegerar = new List<PropriedadeRegerar>();
            propriedadesRegerar.AddRange(paraExcluir);

            var paraInserir = entidade.Propriedades
                .Where(w => !entidadeHistorico.Propriedades.Any(a => a.Nome == w.Nome))
                .Select(s => new PropriedadeRegerar
                {
                    Propriedade = s,
                    Status = eStatusPropriedade.Inserir
                });

            propriedadesRegerar.AddRange(paraInserir);
            return propriedadesRegerar;
        }

        public Entidade RetornarEntidadeHistorico(string nomeEntidade)
        {
            return JsonConvert.DeserializeObject<Entidade>(File.ReadAllText($"{toolsFolder + nomeEntidade}.json"));
        }

        public GitConfig RetornarConfigGit()
        {
            var diretorioFinal = toolsFolder + "Config";
            Diretorio.CriarSeNaoExistirDiretorio(diretorioFinal);

            if(!Diretorio.ExisteArquivos(diretorioFinal, "git.json"))
                return new GitConfig();            

            return JsonConvert.DeserializeObject<GitConfig>(File.ReadAllText($"{toolsFolder}Config\\git.json"));
        }
    }
}