using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilStJ;
using Philatel;
using System.Data.OleDb;
using System.Data;

namespace PhilatelPNC
{
    /// <summary>
    /// Class Planche Non Coupée (PNC)
    /// </summary>
    [Serializable]
    public class PlancheNonCoupée : ArticlePhilatélique
    {
        public PlancheNonCoupée(int p_numéro, string p_motif, DateTime? p_parution, double? p_prixPayé, int p_nbTimbres,
                         string p_titre, int p_nbTimbresDifferents, string p_nomDesigner, int nbLignes, int p_nbColonnes, string p_tailleEtForme)
            : base(p_numéro, p_motif, p_parution, p_prixPayé, p_tailleEtForme)
        {
            Titre = p_titre;
            NombreTimbres = p_nbTimbres;
            NombreTimbresDifferents = p_nbTimbresDifferents;
            NomDesigner = p_nomDesigner;
            NombreLignes = nbLignes;
            NombreColonnes = p_nbColonnes;
            TimbresDifferents = new List<Tuple<string, Tuple<int, Double>>>();
        }

        public string Titre { get; }
        public int NombreTimbres { get; }
        public int NombreTimbresDifferents { get; }
        public string NomDesigner { get; }
        public int NombreLignes { get; }
        public int NombreColonnes { get; }
        public List<Tuple<string, Tuple<int, Double>>> TimbresDifferents { get; set; }

        public override string Catégorie
            => this.GetType().Name; // PlancheNonCoupée

        public override string Description()
            => Catégorie + " " + Titre + " " + NomDesigner;

        public override string ValeurNominale()
        {
            double valeur = 0.0;
            foreach (Tuple<string, Tuple<int, Double>> t in TimbresDifferents)
                try
                {
                    valeur += t.Item2.Item1 * t.Item2.Item2;
                }
                catch { }

            return $"{valeur}$";
        }
    }

    /// <summary>
    /// Fabrique Planche Non Coupée
    /// </summary>
    public class FabriquePlancheNonCoupée : IFabriqueCommande
    {
        // Singleton :
        public static FabriquePlancheNonCoupée InstanceFabrique { get; } = new FabriquePlancheNonCoupée();

        private FabriquePlancheNonCoupée()
        { }

        public string DescriptionPourMenu()
            => "PNC";

        public ICommande CréerCommandeAjouter()
           => new CommandeAjoutPNC();

        public ICommande CréerCommandeModifier(ArticlePhilatélique p_articleCourant)
            => new CommandeModificationPNC(p_articleCourant as PlancheNonCoupée);

        public ICommande CréerCommandeSupprimer(ArticlePhilatélique p_articleCourant)
            => new CommandeSuppression(p_articleCourant);

        public ICommande CréerCommandeAjouter(string p_motif, DateTime p_dateparution)
            => new CommandeAjoutPNC(p_motif, p_dateparution);

        public ICommande CréerCommandeSQLPourLireArticle()
            => new CommandeSQLLirePNC();

        public ICommande CréerCommandeSQLPourCréerColonnes(OleDbConnection p_bd)
            => new CommandeSQLCreerColonnesPNC(p_bd);

        public ICommande CréerCommandeSQLPourEcrireArticle()
            => new CommandeSQLEcrirePNC();
    }

    /// <summary>
    /// Commande Ajout PNC
    /// </summary>
    [Serializable]
    class CommandeAjoutPNC : CommandeAjout
    {
        public CommandeAjoutPNC(string p_motif, DateTime p_dateParution)
            : base(p_motif, p_dateParution)
        { }

        public CommandeAjoutPNC()
            : base()
        { }

        public override DlgSaisieArticle CréerDlgSaisie()
            => new DlgSaisiePNC(TypeDeSaisie.Ajout, null);

        public override DlgSaisieArticle CréerDlgSaisieAvecDonnées(string p_motif, DateTime p_dateParution)
            => new DlgSaisiePNC(p_motif, p_dateParution);
    }

    /// <summary>
    /// Commande Modification PNC
    /// </summary>
    [Serializable]
    class CommandeModificationPNC : CommandeModification
    {
        public CommandeModificationPNC(PlancheNonCoupée p_article)
            : base(p_article)
        { }

        public override DlgSaisieArticle CréerDlgSaisie(ArticlePhilatélique p_article)
            => new DlgSaisiePNC(TypeDeSaisie.Modification, p_article as PlancheNonCoupée);
    }

    /// <summary>
    /// Commande SQL pour lire un article PNC
    /// </summary>
    [Serializable]
    public class CommandeSQLLirePNC : CommandeLireSQL
    {
        public override ArticlePhilatélique SQLLireArticle( OleDbConnection m_bd, int p_numero)
        {
            ArticlePhilatélique Article = null;
            try
            {
                using (BdReader bdr = new BdReader(m_bd,
                        "SELECT numero, motif, date_parution, prix_payé, " +
                        "nombre_timbres, titre, nombre_timbres_diff, nom_designer, " +
                        "nb_lignes, nb_colonnes, taille_forme " +
                        "FROM Articles " +
                        "WHERE numero=?", p_numero))
                {
                    if (!bdr.Read() && !bdr.IsDBNull(1) && !bdr.IsDBNull(2) && !bdr.IsDBNull(5) &&
                         !bdr.IsDBNull(6) && !bdr.IsDBNull(7) && !bdr.IsDBNull(8) && !bdr.IsDBNull(9) &&
                         !bdr.IsDBNull(10))
                        return null;

                    Article = new PlancheNonCoupée(
                    bdr.GetInt32(0), bdr.GetString(1), bdr.GetDateTimeOuNull(2), bdr.GetDoubleOuNull(3),
                    bdr.GetInt32(4), bdr.GetString(5), bdr.GetInt32(6), bdr.GetString(7),
                    bdr.GetInt32(8), bdr.GetInt32(9), bdr.GetString(10));
                }
            }
            catch { }
            return Article;
        }
    }

    /// <summary>
    /// Commande SQL pour Ecrire un article PNC
    /// </summary>
    [Serializable]
    public class CommandeSQLEcrirePNC : CommandeEcrireSQL
    {
        public override bool SQLEcrireArticle(OleDbConnection m_bd, ArticlePhilatélique p_article)
        {
            PlancheNonCoupée Article = p_article as PlancheNonCoupée;
            try
            {
                BdNonQuery insert = new BdNonQuery(m_bd,
                "INSERT INTO Articles(type, numero, motif, date_parution, prix_payé, " +
                "nombre_timbres, titre, nombre_timbres_diff, nom_designer, " +
                "nb_lignes, nb_colonnes, taille_forme) " +
                "VALUES(?,?,?,?,?, ?,?,?,? ,?,?,?)",
                Article.GetType().ToString(), Article.Numéro, Article.Motif, Article.Parution.Value.Date, Article.PrixPayé,
                Article.NombreTimbres, Article.Titre, Article.NombreTimbresDifferents, Article.NomDesigner,
                Article.NombreLignes, Article.NombreColonnes, Article.TailleEtForme);

                insert.ExecuteNonQuery();
            }
            catch (Exception e){ return false; }
            return true;
        }
    }

    /// <summary>
    /// Commande SQL Creer Colonnes pour PNC
    /// </summary>
    [Serializable]
    public class CommandeSQLCreerColonnesPNC : CommandeCreerColonnesSQL
    {
        public CommandeSQLCreerColonnesPNC(OleDbConnection p_bd) : base(p_bd)
        {}

        public override bool SQLCreerColonnes(OleDbConnection m_bd)
        {
            ArticlePhilatélique Article = null;

            Tuple<string, string>[] colonnes = {
                new Tuple<string, string>("type", "Text(80)"),
                new Tuple<string, string>("numero", "Integer"),
                new Tuple<string, string>("motif", "Text(50)"),
                new Tuple<string, string>("date_parution", "Date"),
                new Tuple<string, string>("prix_payé", "Double"),
                new Tuple<string, string>("nombre_timbres", "Integer"),
                new Tuple<string, string>("titre", "Text(50)"),
                new Tuple<string, string>("nombre_timbres_diff", "Integer"),
                new Tuple<string, string>("nom_designer", "Text(50)"),    
                new Tuple<string, string>("nb_lignes", "Integer"),
                new Tuple<string, string>("nb_colonnes", "Integer"),
                new Tuple<string, string>("taille_forme", "Text(50)")
            };

            try
            {
                RequeteSQL.InsererColonnesSiNonExistantes("Articles", colonnes);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
