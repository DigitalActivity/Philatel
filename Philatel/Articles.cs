using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtilStJ;
using Philatel;
using System.Data.OleDb;
using System.Data;

namespace Philatel
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////// Types et fonctions utilitaires (dans la classe statique Timbre)
    ////////////

    [Serializable]
    public enum Coin { SupérieurGauche, SupérieurDroit, InférieurGauche, InférieurDroit }

    [Serializable]
    public enum Oblitération { Aucune, Normale }  // Pourrait un jour contenir Spéciale, Erronné, etc. ?

    static public class Timbre
    {
        public static string ValeurEnString(double p_valeur)
            => (p_valeur < 1.0) ? $"{(int)Math.Round(p_valeur * 100)} ¢" : $"{p_valeur: C}";
        // N.B. Il manque probablement un test pour savoir s'il faut espace avant ¢ (il suffirait de
        //      regarder si :C en met un)

        public static string CoinEnString(Coin p_coin)
        {
            switch (p_coin)
            {
                case Coin.SupérieurGauche: return "supérieur gauche";
                case Coin.SupérieurDroit: return "supérieur droit";
                case Coin.InférieurGauche: return "inférieur gauche";
                case Coin.InférieurDroit: return "inférieur droit";
                default: Debug.Assert(false); return "";
            }
        }

        public static string OblitérationEnString(Oblitération p_oblitération)
        {
            switch (p_oblitération)
            {
                case Oblitération.Aucune: return "non oblitéré";
                case Oblitération.Normale: return "oblitéré";
                default: Debug.Assert(false); return "";
            }
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////// Deux types d'articles de base (leurs fabriques et commandes suivent)
    ////////////

    [Serializable]
    public class TimbreSeul : ArticlePhilatélique
    {
        public TimbreSeul(int p_numéro, string p_motif, DateTime? p_parution, double? p_prixPayé,
                          double p_valeurTimbre, Oblitération p_oblitération, string p_tailleEtForme)
            : base(p_numéro, p_motif, p_parution, p_prixPayé, p_tailleEtForme)
        {
            ValeurTimbre = p_valeurTimbre;
            Oblitération = p_oblitération;
        }

        public override string Catégorie => "Timbre seul";

        public double ValeurTimbre { get; }
        public Oblitération Oblitération { get; }

        public override string Description()
            => Catégorie + " " + Timbre.OblitérationEnString(Oblitération);

        public override string ValeurNominale()
            => Timbre.ValeurEnString(ValeurTimbre);
    }

    [Serializable]
    public class BlocDeCoin : ArticlePhilatélique
    {
        public BlocDeCoin(int p_numéro, string p_motif, DateTime? p_parution, double? p_prixPayé,
                          Coin p_coin, double p_valeurTimbre, int p_nbTimbres, string p_tailleEtForme)
            : base(p_numéro, p_motif, p_parution, p_prixPayé, p_tailleEtForme)
        {
            Coin = p_coin;
            ValeurTimbre = p_valeurTimbre;
            NbTimbres = p_nbTimbres;
        }

        public override string Catégorie => "Bloc de coin";

        public Coin Coin { get; }

        public double ValeurTimbre { get; }

        public int NbTimbres { get; }

        public override string Description()
            => Catégorie + " " + Timbre.CoinEnString(Coin);

        public override string ValeurNominale()
            => $"{NbTimbres} × {Timbre.ValeurEnString(ValeurTimbre)}";
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////// Les fabriques des deux types d'articles de base
    ////////////

    public class FabriqueTimbreSeul : IFabriqueCommande
    {
        // Singleton :
        public static FabriqueTimbreSeul Instance { get; } = new FabriqueTimbreSeul();

        private FabriqueTimbreSeul()
        { }

        public string DescriptionPourMenu()
            => "Timbre seul";

        public ICommande CréerCommandeAjouter()
            => new CommandeAjoutTS();

        public ICommande CréerCommandeModifier(ArticlePhilatélique p_articleCourant)
            => new CommandeModificationTS(p_articleCourant as TimbreSeul);

        public ICommande CréerCommandeSupprimer(ArticlePhilatélique p_articleCourant)
            => new CommandeSuppression(p_articleCourant); // Approche générale

        public ICommande CréerCommandeAjouter(string p_motif, DateTime p_dateparution)
            => new CommandeAjoutTS(p_motif, p_dateparution);

        public ICommande CréerCommandeSQLPourLireArticle()
            => new CommandeSQLLireTS();

        public ICommande CréerCommandeSQLPourCréerColonnes(OleDbConnection m_bd)
            => new CommandeSQLCreerColonnesTS(m_bd);

        public ICommande CréerCommandeSQLPourEcrireArticle()
            => new CommandeSQLEcrireTS();
    }

    public class FabriqueBlocDeCoin : IFabriqueCommande
    {
        // Singleton :
        public static FabriqueBlocDeCoin Instance { get; } = new FabriqueBlocDeCoin();

        private FabriqueBlocDeCoin()
        { }

        public string DescriptionPourMenu()
            => "Bloc de coin";

        public ICommande CréerCommandeAjouter()
            => new CommandeAjoutBC();

        public ICommande CréerCommandeModifier(ArticlePhilatélique p_articleCourant)
            => new CommandeModificationBC(p_articleCourant as BlocDeCoin);

        public ICommande CréerCommandeSupprimer(ArticlePhilatélique p_articleCourant)
            => new CommandeSuppressionBC(p_articleCourant as BlocDeCoin); // Particulier

        public ICommande CréerCommandeAjouter(string p_motif, DateTime p_dateparution)
            => new CommandeAjoutBC(p_motif, p_dateparution);

        public ICommande CréerCommandeSQLPourLireArticle()
            => new CommandeSQLLireBC();

        public ICommande CréerCommandeSQLPourCréerColonnes(OleDbConnection m_bd)
            => new CommandeSQLCreerColonnessBC(m_bd);

        public ICommande CréerCommandeSQLPourEcrireArticle()
            => new CommandeSQLEcrireBC();
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////// Les commandes des deux types d'articles de base
    ////////////

    [Serializable]
    public class CommandeAjoutTS : CommandeAjout
    {
        public CommandeAjoutTS(string p_motif, DateTime p_dateParution)
            : base(p_motif, p_dateParution)
        { }

        public CommandeAjoutTS()
            : base()
        { }

        public override DlgSaisieArticle CréerDlgSaisie()
            => new DlgSaisieTimbreSeul(TypeDeSaisie.Ajout, null);

        public override DlgSaisieArticle CréerDlgSaisieAvecDonnées(string p_motif, DateTime p_dateParution)
            => new DlgSaisieTimbreSeul(p_motif, p_dateParution);
    }

    [Serializable]
    public class CommandeSQLLireTS : CommandeLireSQL
    {
        public override ArticlePhilatélique SQLLireArticle(OleDbConnection m_bd, int p_numero)
        {
            ArticlePhilatélique Article = null;
            try
            {
                using (BdReader bdr = new BdReader(m_bd,
                        "SELECT numero, motif, date_parution, prix_payé, " +
                        "valeur_timbre, obliteration, taille_forme " +
                        "FROM Articles " +
                        "WHERE numero=?", p_numero))
                {
                    if (!bdr.Read() && !bdr.IsDBNull(1) && !bdr.IsDBNull(4) && !bdr.IsDBNull(5) &&
                         !bdr.IsDBNull(6))
                        return null;

                    Article = new TimbreSeul(
                    bdr.GetInt32(0), bdr.GetString(1), bdr.GetDateTime(2), bdr.GetDoubleOuNull(3), 
                    bdr.GetDouble(4), (Oblitération) bdr.GetInt32(5), bdr.GetString(6));
                }
            }
            catch { }
            return Article;
        }
    }

    [Serializable]
    public class CommandeSQLEcrireTS : CommandeEcrireSQL
    {


        public override bool SQLEcrireArticle(OleDbConnection m_bd, ArticlePhilatélique p_article)
        {
            TimbreSeul Article = p_article as TimbreSeul;

            try
            {
                BdNonQuery insert = new BdNonQuery(m_bd,
                "INSERT INTO Articles(type, numero, motif, date_parution, prix_payé, " +
                "valeur_timbre, obliteration, taille_forme) " + 
                "VALUES(?,?,?,?,? ,?,?,?)",
                Article.GetType().ToString(), Article.Numéro, Article.Motif, Article.Parution.Value.Date, Article.PrixPayé,
                Article.ValeurTimbre, (int)Article.Oblitération, Article.TailleEtForme);

                insert.ExecuteNonQuery();
            }
            catch(Exception e) {
                MB.AvertirCritique("Une erreur s'est produite lors de l'insertion d'un nouvel article dans la base de donnée. \n\n" +
                                       "{0}\n\n", e.Message);
                return false; }
            return true;
        }
    }

    [Serializable]
    public class CommandeSQLCreerColonnesTS : CommandeCreerColonnesSQL
    {
        public CommandeSQLCreerColonnesTS(OleDbConnection p_bd) : base(p_bd)
        { }

        public override bool SQLCreerColonnes(OleDbConnection m_bd)
        {
            ArticlePhilatélique Article = null;

            Tuple<string, string>[] colonnes = {
                new Tuple<string, string>("type", "Text(80)"),
                new Tuple<string, string>("numero", "Integer"),
                new Tuple<string, string>("motif", "Text(50)"),
                new Tuple<string, string>("date_parution", "Date"),
                new Tuple<string, string>("prix_payé", "Double"),
                new Tuple<string, string>("valeur_timbre", "Double"),
                new Tuple<string, string>("obliteration", "Integer"),
                new Tuple<string, string>("taille_forme", "Text(50)"),
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

    [Serializable]
    public class CommandeModificationTS : CommandeModification
    {
        public CommandeModificationTS(TimbreSeul p_article)
            : base(p_article)
        { }

        public override DlgSaisieArticle CréerDlgSaisie(ArticlePhilatélique p_article)
            => new DlgSaisieTimbreSeul(TypeDeSaisie.Modification, p_article as TimbreSeul);
    }

    [Serializable]
    public class CommandeAjoutBC : CommandeAjout
    {
        public CommandeAjoutBC(string p_motif, DateTime p_dateParution)
            : base(p_motif, p_dateParution)
        { }

        public CommandeAjoutBC()
            : base()
        { }

        public override DlgSaisieArticle CréerDlgSaisie()
            => new DlgSaisieBlocDeCoin(TypeDeSaisie.Ajout, null);

        public override DlgSaisieArticle CréerDlgSaisieAvecDonnées(string p_motif, DateTime p_dateParution)
            => new DlgSaisieBlocDeCoin(p_motif, p_dateParution);
    }

    [Serializable]
    public class CommandeSQLLireBC : CommandeLireSQL 
    {
        public override ArticlePhilatélique SQLLireArticle(OleDbConnection m_bd, int p_numero)
        {
            ArticlePhilatélique Article = null;
            try
            {
                using (BdReader bdr = new BdReader(m_bd,
                        "SELECT numero, motif, date_parution, prix_payé, " +
                        "coin, valeur_timbre, nombre_timbres, taille_forme " +
                        "FROM Articles " +
                        "WHERE numero=?", p_numero))
                {
                    if (!bdr.Read() && !bdr.IsDBNull(1) && !bdr.IsDBNull(4) && !bdr.IsDBNull(5) &&
                         !bdr.IsDBNull(6) && !bdr.IsDBNull(7))
                        return null;

                    Article = new BlocDeCoin(
                    bdr.GetInt32(0), bdr.GetString(1), bdr.GetDateTimeOuNull(2), bdr.GetDoubleOuNull(3),
                    (Coin)bdr.GetInt32(4), bdr.GetDouble(5), bdr.GetInt32(6), bdr.GetString(7));
                }
            }
            catch { }
            return Article;
        }
    }

    [Serializable]
    public class CommandeSQLEcrireBC : CommandeEcrireSQL
    {
        public override bool SQLEcrireArticle(OleDbConnection m_bd, ArticlePhilatélique p_article)
        {
            BlocDeCoin Article = p_article as BlocDeCoin;
            try
            {
                BdNonQuery insert = new BdNonQuery(m_bd,
                "INSERT INTO Articles(type, numero, motif, date_parution, prix_payé, " +
                "coin, valeur_timbre, nombre_timbres, taille_forme ) " +
                "VALUES(?,?,?,?,? ,?,?,?,?)",
                Article.GetType().ToString(), Article.Numéro, Article.Motif, Article.Parution.Value.Date, Article.PrixPayé,
                (int)Article.Coin, Article.ValeurTimbre, Article.NbTimbres, Article.TailleEtForme);

                insert.ExecuteNonQuery();
            }
            catch { return false; }
            return true;
        }
    }

    [Serializable]
    public class CommandeSQLCreerColonnessBC : CommandeCreerColonnesSQL
    {
        public CommandeSQLCreerColonnessBC(OleDbConnection p_bd) : base(p_bd)
        { }

        public override bool SQLCreerColonnes(OleDbConnection m_bd)
        {
            ArticlePhilatélique Article = null;

            Tuple<string, string>[] colonnes = {
                new Tuple<string, string>("type", "Text(80)"),
                new Tuple<string, string>("numero", "Integer"),
                new Tuple<string, string>("motif", "Text(50)"),
                new Tuple<string, string>("date_parution", "Date"),
                new Tuple<string, string>("prix_payé", "Double"),
                new Tuple<string, string>("coin", "Integer"),
                new Tuple<string, string>("valeur_timbre", "Double"),
                new Tuple<string, string>("nombre_timbres", "Integer"),
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

    [Serializable]
    public class CommandeModificationBC : CommandeModification
    {
        public CommandeModificationBC(BlocDeCoin p_article)
            : base(p_article)
        { }

        public override DlgSaisieArticle CréerDlgSaisie(ArticlePhilatélique p_article)
            => new DlgSaisieBlocDeCoin(TypeDeSaisie.Modification, p_article as BlocDeCoin);
    }

    [Serializable]
    public class CommandeSuppressionBC : CommandeSuppression
    {
        public CommandeSuppressionBC(BlocDeCoin p_article)
            : base(p_article)
        { }

        public override bool ConfirmerSuppression(ArticlePhilatélique p_article)
        {
            return DialogResult.OK ==
                new DlgSaisieBlocDeCoin(TypeDeSaisie.Suppression, p_article as BlocDeCoin).ShowDialog();
        }
    }
}