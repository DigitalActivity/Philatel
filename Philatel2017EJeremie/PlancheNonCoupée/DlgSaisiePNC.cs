using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Philatel;
using UtilStJ;
using System.Diagnostics;

namespace PhilatelPNC
{
    partial class DlgSaisiePNC : DlgSaisieArticle
    {
        private PlancheNonCoupée m_pnc;
        private readonly int TAILLE_FENETRE_INITIALE;

        /// <summary>
        /// Constructeur Dlg Saisie PNC données Completées
        /// </summary>
        /// <param name="p_opération">motif</param>
        /// <param name="p_dateParution">Date Parution</param>
        public DlgSaisiePNC(string p_motif, DateTime p_dateParution)
            : this(TypeDeSaisie.Ajout, null)
        {
            SetComboBoxMotif(p_motif);
            SetDateParution(p_dateParution);
        }

        /// <summary>
        /// Constructeur Dlg Saisie PNC
        /// </summary>
        /// <param name="p_opération"></param>
        /// <param name="p_pnc"></param>
        public DlgSaisiePNC(TypeDeSaisie p_opération, PlancheNonCoupée p_pnc)
             : base(p_opération, p_pnc)
        {
            InitializeComponent();
            TAILLE_FENETRE_INITIALE = this.Height;
            dataGridTimbresDifferents.AllowUserToAddRows = false;
            m_pnc = p_pnc;

            switch (p_opération)
            {
                case TypeDeSaisie.Ajout: Text = "Ajout d'une planche non coupée"; break;
                case TypeDeSaisie.Modification: Text = "Modification d'une planche non coupée"; break;
                case TypeDeSaisie.Autre: Debug.Assert(false, "Opération non implémentée"); break;
            }

            // Initialiser les champs
            if (m_pnc != null)
            {
                labelValeurNominale.Text = $"{m_pnc.ValeurNominale():F2}$";
                this.textBoxTitre.Text = m_pnc.Titre;
                this.textBoxNbTimbres.Text = m_pnc.NombreTimbres.ToString();
                setNbTimbresDifferents(m_pnc.NombreTimbresDifferents.ToString());
                this.textBoxNbLignes.Text = m_pnc.NombreLignes.ToString();
                this.textBoxNbColonnes.Text = m_pnc.NombreColonnes.ToString();
                this.labelValeurNominale.Text = m_pnc.ValeurNominale();
                foreach (Tuple<string, Tuple<int, Double>> timb in m_pnc.TimbresDifferents)
                    dataGridTimbresDifferents.Rows.Add(timb.Item1, timb.Item2.Item1, timb.Item2.Item2);

                // Adapter la taille de la fenetre a la taille de dataGridTimbresDifferents avec les differents timbres sur la PNC
                dataGridTimbresDifferents.Height = dataGridTimbresDifferents.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + 40;
                int hauteur = dataGridTimbresDifferents.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
                this.Height = TAILLE_FENETRE_INITIALE + (hauteur <= dataGridTimbresDifferents.MaximumSize.Height ?
                                                            hauteur : dataGridTimbresDifferents.MaximumSize.Height);
            }
        }

        /// <summary>
        /// set Nb de Timbres Differents sans déclancher l'evenement textBoxNbTimbresDiff_TextChanged
        /// (L'evenement ne sait pas si la valeur est modifiée par le code ou par l'utilisateur)
        /// </summary>
        private bool ignoreEventTextChanged = false;
        private void setNbTimbresDifferents(string p_texte)
        {
            ignoreEventTextChanged = true;
            this.textBoxNbTimbresDiff.Text = p_texte;
            ignoreEventTextChanged = false;
        }

        /// <summary>
        /// Evenement Quand le nombre de timbres differents est modifié. Ajouter de nouvelles lignes dans datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxNbTimbresDiff_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEventTextChanged || textBoxNbTimbresDiff.Text == "")
                return;

            int nb = Int32AvecMinimum(textBoxNbTimbresDiff, 0, "Nombre de  timbres differents");

            if (nb < dataGridTimbresDifferents.RowCount)
            {
                while (dataGridTimbresDifferents.RowCount > nb)
                   dataGridTimbresDifferents.Rows.Remove(dataGridTimbresDifferents.Rows[dataGridTimbresDifferents.RowCount - 1]);
            }
            else if (nb > dataGridTimbresDifferents.RowCount)
            {
                if (m_pnc == null)
                    while (dataGridTimbresDifferents.RowCount < nb)
                        dataGridTimbresDifferents.Rows.Add();
                else if (m_pnc.TimbresDifferents.Count <= nb)
                {
                    dataGridTimbresDifferents.Rows.Clear();
                    foreach (Tuple<string, Tuple<int, Double>> timb in m_pnc.TimbresDifferents)
                        dataGridTimbresDifferents.Rows.Add(timb.Item1, timb.Item2.Item1, timb.Item2.Item2);

                    while (dataGridTimbresDifferents.RowCount < nb)
                        dataGridTimbresDifferents.Rows.Add();
                }
            }

            // Calculer la valeure nominale de tous les timbres dans datagrid
            this.labelValeurNominale.Text = m_pnc?.ValeurNominale();
            // Adapter la fenetre
            dataGridTimbresDifferents.Height = dataGridTimbresDifferents.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + 40;
            int hauteur = dataGridTimbresDifferents.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            this.Height = TAILLE_FENETRE_INITIALE + (hauteur <= dataGridTimbresDifferents.MaximumSize.Height ? 
                                                        hauteur : dataGridTimbresDifferents.MaximumSize.Height);
         }

        /// <summary>
        /// Evenement quand une cellule de datagridview est modifiée. Vérifier les valeurs et calculer la valeur nominale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridTimbresUniques_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridTimbresDifferents.RowCount > 1) // ligne 1 c'est le header
            {
                string nom = "";
                double valeur = 0.0;
                int nombre = 0;
                nom = (string)dataGridTimbresDifferents[0, e.RowIndex]?.Value ?? "";

                if (dataGridTimbresDifferents[1, e.RowIndex]?.Value != null)// car operateur ?? impossible sur int ou double
                    int.TryParse(dataGridTimbresDifferents[1, e.RowIndex].Value.ToString(), out nombre);

                if (dataGridTimbresDifferents[2, e.RowIndex]?.Value != null)
                    double.TryParse(dataGridTimbresDifferents[2, e.RowIndex].Value.ToString(),out valeur);
            }

            this.labelValeurNominale.Text = m_pnc?.ValeurNominale();
        }

        /// <summary>
        /// Finir la validation
        /// </summary>
        /// <param name="p_motif"></param>
        /// <param name="p_parution"></param>
        /// <param name="p_prixPayé"></param>
        /// <returns></returns>
        public override bool FinirValidation(string p_motif, DateTime? p_parution, double? p_prixPayé, string p_tailleEtForme)
        {
            int nbTimbres = Int32AvecMinimum(textBoxNbTimbres, 0, "Nombre de timbres");
            int nbTimbresDifferents = Int32DansIntervalle(textBoxNbTimbresDiff, 0, nbTimbres, "Nombre de timbres differents");
            int nblignes = Int32AvecMinimum(textBoxNbTimbresDiff, 0, "Nombre de lignes");
            int nbcolonnes = Int32AvecMinimum(textBoxNbTimbresDiff, 0, "Nombre de colonnes");
            string titre = StringAvecLongueurMinimum(textBoxTitre, 1, "Titre planche non coupée");
            string designer = StringAvecLongueurMinimum(textBoxTitre, 1, "Nom designer planche non coupée");

            m_pnc = new PlancheNonCoupée(
                (m_pnc != null) ? Article.Numéro : Document.Instance.NuméroNouvelArticle(),
                p_motif, p_parution, p_prixPayé, nbTimbres,
                titre, nbTimbresDifferents, designer, nblignes, nbcolonnes, p_tailleEtForme);

            string nom;
            int nombre = 0;
            double valeur = 0.0;
            m_pnc.TimbresDifferents.Clear();
            for (int i = 0; i < nbTimbresDifferents; i++)
            {
                nom = dataGridTimbresDifferents[0, i]?.Value?.ToString() ?? "";
                int.TryParse(dataGridTimbresDifferents[1, i]?.Value?.ToString() ?? "0", out nombre);
                double.TryParse(dataGridTimbresDifferents[2, i]?.Value?.ToString() ?? "0,0", out valeur);
                m_pnc.TimbresDifferents.Add(new Tuple<string, Tuple<int, double>>
                                            (nom, new Tuple<int, double>(nombre, valeur)));
            }
            
            Article = m_pnc;
            this.labelValeurNominale.Text = m_pnc.ValeurNominale();
            return true;
        }
    }
}
