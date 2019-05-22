using System.ComponentModel;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums
{
    public enum eTipoOrm
    {
        [Description("EntityFrameworkDataAccess")]
        EntityFramework,
        [Description("NHibernateDataAccess")]
        NHibernate
    }
}
