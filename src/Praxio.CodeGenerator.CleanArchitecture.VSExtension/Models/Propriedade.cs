using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models
{
    public class Propriedade
    {
        public string Nome { get; set; }
        public string NomePlural { get; set; }
        public bool Nullable { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string ExpressaoRegular { get; set; }
        public bool IsCollection { get; set; }
        public eTipoPropriedade Tipo { get; set; }
    }
}