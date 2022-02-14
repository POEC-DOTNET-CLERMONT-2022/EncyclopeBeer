using System.Collections.ObjectModel;

namespace Ipme.WikiBeer.Models
{
    public class GenericListModel<T> : ObservableObject, IGenericListModel<T>  where T : ObservableObject, IDeepClonable<T>
    {
        private ObservableCollection<T> _list;

        private T? _current;

        private T? _toModify;

        public ObservableCollection<T> List
        {
            get { return _list; }
            set
            {
                if (_list != value)
                {
                    _list = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        public T Current
        {
            get { return _current; }
            set
            {
                if (_current != value)
                {
                    _current = value;
                    OnNotifyPropertyChanged();
                    if(_current != null)
                    {
                        ToModify = Current.DeepClone();
                    }
                }
            }
        }

        public T ToModify
        {
            get
            {
                return _toModify;
            }
            set
            {
                _toModify = value;
                OnNotifyPropertyChanged();
            }
        }
    }
}

