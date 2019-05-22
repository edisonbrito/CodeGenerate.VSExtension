using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models
{
    public class ConfiguracaoEntidade
    {
        public bool AddMigration { get; set; }
        public bool UpdateBase { get; set; }
        public bool Baseline { get; set; }
        public int NumeroMigration { get; set; }
        public eGeraCodigo GeraCodigo { get; set; }

        public ConfiguracaoEntidade(bool addMigration, bool updateBase, bool baseline, eGeraCodigo geraCodigo)
        {
            AddMigration = addMigration;
            UpdateBase = updateBase;
            Baseline = baseline;            
            GeraCodigo = geraCodigo;
        }

        public ConfiguracaoEntidade(bool addMigration, bool updateBase, int numeroMigration, eGeraCodigo geraCodigo)
        {
            AddMigration = addMigration;
            UpdateBase = updateBase;            
            NumeroMigration = numeroMigration;
            GeraCodigo = geraCodigo;
        }
    }
}