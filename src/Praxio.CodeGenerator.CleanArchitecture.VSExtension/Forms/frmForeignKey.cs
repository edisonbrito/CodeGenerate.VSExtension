using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Base.Backend;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util;
using System;
using System.Windows.Forms;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Forms
{
    public partial class frmForeignKey : Form
    {
        private readonly frmExtension parent;        

        public frmForeignKey()
        {
            InitializeComponent();
            HelperContrutor();
        }

        public frmForeignKey(frmExtension _parent)
        {
            InitializeComponent();
            parent = _parent;
            HelperContrutor();
        }

        private void HelperContrutor()
        {
            var helper = new ModelHelper();
            cbxEntidade.DataSource = helper.ListarArquivos();
            DesabilitarCollection();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var propriedade = new Propriedade
            {
                Nome = cbxEntidade.Text.Replace("Model.cs", ""),
                Tipo = eTipoPropriedade.Reference,
                Nullable = ckbPermiteNulo.Checked,
                IsCollection = ckbCollection.Checked,
                NomePlural = txtNomePropriedade.Text.ToPascalCase()
            };

            parent.PopularPropriedadesForm(propriedade);
            Close();
        }

        private void HabilitarCollection()
        {
            txtNomePropriedade.Visible = true;
            lblNomeEntidadePlural.Visible = true;

            txtNomePropriedade.Text = cbxEntidade.Text.Replace("Model.cs","");
        }

        private void DesabilitarCollection()
        {
            txtNomePropriedade.Visible = false;
            lblNomeEntidadePlural.Visible = false;
            txtNomePropriedade.Text = "";
        }

        private void chkCollection_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbCollection.Checked)
                HabilitarCollection();
            else
                DesabilitarCollection();
            
        }

    }
}
