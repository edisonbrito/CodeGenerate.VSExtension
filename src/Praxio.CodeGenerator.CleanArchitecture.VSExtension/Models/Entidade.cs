using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models
{
    public class Entidade
    {
        public bool Regerar { get; set; }
        public string Nome { get; set; }
        public IList<Propriedade> Propriedades { get; set; }

        public Entidade(string nome, IList<Propriedade> propriedades, bool regerar)
        {
            Nome = nome;
            Propriedades = propriedades;
            Regerar = regerar;
        }

        public IEnumerable<Propriedade> PropriedadesAValidar()
        {
            return Propriedades
                .Where(s => s.Tipo != eTipoPropriedade.Bool &&
                (!s.Nullable || !string.IsNullOrWhiteSpace(s.ExpressaoRegular) || s.Min > 0 || s.Max > 0));
        }
    }
}