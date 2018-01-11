namespace PhilatelPPJO
{
    partial class DlgSaisiePPJO
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
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxValeurTimbres = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "&Valeur totale des timbres :";
            // 
            // textBoxValeurTimbres
            // 
            this.textBoxValeurTimbres.Location = new System.Drawing.Point(146, 117);
            this.textBoxValeurTimbres.Name = "textBoxValeurTimbres";
            this.textBoxValeurTimbres.Size = new System.Drawing.Size(59, 20);
            this.textBoxValeurTimbres.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(211, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "$";
            // 
            // DlgSaisiePPJO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 203);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxValeurTimbres);
            this.Controls.Add(this.label6);
            this.Name = "DlgSaisiePPJO";
            this.Text = "DlgSaisiePPJO";
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.textBoxValeurTimbres, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxValeurTimbres;
        private System.Windows.Forms.Label label6;
    }
}