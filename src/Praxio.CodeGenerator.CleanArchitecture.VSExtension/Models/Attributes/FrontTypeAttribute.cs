using System;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Attributes
{
    public class FrontTypeAttribute : Attribute
    {
        public string Type { get; }

        public FrontTypeAttribute(string type)
        {
            Type = type;
        }
    }
}