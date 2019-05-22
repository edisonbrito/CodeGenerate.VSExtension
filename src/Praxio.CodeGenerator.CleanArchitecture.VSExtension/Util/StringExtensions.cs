namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string conteudo)
        {
            if (!string.IsNullOrWhiteSpace(conteudo))
                return char.ToLowerInvariant(conteudo[0]) + conteudo.Substring(1);

            return conteudo;
        }

        public static string ToPascalCase(this string conteudo)
        {
            if (!string.IsNullOrWhiteSpace(conteudo))
                return char.ToUpperInvariant(conteudo[0]) + conteudo.Substring(1);

            return conteudo;
        }
    }
}