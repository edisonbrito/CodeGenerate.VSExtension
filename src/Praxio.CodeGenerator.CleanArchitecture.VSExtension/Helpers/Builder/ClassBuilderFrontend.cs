using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Base.Frontend;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Builder
{
    public class ClassBuilderFrontend
    {
        private readonly InterfaceHelper _interfaceHelper;
        private readonly ValidationHelper _validationHelper;
        private readonly UseCaseHelper _useCaseHelper;
        private readonly ModelHelper _modelHelper;

        public ClassBuilderFrontend()
        {
            _interfaceHelper = new InterfaceHelper();
            _validationHelper = new ValidationHelper();
            _useCaseHelper = new UseCaseHelper();
            _modelHelper = new ModelHelper();            
        }

        public void CriarArquivos(Entidade entidade, string nomeProjetoFront)
        {
            if (!entidade.Regerar)
            {
                _interfaceHelper.CriarArquivos(entidade.Nome, nomeProjetoFront);
                _useCaseHelper.CriarArquivos(entidade.Nome, nomeProjetoFront);
            }

            _validationHelper.CriarArquivos(entidade, nomeProjetoFront);
            _modelHelper.CriarArquivos(entidade, nomeProjetoFront);
        }
    }
}