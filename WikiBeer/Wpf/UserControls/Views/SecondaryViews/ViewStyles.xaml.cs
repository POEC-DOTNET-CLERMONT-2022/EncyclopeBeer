using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Views.SecondaryViews
{
    /// <summary>
    /// Logique d'interaction pour ViewStyle.xaml
    /// </summary>
    public partial class ViewStyles : UserControl
    {
        private IDataManager<BeerStyleModel, BeerStyleDto> _styleDataManager = ((App)Application.Current).StyleDataManager;

        public IGenericListModel<BeerStyleModel> Styles { get; }

        public ViewStyles()
        {
            Styles = new GenericListModel<BeerStyleModel>();
            InitializeComponent();
        }

        public async void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadStyles();
            Styles.ToModify = null;
        }

        public async Task LoadStyles()
        {
            var styles = await _styleDataManager.GetAll();
            Styles.List = new ObservableCollection<BeerStyleModel>(styles);
        }

        private async void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            var newStyle = await _styleDataManager.Add(Styles.ToModify);
            Styles.List.Add(newStyle);
            Styles.ToModify = null;

            Update_Button.Visibility = Visibility.Visible;
            Delete_Button.Visibility = Visibility.Visible;
            Create_Button.Visibility = Visibility.Collapsed;
            ListOverlay.Visibility = Visibility.Collapsed;
        }

        private async void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Styles.ToModify != null)
            {
                await _styleDataManager.Update(Styles.ToModify.Id, Styles.ToModify);
                var index = Styles.List.IndexOf(Styles.Current);
                Styles.List[index] = Styles.ToModify.DeepClone();
            }
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Styles.ToModify != null)
            {
                await _styleDataManager.DeleteById(Styles.ToModify.Id);
                Styles.List.Remove(Styles.Current);
                Styles.ToModify = null;
            }
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            Update_Button.Visibility = Visibility.Visible;
            Delete_Button.Visibility = Visibility.Visible;
            Create_Button.Visibility = Visibility.Collapsed;
            ListOverlay.Visibility = Visibility.Collapsed;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            Update_Button.Visibility = Visibility.Collapsed;
            Delete_Button.Visibility = Visibility.Collapsed;
            Create_Button.Visibility = Visibility.Visible;
            ListOverlay.Visibility = Visibility.Visible;
            Styles.ToModify = new BeerStyleModel(string.Empty, string.Empty);
        }
    }
}
