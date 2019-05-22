using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util;
using System;
using System.Windows.Forms;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Forms
{
    public partial class frmCriarPropriedade : Form
    {
        private readonly frmExtension parent;

        public frmCriarPropriedade()
        {   
            InitializeComponent();
        }

        public frmCriarPropriedade(frmExtension _parent)
        {
            InitializeComponent();
            parent = _parent;
        }

        private bool ValidarForm(Propriedade propriedade)
        {
            if (string.IsNullOrWhiteSpace(propriedade.Nome))
            {
                MessageBox.Show("Necessário informar o nome da propriedade.");
                return false;
            }

            if (propriedade.Nome.ToUpper() == "ID")
            {
                MessageBox.Show("Não é necessário informar a propriedade Id, pois é herdada por padrão.");
                return false;
            }

            var tipo = (eTipoPropriedade)Enum.Parse(typeof(eTipoPropriedade), cbxTipo.Text);
            if (propriedade.Tipo == eTipoPropriedade.String && propriedade.Max == 0)
            {
                MessageBox.Show("Para propriedades do tipo String, é necessário definir o tamanho máximo de caracteres.");
                return false;
            }

            if (propriedade.Min > propriedade.Max)
            {
                MessageBox.Show("Quantidade mínima de caracteres não pode ser superior à quantidade máxima.");
                return false;
            }

            return true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            var tipo = (eTipoPropriedade)Enum.Parse(typeof(eTipoPropriedade), cbxTipo.Text);
            var propriedade = new Propriedade
            {
                Nome = txtNomePropriedade.Text.ToPascalCase(),
                Tipo = tipo,
                Nullable = ckbPermiteNulo.Checked,
                ExpressaoRegular = txtExpressao.Text,
            };

            if (!string.IsNullOrWhiteSpace(txtMin.Text))
                propriedade.Min = Convert.ToInt32(txtMin.Text);

            if (!string.IsNullOrWhiteSpace(txtMax.Text))
                propriedade.Max = Convert.ToInt32(txtMax.Text);

            if (!ValidarForm(propriedade))
                return;

            parent.PopularPropriedadesForm(propriedade);
            LimparDefinicaoPropriedade();

            Close();
            parent.btnRemoverPropriedade.Enabled = true;
        }

        public void LimparDefinicaoPropriedade()
        {
            txtNomePropriedade.Text = string.Empty;
            cbxTipo.SelectedIndex = 0;
            ckbPermiteNulo.Checked = false;
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tipo = Enum.Parse(typeof(eTipoPropriedade), cbxTipo.Text);

            switch (tipo)
            {
                case eTipoPropriedade.Bool:
                    BloqueiaTodosAtributos();
                    break;                    
                case eTipoPropriedade.Byte:
                    BloqueiaExpressao();
                    break;
                case eTipoPropriedade.Short:
                    BloqueiaExpressao();
                    break;
                case eTipoPropriedade.DateTime:
                    BloqueiaMinMaxExpressao();
                    break;
                case eTipoPropriedade.Decimal:
                    BloqueiaExpressao();
                    break;
                case eTipoPropriedade.Double:
                    BloqueiaExpressao();
                    break;
                case eTipoPropriedade.Guid:
                    BloqueiaMinMaxExpressao();
                    break;
                case eTipoPropriedade.Int:
                    BloqueiaExpressao();
                    break;
                case eTipoPropriedade.Long:
                    BloqueiaExpressao();
                    break;
                case eTipoPropriedade.String:
                    DesbloqueiaTodosAtributos();
                    break;

                default:
                    throw new Exception("Unexpected Case");
            }
        }

        private void BloqueiaTodosAtributos()
        {
            txtMin.Enabled = false;
            txtMax.Enabled = false;
            txtExpressao.Enabled = false;
            ckbPermiteNulo.Enabled = false;
        }

        private void BloqueiaExpressao()
        {
            txtMin.Enabled = true;
            txtMax.Enabled = true;
            txtExpressao.Enabled = false;
            ckbPermiteNulo.Enabled = true;
        }

        private void BloqueiaMinMaxExpressao()
        {
            txtMin.Enabled = false;
            txtMax.Enabled = false;
            txtExpressao.Enabled = false;
            ckbPermiteNulo.Enabled = true;
        }

        private void DesbloqueiaTodosAtributos()
        {
            txtMin.Enabled = true;
            txtMax.Enabled = true;
            txtExpressao.Enabled = true;
            ckbPermiteNulo.Enabled = true;
        }

        private void txtMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar)) && (e.KeyChar != 8);
        }

        private void txtMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar)) && (e.KeyChar != 8);
        }
    }
}
