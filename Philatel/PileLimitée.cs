using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philatel
{
    [Serializable]
    public class PileLimitée<T>
    {
        const int TAILLE_PAR_DEFAULT = 100;
        private T[] m_list_elements;
        private int m_capacité;
        private int m_tête;
        private int m_nbElements;

        /// <summary>
        /// Constructeur par default
        /// </summary>
        public PileLimitée() : this(TAILLE_PAR_DEFAULT)
        {}

        /// <summary>
        /// Constructeur avec capacité en parametre
        /// </summary>
        /// <param name="p_capacité">Capacité de la pile</param>
        public PileLimitée (int p_capacité) 
        {
            if (p_capacité <= 0) throw new ArgumentException("Capacité doit être positive");
            m_capacité = p_capacité;
            m_list_elements = new T[m_capacité];
            m_nbElements = 0;
            m_tête = m_capacité - 1;
        }

        /// <summary>
        /// Nombre d'éléments dans la pile
        /// </summary>
        /// <returns>size</returns>
        public int NbÉléments()   // donne le nombre d'éléments
            => m_nbElements;

        /// <summary>
        /// Capacité de la pile
        /// </summary>
        /// <returns>capacity</returns>
        public int Capacité()     // donne la capacité de la pile
            => m_list_elements.Length;

        /// <summary>
        /// Est pleine
        /// </summary>
        /// <returns>true quand pile pleinn, false sinon</returns>
        public bool Pleine()      // si NbÉléments == Capacité
            => m_nbElements == m_capacité;

        /// <summary>
        /// Est vide
        /// </summary>
        /// <returns>true quand la pile est vide</returns>
        public bool Vide()     // si NbÉléments == 0
            => m_nbElements == 0;

        /// <summary>
        /// Empiler
        /// </summary>
        /// <param name="p_element"></param>
        /// <returns>false si element perdue à cause de la nouvelle insertion</returns>
        public bool Empiler(T p_element)  // renvoie false si on a perdu une valeur à cause de la nouvelle à conserver.
        {
            bool aucuneValPerdue; // false si on a perdu une valeur
            m_tête = (m_tête + 1) % m_capacité;
            m_list_elements[m_tête] = p_element;

            if (m_nbElements == m_capacité)
            {
                aucuneValPerdue = false;
            }
            else
            {
                ++m_nbElements;
                aucuneValPerdue = true;
            }

            return aucuneValPerdue;
        }

        /// <summary>
        /// Dépiler
        /// </summary>
        /// <returns>Element en top</returns>
        public T Dépiler()      // renvoie la valeur empilée la plus récente.Lève une exception si pile vide.
        {
            T element;
            if (m_nbElements == 0)
                throw new ArgumentNullException("Dépiler", "Pile vide");

            element = m_list_elements[m_tête];
            m_tête = (m_tête - 1) % Capacité() == -1 ? Capacité() - 1 : (m_tête - 1) % Capacité();
            --m_nbElements;
            return element;
        }

        /// <summary>
        /// Vider
        /// </summary>
        public void Vider()     // ne devrait que remettre le NbÉléments à 0...
        {
            m_list_elements = new T[m_capacité];
            m_tête = Capacité() - 1;
        }
    }
}
