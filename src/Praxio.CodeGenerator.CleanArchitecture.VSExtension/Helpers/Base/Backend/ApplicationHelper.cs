using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Base.Backend
{
    public class ApplicationHelper : BaseHelper
    {
        private const string Inserir = "Inserir";
        private const string Alterar = "Alterar";
        private readonly string _nomeProjeto;
        private readonly string _namespaceDomain;
        private readonly string _diretorioProjeto;
        
        public ApplicationHelper() : base(eProjeto.Application)
        {
            _namespaceDomain = ProjectHelper.ProjectDetails(eProjeto.Domain).Nome;            
            _nomeProjeto = InfoProjeto.Nome;
            _diretorioProjeto = InfoProjeto.Diretorio;
        }

        public void CriarArquivos(Entidade entidade)
        {
            Diretorio.CriarSeNaoExistirDiretorio(_diretorioProjeto + $@"\UseCases\{entidade.Nome}\Base\");
            Diretorio.CriarSeNaoExistirDiretorio(_diretorioProjeto + $@"\UseCases\{entidade.Nome}\Base\Validations\");
            Diretorio.CriarSeNaoExistirDiretorio(_diretorioProjeto + $@"\Interfaces\Repositories\");
            Diretorio.CriarSeNaoExistirDiretorio(_diretorioProjeto + $@"\Interfaces\UseCases\");

            if (entidade.Regerar)
            {
                CriarRequestGenerated(entidade);
                CriarResponseGenerated(entidade);
                CriarValidacoesInserirGenerated(entidade);
                CriarValidacoesAlterarGenerated(entidade);
            }
            else
            {
                CriarRequestGenerated(entidade);
                CriarRequest(entidade);
                CriarResponseGenerated(entidade);                                
                CriarResponse(entidade);
                CriarIRepository(entidade.Nome);
                CriarIUseCase(entidade.Nome);
                CriarUseCase(entidade.Nome);
                CriarValidacoesInserirGenerated(entidade);
                CriarValidacoesInserir(entidade);
                CriarValidacoesAlterarGenerated(entidade);                
                CriarValidacoesAlterar(entidade);
            }
        }

        private void CriarIRepository(string nomeEntidade)
        {
            var resourceName = ResourceNameTemplates + "Backend.Application.Repository.nameIRepository.txt";
            var path = _diretorioProjeto;
            path += $@"\Interfaces\Repositories\";
            var fileDestino = $"{path}I{nomeEntidade}Repository.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();
              
                var textoTratado = textoTemplate
                    .Replace("{{name}}", nomeEntidade)
                    .Replace("{{namespace}}", _nomeProjeto)
                    .Replace("{{namespaceDomain}}", _namespaceDomain);
                File.WriteAllText(fileDestino, textoTratado);
            }
        }

        private void CriarRequestGenerated(Entidade entidade)
        {
            var estruturaPastas = $@"UseCases\{entidade.Nome}\Base\";
            var path = $@"{_diretorioProjeto}\{estruturaPastas}";

            CriarInserirRequestGenerated(entidade, estruturaPastas, path);
            CriarAlterarRequestGenerated(entidade, estruturaPastas, path);
        }

        private void CriarInserirRequestGenerated(Entidade entidade, string estruturaPastas, string path)
        {
            var nomeEntidade = entidade.Nome;
            var resourceName = ResourceNameTemplates + "Backend.Application.Requests.nameInserirRequestGenerated.txt";            
            var fileInserir = $"{path + Inserir + nomeEntidade}Request.Generated.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();

                var textoInserir = TratarPropriedades(entidade.Propriedades, textoTemplate, eTratarConteudo.Request, true);
                textoInserir = textoInserir
                    .Replace("{{name}}", nomeEntidade)
                    .Replace("{{namespace}}", _nomeProjeto);
                File.WriteAllText(fileInserir, textoInserir);
            }

            if (!entidade.Regerar)
                VincularPartialClass(InfoProjeto, $"{Inserir + nomeEntidade}Request", $"{estruturaPastas + Inserir + nomeEntidade}Request");
        }

        private void CriarAlterarRequestGenerated(Entidade entidade, string estruturaPastas, string path)
        {
            var nomeEntidade = entidade.Nome;
            var resourceName = ResourceNameTemplates + "Backend.Application.Requests.nameAlterarRequestGenerated.txt";            
            var fileAlterar = $"{path + Alterar + nomeEntidade}Request.Generated.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();

                var textoAlterar = TratarPropriedades(entidade.Propriedades, textoTemplate, eTratarConteudo.Request, true);
                textoAlterar = textoAlterar
                    .Replace("{{name}}", nomeEntidade)
                    .Replace("{{namespace}}", _nomeProjeto);
                File.WriteAllText(fileAlterar, textoAlterar);
            }

            if (!entidade.Regerar)
                VincularPartialClass(InfoProjeto, $"{Alterar + nomeEntidade}Request", $"{estruturaPastas + Alterar + nomeEntidade}Request");
        }

        private void CriarRequest(Entidade entidade)
        {
            var nomeEntidade = entidade.Nome;          
            var resourceName = ResourceNameTemplates + "Backend.Application.Requests.nameRequest.txt";
            var diretorioUseCases = $@"\UseCases\{nomeEntidade}\Base\";
            var path = _diretorioProjeto;
            path += $@"\{diretorioUseCases}";
            var fileInserir = $"{path + Inserir + nomeEntidade}Request.cs";
            var fileAlterar = $"{path + Alterar + nomeEntidade}Request.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();
                
                var textoInserir = textoTemplate
                    .Replace("{{method}}", Inserir)
                    .Replace("{{name}}", nomeEntidade)
                    .Replace("{{namespace}}", _nomeProjeto);
                File.WriteAllText(fileInserir, textoInserir);

                var textoAlterar = textoTemplate
                    .Replace("{{method}}", Alterar)
                    .Replace("{{name}}", nomeEntidade)
                    .Replace("{{namespace}}", _nomeProjeto);
                File.WriteAllText(fileAlterar, textoAlterar);
            }
        }

        private void CriarResponseGenerated(Entidade entidade)
        {
            var nomeEntidade = entidade.Nome;
            var resourceName = ResourceNameTemplates + "Backend.Application.Responses.nameResponseGenerated.txt";
            var estruturaPastas = $@"UseCases\{nomeEntidade}\Base\";
            var path = _diretorioProjeto;
            path += $@"\{estruturaPastas}";
            var fileDestino = $"{path + nomeEntidade}Response.Generated.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();

                var textoTratado = TratarPropriedades(entidade.Propriedades, textoTemplate, eTratarConteudo.Response);
                textoTratado = textoTratado
                    .Replace("{{name}}", nomeEntidade)
                    .Replace("{{namespace}}", _nomeProjeto);
                File.WriteAllText(fileDestino, textoTratado);
            }

            if (!entidade.Regerar)
                VincularPartialClass(InfoProjeto, $"{nomeEntidade}Response", $"{estruturaPastas + nomeEntidade}Response");
        }

        private void CriarResponse(Entidade entidade)
        {
            var nomeEntidade = entidade.Nome;
            var resourceName = ResourceNameTemplates + "Backend.Application.Responses.nameResponse.txt";
            var path = _diretorioProjeto;
            path += $@"\UseCases\{nomeEntidade}\Base\";
            var fileDestino = $"{path + nomeEntidade}Response.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();

                var textoTratado = textoTemplate
                    .Replace("{{name}}", nomeEntidade)
                    .Replace("{{namespace}}", _nomeProjeto);
                File.WriteAllText(fileDestino, textoTratado);
            }
        }

        private void CriarUseCase(string nomeEntidade)
        {   
            var resourceName = ResourceNameTemplates + "Backend.Application.UseCases.nameUseCaseBase.txt";
            var path = _diretorioProjeto;
            path += $@"\UseCases\{nomeEntidade}\Base\";
            var fileDestino = $"{path + nomeEntidade}UseCase.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();
              
                var textoTratado = textoTemplate
                    .Replace("{{name}}", nomeEntidade)
                    .Replace("{{namespace}}", _nomeProjeto)
                    .Replace("{{namespaceDomain}}", _namespaceDomain);
                textoTratado = textoTratado.Replace("{{nameCamelCase}}", nomeEntidade.ToCamelCase());
                File.WriteAllText(fileDestino, textoTratado);
            }
        }

        private void CriarIUseCase(string nomeEntidade)
        {
            var resourceName = ResourceNameTemplates + "Backend.Application.UseCases.nameIUseCaseBase.txt";
            var path = _diretorioProjeto;
            path += $@"\Interfaces\UseCases\";
            var fileDestino = $"{path}I{nomeEntidade}UseCase.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();
              
                var textoTratado = textoTemplate.Replace("{{name}}", nomeEntidade);
                textoTratado = textoTratado
                    .Replace("{{nameCamelCase}}", nomeEntidade.ToCamelCase())
                    .Replace("{{namespace}}", _nomeProjeto);
                File.WriteAllText(fileDestino, textoTratado);
            }
        }

        private void CriarValidacoesInserirGenerated(Entidade entidade)
        {
            var resourceName = ResourceNameTemplates + "Backend.Application.Validation.nameInserirValidatorGenerated.txt";
            var path = _diretorioProjeto;
            path += $@"\UseCases\{entidade.Nome}\Base\Validations\";
            var fileDestino = $"{path}Inserir{entidade.Nome}ValidatorGenerated.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();
                var textoTratado = TrataConteudoInternoValidation(entidade, textoTemplate, eTipoValidacao.Inserir);

                textoTratado = textoTratado
                   .Replace("{{name}}", entidade.Nome)
                   .Replace("{{namespace}}", _nomeProjeto)
                   .Replace("{{tipo}}", eTipoValidacao.Inserir.ToString());

                File.WriteAllText(fileDestino, textoTratado);
            }
        }

        private void CriarValidacoesInserir(Entidade entidade)
        {
            var resourceName = ResourceNameTemplates + "Backend.Application.Validation.Validator.txt";
            var path = _diretorioProjeto;
            path += $@"\UseCases\{entidade.Nome}\Base\Validations\";
            var fileDestino = $"{path}Inserir{entidade.Nome}Validator.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();

                var textoTratado = textoTemplate
                   .Replace("{{name}}", entidade.Nome)
                   .Replace("{{namespace}}", _nomeProjeto)
                   .Replace("{{tipo}}", eTipoValidacao.Inserir.ToString());

                File.WriteAllText(fileDestino, textoTratado);
            }
        }

        private void CriarValidacoesAlterarGenerated(Entidade entidade)
        {
            var resourceName = ResourceNameTemplates + "Backend.Application.Validation.nameAlterarValidatorGenerated.txt";
            var path = _diretorioProjeto;
            path += $@"\UseCases\{entidade.Nome}\Base\Validations\";
            var fileDestino = $"{path}Alterar{entidade.Nome}ValidatorGenerated.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();
                var textoTratado = TrataConteudoInternoValidation(entidade, textoTemplate, eTipoValidacao.Alterar);

                textoTratado = textoTratado
                   .Replace("{{name}}", entidade.Nome)
                   .Replace("{{namespace}}", _nomeProjeto)
                   .Replace("{{tipo}}", eTipoValidacao.Alterar.ToString());

                File.WriteAllText(fileDestino, textoTratado);
            }
        }

        private void CriarValidacoesAlterar(Entidade entidade)
        {
            var resourceName = ResourceNameTemplates + "Backend.Application.Validation.Validator.txt";
            var path = _diretorioProjeto;
            path += $@"\UseCases\{entidade.Nome}\Base\Validations\";
            var fileDestino = $"{path}Alterar{entidade.Nome}Validator.cs";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var textoTemplate = reader.ReadToEnd();

                var textoTratado = textoTemplate
                   .Replace("{{name}}", entidade.Nome)
                   .Replace("{{namespace}}", _nomeProjeto)
                   .Replace("{{tipo}}", eTipoValidacao.Alterar.ToString());

                File.WriteAllText(fileDestino, textoTratado);
            }
        }

        public string TrataConteudoInternoValidation(Entidade entidade, string texto, eTipoValidacao tipoValidacao)
        {
            var propriedades = entidade.PropriedadesAValidar().ToList();
            if (!propriedades.Any())
            {
                return texto
                    .Replace("{{property}}", "")
                    .Replace("{{validations}}", "");
            }

            var sbConstrutor = new StringBuilder();
            var sbCorpo = new StringBuilder();

            if (tipoValidacao == eTipoValidacao.Alterar)
                sbConstrutor.Append("\n" + TabbedString(3));

            for (int i = 0; i < propriedades.Count; i++)
            {
                var propriedade = propriedades[i];
                var ultimoItem = i == propriedades.Count - 1;
                var finalDaLinha = ultimoItem ? string.Empty : "\n" + TabbedString(3);
                string nomePropriedadeValidacao = RetornarNomePropriedadeValidacao(propriedade);

                sbConstrutor.Append($"Validate{propriedade.Nome}();{finalDaLinha}");

                sbCorpo
                    .Append($"\n\n{TabbedString(2)}")
                    .Append($"protected void Validate{propriedade.Nome}()\n{TabbedString(2)}{{")                    
                    .Append($"\n{TabbedString(3)}RuleFor(r => r.{nomePropriedadeValidacao})");

                var espacamento = TabbedString(4);
                bool tipoString = propriedade.Tipo == eTipoPropriedade.String;

                if (!propriedade.Nullable)
                {
                    sbCorpo.Append($"\n{espacamento}.NotEmpty()");
                    sbCorpo.Append($"\n{espacamento}.WithMessage(string.Format(Mensagens.Obrigatorio, \"{nomePropriedadeValidacao}\"))");
                }

                if (propriedade.Min == 0 && propriedade.Max > 0)
                {
                    var validacao = tipoString ? "MaximumLength" : "LessThanOrEqualTo";
                    var mensagemValidacao = tipoString ? "TamanhoMaximo" : "ValorMaximo";
                    sbCorpo.Append($"\n{espacamento}.{validacao}({propriedade.Max})");
                    sbCorpo.Append($"\n{espacamento}.WithMessage(string.Format(Mensagens.{mensagemValidacao}, \"{propriedade.Nome}\", \"{propriedade.Max}\"))");
                }

                if (propriedade.Min > 0 && propriedade.Max > 0)
                {
                    var validacao = tipoString ? "Length" : "InclusiveBetween";
                    var mensagemValidacao = tipoString ? "IntervaloCaracteres" : "ValorEntre";
                    sbCorpo.Append($"\n{espacamento}.{validacao}({propriedade.Min}, {propriedade.Max})");
                    sbCorpo.Append($"\n{espacamento}.WithMessage(string.Format(Mensagens.{mensagemValidacao}, \"{propriedade.Nome}\", \"{propriedade.Min}\", \"{propriedade.Max}\"))");                        
                }

                if (!string.IsNullOrEmpty(propriedade.ExpressaoRegular))
                {
                    sbCorpo.Append($"\n{espacamento}.Matches(\"{propriedade.ExpressaoRegular}\")");
                    sbCorpo.Append($"\n{TabbedString(3)}.WithMessage(\"Digite sua mensagem\")");
                }

                sbCorpo.Append($";\n{TabbedString(2)}}}");
            }            

            var textoTratado = texto.Replace("{{property}}", sbConstrutor.ToString());
            return textoTratado.Replace("{{validations}}", sbCorpo.ToString());
        }        

        private string RetornarNomePropriedadeValidacao(Propriedade propriedade)
        {
            string nomePropriedadeValidacao = string.Empty;

            if (propriedade.Tipo == eTipoPropriedade.Reference)
            {
                if (propriedade.IsCollection)
                    nomePropriedadeValidacao = "Ids" + propriedade.Nome;
                else
                    nomePropriedadeValidacao = "Id" + propriedade.Nome;
            }
            else
                nomePropriedadeValidacao = propriedade.Nome;

            return nomePropriedadeValidacao;
        }
    }
}