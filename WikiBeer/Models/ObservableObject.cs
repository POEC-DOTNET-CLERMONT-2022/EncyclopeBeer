using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ipme.WikiBeer.Models
{
    public class ObservableObject : INotifyPropertyChanged
    {
        // PropertyChangedEventHandler est un type particulier de EventHandler
        public event PropertyChangedEventHandler? PropertyChanged; // évènement

        // le = "" est nécessaire pour tuilser le OnNotifyPropertyChanged()
        // [on a forcément besoin d'une valeur par défaut.]
        protected virtual void OnNotifyPropertyChanged([CallerMemberName] string propertyname = "") 
        {
            if (PropertyChanged != null)
            {
                // this est le sender, et le reste est l'event args (les arguments de l'objet)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
