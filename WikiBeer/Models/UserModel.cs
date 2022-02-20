using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private string _nickName;
        public string NickName
        {
            get { return _nickName; }
            set
            {
                if (_nickName != value)
                {
                    _nickName = value;
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

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
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

        private CountryModel _country;
        public CountryModel Country
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

        public UserModel(Guid id, string nickName, DateTime birthDate, string email, int hashCode, 
            bool isCertified, CountryModel country, ObservableCollection<Guid> favotiteBeerIds)
        {
            Id = id;
            NickName = nickName;
            BirthDate = birthDate;
            Email = email;
            HashCode = hashCode;
            IsCertified = isCertified;
            Country = country;
            FavoriteBeerIds = favotiteBeerIds;
        }

        public UserModel(string nickName, DateTime birthDate, string email, int hashCode,
            bool isCertified, CountryModel country, ObservableCollection<Guid> favoriteBeerIds)
            : this(Guid.Empty, nickName, birthDate, email, hashCode, isCertified, country, favoriteBeerIds)
        {
        }

        public UserModel(UserModel user)
        {
            Id = user.Id;
            NickName = user.NickName;
            BirthDate = user.BirthDate;
            Email = user.Email;
            HashCode = user.HashCode;
            IsCertified = user.IsCertified;
            Country = user.Country.DeepClone();
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
