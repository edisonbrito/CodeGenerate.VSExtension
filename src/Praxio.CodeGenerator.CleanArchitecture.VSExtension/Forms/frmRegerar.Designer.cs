namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Forms
{
    partial class frmRegerar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegerar));
            this.cbxJson = new System.Windows.Forms.ComboBox();
            this.btnIr = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbxJson
            // 
            this.cbxJson.FormattingEnabled = true;
            this.cbxJson.Location = new System.Drawing.Point(18, 22);
            this.cbxJson.Name = "cbxJson";
            this.cbxJson.Size = new System.Drawing.Size(239, 21);
            this.cbxJson.TabIndex = 0;
            
            // 
            // btnIr
            // 
            this.btnIr.Location = new System.Drawing.Point(157, 59);
            this.btnIr.Name = "btnIr";
            this.btnIr.Size = new System.Drawing.Size(100, 23);
            this.btnIr.TabIndex = 1;
            this.btnIr.Text = "Ir";
            this.btnIr.UseVisualStyleBackColor = true;
            this.btnIr.Click += new System.EventHandler(this.btnIr_Click);
            // 
            // frmRegerar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 99);
            this.Controls.Add(this.btnIr);
            this.Controls.Add(this.cbxJson);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRegerar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Escolher Entidade";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxJson;
        private System.Windows.Forms.Button btnIr;
    }
}