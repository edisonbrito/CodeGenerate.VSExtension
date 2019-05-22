using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Base.Backend;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Base.Backend.Infra;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Builder
{
    public class ClassBuilderBackend
    {
        private readonly ControllerHelper _controllerHelper;
        private readonly ApplicationHelper _applicationHelper;
        private readonly ModelHelper _modelHelper;
        private readonly DataHelper _dataHelper;
        private readonly MigrationsHelper _migrationsHelper;
        private readonly MapperReferenciasHelper _mapperHelper;
        private readonly JsonHelper _jsonHelper;

        public ClassBuilderBackend(JsonHelper jsonHelper)
        {
            _jsonHelper = jsonHelper;
            _controllerHelper = new ControllerHelper();
            _applicationHelper = new ApplicationHelper();
            _modelHelper = new ModelHelper();
            _dataHelper = new DataHelper();
            _mapperHelper = new MapperReferenciasHelper();
            _migrationsHelper = new MigrationsHelper(_jsonHelper);
        }

        public void CriarArquivos(Entidade entidade, ConfiguracaoEntidade configuracao)
        {
            switch (configuracao.GeraCodigo)
            {
                case eGeraCodigo.Crud:
                    _controllerHelper.CriarArquivos(entidade);
                    _applicationHelper.CriarArquivos(entidade);
                    _modelHelper.CriarArquivos(entidade);
                    _dataHelper.CriarArquivos(entidade);
                    _mapperHelper.CriarArquivos(entidade);
                    break;
                case eGeraCodigo.InfraDomain:
                    _applicationHelper.CriarArquivos(entidade);
                    _modelHelper.CriarArquivos(entidade);
                    _dataHelper.CriarArquivos(entidade);
                    break;
                case eGeraCodigo.Domain:
                    _modelHelper.CriarArquivos(entidade);
                    break;
                default:
                    break;
            }

            if (configuracao.AddMigration)
                _migrationsHelper.CriarArquivos(entidade, configuracao);

            _jsonHelper.CriarJsonHistorico(entidade);
        }
    }
}
