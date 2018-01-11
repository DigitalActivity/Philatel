namespace Philatel
{
    partial class DlgSaisieTimbreSeul
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
            this.textBoxValeurTimbre = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxOblitéré = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "&Valeur du timbre :";
            // 
            // textBoxValeurTimbre
            // 
            this.textBoxValeurTimbre.Location = new System.Drawing.Point(110, 117);
            this.textBoxValeurTimbre.Name = "textBoxValeurTimbre";
            this.textBoxValeurTimbre.Size = new System.Drawing.Size(66, 20);
            this.textBoxValeurTimbre.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(182, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "$";
            // 
            // checkBoxOblitéré
            // 
            this.checkBoxOblitéré.AutoSize = true;
            this.checkBoxOblitéré.Location = new System.Drawing.Point(18, 143);
            this.checkBoxOblitéré.Name = "checkBoxOblitéré";
            this.checkBoxOblitéré.Size = new System.Drawing.Size(62, 17);
            this.checkBoxOblitéré.TabIndex = 10;
            this.checkBoxOblitéré.Text = "&Oblitéré";
            this.checkBoxOblitéré.UseVisualStyleBackColor = true;
            // 
            // DlgSaisieTimbreSeul
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 213);
            this.Controls.Add(this.checkBoxOblitéré);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxValeurTimbre);
            this.Controls.Add(this.label6);
            this.Name = "DlgSaisieTimbreSeul";
            this.Text = "DlgSaisieTimbreSeul";
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.textBoxValeurTimbre, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.checkBoxOblitéré, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxValeurTimbre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxOblitéré;
    }
}