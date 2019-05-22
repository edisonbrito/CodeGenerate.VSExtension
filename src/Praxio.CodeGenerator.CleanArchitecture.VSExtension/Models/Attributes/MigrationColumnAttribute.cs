using System;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Attributes
{
    public class MigrationColumnAttribute : Attribute
    {
        public string ColumnType { get; }

        public MigrationColumnAttribute(string columnType)
        {
            ColumnType = columnType;
        }
    }
}
