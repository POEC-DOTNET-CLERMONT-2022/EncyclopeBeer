using System.Collections.ObjectModel;

namespace Ipme.WikiBeer.Models
{
    public interface IGenericListModel<T> where T : ObservableObject, IDeepClonable<T>
    {
        public ObservableCollection<T> List { get; set; }

        public T? Current { get; set; }

        public T? ToModify { get; set; }

    }
}
