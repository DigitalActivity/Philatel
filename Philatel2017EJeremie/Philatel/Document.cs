using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UtilStJ;
using static UtilStJ.MB;

namespace Philatel
{
    public class Document : DocumentObservable<Document>
    {
        const string NomFichierPhilatélie = "Philatélie.données";
        const int NoPremierArticle = 1;
        public const string NomFichierBD = "../../../DatabasePhilatelique.accdb";
        string strAccessConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + NomFichierBD;
        static Lazy<Document> m_docUnique = new Lazy<Document>(() => new Document());
        public static Document Instance => m_docUnique.Value;

        // Les valeurs sérialisées/désérialisées :
        List<ArticlePhilatélique> m_articles;
        int m_noProchainArticle;
        bool m_docModifié = false;

        // TP1 stack remplacé par PileLimitée
        public PileLimitée<ICommande> m_commandesAnnulables = new PileLimitée<ICommande>(5);   // Commandes pour le Annuler/Ctrl+Z
        public PileLimitée<ICommande> m_commandesRétablissbles = new PileLimitée<ICommande>(5); // Commandes pour le Retablir/Ctrl+Y

        private Document()
        {
            try
            {
                // Recuperer le document
                Récupérer();
                try
                {
                    // Connecter la base des données
                    RequeteSQL.Connecterbd(strAccessConn);
                    // Chaque fabrique crée les colonnes du type d'article associé dans la base de donnée 
                    IEnumerator<IFabriqueCommande> fab = LesFabriques.GetEnumerator();
                    while (fab.MoveNext())
                        fab.Current.CréerCommandeSQLPourCréerColonnes(RequeteSQL.m_bd).Exécuter();
                }
                catch
                {
                    AvertirCritique("La lecture de « {0} » a échoué.\n" +
                                    "Le programme va continuer sans utiliser la base des données.\n" +
                                    "Les données sont toujours sauvegardées dans le document « {1} »",
                                    NomFichierBD, NomFichierPhilatélie);
                }
            }
            catch
            {
                AvertirCritique("La lecture de « {0} » a échoué.\n" +
                                "Le programme va s'arrêter.\n",
                                NomFichierPhilatélie);

                Environment.Exit(0);  // Permet d'arrêter le programme directement.
            }
        }

        /// <summary>
        /// Termine l'accès aux données et s'assure qu'elles sont bien enregistrées (un message est affichée
        /// si ce n'est pas le cas).
        /// </summary>
        public void Fermer()
        {
            // Deconnecter a la base de données
            RequeteSQL.m_bd.Close();

            if (m_docModifié)
            {
                try
                {
                    Enregistrer();
                }
                catch
                {
                    AvertirCritique(
                        "***** ERREUR *****\n" +
                        "Les données n'ont pas pu être enregistrées dans {0}.",
                        NomFichierPhilatélie);
                }
            }
        }

        /// <summary>
        /// Permet de retrouver un type (sous forme System.Type) dans un assemblage qui est
        /// peut-être autre que l'assemblage courant. Le lien est fait simplement par le nom de
        /// l'assemblage et non par sa version (etc.). 
        /// N.B. On dirait que c'est ce que devrait faire par défaut la (dé)sérialisation, mais
        ///      non, elle ne prend pas de chances si on ne l'aide pas...
        /// </summary>
        class LierAssemblagesSimplement : SerializationBinder
        {
            /// <summary>
            /// La désérialisation appelle cette fonction en donnant le nom (complet, avec version,
            /// etc.) de l'assemblage qu'elle pense responsable pour le type dont le nom est fourni
            /// par p_nomType. Elle retrouve l'assemblage (en considérant seulement le nom), puis
            /// elle y cherche et renvoie le System.Type du type recherché.
            /// </summary>
            /// <param name="p_nomAssemblageEtc">sous la forme "nom, Version=1.0.0.1, ...", on
            /// s'intéressera seulement à ce qui vient avant la première virgule</param>
            /// <param name="p_nomType">sous la forme "nomDuNamespace.nom"</param>
            /// <returns>le descripteur de type ou null si non trouvé</returns>
            public override Type BindToType(string p_nomAssemblageEtc, string p_nomType)
            {
                string nomBase = p_nomAssemblageEtc.Substring(0, p_nomAssemblageEtc.IndexOf(','));

                var assemblage = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(a => a.FullName.StartsWith(nomBase + ","));

                return assemblage?.GetType(p_nomType);
            }
        }

        // Les deux opérations qui suivent pourraient peut-être être offertes publiquement... (seulement si
        // on veut bien laisser comprendre à l'utilisateur qu'on n'utilise pas de base de données)
        void Récupérer()
        {
            try
            {
                using (var ficArticles = File.OpenRead(NomFichierPhilatélie))
                {
                    var formateur = new BinaryFormatter();
                    formateur.Binder = new LierAssemblagesSimplement();
                    m_articles = (List<ArticlePhilatélique>)formateur.Deserialize(ficArticles);
                    m_noProchainArticle = (int)formateur.Deserialize(ficArticles);
                    m_commandesAnnulables = (PileLimitée<ICommande>)formateur.Deserialize(ficArticles);
                }
            }
            catch (FileNotFoundException)
            {
                m_articles = new List<ArticlePhilatélique>();
                m_noProchainArticle = NoPremierArticle;
            }
        }

        void Enregistrer()
        {
            using (var ficArticles = File.Create(NomFichierPhilatélie))
            {
                var formateur = new BinaryFormatter();
                formateur.Serialize(ficArticles, m_articles);
                formateur.Serialize(ficArticles, m_noProchainArticle);
                formateur.Serialize(ficArticles, m_commandesAnnulables);
            }
        }

        /*

        /// <summary>
        /// Renvoie un accès (en lecture seule) à tous les articles (sans ordre particulier).
        /// </summary>
        /// <returns>un accès non modifiable à tous les articles (sans ordre particulier)</returns>
        public IEnumerable<ArticlePhilatélique> TousLesArticles()
            => m_articles;
        // Ou encore (mais ça ne donnerait rien de plus ici) :
        //    foreach (var article in m_articles) 
        //        yield return article;

        */

        /// <summary>
        /// Renvoie un numéro d'article pas encore utilisé.
        /// </summary>
        /// <returns>un numéro d'article pas encore utilisé</returns>
        public int NuméroNouvelArticle()
            => m_noProchainArticle++;

        /*
        
        /// <summary>
        /// Renvoie l'article demandé (par son numéro) ou null s'il n'existe pas.
        /// </summary>
        /// <param name="p_numéro">le numéro de l'article désiré</param>
        /// <returns>l'article dont on a fourni le numéro ou null s'il n'existe pas</returns>
        public ArticlePhilatélique ArticleSelonNuméro(int p_numéro)
            => m_articles.Find(a => a.Numéro == p_numéro);

        /// <summary>
        /// Ajoute l'article (les observateurs sont ensuite notifiés).
        /// </summary>
        /// <param name="p_article">l'article à ajouter</param>
        public void Ajouter(ArticlePhilatélique p_article)
        {
            m_articles.Add(p_article);
            m_docModifié = true;
            RequeteSQL.AjouterArticle(p_article);
            Notifier(this);
        }

        /// <summary>
        /// Inscrit la nouvelle version de l'article (les observateurs sont ensuite notifiés).
        /// (un article portant le même numéro doit déjà exister)
        /// </summary>
        /// <param name="p_article">l'article pour remplacer l'ancien</param>
        public void Modifier(ArticlePhilatélique p_article)
        {
            int indiceArticle = m_articles.FindIndex(a => a.Numéro == p_article.Numéro);
            Debug.Assert(indiceArticle != -1);
            m_articles[indiceArticle] = p_article;
            m_docModifié = true;
            RequeteSQL.ModifierArticle(p_article);
            Notifier(this);
        }

        /// <summary>
        /// Retire un article des articles conservés.
        /// </summary>
        /// <param name="p_noArticle">le numéro de l'article à retirer</param>
        /// <returns>true si l'article existait et a été retiré, false sinon</returns>
        public bool RetirerArticle(int p_noArticle)
        {
            int indiceArticle = m_articles.FindIndex(a => a.Numéro == p_noArticle);
            if (indiceArticle == -1)
                return false;

            RequeteSQL.SupprimerArticle(p_noArticle);
            m_articles.RemoveAtEnOrdre(indiceArticle);
            m_docModifié = true;
            Notifier(this);
            return true;
        }

        /// <summary>
        /// Retire tous les articles de la liste.
        /// </summary>
        public void RetirerTousLesArticles()
        {
            m_articles.Clear();
            m_docModifié = true;
            Notifier(this);
        }

    */
    }
}
