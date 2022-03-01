using System.Collections.ObjectModel;

namespace Ipme.WikiBeer.Models
{
    public class UserModel : ObservableObject, IDeepClonable<UserModel>
    {
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private string _nickname;
        public string Nickname
        {
            get { return _nickname; }
            set
            {
                if (_nickname != value)
                {
                    _nickname = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (_birthDate != value)
                {
                    _birthDate = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private int _hashCode;
        public int HashCode
        {
            get { return _hashCode; }
            set
            {
                if (_hashCode != value)
                {
                    _hashCode = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private bool _isCertified;
        public bool IsCertified
        {
            get { return _isCertified; }
            set
            {
                if (_isCertified != value)
                {
                    _isCertified = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private ConnectionInfosModel _connectionInfos;
        public ConnectionInfosModel? ConnectionInfos
        {
            get { return _connectionInfos; }
            set
            {
                if (_connectionInfos != value)
                {
                    _connectionInfos = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private CountryModel? _country;
        public CountryModel? Country
        {
            get { return _country; }
            set
            {
                if (_country != value)
                {
                    _country = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<Guid> _favoriteBeerIds;
        public ObservableCollection<Guid> FavoriteBeerIds
        {
            get { return _favoriteBeerIds; }
            set
            {
                if (_favoriteBeerIds != value)
                {
                    _favoriteBeerIds = value;
                    OnNotifyPropertyChanged();
                }
            }
        }
        public UserModel()
        {
        }

        public UserModel(Guid id, string nickname, DateTime birthDate, bool isCertified, ConnectionInfosModel connectionInfos, CountryModel? country, ObservableCollection<Guid> favoriteBeerIds)
        {
            Id = id;
            Nickname = nickname ?? String.Empty;
            BirthDate = birthDate;        
            IsCertified = isCertified;
            ConnectionInfos = connectionInfos;
            Country = country;
            FavoriteBeerIds = favoriteBeerIds ?? new ObservableCollection<Guid>();
        }

        public UserModel(string nickname, DateTime birthDate, bool isCertified, 
            ConnectionInfosModel connectionInfos, CountryModel? country, ObservableCollection<Guid> favoriteBeerIds)
            : this(Guid.Empty, nickname, birthDate, isCertified, connectionInfos, country, favoriteBeerIds)
        {
        }

        private UserModel(UserModel user)
        {
            Id = user.Id;
            Nickname = user.Nickname;
            BirthDate = user.BirthDate;            
            IsCertified = user.IsCertified;
            ConnectionInfos = user.ConnectionInfos?.DeepClone();
            Country = user.Country?.DeepClone();
            FavoriteBeerIds = new ObservableCollection<Guid>();
            foreach (var beerId in user.FavoriteBeerIds)
            {
                FavoriteBeerIds.Add(beerId); 
            }
        }

        public UserModel DeepClone()
        {
            return new UserModel(this);
        }
    }
}
