namespace PhilatelPNC
{
    partial class DlgSaisiePNC
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
            this.labelTitre = new System.Windows.Forms.Label();
            this.textBoxTitre = new System.Windows.Forms.TextBox();
            this.labellabelNombreTimbres = new System.Windows.Forms.Label();
            this.textBoxNbTimbres = new System.Windows.Forms.TextBox();
            this.labellabelNombreTimbresDiff = new System.Windows.Forms.Label();
            this.textBoxNbTimbresDiff = new System.Windows.Forms.TextBox();
            this.textBoxNbLignes = new System.Windows.Forms.TextBox();
            this.labelNombreLignes = new System.Windows.Forms.Label();
            this.textBoxNbColonnes = new System.Windows.Forms.TextBox();
            this.labellabelNombreColonnes = new System.Windows.Forms.Label();
            this.dataGridTimbresDifferents = new System.Windows.Forms.DataGridView();
            this.nomTimbre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valeurTimbre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.labelValeurNominale = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTimbresDifferents)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitre
            // 
            this.labelTitre.AutoSize = true;
            this.labelTitre.Location = new System.Drawing.Point(13, 120);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(34, 13);
            this.labelTitre.TabIndex = 1001;
            this.labelTitre.Text = "Titre :";
            // 
            // textBoxTitre
            // 
            this.textBoxTitre.Location = new System.Drawing.Point(53, 117);
            this.textBoxTitre.Name = "textBoxTitre";
            this.textBoxTitre.Size = new System.Drawing.Size(177, 20);
            this.textBoxTitre.TabIndex = 1002;
            // 
            // labellabelNombreTimbres
            // 
            this.labellabelNombreTimbres.AutoSize = true;
            this.labellabelNombreTimbres.Location = new System.Drawing.Point(13, 198);
            this.labellabelNombreTimbres.Name = "labellabelNombreTimbres";
            this.labellabelNombreTimbres.Size = new System.Drawing.Size(101, 13);
            this.labellabelNombreTimbres.TabIndex = 1003;
            this.labellabelNombreTimbres.Text = "Nombre de timbres :";
            // 
            // textBoxNbTimbres
            // 
            this.textBoxNbTimbres.Location = new System.Drawing.Point(115, 195);
            this.textBoxNbTimbres.Name = "textBoxNbTimbres";
            this.textBoxNbTimbres.Size = new System.Drawing.Size(26, 20);
            this.textBoxNbTimbres.TabIndex = 1004;
            // 
            // labellabelNombreTimbresDiff
            // 
            this.labellabelNombreTimbresDiff.AutoSize = true;
            this.labellabelNombreTimbresDiff.Location = new System.Drawing.Point(13, 224);
            this.labellabelNombreTimbresDiff.Name = "labellabelNombreTimbresDiff";
            this.labellabelNombreTimbresDiff.Size = new System.Drawing.Size(132, 13);
            this.labellabelNombreTimbresDiff.TabIndex = 1005;
            this.labellabelNombreTimbresDiff.Text = "Nombre timbres differents :";
            // 
            // textBoxNbTimbresDiff
            // 
            this.textBoxNbTimbresDiff.Location = new System.Drawing.Point(151, 221);
            this.textBoxNbTimbresDiff.Name = "textBoxNbTimbresDiff";
            this.textBoxNbTimbresDiff.Size = new System.Drawing.Size(26, 20);
            this.textBoxNbTimbresDiff.TabIndex = 1006;
            this.textBoxNbTimbresDiff.TextChanged += new System.EventHandler(this.textBoxNbTimbresDiff_TextChanged);
            // 
            // textBoxNbLignes
            // 
            this.textBoxNbLignes.Location = new System.Drawing.Point(115, 143);
            this.textBoxNbLignes.Name = "textBoxNbLignes";
            this.textBoxNbLignes.Size = new System.Drawing.Size(26, 20);
            this.textBoxNbLignes.TabIndex = 1007;
            // 
            // labelNombreLignes
            // 
            this.labelNombreLignes.AutoSize = true;
            this.labelNombreLignes.Location = new System.Drawing.Point(13, 146);
            this.labelNombreLignes.Name = "labelNombreLignes";
            this.labelNombreLignes.Size = new System.Drawing.Size(80, 13);
            this.labelNombreLignes.TabIndex = 1008;
            this.labelNombreLignes.Text = "Nombre lignes :";
            // 
            // textBoxNbColonnes
            // 
            this.textBoxNbColonnes.Location = new System.Drawing.Point(115, 169);
            this.textBoxNbColonnes.Name = "textBoxNbColonnes";
            this.textBoxNbColonnes.Size = new System.Drawing.Size(26, 20);
            this.textBoxNbColonnes.TabIndex = 1009;
            // 
            // labellabelNombreColonnes
            // 
            this.labellabelNombreColonnes.AutoSize = true;
            this.labellabelNombreColonnes.Location = new System.Drawing.Point(13, 172);
            this.labellabelNombreColonnes.Name = "labellabelNombreColonnes";
            this.labellabelNombreColonnes.Size = new System.Drawing.Size(96, 13);
            this.labellabelNombreColonnes.TabIndex = 1010;
            this.labellabelNombreColonnes.Text = "Nombre colonnes :";
            // 
            // dataGridTimbresDifferents
            // 
            this.dataGridTimbresDifferents.AllowUserToOrderColumns = true;
            this.dataGridTimbresDifferents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTimbresDifferents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nomTimbre,
            this.nombre,
            this.valeurTimbre});
            this.dataGridTimbresDifferents.Location = new System.Drawing.Point(16, 254);
            this.dataGridTimbresDifferents.MaximumSize = new System.Drawing.Size(228, 120);
            this.dataGridTimbresDifferents.MinimumSize = new System.Drawing.Size(228, 22);
            this.dataGridTimbresDifferents.Name = "dataGridTimbresDifferents";
            this.dataGridTimbresDifferents.Size = new System.Drawing.Size(228, 22);
            this.dataGridTimbresDifferents.TabIndex = 1011;
            this.dataGridTimbresDifferents.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridTimbresUniques_CellValueChanged);
            // 
            // nomTimbre
            // 
            this.nomTimbre.HeaderText = "Timbre";
            this.nomTimbre.MaxInputLength = 100;
            this.nomTimbre.MinimumWidth = 50;
            this.nomTimbre.Name = "nomTimbre";
            this.nomTimbre.Width = 80;
            // 
            // nombre
            // 
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.Width = 45;
            // 
            // valeurTimbre
            // 
            this.valeurTimbre.HeaderText = "Valeur";
            this.valeurTimbre.MaxInputLength = 15;
            this.valeurTimbre.MinimumWidth = 20;
            this.valeurTimbre.Name = "valeurTimbre";
            this.valeurTimbre.Width = 60;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(156, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 1012;
            this.label5.Text = "Valeur nominale :";
            // 
            // labelValeurNominale
            // 
            this.labelValeurNominale.AutoSize = true;
            this.labelValeurNominale.Location = new System.Drawing.Point(202, 159);
            this.labelValeurNominale.Name = "labelValeurNominale";
            this.labelValeurNominale.Size = new System.Drawing.Size(28, 13);
            this.labelValeurNominale.TabIndex = 1013;
            this.labelValeurNominale.Text = "0.0$";
            // 
            // DlgSaisiePNC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 344);
            this.Controls.Add(this.labelValeurNominale);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridTimbresDifferents);
            this.Controls.Add(this.labellabelNombreColonnes);
            this.Controls.Add(this.textBoxNbColonnes);
            this.Controls.Add(this.labelNombreLignes);
            this.Controls.Add(this.textBoxNbLignes);
            this.Controls.Add(this.textBoxNbTimbresDiff);
            this.Controls.Add(this.labellabelNombreTimbresDiff);
            this.Controls.Add(this.textBoxNbTimbres);
            this.Controls.Add(this.labellabelNombreTimbres);
            this.Controls.Add(this.textBoxTitre);
            this.Controls.Add(this.labelTitre);
            this.Name = "DlgSaisiePNC";
            this.Text = "DlgSaisiePNC";
            this.Controls.SetChildIndex(this.labelTitre, 0);
            this.Controls.SetChildIndex(this.textBoxTitre, 0);
            this.Controls.SetChildIndex(this.labellabelNombreTimbres, 0);
            this.Controls.SetChildIndex(this.textBoxNbTimbres, 0);
            this.Controls.SetChildIndex(this.labellabelNombreTimbresDiff, 0);
            this.Controls.SetChildIndex(this.textBoxNbTimbresDiff, 0);
            this.Controls.SetChildIndex(this.textBoxNbLignes, 0);
            this.Controls.SetChildIndex(this.labelNombreLignes, 0);
            this.Controls.SetChildIndex(this.textBoxNbColonnes, 0);
            this.Controls.SetChildIndex(this.labellabelNombreColonnes, 0);
            this.Controls.SetChildIndex(this.dataGridTimbresDifferents, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.labelValeurNominale, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTimbresDifferents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.TextBox textBoxTitre;
        private System.Windows.Forms.Label labellabelNombreTimbres;
        private System.Windows.Forms.TextBox textBoxNbTimbres;
        private System.Windows.Forms.Label labellabelNombreTimbresDiff;
        private System.Windows.Forms.TextBox textBoxNbTimbresDiff;
        private System.Windows.Forms.TextBox textBoxNbLignes;
        private System.Windows.Forms.Label labelNombreLignes;
        private System.Windows.Forms.TextBox textBoxNbColonnes;
        private System.Windows.Forms.Label labellabelNombreColonnes;
        private System.Windows.Forms.DataGridView dataGridTimbresDifferents;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomTimbre;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn valeurTimbre;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelValeurNominale;
    }
}