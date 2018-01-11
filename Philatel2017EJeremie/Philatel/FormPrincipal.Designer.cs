namespace Philatel
{
    partial class FormPrincipal
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
            this.menuStripPrincipal = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fichierQuitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opérationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opérationsAnnulerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opérationsRetablirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.opérationsAjouterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opérationsModifierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opérationsSupprimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aideÀproposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewArticles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDétails = new System.Windows.Forms.TextBox();
            this.buttonAfficher = new System.Windows.Forms.Button();
            this.buttonModifier = new System.Windows.Forms.Button();
            this.buttonSupprimer = new System.Windows.Forms.Button();
            this.buttonEffacerTout = new System.Windows.Forms.Button();
            this.menuStripPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripPrincipal
            // 
            this.menuStripPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.opérationsToolStripMenuItem,
            this.aideToolStripMenuItem});
            this.menuStripPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuStripPrincipal.Name = "menuStripPrincipal";
            this.menuStripPrincipal.Size = new System.Drawing.Size(584, 24);
            this.menuStripPrincipal.TabIndex = 0;
            this.menuStripPrincipal.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierQuitterToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fichierToolStripMenuItem.Text = "&Fichier";
            // 
            // fichierQuitterToolStripMenuItem
            // 
            this.fichierQuitterToolStripMenuItem.Name = "fichierQuitterToolStripMenuItem";
            this.fichierQuitterToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.fichierQuitterToolStripMenuItem.Text = "&Quitter";
            this.fichierQuitterToolStripMenuItem.Click += new System.EventHandler(this.fichierQuitterToolStripMenuItem_Click);
            // 
            // opérationsToolStripMenuItem
            // 
            this.opérationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opérationsAnnulerToolStripMenuItem,
            this.opérationsRetablirToolStripMenuItem,
            this.toolStripSeparator1,
            this.opérationsAjouterToolStripMenuItem,
            this.opérationsModifierToolStripMenuItem,
            this.opérationsSupprimerToolStripMenuItem});
            this.opérationsToolStripMenuItem.Name = "opérationsToolStripMenuItem";
            this.opérationsToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.opérationsToolStripMenuItem.Text = "&Opérations";
            this.opérationsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.ActiverDésactiverMenus);
            // 
            // opérationsAnnulerToolStripMenuItem
            // 
            this.opérationsAnnulerToolStripMenuItem.Name = "opérationsAnnulerToolStripMenuItem";
            this.opérationsAnnulerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.opérationsAnnulerToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.opérationsAnnulerToolStripMenuItem.Text = "&Annuler";
            this.opérationsAnnulerToolStripMenuItem.Click += new System.EventHandler(this.opérationsAnnulerToolStripMenuItem_Click);
            // 
            // opérationsRetablirToolStripMenuItem
            // 
            this.opérationsRetablirToolStripMenuItem.Name = "opérationsRetablirToolStripMenuItem";
            this.opérationsRetablirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.opérationsRetablirToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.opérationsRetablirToolStripMenuItem.Text = "&Retablir";
            this.opérationsRetablirToolStripMenuItem.Click += new System.EventHandler(this.opérationsRetablirToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
            // 
            // opérationsAjouterToolStripMenuItem
            // 
            this.opérationsAjouterToolStripMenuItem.Name = "opérationsAjouterToolStripMenuItem";
            this.opérationsAjouterToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.opérationsAjouterToolStripMenuItem.Text = "A&jouter";
            // 
            // opérationsModifierToolStripMenuItem
            // 
            this.opérationsModifierToolStripMenuItem.Name = "opérationsModifierToolStripMenuItem";
            this.opérationsModifierToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.opérationsModifierToolStripMenuItem.Text = "&Modifier...";
            this.opérationsModifierToolStripMenuItem.Click += new System.EventHandler(this.opérationsModifierToolStripMenuItem_Click);
            // 
            // opérationsSupprimerToolStripMenuItem
            // 
            this.opérationsSupprimerToolStripMenuItem.Name = "opérationsSupprimerToolStripMenuItem";
            this.opérationsSupprimerToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.opérationsSupprimerToolStripMenuItem.Text = "&Supprimer...";
            this.opérationsSupprimerToolStripMenuItem.Click += new System.EventHandler(this.opérationsSupprimerToolStripMenuItem_Click);
            // 
            // aideToolStripMenuItem
            // 
            this.aideToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aideÀproposToolStripMenuItem});
            this.aideToolStripMenuItem.Name = "aideToolStripMenuItem";
            this.aideToolStripMenuItem.Size = new System.Drawing.Size(24, 20);
            this.aideToolStripMenuItem.Text = "&?";
            // 
            // aideÀproposToolStripMenuItem
            // 
            this.aideÀproposToolStripMenuItem.Name = "aideÀproposToolStripMenuItem";
            this.aideÀproposToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aideÀproposToolStripMenuItem.Text = "À &propos";
            this.aideÀproposToolStripMenuItem.Click += new System.EventHandler(this.aideÀproposToolStripMenuItem_Click);
            // 
            // listViewArticles
            // 
            this.listViewArticles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewArticles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewArticles.FullRowSelect = true;
            this.listViewArticles.HideSelection = false;
            this.listViewArticles.Location = new System.Drawing.Point(12, 44);
            this.listViewArticles.MultiSelect = false;
            this.listViewArticles.Name = "listViewArticles";
            this.listViewArticles.ShowItemToolTips = true;
            this.listViewArticles.Size = new System.Drawing.Size(464, 250);
            this.listViewArticles.TabIndex = 2;
            this.listViewArticles.UseCompatibleStateImageBehavior = false;
            this.listViewArticles.View = System.Windows.Forms.View.Details;
            this.listViewArticles.SelectedIndexChanged += new System.EventHandler(this.listViewArticles_SelectedIndexChanged);
            this.listViewArticles.DoubleClick += new System.EventHandler(this.listViewArticles_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Description";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Motif";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Parution";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Prix payé";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "&Articles :";
            // 
            // textBoxDétails
            // 
            this.textBoxDétails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDétails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDétails.Location = new System.Drawing.Point(12, 300);
            this.textBoxDétails.Multiline = true;
            this.textBoxDétails.Name = "textBoxDétails";
            this.textBoxDétails.ReadOnly = true;
            this.textBoxDétails.Size = new System.Drawing.Size(464, 52);
            this.textBoxDétails.TabIndex = 4;
            // 
            // buttonAfficher
            // 
            this.buttonAfficher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAfficher.Location = new System.Drawing.Point(482, 44);
            this.buttonAfficher.Name = "buttonAfficher";
            this.buttonAfficher.Size = new System.Drawing.Size(94, 23);
            this.buttonAfficher.TabIndex = 3;
            this.buttonAfficher.Text = "A&fficher";
            this.buttonAfficher.UseVisualStyleBackColor = true;
            this.buttonAfficher.Click += new System.EventHandler(this.buttonAfficher_Click);
            // 
            // buttonModifier
            // 
            this.buttonModifier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonModifier.Location = new System.Drawing.Point(482, 73);
            this.buttonModifier.Name = "buttonModifier";
            this.buttonModifier.Size = new System.Drawing.Size(94, 23);
            this.buttonModifier.TabIndex = 5;
            this.buttonModifier.Text = "&Modifier";
            this.buttonModifier.UseVisualStyleBackColor = true;
            this.buttonModifier.Click += new System.EventHandler(this.buttonModifier_Click);
            // 
            // buttonSupprimer
            // 
            this.buttonSupprimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSupprimer.Location = new System.Drawing.Point(482, 102);
            this.buttonSupprimer.Name = "buttonSupprimer";
            this.buttonSupprimer.Size = new System.Drawing.Size(94, 23);
            this.buttonSupprimer.TabIndex = 6;
            this.buttonSupprimer.Text = "&Supprimer";
            this.buttonSupprimer.UseVisualStyleBackColor = true;
            this.buttonSupprimer.Click += new System.EventHandler(this.buttonSupprimer_Click);
            // 
            // buttonEffacerTout
            // 
            this.buttonEffacerTout.Location = new System.Drawing.Point(482, 271);
            this.buttonEffacerTout.Name = "buttonEffacerTout";
            this.buttonEffacerTout.Size = new System.Drawing.Size(94, 23);
            this.buttonEffacerTout.TabIndex = 7;
            this.buttonEffacerTout.Text = "Effacer Tout";
            this.buttonEffacerTout.UseVisualStyleBackColor = true;
            this.buttonEffacerTout.Click += new System.EventHandler(this.buttonEffacerTout_Click);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 364);
            this.Controls.Add(this.buttonEffacerTout);
            this.Controls.Add(this.buttonSupprimer);
            this.Controls.Add(this.buttonModifier);
            this.Controls.Add(this.buttonAfficher);
            this.Controls.Add(this.textBoxDétails);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewArticles);
            this.Controls.Add(this.menuStripPrincipal);
            this.MainMenuStrip = this.menuStripPrincipal;
            this.Name = "FormPrincipal";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPrincipal_FormClosing);
            this.menuStripPrincipal.ResumeLayout(false);
            this.menuStripPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripPrincipal;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fichierQuitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opérationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aideÀproposToolStripMenuItem;
        private System.Windows.Forms.ListView listViewArticles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDétails;
        private System.Windows.Forms.Button buttonAfficher;
        private System.Windows.Forms.Button buttonModifier;
        private System.Windows.Forms.Button buttonSupprimer;
        private System.Windows.Forms.ToolStripMenuItem opérationsAnnulerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem opérationsAjouterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opérationsModifierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opérationsSupprimerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opérationsRetablirToolStripMenuItem;
        private System.Windows.Forms.Button buttonEffacerTout;
    }
}

