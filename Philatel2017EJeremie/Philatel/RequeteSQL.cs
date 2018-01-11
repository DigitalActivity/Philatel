using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Xml.Serialization;
using UtilStJ;

namespace Philatel
{
    public static class RequeteSQL
    {
        public static OleDbConnection m_bd { get; private set; }

        /// <summary>
        /// Connecter à la base des données
        /// </summary>
        /// <param name="p_strAccessConnection"></param>
        public static void Connecterbd(string p_strAccessConnection)
        {
            try
            {
                m_bd = new OleDbConnection(p_strAccessConnection);
                m_bd.Open();
            }
            catch (Exception e)
            {
                MB.AvertirCritique("La base de données n'est pas accessible. « {0} »\n\n" +
                                   "{1}\n\n" +
                                   "Le programme va s'arrêter.\n",
                                   p_strAccessConnection, e.Message);

                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Ajouter un Article à la base de donnée
        /// </summary>
        /// <param name="p_article"></param>
        /// <returns>true quand operation reussit, false sinon</returns>
        public static bool AjouterArticle(ArticlePhilatélique p_article)
        {
            // Obtenir la bonne fabrique
            IFabriqueCommande f = LesFabriques.FabriqueDe(p_article.GetType());
            CommandeEcrireSQL cmdEcrireArticle = (CommandeEcrireSQL)f.CréerCommandeSQLPourEcrireArticle();
            // Executer le sql correspondant à l'article
            try
            {
                return cmdEcrireArticle.ExecuterSQLEnregistrerArticle(m_bd, p_article);
            }
            catch (Exception e)
            {
                MB.AvertirCritique("Article ne peut etre ajouté à la base des données. « {0} ».",
                                   e.Message);
                return false;
            }
        }

        /// <summary>
        /// Supprimer un article
        /// </summary>
        /// <param name="p_Production">Numéro de production à supprimer</param>
        public static void SupprimerArticle(int p_numArticle)
        {
            BdNonQuery delete = new BdNonQuery(m_bd,
                "DELETE FROM Articles " +
                "WHERE numero=?",
                p_numArticle);
            try
            {
                delete.ExecuteNonQuery();
            }
            catch { }
        }

        /// <summary>
        /// Modifier un article dans la base des données
        /// </summary>
        /// <param name="p_article"></param>
        public static void ModifierArticle(ArticlePhilatélique p_article)
        {
            try
            {
                SupprimerArticle(p_article.Numéro);
                AjouterArticle(p_article);
            }
            catch { }
        }

        /// <summary>
        /// Obtenir le nombre d'articles dans la base des données
        /// </summary>
        /// <returns></returns>
        public static int NbArticles()
            => BdScalar.Get<int>(m_bd, "SELECT COUNT(numero) FROM Articles");

        /// <summary>
        /// Renvoie tous les artiles.
        /// </summary>
        /// <returns>Toutes les productions.</returns>
        public static IEnumerable<ArticlePhilatélique> ToutesLesArticles()
        {
            using (BdReader bdr = new BdReader(m_bd,
                "SELECT type, numero " +
                "FROM Articles "))
            {
                while (bdr.Read())
                {
                    // Obtenir la bonne fabrique
                    IFabriqueCommande f = LesFabriques.TrouverFabrique(bdr.GetString(0));
                    // Obtenir la commande sql du type à chercher
                    CommandeLireSQL cmdLireArticle = (CommandeLireSQL)f.CréerCommandeSQLPourLireArticle();
                    // Obtenir l'article avec la bonne commande sql
                    ArticlePhilatélique article = cmdLireArticle.ExecuterSQLLireArticle(m_bd, bdr.GetInt32(1));
                    yield return article;
                }
            }
        }

        /// <summary>
        /// Inserer Colonnes Si Non Existantes à une table dans la base des données
        /// </summary>
        /// <param name="p_nomTable">table à alterer</param>
        /// <param name="p_colonnes">table de colonnes à ajouter (nom, type)</param>
        public static void InsererColonnesSiNonExistantes(string p_nomTable, Tuple<string, string>[] p_colonnes)
        {
            foreach (Tuple<string, string> n in p_colonnes)
            {
                try
                {
                    if (!ColonneExiste(n.Item1))
                    {
                        BdNonQuery ajouterColonne = new BdNonQuery(m_bd,
                        "ALTER TABLE " + p_nomTable + " ADD " + n.Item1 + " " + n.Item2 + ";");

                        ajouterColonne.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    MB.AvertirCritique("Une erreur s'est produite lors de l'insertion d'une nouvelle colonne. \n\n" +
                                       "{2}\n\n" +
                                       "« {0} {1}».\n",
                                       n.Item1, n.Item2, e.Message);
                }
            }
        }

        /// <summary>
        /// Verifier si une colonne existe
        /// </summary>
        /// <param name="p_nomColonne">nom de la colonne à chercher</param>
        /// <returns></returns>
        private static bool ColonneExiste(string p_nomColonne)
        {
            DataTable schema = m_bd.GetSchema("COLUMNS");
            DataRow[] c = schema.Select("TABLE_NAME='Articles' AND COLUMN_NAME='" + p_nomColonne + "'");
            return c.Length != 0;
        }

        /// <summary>
        /// Retourne toutes les colonnes de la table
        /// </summary>
        /// <param name="p_nomColonne">nom de la colonne à chercher</param>
        /// <returns></returns>
        public static DataRow[] ColonnesDeLaTable(string p_nomTable)
        {
            DataTable schema = m_bd.GetSchema("COLUMNS");
            DataRow[] c = schema.Select("TABLE_NAME='" + p_nomTable + "'");
            return c;
        }

        /// <summary>
        /// Retire tous les articles de la Base de Données.
        /// </summary>
        public static void RetirerTousLesArticles()
        {
            BdNonQuery supprimerTout = new BdNonQuery(m_bd,
                "DELETE * FROM Articles");
            supprimerTout.ExecuteNonQuery();
        }

        public static ArticlePhilatélique ArticleSelonNumero(int p_numero)
        {
            string type = BdScalar.Get<string>(m_bd,
               "SELECT type " +
                "FROM Articles " +
                "WHERE numero=?", p_numero);

            // Obtenir la bonne fabrique
            IFabriqueCommande f = LesFabriques.TrouverFabrique(type);
            // Obtenir la commande sql du type à chercher
            CommandeLireSQL cmdLireArticle = (CommandeLireSQL)f.CréerCommandeSQLPourLireArticle();
            // Obtenir l'article avec la bonne commande sql
            ArticlePhilatélique article = cmdLireArticle.ExecuterSQLLireArticle(m_bd, p_numero);
            return article;
        }

    } // FIN RequeteSQL
} // FIN namespace
