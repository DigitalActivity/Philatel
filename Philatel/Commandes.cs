using System;
using System.Windows.Forms;
using System.Collections.Generic;
using UtilStJ;
using static UtilStJ.MB;
using System.Data.OleDb;

namespace Philatel
{
    /// Interface ICommande :
    /// <summary>
    /// interface de toutes les commandes. La fonction Exécuter renvoie un bool qui permet de
    /// savoir s'il y a vraiment eu opération et donc s'il faut permettre le Annuler (Undo)
    /// </summary>
    public interface ICommande
    {
        bool Exécuter();
        void Annuler();
        void Retablir();
        // ICommande Cloner();  // Pas utile dans cette version
    }

    /// Classe abstraite CommandeAjout
    /// <summary>
    /// permet l'ajout d'un article (abstraite car on doit définir CréerDlgSaisie).
    /// </summary>
    [Serializable]
    public abstract class CommandeAjout : ICommande
    {
        int m_numéroArticleAjouté;
        ArticlePhilatélique m_article;

        // Données qui peuvent etre chargées dans le dialog
        private string m_motif;
        private DateTime m_dateParution;

        /// <summary>
        /// Constructeur Commande Ajout, sans charger les données
        /// </summary>
        public CommandeAjout()
        {}

        /// <summary>
        /// Constructeur Commande Ajout, avec charger les données
        /// </summary>
        /// <param name="p_motif"></param>
        /// <param name="p_dateParution"></param>
        public CommandeAjout(string p_motif, DateTime p_dateParution)
        {
            m_motif = p_motif;
            m_dateParution = p_dateParution;
        }

        public bool Exécuter() // Template Method (appelle une Factory Method)
        {
            DlgSaisieArticle d;

            if (m_motif != null)
                d = CréerDlgSaisieAvecDonnées(m_motif, m_dateParution);
            else
                d = CréerDlgSaisie();

            if (d.ShowDialog() == DialogResult.Cancel)
                return false;

            ArticlePhilatélique article = d.Extraire();
            m_numéroArticleAjouté = article.Numéro;
            m_article = article;
            Document.Instance.Ajouter(article);
            return true;
        }

        public void Annuler()
            => Document.Instance.RetirerArticle(m_numéroArticleAjouté);

        public void Retablir()
        {
            Document.Instance.Ajouter(m_article);
        }

        public abstract DlgSaisieArticle CréerDlgSaisie();  // Factory Method
        public abstract DlgSaisieArticle CréerDlgSaisieAvecDonnées(string p_motif, DateTime p_dateParution);
    }

    /// Classe abstraite CommandeModification
    /// <summary>
    /// permet la modification d'un article (abstraite car on doit définir CréerDlgSaisie).
    /// </summary>
    [Serializable]
    public abstract class CommandeModification : ICommande
    {
        public CommandeModification(ArticlePhilatélique p_articleCourant)
        {
            m_article = p_articleCourant;
        }

        ArticlePhilatélique m_article;
        ArticlePhilatélique m_articleSave;

        public bool Exécuter() // Template Method (appelle une Factory Method)
        {
            DlgSaisieArticle d = CréerDlgSaisie(m_article);

            if (d.ShowDialog() == DialogResult.Cancel)
                return false;

            Document.Instance.RetirerArticle(m_article.Numéro);
            Document.Instance.Ajouter(d.Extraire());
            return true;
        }

        public void Annuler()
        {
            m_articleSave = Document.Instance.ArticleSelonNuméro(m_article.Numéro);
            Document.Instance.RetirerArticle(m_article.Numéro);
            Document.Instance.Ajouter(m_article);
        }

        public void Retablir()
        {
            Document.Instance.RetirerArticle(m_article.Numéro);
            Document.Instance.Ajouter(m_articleSave);
        }

        public abstract DlgSaisieArticle CréerDlgSaisie(ArticlePhilatélique p_article);  // Factory Method
    }

    /// Classe CommandeSuppression :
    /// <summary>
    /// permet la suppression d'un article (pas abstraite mais on peut en dériver si on n'aime pas la façon
    /// dont est fait la confirmation dans la fonction ConfirmerSuppression de base).
    /// </summary>
    [Serializable]
    public class CommandeSuppression : ICommande
    {
        public CommandeSuppression(ArticlePhilatélique p_articleCourant)
        {
            m_article = p_articleCourant;
        }

        ArticlePhilatélique m_article;

        public bool Exécuter()  // Template Method (ConfirmerSuppression peut être supplantée)
        {
            if (!ConfirmerSuppression(m_article))
                return false;

            Document.Instance.RetirerArticle(m_article.Numéro);
            return true;
        }

        public void Annuler()
            => Document.Instance.Ajouter(m_article);

        public void Retablir()
            => Document.Instance.RetirerArticle(m_article.Numéro);

        public virtual bool ConfirmerSuppression(ArticlePhilatélique p_article)
            => ConfirmerOuiNon("Êtes-vous sûr de vouloir retirer\n\n" +
                                   p_article.ToString() +
                                   "\n\n?");
    }

    /// Lire un article à partir de la base donnée
    /// <summary>
    /// Lire un article avec le bon code sql, ne prendre en consideration que les champs connus de ce type
    /// </summary>
    [Serializable]
    public abstract class CommandeLireSQL : ICommande
    {
        int m_numéroArticle;
        ArticlePhilatélique m_article;

        public bool Exécuter()
            => false;

        public void Annuler()
        { }

        public void Retablir()
        { }

        public ArticlePhilatélique ExecuterSQLLireArticle(OleDbConnection p_bd, int p_numeroArticle)
            => SQLLireArticle(p_bd, p_numeroArticle);

        public abstract ArticlePhilatélique SQLLireArticle(OleDbConnection p_bd, int p_numeroArticle);
    }

    /// Enregistrer un article dans de la base donnée
    /// <summary>
    /// ecrire un article avec le bon code sql
    /// </summary>
    [Serializable]
    public abstract class CommandeEcrireSQL : ICommande
    {
        int m_numéroArticle;
        ArticlePhilatélique m_article;

        public bool Exécuter()
            => false;

        public void Annuler()
        { }

        public void Retablir()
        { }

        public bool ExecuterSQLEnregistrerArticle(OleDbConnection p_bd, ArticlePhilatélique p_article)
            => SQLEcrireArticle(p_bd, p_article);

        public abstract bool SQLEcrireArticle(OleDbConnection p_bd, ArticlePhilatélique p_article);
    }

    /// Lire un article à partir de la base donnée
    /// <summary>
    /// Lire un article avec le bon code sql, ne prendre en consideration que les champs connus de ce type
    /// </summary>
    [Serializable]
    public abstract class CommandeCreerColonnesSQL : ICommande
    {
        OleDbConnection m_bd;

        public CommandeCreerColonnesSQL(OleDbConnection p_bd)
        {
            m_bd = p_bd;
        }

        int m_numéroArticle;
        ArticlePhilatélique m_article;

        public bool Exécuter()
            => SQLCreerColonnes(m_bd);

        public void Annuler()
        { }

        public void Retablir()
        { }

        public abstract bool SQLCreerColonnes(OleDbConnection p_bd);
    }

    /// <summary>
    /// Permet la suppression de l'entièreté de la liste d'articles philatéliques,
    /// suite à une confirmation de l'utilisateur.
    /// </summary>
    [Serializable]
    public class CommandeEffacerTout : ICommande
    {
        List<ArticlePhilatélique> m_tousLesArticles = new List<ArticlePhilatélique>();
        public CommandeEffacerTout()
        {
            m_tousLesArticles.AddRange(Document.Instance.TousLesArticles());
        }


        public void Annuler()
        {
            foreach (ArticlePhilatélique article in m_tousLesArticles)
            {
                Document.Instance.Ajouter(article);
            }
        }
        
        public bool Exécuter()
        {
            if (!ConfirmerOuiNon("Êtes-vous sûr de vouloir supprimer la totalité des articles de la liste?"))
                return false;

            Document.Instance.RetirerTousLesArticles();
            return true;
        }

        public void Retablir()
        {
            Document.Instance.RetirerTousLesArticles();
        }
    }
}
