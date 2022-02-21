using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ipme.WikiBeer.Wpf.UserControls.Views.SecondaryViews
{
    /// <summary>
    /// Logique d'interaction pour ViewUsers.xaml
    /// </summary>
    public partial class ViewUsers : UserControl, INotifyPropertyChanged
    {
        private IDataManager<UserModel, UserDto> _userDataManager = ((App)Application.Current).UserDataManager;

        public IGenericListModel<UserModel> Users { get; }

        private string _textSearch;
        public string TextSearch
        {
            get
            {
                return _textSearch;
            }
            set
            {
                _textSearch = value;
                OnPropertyChanged();
                ((CollectionViewSource)Resources["UsersViewSource"]).View.Refresh();
            }
        }

        public ViewUsers()
        {
            Users = new GenericListModel<UserModel>();
            InitializeComponent();
        }

        public async void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadUsers();
        }

        public async Task LoadUsers()
        {
            var users = await _userDataManager.GetAll();
            Users.List = new ObservableCollection<UserModel>(users);
        }

        private async void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            await _userDataManager.Add(Users.ToModify);
            Users.List.Add(Users.ToModify);
            Users.ToModify = null;

            Update_Button.Visibility = Visibility.Visible;
            Delete_Button.Visibility = Visibility.Visible;
            Create_Button.Visibility = Visibility.Collapsed;
            ListOverlay.Visibility = Visibility.Collapsed;
        }

        private async void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Users.ToModify != null)
            {
                await _userDataManager.Update(Users.ToModify.Id, Users.ToModify);
                var index = Users.List.IndexOf(Users.Current);
                Users.List[index] = Users.ToModify.DeepClone();
            }
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Users.ToModify != null)
            {
                await _userDataManager.DeleteById(Users.ToModify.Id);
                Users.List.Remove(Users.Current);
                Users.Current = null;
            }
        }

        public void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            var user = e.Item as UserModel;

            if (string.IsNullOrWhiteSpace(TextSearch))
            {
                e.Accepted = true;
                return;
            }

            if (user != null)
            {
                string searchParams = BuildBeerSearchParams(user);
                if (!string.IsNullOrWhiteSpace(searchParams) && searchParams.Contains(TextSearch.ToLower()))
                {
                    e.Accepted = true;
                    return;
                }

                e.Accepted = false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string BuildBeerSearchParams(UserModel user)
        {
            string searchParams = user.NickName;

            if (user.Country != null)
            {
                searchParams = searchParams + " " + user.Country.Name;
            }

            return searchParams.ToLower();
        }
    }
}
