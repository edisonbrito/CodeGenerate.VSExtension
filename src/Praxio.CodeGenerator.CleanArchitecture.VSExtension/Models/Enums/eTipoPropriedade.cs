using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Attributes;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util;
using System.ComponentModel;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums
{
    public enum eTipoPropriedade
    {
        [FrontType("string")]
        [Description("string")]
        [MigrationColumn("AsAnsiString({{max-length}})")]
        String,

        [FrontType(Constantes.TipoNumericoFront)]
        [Description("byte")]
        [MigrationColumn("AsByte()")]
        Byte,

        [FrontType("boolean")]
        [Description("bool")]
        [MigrationColumn("AsBoolean()")]
        Bool,

        [FrontType(Constantes.TipoNumericoFront)]
        [Description("int")]
        [MigrationColumn("AsInt32()")]
        Int,

        [FrontType("Date")]
        [Description("DateTime")]
        [MigrationColumn("AsDateTime()")]
        DateTime,

        [FrontType("string")]
        [Description("Guid")]
        [MigrationColumn("AsGuid()")]
        Guid,

        [FrontType(Constantes.TipoNumericoFront)]
        [Description("long")]
        [MigrationColumn("AsInt64()")]
        Long,

        [FrontType(Constantes.TipoNumericoFront)]
        [Description("decimal")]
        [MigrationColumn("AsDecimal()")]
        Decimal,

        [FrontType(Constantes.TipoNumericoFront)]
        [Description("double")]
        [MigrationColumn("AsDouble()")]
        Double,

        [FrontType(Constantes.TipoNumericoFront)]
        [Description("float")]
        [MigrationColumn("AsFloat()")]
        Float,

        [FrontType(Constantes.TipoNumericoFront)]
        [Description("short")]
        [MigrationColumn("AsInt16()")]
        Short,

        [FrontType("Reference")]
        [Description("Reference")]
        [MigrationColumn("AsInt32()")]
        Reference  
    }
}