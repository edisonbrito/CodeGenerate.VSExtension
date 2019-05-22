using System.ComponentModel;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums
{
    public enum eTratarConteudo
    {
        [Description("Model")]
        Model,
        [Description("Data")]
        Data,
        [Description("Request")]
        Request,
        [Description("Response")]
        Response
    }
}