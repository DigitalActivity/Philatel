using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Philatel
{
    /// <summary>
    /// Conserve et permet de récupérer des fabriques pour des commandes.
    /// Offre aussi une opération pour ajouter les commandes au menu du programme (ce n'est pas tellement
    /// « fabrique » alors aurait pu être dans un truc séparé, ou alors on pourrait changer le nom de la
    /// classe pour quelque chose de moins précis que LesFabriques !)
    /// </summary>
    static class LesFabriques
    {
        static Dictionary<Type, IFabriqueCommande> m_fabriques = new Dictionary<Type, IFabriqueCommande>();

        public static void Ajouter(Type p_typeArticle, IFabriqueCommande p_fabrique) 
            => m_fabriques.Add(p_typeArticle, p_fabrique);

        // Obtenir une fabrique selon le type d'article
        public static IFabriqueCommande FabriqueDe(Type p_type) 
            => m_fabriques[p_type];

        // Obtenir une fabrique selon type de fabrique
        public static IFabriqueCommande TrouverFabrique(string p_typeDeFabrique)
        {
            if (p_typeDeFabrique.Contains("Fabrique"))
                p_typeDeFabrique = p_typeDeFabrique.Remove(0, p_typeDeFabrique.IndexOf("Fabrique") + 8);
            foreach (Type fc in m_fabriques.Keys)
            {
                if(fc.ToString().Contains(p_typeDeFabrique.ToString()))
                    return m_fabriques[fc];
            }

            return null;
        }

        public static IEnumerator<IFabriqueCommande> GetEnumerator()
        {
            // garder seulement les fabriques, pas de clé
            List<IFabriqueCommande> fabriques = new List<IFabriqueCommande>();
            foreach (var f in m_fabriques)
            {
                fabriques.Add(f.Value);
            }

            return fabriques.GetEnumerator();
        }
    }

    /// <summary>
    /// Abstract Factory pour des commandes, et un peu plus (la description de la commande pour le menu).
    /// </summary>
    public interface IFabriqueCommande
    {
        string DescriptionPourMenu();
        // Commandes pour la base des données
        ICommande CréerCommandeSQLPourLireArticle();
        ICommande CréerCommandeSQLPourEcrireArticle();
        ICommande CréerCommandeSQLPourCréerColonnes(OleDbConnection m_bd);
        // COmmandes pour traiter les articles
        ICommande CréerCommandeAjouter();
        ICommande CréerCommandeAjouter(string p_motif, DateTime p_dateParution);
        ICommande CréerCommandeModifier(ArticlePhilatélique p_article);
        ICommande CréerCommandeSupprimer(ArticlePhilatélique p_article);
    }
}