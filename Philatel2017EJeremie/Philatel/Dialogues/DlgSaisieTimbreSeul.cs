using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtilStJ;

namespace Philatel
{
    public partial class DlgSaisieTimbreSeul : DlgSaisieArticle
    {
        public DlgSaisieTimbreSeul(string p_opération, DateTime p_dateParution)
            : this(TypeDeSaisie.Ajout, null)
        {
            SetComboBoxMotif(p_opération);
            SetDateParution(p_dateParution);
        }

        public DlgSaisieTimbreSeul(TypeDeSaisie p_opération, TimbreSeul p_timbre)
            : base(p_opération, p_timbre)
        {
            InitializeComponent();
            CorrecteurDécimal.Corriger(textBoxValeurTimbre);

            switch (p_opération)
            {
                case TypeDeSaisie.Ajout: Text = "Ajout d'un timbre seul"; break;
                case TypeDeSaisie.Modification: Text = "Modification d'un timbre seul"; break;
                case TypeDeSaisie.Autre: Debug.Assert(false, "Opération non implémentée"); break;
            }

            if (p_timbre != null)
            {
                textBoxValeurTimbre.Text = $"{p_timbre.ValeurTimbre:F2}";
                checkBoxOblitéré.Checked = p_timbre.Oblitération == Oblitération.Normale;
            }
        }

        public override bool FinirValidation(string p_motif, DateTime? p_parution, double? p_prixPayé, string p_tailleEtForme)
        {
            double valeurTimbre = DoubleAvecMinimum(textBoxValeurTimbre, 0.01, "Valeur du timbre");
            Oblitération oblitération = checkBoxOblitéré.Checked ? Oblitération.Normale : Oblitération.Aucune;

            Article = new TimbreSeul(
                (Article != null) ? Article.Numéro : Document.Instance.NuméroNouvelArticle(),
                p_motif, p_parution, p_prixPayé, valeurTimbre, oblitération, p_tailleEtForme);
            return true;
        }
    }
}

