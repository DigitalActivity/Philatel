using System;
using System.Collections;
using System.Collections.Generic;

// DES INTERFACES POUR UNE IMPLÉMENTATION DU MODÈLE OBSERVATEUR SONT MAINTENANT DANS .NET (>= 4.0) :
// http://msdn.microsoft.com/en-us/library/dd990377.aspx
// Par conséquent, j'ai laissé tombé mes propres interfaces et classes...

namespace UtilStJ
{
    // Type de délégué DChangement<T>
    /// <summary>
    /// Type pour les méthodes qui seront appelées lors de la notification du modèle observateur
    /// offert par DocumentObservable&lt;T> (un IObservable&lt;T>), mais qui ne veulent que le
    /// pull (elles recevront seulement la donnée qui devrait être le document).
    /// </summary>
    /// <typeparam name="T">Le type des données qui seront envoyées par le document lors des
    /// notifications : ici ce sera normalement le document justement.</typeparam>
    /// <param name="doc">Le document qui envoie la notification.</param>
    public delegate void DChangement<T>(T doc);

    // Type de délégué DChangementPushPull<T>
    /// <summary>
    /// Type pour les méthodes qui seront appelées lors de la notification du modèle observateur
    /// offert par DocumentObservable&lt;T> (un IObservable&lt;T>). Ce type de délégué reçoit le
    /// document et les données de notification, permettant ainsi de faire du modèle push et pull
    /// (contrairement à ce qui est offert par l'interface System.IObservable&lt;T> de base, qui
    /// est exclusivement push, en principe).
    /// </summary>
    /// <typeparam name="T">Le type des données qui seront envoyées par le document lors des
    /// notifications.</typeparam>
    /// <param name="doc">Le document qui envoie la notification.</param>
    /// <param name="info">La donnée envoyée par la notification.</param>
    public delegate void DChangementPushPull<T>(DocumentObservable<T> doc, T info);

    // Classe générique DocumentObservable<T>
    /// <summary>
    /// Classe générique dont devrait dériver les classes de documents pour être le sujet dans une
    /// implémentation du modèle observateur (les observateurs seront des System.IObserver&lt;T>).
    /// En plus d'implémenter la façon standard .NET, cette classe offre une notification directe
    /// par un événement Changement qui implémente directement le modèle pull en plus du push.
    /// Cette implémentation de base n'appelle jamais directement OnError ni OnCompleted sur ses
    /// observateurs...
    /// </summary>
    /// <typeparam name="T">Le type des données qui seront envoyées par le document lors des
    /// notifications. Pour implémenter un modèle pull, on peut utiliser CRTP et utiliser la
    /// classe de document pour ce type (class Document : DocumentObservable&lt;ClDocument>).
    /// Sinon on peut utiliser un type spécifique ou simplement object.
    /// </typeparam>
    public abstract class DocumentObservable<T> : IObservable<T>
    {
        // Événement Changement
        /// <summary>
        /// Événement permettant l'inscription à des notifications de type pull lors des changements.
        /// </summary>
        public event DChangement<T> Changement;

        // Événement ChangementPull
        /// <summary>
        /// Événement permettant l'inscription à des notifications de type push/pull lors des changements.
        /// </summary>
        public event DChangementPushPull<T> ChangementPushPull;

        // Méthode DocumentObservable<T>.Subscribe
        /// <summary>
        /// Implémentation de IObservable&lt;T>.Subscribe permettant l'inscription d'observateurs
        /// à la liste des observateurs à notifier.
        /// </summary>
        /// <param name="observer">L'observateur à ajouter à la liste des observateurs.</param>
        /// <returns>Un objet permettant la désinscription automatique ou manuelle.</returns>
        public IDisposable Subscribe(IObserver<T> observer)
        {
            m_observateurs.Add(observer);
            return new Désinscription<T>(this, observer);
        }

        // Méthode DocumentObservable<T>.Unsubscribe
        /// <summary>
        /// Méthode permettant la désinscription d'un observateur. N.B. Nom anglais pour
        /// uniformiser seulement, car cette méthode n'est pas dans IObservable&lt;T>. En
        /// principe, c'est Dispose() qui sera appelé, car c'est seulement un IDisposable qui
        /// est renvoyé par Subscribe.
        /// </summary>
        /// <param name="observer">L'observateur à retire de la liste des observateurs.</param>
        /// <returns>Un objet permettant la désinscription automatique ou manuelle.</returns>
        public void Unsubscribe(IObserver<T> observer)
        {
            m_observateurs.Remove(observer);
        }

        // Méthode DocumentObservable<T>.Notifier
        /// <summary>
        /// Doit être appelée par la classe de document dérivée pour notifier tous les observateurs.
        /// Pour le modèle pull, le paramètre sera normalement le document.
        /// </summary>
        /// <param name="p_info">L'information à communiquer aux observateurs (sera normalement le
        /// document en modèle pull).</param>
        protected void Notifier(T p_info)
        {
            Changement?.Invoke(p_info);
            ChangementPushPull?.Invoke(this, p_info);

            foreach (IObserver<T> obs in m_observateurs)
                obs.OnNext(p_info);
        }

        System.Collections.Generic.List<IObserver<T>> m_observateurs = new List<IObserver<T>>();
    }

    /// <summary>
    /// Classe de désinscription pour les observateurs. Parce qu'elle est IDisposable, la
    /// désinscription pourrait être automatisée par un using (...), par l'appel à Dispose,
    /// ou on peut appeler directement Désinscrire sur l'objet.
    /// </summary>
    /// <typeparam name="T">Le type des données envoyées par le document lors des notifications.
    /// </typeparam>
    public class Désinscription<T> : IDisposable
    {
        internal Désinscription(DocumentObservable<T> p_document, IObserver<T> p_observateur)
        {
            m_document = p_document;
            m_observateur = p_observateur;
        }

        DocumentObservable<T> m_document;
        IObserver<T> m_observateur;

        // Méthode Désinscription<T>.Désinscrire
        /// <summary>
        /// Désinscrit l'observateur du document, puis annule les références utilisées pour
        /// simplifier le travail du GC. Voir aussi Dispose().
        /// </summary>
        public void Désinscrire()
        {
            if (m_document != null)
                m_document.Unsubscribe(m_observateur);

            m_document = null;
            m_observateur = null;
        }

        // Méthode Désinscription<T>.Dispose
        /// <summary>
        /// Implémentation de la méthode de l'interface IDisposable. S'occupe de faire la
        /// désinscription, exactement comme la méthode Désinscrire.
        /// </summary>
        public void Dispose()
        {
            Désinscrire();
        }

        /* On pourrait penser qu'il serait logique de faire aussi :
        ~Désinscription()
        {
            Dispose();
        }
         * mais ça obligerait à conserver l'objet de déconstruction dans l'observateur même s'il
         * n'a pas l'intention de se désinscrire un jour (car le GC pourrait appeler le 
         * finaliser/destructeur n'importe quand et faire la désinscription).
         */
    }
}
