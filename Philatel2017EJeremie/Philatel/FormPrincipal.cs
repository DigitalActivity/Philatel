using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Xml.Serialization;
using UtilStJ;

namespace Philatel
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            new TrieurListe(listViewArticles, "ttdN").TrierSelonColonne(0);

            Text = InfoApp.Nom;
            // l'observateur ne sert plus puisqu'on n'utilise plus m_doc.
            //m_doc.Changement += MettreÀJour; // Système d'inscription en observateur

            CompléterLeMenu(opérationsAjouterToolStripMenuItem, OpérationsAjouter);
            CompléterLeSousMenu(opérationsAjouterToolStripMenuItem, OpérationsAjouterAvecDonnées);

            MettreÀJour();
        }

        public static void CompléterLeMenu(ToolStripMenuItem p_menu, EventHandler p_eh)
        {
            IEnumerator<IFabriqueCommande> f = LesFabriques.GetEnumerator();
            f.MoveNext();
            string descriptionPourMenu;

            while (f.Current != null)
            {
                descriptionPourMenu = TrouverNomPourMenuItems(f.Current.DescriptionPourMenu());

                var tsi = new ToolStripMenuItem(descriptionPourMenu, null, p_eh);
                tsi.Tag = f.Current;
                p_menu.DropDownItems.Add(tsi);
                descriptionPourMenu = "";
                f.MoveNext();
            }
        }

        public static void CompléterLeSousMenu(ToolStripMenuItem p_menu, EventHandler p_eh)
        {
            IFabriqueCommande f = null; // fabrique qui correspond a item
            IEnumerable<ArticlePhilatélique> derniersArticles = RequeteSQL.ToutesLesArticles();

            foreach (ToolStripMenuItem i in p_menu.DropDownItems)
            {
                IEnumerator<IFabriqueCommande> fs = LesFabriques.GetEnumerator();
                List<string> motifsExistants = new List<string>();

                fs.MoveNext();
                bool continuer = true;
                while (fs.Current != null && continuer)
                {
                    string nomType = i.Tag.GetType().ToString();
                    f = LesFabriques.TrouverFabrique(nomType);
                    if (f != null) break;
                    fs.MoveNext();
                }

                foreach (var article in RequeteSQL.ToutesLesArticles().Reverse())
                {
                    if (motifsExistants.Count() > 0 && !motifsExistants.Contains(article.Motif))
                    {
                        string descriptionPourMenu = TrouverNomPourMenuItems(f.DescriptionPourMenu() + " " + article.Motif);
                        motifsExistants.Add(article.Motif);
                        var tsi = new ToolStripMenuItem(descriptionPourMenu, null, p_eh, article.Motif + "@" + article.Parution);
                        tsi.Tag = f;
                        i.DropDownItems.Add(tsi);
                    }
                }
            }
        }

        Document m_doc = Document.Instance;
        ArticlePhilatélique m_articleCourant = null;
        // TP1 stack remplacé par PileLimitée
        PileLimitée<ICommande> m_commandesAnnulables = Document.Instance.m_commandesAnnulables;   // Commandes pour le Annuler/Ctrl+Z
        PileLimitée<ICommande> m_commandesRétablissbles = Document.Instance.m_commandesRétablissbles; // Commandes pour le Retablir/Ctrl+Y

        public void MettreÀJour()
        {
            listViewArticles.Items.Clear();

            foreach (var article in RequeteSQL.ToutesLesArticles())
            {
                ListViewItem lvi = new ListViewItem(article.Description());
                lvi.Tag = article.Numéro;
                lvi.SubItems.Add(article.Motif);
                lvi.SubItems.Add(article.Parution.HasValue ? article.Parution.Value.ToShortDateString() : "");
                lvi.SubItems.Add($"{article.PrixPayé:C}");
                listViewArticles.Items.Add(lvi);
            }

            listViewArticles_SelectedIndexChanged(null, null);
        }

        private void aideÀproposToolStripMenuItem_Click(object sender, EventArgs e)
            => new DlgÀPropos().ShowDialog();

        private void fichierQuitterToolStripMenuItem_Click(object sender, EventArgs e)
            => Close();

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
            => m_doc.Fermer();

        private void listViewArticles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewArticles.SelectedItems.Count == 0)
            {
                textBoxDétails.Text = "";
                m_articleCourant = null;
            }
            else
            {
                int noArticle = (int)listViewArticles.SelectedItems[0].Tag;
                m_articleCourant = RequeteSQL.ArticleSelonNumero(noArticle);
                textBoxDétails.Text = m_articleCourant.ToString().Replace("\n", "\r\n"); ;
            }

            ActiverDésactiverContrôlesPourLaListe();
        }

        private void ActiverDésactiverContrôlesPourLaListe()
        {
            bool actif = m_articleCourant != null;

            buttonAfficher.Enabled = actif;
            buttonModifier.Enabled = actif;
            buttonSupprimer.Enabled = actif;
        }

        private void ActiverDésactiverMenus(object sender, EventArgs e)
        {
            opérationsAnnulerToolStripMenuItem.Enabled = m_commandesAnnulables.NbÉléments() != 0;
            opérationsRetablirToolStripMenuItem.Enabled = m_commandesRétablissbles.NbÉléments() != 0;

            bool actif = m_articleCourant != null;

            opérationsModifierToolStripMenuItem.Enabled = actif;
            opérationsSupprimerToolStripMenuItem.Enabled = actif;
        }

        private void opérationsAnnulerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var commandeAnnuler = m_commandesAnnulables.Dépiler();
            m_commandesRétablissbles.Empiler(commandeAnnuler);
            commandeAnnuler.Annuler();
            MettreÀJour();
        }

        private void opérationsRetablirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var commandeRetablir = m_commandesRétablissbles.Dépiler();
            m_commandesAnnulables.Empiler(commandeRetablir);
            commandeRetablir.Retablir();
            MettreÀJour();
        }

        public void OpérationsAjouter(object p_sender, EventArgs p_e)
        {
            var tsi = (ToolStripMenuItem)p_sender;
            var fab = (IFabriqueCommande)tsi.Tag;
            var commande = fab.CréerCommandeAjouter();

            if (commande.Exécuter())
                m_commandesAnnulables.Empiler(commande);
            MettreÀJour();
        }

        public void OpérationsAjouterAvecDonnées(object p_sender, EventArgs p_e)
        {
            var tsi = (ToolStripMenuItem)p_sender;
            var fab = (IFabriqueCommande)tsi.Tag;

            var regex = new Regex(@"\b\d{4}\-\d{2}-\d{2}\b");
            DateTime dt = new DateTime();
            string[] tokents = tsi.Name.Split('@');

            foreach (Match m in regex.Matches(tokents[1]))
            {
                if (DateTime.TryParseExact(m.Value, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out dt))
                    Console.WriteLine(dt.ToString());
            }

            var commande = fab.CréerCommandeAjouter(tokents[0], dt);

            if (commande.Exécuter())
                m_commandesAnnulables.Empiler(commande);
            MettreÀJour();
        }

        private void opérationsModifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fab = LesFabriques.FabriqueDe(m_articleCourant.GetType());
            var commande = fab.CréerCommandeModifier(m_articleCourant);

            if (commande.Exécuter())
                m_commandesAnnulables.Empiler(commande);
            MettreÀJour();
        }

        private void opérationsSupprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fab = LesFabriques.FabriqueDe(m_articleCourant.GetType());
            var commande = fab.CréerCommandeSupprimer(m_articleCourant);

            if (commande.Exécuter())
                m_commandesAnnulables.Empiler(commande);
            MettreÀJour();
        }

        private void buttonAfficher_Click(object sender, EventArgs e)
        {
            MB.Avertir($"#{m_articleCourant.Numéro} {m_articleCourant.Description()}" +
                    " : Paresse ! (il faudrait tout afficher, sauf le numéro, interne)");
        }// J'aurais pu mettre « => », mais je continue la règle « plus qu'une ligne == bloc ».


        // On aurait pu associer directement ces trois événements aux fonctions déjà existantes...
        private void buttonModifier_Click(object sender, EventArgs e)
            => opérationsModifierToolStripMenuItem_Click(sender, e);

        private void buttonSupprimer_Click(object sender, EventArgs e)
            => opérationsSupprimerToolStripMenuItem_Click(sender, e);

        private void listViewArticles_DoubleClick(object sender, EventArgs e)
            => buttonModifier_Click(sender, e);  // Ou ... afficher ...

        // Cette méthode est ajoutée pour que les raccourcis clavier soient actifs ou pas, correctement.
        // Dans les premières versions de .NET (et dans les MFC, et peut-être ailleurs), les raccourcis
        // clavier généraient l'évènement DropDownOpening du menu concerné, mais ce n'est pas le cas
        // avec les ToolStripMenuItem, alors on active manuellement la mise à jour quand une touche
        // qui pourrait être un raccourci est détectée. On pourrait faire ça encore plus « subtilement »
        // si la mise à jour est longue, en détectant seulement les raccourcis vraiment utilisés, etc.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData & Keys.Control) == Keys.Control)
                ActiverDésactiverMenus(null, null);

            // Ou, s'il y a des raccourcis avec Alt, comme Alt+F2, etc. : 
            // if (... || (keyData & Keys.Alt) == Keys.Alt || ...) 
            // Ou, s'il y des raccourcis simples comme F2, etc., sans modificateur :
            // if (... || (F1 <= keyData && keyData <= F12) || ...)  

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Efface l'entièreté de la liste suita à une confirmation de l'utilisateur.
        private void buttonEffacerTout_Click(object sender, EventArgs e)
        {
            var commande = new CommandeEffacerTout();

            if (commande.Exécuter())
                m_commandesAnnulables.Empiler(commande);
            MettreÀJour();
        }

        private static List<char> listeCharsExistants = new List<char>();
        private static string TrouverNomPourMenuItems(string p_nomItem)
        {
            string descriptionPourMenu = p_nomItem;
            char c;
            foreach (string s in descriptionPourMenu.Split(' ', ',', '.', '-', '\'', '\"'))
            {
                c = s[0]; // Convert.ToChar(s.Substring(0, 1));
                if (!listeCharsExistants.Contains(Char.ToLower(c)))
                {
                    listeCharsExistants.Add(Char.ToLower(c));
                    descriptionPourMenu = descriptionPourMenu.Insert(descriptionPourMenu.IndexOf(s), "&");
                    return descriptionPourMenu;
                }
            }
            // Si aucune des premières lettres n'est disponible, on recherche la première voyelle non utilisée.
            try
            {
                c = descriptionPourMenu.First(
                        x => !listeCharsExistants.Contains(Char.ToLower(x))
                        && "aeiouy".Contains(Char.ToLower(x)));
                descriptionPourMenu = descriptionPourMenu.Insert(
                    descriptionPourMenu.IndexOf(c), "&");
            }
            catch (Exception) // Si aucune voyelle n'est disponible, prendre la première lettre disponible.
            {
                descriptionPourMenu = descriptionPourMenu.Insert(
                    descriptionPourMenu.IndexOf(
                        descriptionPourMenu.First(
                            x => !listeCharsExistants.Contains(Char.ToLower(x)))), "&");
            }
            return descriptionPourMenu;
        }
    }
}
