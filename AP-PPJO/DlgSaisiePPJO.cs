using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Philatel;
using UtilStJ;

namespace PhilatelPPJO
{
    partial class DlgSaisiePPJO : DlgSaisieArticle
    {

        public DlgSaisiePPJO(string p_opération, DateTime p_dateParution)
            : this(TypeDeSaisie.Ajout, null)
        {
            SetComboBoxMotif(p_opération);
            SetDateParution(p_dateParution);
        }

        public DlgSaisiePPJO(TypeDeSaisie p_opération, PPJO p_ppjo)
            : base(p_opération, p_ppjo)
        {
            InitializeComponent();
            CorrecteurDécimal.Corriger(textBoxValeurTimbres);

            switch (p_opération)
            {
                case TypeDeSaisie.Ajout: Text = "Ajout d'un pli premier jour officiel"; break;
                case TypeDeSaisie.Modification: Text = "Modification d'un pli premier jour officiel"; break;
                case TypeDeSaisie.Autre: Debug.Assert(false, "Opération non implémentée"); break;
            }

            if (p_ppjo != null)
                textBoxValeurTimbres.Text = $"{p_ppjo.ValeurTimbres:F2}";
        }

        public override bool FinirValidation(string p_motif, DateTime? p_parution, double? p_prixPayé, string p_tailleEtForme)
        {
            double valeurTimbres = DoubleAvecMinimum(textBoxValeurTimbres, 0.01, "Valeur des timbres");

            Article = new PPJO(
                (Article != null) ? Article.Numéro : Document.Instance.NuméroNouvelArticle(),
                p_motif, p_parution, p_prixPayé, valeurTimbres, p_tailleEtForme);
            return true;
        }
    }
}
