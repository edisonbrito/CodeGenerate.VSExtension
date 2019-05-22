using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Forms
{
    public partial class frmRegerar : Form
    {
        public frmRegerar(IEnumerable<FileInfo> jsons)
        {
            InitializeComponent();
            cbxJson.DataSource = jsons.ToList();            
        }

        private void btnIr_Click(object sender, EventArgs e)
        {
            var jsonSelecionado = (FileInfo)cbxJson.SelectedValue;
            if (jsonSelecionado != null)
            {
                var form = new frmExtension(jsonSelecionado.FullName);
                Close();
                form.Show();                
            }
        }
    }
}