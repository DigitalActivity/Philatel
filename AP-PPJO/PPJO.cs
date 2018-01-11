using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Philatel;
using UtilStJ;
using System.Data.OleDb;

namespace PhilatelPPJO
{
    /// <summary>
    /// Class article PPJO
    /// </summary>
    [Serializable]
    public class PPJO : ArticlePhilatélique
    {
        public PPJO(int p_numéro, string p_motif, DateTime? p_parution, double? p_prixPayé,
                                        double p_valeurTimbres, string p_tailleEtForme)
            : base(p_numéro, p_motif, p_parution, p_prixPayé, p_tailleEtForme)
        {
            ValeurTimbres = p_valeurTimbres;
        }

        public override string Catégorie
            => "Pli premier jour officiel";

        public double ValeurTimbres { get; }

        public override string Description()
            => Catégorie;

        public override string ValeurNominale()
            => Timbre.ValeurEnString(ValeurTimbres);
    }

    /// <summary>
    /// Fabrique PPJO
    /// </summary>
    public class FabriquePPJO : IFabriqueCommande
    {
        // Singleton :
        public static FabriquePPJO InstanceFabrique { get; } = new FabriquePPJO();

        private FabriquePPJO()
        { }

        public string DescriptionPourMenu()
            => "Pli premier jour officiel (PPJO)";

        public ICommande CréerCommandeAjouter()
           => new CommandeAjoutPPJO();

        public ICommande CréerCommandeModifier(ArticlePhilatélique p_articleCourant)
            => new CommandeModificationPPJO(p_articleCourant as PPJO);

        public ICommande CréerCommandeSupprimer(ArticlePhilatélique p_articleCourant)
            => new CommandeSuppression(p_articleCourant);

        public ICommande CréerCommandeAjouter(string p_motif, DateTime p_dateparution)
            => new CommandeAjoutPPJO(p_motif, p_dateparution);

        public ICommande CréerCommandeSQLPourLireArticle()
            => new CommandeSQLLirePPJO();

        public ICommande CréerCommandeSQLPourCréerColonnes(OleDbConnection p_bd)
            => new CommandeSQLCreerColonnesPPJO(p_bd);

        public ICommande CréerCommandeSQLPourEcrireArticle()
            => new CommandeSQLEcrirePPJO();
    }

    /// <summary>
    /// Commande Ajout PPJO
    /// </summary>
    [Serializable]
    class CommandeAjoutPPJO : CommandeAjout
    {
        public CommandeAjoutPPJO(string p_motif, DateTime p_dateParution)
            : base(p_motif, p_dateParution)
        { }

        public CommandeAjoutPPJO()
            : base()
        { }

        public override DlgSaisieArticle CréerDlgSaisie()
            => new DlgSaisiePPJO(TypeDeSaisie.Ajout, null);

        public override DlgSaisieArticle CréerDlgSaisieAvecDonnées(string p_motif, DateTime p_dateParution)
            => new DlgSaisiePPJO(p_motif, p_dateParution);
    }

    /// <summary>
    /// Commande Modification PPJO
    /// </summary>
    [Serializable]
    class CommandeModificationPPJO : CommandeModification
    {
        public CommandeModificationPPJO(PPJO p_article)
            : base(p_article)
        { }

        public override DlgSaisieArticle CréerDlgSaisie(ArticlePhilatélique p_article)
            => new DlgSaisiePPJO(TypeDeSaisie.Modification, p_article as PPJO);
    }

    /// <summary>
    /// Commande SQL pour Lire un article PPJO
    /// </summary>
    [Serializable]
    public class CommandeSQLLirePPJO : CommandeLireSQL
    {
        public override ArticlePhilatélique SQLLireArticle(OleDbConnection m_bd, int p_numero)
        {
            ArticlePhilatélique Article = null;
            try
            {
                using (BdReader bdr = new BdReader(m_bd,
                        "SELECT numero, motif, date_parution, prix_payé, " +
                        "valeur_timbre, taille_forme " +
                        "FROM Articles " +
                        "WHERE numero=?", p_numero))
                {
                    if (!bdr.Read() && !bdr.IsDBNull(0) && !bdr.IsDBNull(1) && !bdr.IsDBNull(4) && !bdr.IsDBNull(5))
                        return null;

                    Article = new PPJO(
                    bdr.GetInt32(0), bdr.GetString(1), bdr.GetDateTimeOuNull(2), bdr.GetDoubleOuNull(3),
                    bdr.GetDouble(4), bdr.GetString(5));
                }
            }
            catch { }
            return Article;
        }
    }

    /// <summary>
    /// Commande SQL pour ecrire PPJO
    /// </summary>
    [Serializable]
    public class CommandeSQLEcrirePPJO : CommandeEcrireSQL
    {
        public override bool SQLEcrireArticle(OleDbConnection m_bd, ArticlePhilatélique p_article)
        {
            PPJO Article = p_article as PPJO;
            try
            {
                BdNonQuery insert = new BdNonQuery(m_bd,
                "INSERT INTO Articles(type, numero, motif, date_parution, " +
                "prix_payé, valeur_timbre, taille_forme) " +
                "VALUES(?,?,?,?,?, ?,?)",
                Article.GetType().ToString(), Article.Numéro, Article.Motif, Article.Parution.Value.Date,
                Article.PrixPayé, Article.ValeurTimbres, Article.TailleEtForme);

                insert.ExecuteNonQuery();
            }
            catch { return false; }
            return true;
        }
    }

    /// <summary>
    /// Commande SQL Creer Colonnes pour PPJO
    /// </summary>
    [Serializable]
    public class CommandeSQLCreerColonnesPPJO : CommandeCreerColonnesSQL
    {
        public CommandeSQLCreerColonnesPPJO(OleDbConnection p_bd) : base(p_bd)
        { }

        public override bool SQLCreerColonnes(OleDbConnection m_bd)
        {
            ArticlePhilatélique Article = null;

            Tuple<string, string>[] colonnes = {
                new Tuple<string, string>("type", "Text(80)"),
                new Tuple<string, string>("numero", "Integer"),
                new Tuple<string, string>("motif", "Text(50)"),
                new Tuple<string, string>("prix_payé", "Double"),
                new Tuple<string, string>("valeur_timbre", "Double"),
                new Tuple<string, string>("taille_forme", "Text(50)")
            };

            try
            {
                RequeteSQL.InsererColonnesSiNonExistantes("Articles", colonnes);
            }
            catch {
                return false; // un problem est survenu
            }
            return true; // tables crées
        }
    }
}
