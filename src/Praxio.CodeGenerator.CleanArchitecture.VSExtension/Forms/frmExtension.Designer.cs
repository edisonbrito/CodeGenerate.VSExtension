namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Forms
{
    partial class frmExtension
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExtension));
            this.btnGerar = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtDiretorioProjetoFront = new System.Windows.Forms.TextBox();
            this.btnDiretorioProjetoFront = new System.Windows.Forms.Button();
            this.ckbGerarFront = new System.Windows.Forms.CheckBox();
            this.lblDiretorioProjetoFront = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ckbBaseline = new System.Windows.Forms.CheckBox();
            this.txtNumeroMigration = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbUpdateBase = new System.Windows.Forms.CheckBox();
            this.ckbAddMigration = new System.Windows.Forms.CheckBox();
            this.gbGerarCodigo = new System.Windows.Forms.GroupBox();
            this.Domain = new System.Windows.Forms.RadioButton();
            this.InfraDomain = new System.Windows.Forms.RadioButton();
            this.Crud = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAdicionarReferencia = new System.Windows.Forms.Button();
            this.btnRemoverPropriedade = new System.Windows.Forms.Button();
            this.lstPropriedades = new System.Windows.Forms.ListView();
            this.btnAdicionarPropriedade = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblClassName = new System.Windows.Forms.Label();
            this.txtNomeClasse = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gbConfiguracoesFront = new System.Windows.Forms.GroupBox();
            this.btnDiretorioGit = new System.Windows.Forms.Button();
            this.btnClonar = new System.Windows.Forms.Button();
            this.txtDiretorioGit = new System.Windows.Forms.TextBox();
            this.txtUrlGit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbGerarCodigo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.gbConfiguracoesFront.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGerar
            // 
            this.btnGerar.Location = new System.Drawing.Point(11, 610);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(620, 39);
            this.btnGerar.TabIndex = 25;
            this.btnGerar.Text = "GERAR";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(11, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(621, 592);
            this.tabControl1.TabIndex = 26;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.gbGerarCodigo);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(613, 566);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Principal";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtDiretorioProjetoFront);
            this.groupBox5.Controls.Add(this.btnDiretorioProjetoFront);
            this.groupBox5.Controls.Add(this.ckbGerarFront);
            this.groupBox5.Controls.Add(this.lblDiretorioProjetoFront);
            this.groupBox5.Location = new System.Drawing.Point(7, 500);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(595, 53);
            this.groupBox5.TabIndex = 38;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Front-end";
            // 
            // txtDiretorioProjetoFront
            // 
            this.txtDiretorioProjetoFront.Location = new System.Drawing.Point(306, 19);
            this.txtDiretorioProjetoFront.Name = "txtDiretorioProjetoFront";
            this.txtDiretorioProjetoFront.ReadOnly = true;
            this.txtDiretorioProjetoFront.Size = new System.Drawing.Size(280, 20);
            this.txtDiretorioProjetoFront.TabIndex = 36;
            // 
            // btnDiretorioProjetoFront
            // 
            this.btnDiretorioProjetoFront.Enabled = false;
            this.btnDiretorioProjetoFront.Image = global::Praxio.CodeGenerator.CleanArchitecture.VSExtension.Properties.Resources.addFolder;
            this.btnDiretorioProjetoFront.Location = new System.Drawing.Point(245, 17);
            this.btnDiretorioProjetoFront.Name = "btnDiretorioProjetoFront";
            this.btnDiretorioProjetoFront.Size = new System.Drawing.Size(33, 23);
            this.btnDiretorioProjetoFront.TabIndex = 37;
            this.btnDiretorioProjetoFront.UseVisualStyleBackColor = true;
            this.btnDiretorioProjetoFront.Click += new System.EventHandler(this.btnDiretorioProjetoFront_Click);
            // 
            // ckbGerarFront
            // 
            this.ckbGerarFront.AutoSize = true;
            this.ckbGerarFront.Location = new System.Drawing.Point(7, 22);
            this.ckbGerarFront.Name = "ckbGerarFront";
            this.ckbGerarFront.Size = new System.Drawing.Size(76, 17);
            this.ckbGerarFront.TabIndex = 34;
            this.ckbGerarFront.Text = "Gerar front";
            this.ckbGerarFront.UseVisualStyleBackColor = true;
            this.ckbGerarFront.CheckedChanged += new System.EventHandler(this.ckbGerarFront_CheckedChanged);
            // 
            // lblDiretorioProjetoFront
            // 
            this.lblDiretorioProjetoFront.AutoSize = true;
            this.lblDiretorioProjetoFront.Location = new System.Drawing.Point(137, 23);
            this.lblDiretorioProjetoFront.Name = "lblDiretorioProjetoFront";
            this.lblDiretorioProjetoFront.Size = new System.Drawing.Size(99, 13);
            this.lblDiretorioProjetoFront.TabIndex = 35;
            this.lblDiretorioProjetoFront.Text = "Diretório do projeto:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ckbBaseline);
            this.groupBox4.Controls.Add(this.txtNumeroMigration);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.ckbUpdateBase);
            this.groupBox4.Controls.Add(this.ckbAddMigration);
            this.groupBox4.Location = new System.Drawing.Point(7, 373);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(287, 121);
            this.groupBox4.TabIndex = 33;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "DataBase Migrations";
            // 
            // ckbBaseline
            // 
            this.ckbBaseline.AutoSize = true;
            this.ckbBaseline.Location = new System.Drawing.Point(16, 85);
            this.ckbBaseline.Name = "ckbBaseline";
            this.ckbBaseline.Size = new System.Drawing.Size(66, 17);
            this.ckbBaseline.TabIndex = 4;
            this.ckbBaseline.Text = "Baseline";
            this.ckbBaseline.UseVisualStyleBackColor = true;
            this.ckbBaseline.CheckedChanged += new System.EventHandler(this.ckbBaseline_CheckedChanged);
            // 
            // txtNumeroMigration
            // 
            this.txtNumeroMigration.Location = new System.Drawing.Point(217, 83);
            this.txtNumeroMigration.Name = "txtNumeroMigration";
            this.txtNumeroMigration.Size = new System.Drawing.Size(43, 20);
            this.txtNumeroMigration.TabIndex = 3;
            this.txtNumeroMigration.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeroMigration_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nº migration";
            // 
            // ckbUpdateBase
            // 
            this.ckbUpdateBase.AutoSize = true;
            this.ckbUpdateBase.Location = new System.Drawing.Point(16, 55);
            this.ckbUpdateBase.Name = "ckbUpdateBase";
            this.ckbUpdateBase.Size = new System.Drawing.Size(113, 17);
            this.ckbUpdateBase.TabIndex = 1;
            this.ckbUpdateBase.Text = "Executar migration";
            this.ckbUpdateBase.UseVisualStyleBackColor = true;
            // 
            // ckbAddMigration
            // 
            this.ckbAddMigration.AutoSize = true;
            this.ckbAddMigration.Location = new System.Drawing.Point(16, 24);
            this.ckbAddMigration.Name = "ckbAddMigration";
            this.ckbAddMigration.Size = new System.Drawing.Size(115, 17);
            this.ckbAddMigration.TabIndex = 0;
            this.ckbAddMigration.Text = "Adicionar migration";
            this.ckbAddMigration.UseVisualStyleBackColor = true;
            this.ckbAddMigration.CheckedChanged += new System.EventHandler(this.ckbAddMigration_CheckedChanged);
            // 
            // gbGerarCodigo
            // 
            this.gbGerarCodigo.Controls.Add(this.Domain);
            this.gbGerarCodigo.Controls.Add(this.InfraDomain);
            this.gbGerarCodigo.Controls.Add(this.Crud);
            this.gbGerarCodigo.Location = new System.Drawing.Point(322, 373);
            this.gbGerarCodigo.Name = "gbGerarCodigo";
            this.gbGerarCodigo.Size = new System.Drawing.Size(280, 121);
            this.gbGerarCodigo.TabIndex = 32;
            this.gbGerarCodigo.TabStop = false;
            this.gbGerarCodigo.Text = "Gerar Código";
            // 
            // Domain
            // 
            this.Domain.AutoSize = true;
            this.Domain.Location = new System.Drawing.Point(16, 83);
            this.Domain.Name = "Domain";
            this.Domain.Size = new System.Drawing.Size(100, 17);
            this.Domain.TabIndex = 7;
            this.Domain.TabStop = true;
            this.Domain.Text = "Apenas Domain";
            this.Domain.UseVisualStyleBackColor = true;
            // 
            // InfraDomain
            // 
            this.InfraDomain.AutoSize = true;
            this.InfraDomain.Location = new System.Drawing.Point(16, 54);
            this.InfraDomain.Name = "InfraDomain";
            this.InfraDomain.Size = new System.Drawing.Size(94, 17);
            this.InfraDomain.TabIndex = 6;
            this.InfraDomain.TabStop = true;
            this.InfraDomain.Text = "Infra e Domain";
            this.InfraDomain.UseVisualStyleBackColor = true;
            // 
            // Crud
            // 
            this.Crud.AutoSize = true;
            this.Crud.Location = new System.Drawing.Point(16, 23);
            this.Crud.Name = "Crud";
            this.Crud.Size = new System.Drawing.Size(102, 17);
            this.Crud.TabIndex = 5;
            this.Crud.TabStop = true;
            this.Crud.Text = "CRUD completo";
            this.Crud.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAdicionarReferencia);
            this.groupBox2.Controls.Add(this.btnRemoverPropriedade);
            this.groupBox2.Controls.Add(this.lstPropriedades);
            this.groupBox2.Controls.Add(this.btnAdicionarPropriedade);
            this.groupBox2.Location = new System.Drawing.Point(7, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(597, 255);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Propriedades";
            // 
            // btnAdicionarReferencia
            // 
            this.btnAdicionarReferencia.Location = new System.Drawing.Point(492, 58);
            this.btnAdicionarReferencia.Name = "btnAdicionarReferencia";
            this.btnAdicionarReferencia.Size = new System.Drawing.Size(99, 23);
            this.btnAdicionarReferencia.TabIndex = 8;
            this.btnAdicionarReferencia.Text = "Add Referência";
            this.btnAdicionarReferencia.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdicionarReferencia.UseVisualStyleBackColor = true;
            this.btnAdicionarReferencia.Click += new System.EventHandler(this.btnAdicionarReferencia_Click);
            // 
            // btnRemoverPropriedade
            // 
            this.btnRemoverPropriedade.Location = new System.Drawing.Point(492, 88);
            this.btnRemoverPropriedade.Name = "btnRemoverPropriedade";
            this.btnRemoverPropriedade.Size = new System.Drawing.Size(99, 23);
            this.btnRemoverPropriedade.TabIndex = 7;
            this.btnRemoverPropriedade.Text = "Remover";
            this.btnRemoverPropriedade.UseVisualStyleBackColor = true;
            this.btnRemoverPropriedade.Click += new System.EventHandler(this.btnRemoverPropriedade_Click);
            // 
            // lstPropriedades
            // 
            this.lstPropriedades.GridLines = true;
            this.lstPropriedades.Location = new System.Drawing.Point(16, 29);
            this.lstPropriedades.Name = "lstPropriedades";
            this.lstPropriedades.Size = new System.Drawing.Size(466, 202);
            this.lstPropriedades.TabIndex = 6;
            this.lstPropriedades.UseCompatibleStateImageBehavior = false;
            // 
            // btnAdicionarPropriedade
            // 
            this.btnAdicionarPropriedade.Location = new System.Drawing.Point(492, 29);
            this.btnAdicionarPropriedade.Name = "btnAdicionarPropriedade";
            this.btnAdicionarPropriedade.Size = new System.Drawing.Size(99, 23);
            this.btnAdicionarPropriedade.TabIndex = 5;
            this.btnAdicionarPropriedade.Text = "Add Propriedade";
            this.btnAdicionarPropriedade.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdicionarPropriedade.UseVisualStyleBackColor = true;
            this.btnAdicionarPropriedade.Click += new System.EventHandler(this.btnAdicionarPropriedade_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblClassName);
            this.groupBox1.Controls.Add(this.txtNomeClasse);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 81);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informações Gerais";
            // 
            // lblClassName
            // 
            this.lblClassName.AutoSize = true;
            this.lblClassName.Location = new System.Drawing.Point(6, 39);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(97, 13);
            this.lblClassName.TabIndex = 1;
            this.lblClassName.Text = "Nome da entidade:";
            // 
            // txtNomeClasse
            // 
            this.txtNomeClasse.Location = new System.Drawing.Point(109, 36);
            this.txtNomeClasse.Name = "txtNomeClasse";
            this.txtNomeClasse.Size = new System.Drawing.Size(201, 20);
            this.txtNomeClasse.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gbConfiguracoesFront);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(613, 566);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Configurações";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gbConfiguracoesFront
            // 
            this.gbConfiguracoesFront.Controls.Add(this.btnDiretorioGit);
            this.gbConfiguracoesFront.Controls.Add(this.btnClonar);
            this.gbConfiguracoesFront.Controls.Add(this.txtDiretorioGit);
            this.gbConfiguracoesFront.Controls.Add(this.txtUrlGit);
            this.gbConfiguracoesFront.Controls.Add(this.label1);
            this.gbConfiguracoesFront.Controls.Add(this.label3);
            this.gbConfiguracoesFront.Location = new System.Drawing.Point(11, 17);
            this.gbConfiguracoesFront.Name = "gbConfiguracoesFront";
            this.gbConfiguracoesFront.Size = new System.Drawing.Size(590, 123);
            this.gbConfiguracoesFront.TabIndex = 5;
            this.gbConfiguracoesFront.TabStop = false;
            this.gbConfiguracoesFront.Text = "Repositório de Front-end";
            // 
            // btnDiretorioGit
            // 
            this.btnDiretorioGit.Image = global::Praxio.CodeGenerator.CleanArchitecture.VSExtension.Properties.Resources.addFolder;
            this.btnDiretorioGit.Location = new System.Drawing.Point(61, 50);
            this.btnDiretorioGit.Name = "btnDiretorioGit";
            this.btnDiretorioGit.Size = new System.Drawing.Size(33, 23);
            this.btnDiretorioGit.TabIndex = 6;
            this.btnDiretorioGit.UseVisualStyleBackColor = true;
            this.btnDiretorioGit.Click += new System.EventHandler(this.btnDiretorioGit_Click);
            // 
            // btnClonar
            // 
            this.btnClonar.Location = new System.Drawing.Point(9, 88);
            this.btnClonar.Name = "btnClonar";
            this.btnClonar.Size = new System.Drawing.Size(85, 23);
            this.btnClonar.TabIndex = 5;
            this.btnClonar.Text = "Clonar";
            this.btnClonar.UseVisualStyleBackColor = true;
            this.btnClonar.Click += new System.EventHandler(this.btnClonar_Click);
            // 
            // txtDiretorioGit
            // 
            this.txtDiretorioGit.Location = new System.Drawing.Point(107, 52);
            this.txtDiretorioGit.Name = "txtDiretorioGit";
            this.txtDiretorioGit.Size = new System.Drawing.Size(427, 20);
            this.txtDiretorioGit.TabIndex = 3;
            // 
            // txtUrlGit
            // 
            this.txtUrlGit.Location = new System.Drawing.Point(107, 19);
            this.txtUrlGit.Name = "txtUrlGit";
            this.txtUrlGit.Size = new System.Drawing.Size(427, 20);
            this.txtUrlGit.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "URL Git:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Diretório:";
            // 
            // frmExtension
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 659);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnGerar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExtension";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerador de CRUD  v1.0";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.gbGerarCodigo.ResumeLayout(false);
            this.gbGerarCodigo.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.gbConfiguracoesFront.ResumeLayout(false);
            this.gbConfiguracoesFront.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox ckbBaseline;
        private System.Windows.Forms.TextBox txtNumeroMigration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckbUpdateBase;
        private System.Windows.Forms.CheckBox ckbAddMigration;
        private System.Windows.Forms.GroupBox gbGerarCodigo;
        private System.Windows.Forms.RadioButton Domain;
        private System.Windows.Forms.RadioButton InfraDomain;
        private System.Windows.Forms.RadioButton Crud;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAdicionarReferencia;
        public System.Windows.Forms.Button btnRemoverPropriedade;
        public System.Windows.Forms.ListView lstPropriedades;
        private System.Windows.Forms.Button btnAdicionarPropriedade;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblClassName;
        private System.Windows.Forms.TextBox txtNomeClasse;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox gbConfiguracoesFront;
        private System.Windows.Forms.Button btnClonar;
        private System.Windows.Forms.TextBox txtDiretorioGit;
        private System.Windows.Forms.TextBox txtUrlGit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDiretorioProjetoFront;
        private System.Windows.Forms.Label lblDiretorioProjetoFront;
        private System.Windows.Forms.CheckBox ckbGerarFront;
        private System.Windows.Forms.Button btnDiretorioGit;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnDiretorioProjetoFront;
        private System.Windows.Forms.GroupBox groupBox5;
    }
}