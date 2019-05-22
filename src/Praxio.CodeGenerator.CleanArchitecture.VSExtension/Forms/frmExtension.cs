using Newtonsoft.Json;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Helpers.Builder;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Enums;
using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Forms
{
    public partial class frmExtension : Form
    {
        private bool regerar;
        private JsonHelper jsonHelper;
        private GitConfig gitConfig;

        public frmExtension()
        {
            IniciarComponentesForm();
        }

        public frmExtension(string jsonPath)
        {
            regerar = true;                                    
            IniciarComponentesForm();
            CarregarPropriedadesRegerar(jsonPath);            
        }

        private void IniciarComponentesForm()
        {
            jsonHelper = new JsonHelper();
            InitializeComponent();
            ConfiguracaoListView();
            btnRemoverPropriedade.Enabled = regerar;
            ckbAddMigration.Checked = true;
            CarregaConfigGit();
        }

        public void CarregaConfigGit()
        {
            gitConfig = jsonHelper.RetornarConfigGit();

            txtUrlGit.Text = gitConfig.UrlGit;
            txtDiretorioGit.Text = gitConfig.DiretorioGit;            
            txtDiretorioProjetoFront.Text = gitConfig.DiretorioProjetoFront;
        }
                
        private void CarregarPropriedadesRegerar(string jsonPath)
        {
            var entidade = JsonConvert.DeserializeObject<Entidade>(File.ReadAllText(jsonPath));
            txtNomeClasse.Text = entidade.Nome;            

            foreach (var item in entidade.Propriedades)
                PopularPropriedadesForm(item);
        }

        public void PopularPropriedadesForm(Propriedade propriedade)
        {
            var itensParaExibicao = new[] { propriedade.Nome, propriedade.Tipo.ToString(), propriedade.Nullable.ToString() };
            var item = new ListViewItem(itensParaExibicao);
            item.Tag = propriedade;
            lstPropriedades.Items.Add(item);
        }

        private IList<Propriedade> RetornarPropriedades()
        {
            var lstProp = new List<Propriedade>();

            foreach (ListViewItem itemRow in this.lstPropriedades.Items)
                lstProp.Add((Propriedade)itemRow.Tag);

            return lstProp;
        }

        private bool ExisteGerarCodigoSelecionado()
        {
            return gbGerarCodigo.Controls.OfType<RadioButton>().Any(x => x.Checked);
        }

        private eGeraCodigo GeraCodigoSelecionado()
        {
            return (eGeraCodigo)Enum.Parse(typeof(eGeraCodigo), 
                 gbGerarCodigo.Controls.OfType<RadioButton>()
                .Where(r => r.Checked)
                .Select(x => x.Name)
                .First());                           
        }

        private ConfiguracaoEntidade RetornarConfiguracaoEntidade()
        {
            if (ckbBaseline.Checked)
                return new ConfiguracaoEntidade(ckbAddMigration.Checked,
                                                  ckbUpdateBase.Checked,
                                                  ckbBaseline.Checked,                                                  
                                                  GeraCodigoSelecionado()
                                                  );

            return new ConfiguracaoEntidade(ckbAddMigration.Checked, 
                                                  ckbUpdateBase.Checked,                                                  
                                                  txtNumeroMigration.Text == string.Empty ? 0 : int.Parse(txtNumeroMigration.Text),
                                                  GeraCodigoSelecionado()
                                                  );
        }
        
        private bool ProcessaCrud()
        {
            try
            {
                var diretorioProjetoFront = txtDiretorioProjetoFront.Text;
                var lstProp = RetornarPropriedades();
                var entidade = new Entidade(txtNomeClasse.Text.ToPascalCase(), lstProp, regerar);
                var configuracaoEntidade = RetornarConfiguracaoEntidade();
                var builder = new ClassBuilder(jsonHelper);

                if (ckbGerarFront.Checked)
                {
                    builder.CriarArquivos(entidade, configuracaoEntidade, diretorioProjetoFront);
                    SalvarJsonConfig(diretorioProjetoFront);
                }
                else
                    builder.CriarArquivos(entidade, configuracaoEntidade);

                LimparForm();                
                var mensagem = ckbUpdateBase.Checked ? "Arquivos gerados com sucesso!\nVerifique se a migration foi aplicada no banco." : "Arquivos gerados com sucesso!";
                MessageBox.Show(mensagem);
                return true;
            }
            catch (Exception ex)
            {
                return RetornarMensagemDeErro("Ocorreu um erro durante a operação: " + ex);
            }
        }

        private void LimparForm()
        {
            txtNomeClasse.Text = string.Empty;
            lstPropriedades.Items.Clear();
        }
        
        private bool ValidarForm()
        {
            if (string.IsNullOrWhiteSpace(txtNomeClasse.Text))
                return RetornarMensagemDeErro("Necessário informar o nome da entidade.");

            if (regerar && ckbBaseline.Checked)
                return RetornarMensagemDeErro("Para regerar uma entidade, não é póssivel alterar o baseline. Por favor, informe o número da migration.");

            if (ckbAddMigration.Checked && !ckbBaseline.Checked && string.IsNullOrWhiteSpace(txtNumeroMigration.Text))
                return RetornarMensagemDeErro("Necessário informar o número da migration ou se a entidade fará parte do baseline.");

            if (!ExisteGerarCodigoSelecionado())
                return RetornarMensagemDeErro("Selecione pelo menos uma opção de geração de código.");

            if (ckbGerarFront.Checked && string.IsNullOrWhiteSpace(txtDiretorioProjetoFront.Text))
                return RetornarMensagemDeErro("Para a criação dos arquivos de front, é necessário informar o diretório do projeto.");

            return ValidarPropriedades();
        }        

        private bool ValidarPropriedades()
        {
            if (lstPropriedades.Items.Count == 0)
                return RetornarMensagemDeErro("Necessário adicionar ao menos uma propriedade.");

            return true;
        }

        private void ConfiguracaoListView()
        {
            lstPropriedades.View = View.Details;
            lstPropriedades.GridLines = true;
            lstPropriedades.FullRowSelect = true;

            lstPropriedades.Columns.Add("Nome", 190);
            lstPropriedades.Columns.Add("Tipo", 100);
            lstPropriedades.Columns.Add("Nullable", 100);            
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            ExisteGerarCodigoSelecionado();

            if (ValidarForm())
            {
                var processaCrud = ProcessaCrud();
                if (processaCrud && regerar)
                    Close();
            }
        }

        private void txtNumeroMigration_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar)) && (e.KeyChar != 8);
        }

        private void ckbAddMigration_CheckedChanged(object sender, EventArgs e)
        {
            bool deveHabilitar = ckbAddMigration.Checked;
            ckbUpdateBase.Checked = false;
            ckbUpdateBase.Enabled = deveHabilitar;
            ckbBaseline.Checked = false;
            ckbBaseline.Enabled = deveHabilitar;
            txtNumeroMigration.Clear();
            txtNumeroMigration.Enabled = deveHabilitar;
        }

        private void ckbBaseline_CheckedChanged(object sender, EventArgs e)
        {
            bool deveHabilitar = !ckbBaseline.Checked;
            txtNumeroMigration.Clear();
            txtNumeroMigration.Enabled = deveHabilitar;
        }

        private void btnAdicionarReferencia_Click(object sender, EventArgs e)
        {
            var form = new frmForeignKey(this);
            form.Show();
        }

        private void btnAdicionarPropriedade_Click(object sender, EventArgs e)
        {
            var form = new frmCriarPropriedade(this);
            form.Show();
        }

        private void btnRemoverPropriedade_Click(object sender, EventArgs e)
        {
            if (lstPropriedades.SelectedItems.Count > 0)
            {
                var confirmation = MessageBox.Show("Tem certeza que deseja remover?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation == DialogResult.Yes)
                {
                    for (int i = lstPropriedades.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        ListViewItem itm = lstPropriedades.SelectedItems[i];
                        lstPropriedades.Items[itm.Index].Remove();
                    }
                }
            }
            else
                MessageBox.Show("Necessário selecionar uma propriedade da lista.");
        }

        private void btnClonar_Click(object sender, EventArgs e)
        {
            if (!ValidarCloneGit())
                return;

            var urlGit = txtUrlGit.Text;
            var diretorioGit = txtDiretorioGit.Text;
            var posicaoNomeProjetoFront = urlGit.IndexOf("git/");
            var nomeProjetoFront = urlGit.Substring(posicaoNomeProjetoFront + 4);
            var diretorioProjetoFront = $@"{diretorioGit}\{nomeProjetoFront}";

            if (Directory.Exists(diretorioProjetoFront))
            {
                MessageBox.Show("Não foi possível clonar o repositório, pois já existe uma pasta associada a esse projeto no ambiente local.");
                return;
            }

            ExecutarGitCloneESalvarJsonConfig(urlGit, diretorioGit, diretorioProjetoFront);
            MessageBox.Show("Repositório clonado com sucesso!");
        }

        private void ExecutarGitCloneESalvarJsonConfig(string urlGit, string diretorioGit, string diretorioProjetoFront)
        {
            var commandGit = "git clone";            

            LinhaDeComando.Executar($"{commandGit} {urlGit}", diretorioGit);                        
            gitConfig = new GitConfig
            {
                UrlGit = urlGit,                
                DiretorioGit = diretorioGit,
                DiretorioProjetoFront = diretorioProjetoFront
            };
            
            SalvarJsonConfig(gitConfig);
            txtDiretorioProjetoFront.Text = gitConfig.DiretorioProjetoFront;
        }

        private void SalvarJsonConfig(string diretorioProjetoFront)
        {
            gitConfig.DiretorioProjetoFront = diretorioProjetoFront;
            jsonHelper.CriarJsonConfigGit(gitConfig);
        }

        private void SalvarJsonConfig(GitConfig gitConfig)
        {
            jsonHelper.CriarJsonConfigGit(gitConfig);
        }

        private bool ValidarCloneGit()
        {
            if (string.IsNullOrWhiteSpace(txtUrlGit.Text))
                return RetornarMensagemDeErro("Necessário informar a url do repositório.");

            if (string.IsNullOrWhiteSpace(txtDiretorioGit.Text))
                return RetornarMensagemDeErro("Necessário informar o diretório para o repositório local.");

            return true;
        }

        private bool RetornarMensagemDeErro(string mensagem)
        {
            MessageBox.Show(mensagem);
            return false;
        }

        private void ckbGerarFront_CheckedChanged(object sender, EventArgs e)
        {
            btnDiretorioProjetoFront.Enabled = ckbGerarFront.Checked;
            txtDiretorioProjetoFront.ReadOnly = !ckbGerarFront.Checked;            

            if (!ckbGerarFront.Checked)
                txtDiretorioProjetoFront.Text = gitConfig.DiretorioProjetoFront;
        }

        private void btnDiretorioGit_Click(object sender, EventArgs e)
        {
            var folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
                txtDiretorioGit.Text = folderDlg.SelectedPath;
        }

        private void btnDiretorioProjetoFront_Click(object sender, EventArgs e)
        {
            var folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
                txtDiretorioProjetoFront.Text = folderDlg.SelectedPath;
        }
    }
}