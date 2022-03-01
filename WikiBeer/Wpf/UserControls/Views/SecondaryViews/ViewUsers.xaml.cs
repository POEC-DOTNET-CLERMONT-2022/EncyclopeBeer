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

        //public static readonly DependencyProperty UsersProperty =
        //    DependencyProperty.Register("Users", typeof(IGenericListModel<UserModel>), typeof(ViewUsers));

        //public IGenericListModel<UserModel> Users
        //{
        //    get { return GetValue(UsersProperty) as IGenericListModel<UserModel>; }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            SetValue(UsersProperty, value);
        //        }
        //    }
        //}

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
            Users.ToModify = null;
        }

        public async Task LoadUsers()
        {
            var users = await _userDataManager.GetAll();
            Users.List = new ObservableCollection<UserModel>(users);
        }

        private async void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Users.ToModify != null)
            {
                var updatedBeer = await _userDataManager.Update(Users.ToModify.Id, Users.ToModify);
                if (updatedBeer != null)
                {
                    var index = Users.List.IndexOf(Users.Current);
                    Users.List[index] = Users.ToModify.DeepClone();
                    List.SelectedItem = Users.List[index];
                    InfoDisplayer.Text = "Succesfull update";
                }
                else
                {
                    InfoDisplayer.Text = "Something went wrong";
                }
            }
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Users.ToModify != null)
            {
                await _userDataManager.DeleteById(Users.ToModify.Id);
                Users.List.Remove(Users.Current);
                Users.Current = null;
                InfoDisplayer.Text = "Deleted";
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
                string searchParams = BuildUserSearchParams(user);
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

        private string BuildUserSearchParams(UserModel user)
        {
            string searchParams = user.Nickname;

            if (user.Country != null)
            {
                searchParams = searchParams + " " + user.Country.Name;
            }

            return searchParams.ToLower();
        }
    }
}
