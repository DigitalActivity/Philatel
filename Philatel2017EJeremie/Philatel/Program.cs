using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;

namespace Philatel
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // « LesFabriques » est une classe qui gère les fabriques (Abstract Factory) de commandes (surtout).
            // Chaque type de produit philatélique aura sa fabrique pour créer les diverses commandes
            // nécessaires pour ce type (ainsi que la description du type de produit). Il faut une clef pour
            // identifier la fabrique nécessaire, et on veut pouvoir obtenir cette clef à partir d'un article
            // philathélique. Au lieu de créer une méthode virtuelle pour donner le nom du type, ou un autre
            // code, on se sert du System.Type des classes, accessible en général par typeof(), mais aussi à
            // partir d'une donnée par la méthode GetType() (qui renvoie le vrai Type de l'instance et non de
            // la référence elle-même, ce que ferait typeof() dans ce contexte).
            LesFabriques.Ajouter(typeof(TimbreSeul), FabriqueTimbreSeul.Instance);
            LesFabriques.Ajouter(typeof(BlocDeCoin), FabriqueBlocDeCoin.Instance);
            // N.B. Si on voulait rendre le programme plus clair pour les humains, on pourrait utiliser la
            //      propriété .FullName sur les valeurs de type Type : on pourrait ainsi avoir un Dictionary
            //      dont la clef serait une string au lieu d'une valeur de type Type. Mais ce serait juste
            //      une opération de plus, pas plus utile en général...

            // Si on ajoute des classes dérivées de ArticlePhilatélique, il suffit de mettre les fichiers
            // dans le projet et d'ajouter un instruction pour ajouter la fabrique du nouveau type.
            // LesFabriques.Ajouter(typeof(PliPremierJourOfficiel_), FabriquePliPremierJourOfficiel_.Instance);

            // Ou encore, pour n'avoir rien du tout à changer au programme, on peut faire des dll séparés qui
            // seront chargés ici automatiquement :
            var lesDll = System.IO.Directory.GetFiles(@"..\..\..", "AP-*.dll");

            foreach (var nomDLL in lesDll)
            {
                var dll = Assembly.LoadFrom(nomDLL);

                // On regarde s'il y a un type dérivé de ArticlePhilatélique
                Type typePourArticle = dll.GetExportedTypes()
                    .FirstOrDefault(t => t.BaseType == typeof(ArticlePhilatélique));

                // Si oui, on cherchera une fabrique
                if (typePourArticle != null)
                {
                    // On cherche une fabrique (classe qui implémente IFabriqueCommande)
                    Type typePourFabrique = dll.GetExportedTypes()
                        .FirstOrDefault(t => t.GetInterface("IFabriqueCommande") != null);

                    // Si on a un type et une fabrique, on va ajouter ça à LesFabriques
                    if (typePourFabrique != null)
                    {
                        // On cherche la propriété pour créer le singleton
                        var propriétéInstance = typePourFabrique.GetProperty("InstanceFabrique");

                        if (propriétéInstance != null)  // Ça devrait !
                        {
                            // On exécute la propriété pour créer le singleton
                            var fabrique = (IFabriqueCommande)propriétéInstance.GetValue(null);
                            LesFabriques.Ajouter(typePourArticle, fabrique);
                        }
                    }
                }

                // Bonne référence avec exemples simples pour la réflexion :
                // http://www.csharp-examples.net/reflection-examples/
            }

            Application.Run(new FormPrincipal());
        }
    }
}

