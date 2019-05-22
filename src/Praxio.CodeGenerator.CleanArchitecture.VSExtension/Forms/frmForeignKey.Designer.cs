namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Forms
{
    partial class frmForeignKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmForeignKey));
            this.cbxEntidade = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNomeEntidadePlural = new System.Windows.Forms.Label();
            this.txtNomePropriedade = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.ckbCollection = new System.Windows.Forms.CheckBox();
            this.ckbPermiteNulo = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbxEntidade
            // 
            this.cbxEntidade.FormattingEnabled = true;
            this.cbxEntidade.Location = new System.Drawing.Point(18, 37);
            this.cbxEntidade.Name = "cbxEntidade";
            this.cbxEntidade.Size = new System.Drawing.Size(426, 21);
            this.cbxEntidade.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(196, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Escolha a entidade";
            // 
            // lblNomeEntidadePlural
            // 
            this.lblNomeEntidadePlural.AutoSize = true;
            this.lblNomeEntidadePlural.Location = new System.Drawing.Point(15, 102);
            this.lblNomeEntidadePlural.Name = "lblNomeEntidadePlural";
            this.lblNomeEntidadePlural.Size = new System.Drawing.Size(134, 13);
            this.lblNomeEntidadePlural.TabIndex = 2;
            this.lblNomeEntidadePlural.Text = "Nome da entidade (plural) :";
            // 
            // txtNomePropriedade
            // 
            this.txtNomePropriedade.Location = new System.Drawing.Point(155, 99);
            this.txtNomePropriedade.Name = "txtNomePropriedade";
            this.txtNomePropriedade.Size = new System.Drawing.Size(198, 20);
            this.txtNomePropriedade.TabIndex = 5;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(369, 130);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // ckbCollection
            // 
            this.ckbCollection.AutoSize = true;
            this.ckbCollection.Location = new System.Drawing.Point(18, 75);
            this.ckbCollection.Name = "ckbCollection";
            this.ckbCollection.Size = new System.Drawing.Size(72, 17);
            this.ckbCollection.TabIndex = 7;
            this.ckbCollection.Text = "Collection";
            this.ckbCollection.UseVisualStyleBackColor = true;
            this.ckbCollection.CheckedChanged += new System.EventHandler(this.chkCollection_CheckedChanged);
            // 
            // ckbPermiteNulo
            // 
            this.ckbPermiteNulo.AutoSize = true;
            this.ckbPermiteNulo.Location = new System.Drawing.Point(106, 75);
            this.ckbPermiteNulo.Name = "ckbPermiteNulo";
            this.ckbPermiteNulo.Size = new System.Drawing.Size(110, 17);
            this.ckbPermiteNulo.TabIndex = 8;
            this.ckbPermiteNulo.Text = "Permite valor nulo";
            this.ckbPermiteNulo.UseVisualStyleBackColor = true;
            // 
            // frmForeignKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 162);
            this.Controls.Add(this.ckbPermiteNulo);
            this.Controls.Add(this.ckbCollection);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtNomePropriedade);
            this.Controls.Add(this.lblNomeEntidadePlural);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxEntidade);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmForeignKey";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Foreign Key";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxEntidade;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNomeEntidadePlural;
        private System.Windows.Forms.TextBox txtNomePropriedade;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox ckbCollection;
        private System.Windows.Forms.CheckBox ckbPermiteNulo;
    }
}