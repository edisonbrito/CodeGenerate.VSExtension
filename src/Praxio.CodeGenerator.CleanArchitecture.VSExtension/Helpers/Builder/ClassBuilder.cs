using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Builder
{
    public class ClassBuilder
    {
        private readonly JsonHelper _jsonHelper;
        private readonly ClassBuilderBackend _classBuilderBackend;
        private readonly ClassBuilderFrontend _classBuilderFrontend;

        public ClassBuilder(JsonHelper jsonHelper)
        {
            _jsonHelper = jsonHelper;
            _classBuilderBackend = new ClassBuilderBackend(_jsonHelper);
            _classBuilderFrontend = new ClassBuilderFrontend();
        }

        public void CriarArquivos(Entidade entidade, ConfiguracaoEntidade configuracao)
        {
            _classBuilderBackend.CriarArquivos(entidade, configuracao);            
        }

        public void CriarArquivos(Entidade entidade, ConfiguracaoEntidade configuracao, string urlProjetoFront)
        {
            CriarArquivos(entidade, configuracao);
            _classBuilderFrontend.CriarArquivos(entidade, urlProjetoFront);
        }
    }
}