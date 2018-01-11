namespace Philatel
{
    partial class DlgSaisieBlocDeCoin
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
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxNombreTimbres = new System.Windows.Forms.TextBox();
            this.groupBoxCoins = new System.Windows.Forms.GroupBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBoxCoins.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "&Valeur d\'un timbre :";
            // 
            // textBoxValeurTimbre
            // 
            this.textBoxValeurTimbre.Location = new System.Drawing.Point(120, 117);
            this.textBoxValeurTimbre.Name = "textBoxValeurTimbre";
            this.textBoxValeurTimbre.Size = new System.Drawing.Size(66, 20);
            this.textBoxValeurTimbre.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(192, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "$";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "&Nombre de timbres :";
            // 
            // textBoxNombreTimbres
            // 
            this.textBoxNombreTimbres.Location = new System.Drawing.Point(124, 143);
            this.textBoxNombreTimbres.Name = "textBoxNombreTimbres";
            this.textBoxNombreTimbres.Size = new System.Drawing.Size(36, 20);
            this.textBoxNombreTimbres.TabIndex = 11;
            // 
            // groupBoxCoins
            // 
            this.groupBoxCoins.Controls.Add(this.radioButton4);
            this.groupBoxCoins.Controls.Add(this.radioButton3);
            this.groupBoxCoins.Controls.Add(this.radioButton2);
            this.groupBoxCoins.Controls.Add(this.radioButton1);
            this.groupBoxCoins.Location = new System.Drawing.Point(16, 169);
            this.groupBoxCoins.Name = "groupBoxCoins";
            this.groupBoxCoins.Size = new System.Drawing.Size(225, 85);
            this.groupBoxCoins.TabIndex = 12;
            this.groupBoxCoins.TabStop = false;
            this.groupBoxCoins.Text = "&Coin";
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioButton4.Location = new System.Drawing.Point(129, 54);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(86, 17);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Inférieur d&roit";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(7, 54);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(102, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "&Inférieur gauche";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioButton2.Location = new System.Drawing.Point(122, 20);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(93, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Supérieur &droit";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(109, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "&Supérieur gauche";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // DlgSaisieBlocDeCoin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 307);
            this.Controls.Add(this.groupBoxCoins);
            this.Controls.Add(this.textBoxNombreTimbres);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxValeurTimbre);
            this.Controls.Add(this.label6);
            this.Name = "DlgSaisieBlocDeCoin";
            this.Text = "DlgSaisieBlocDeCoin";
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.textBoxValeurTimbre, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.textBoxNombreTimbres, 0);
            this.Controls.SetChildIndex(this.groupBoxCoins, 0);
            this.groupBoxCoins.ResumeLayout(false);
            this.groupBoxCoins.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxValeurTimbre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxNombreTimbres;
        private System.Windows.Forms.GroupBox groupBoxCoins;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}