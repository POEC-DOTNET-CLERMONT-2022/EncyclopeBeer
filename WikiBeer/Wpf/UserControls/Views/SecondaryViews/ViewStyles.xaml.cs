using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using System.Collections.ObjectModel;
using System.Linq;
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

        private BeerStyleModel _newBeer;

        public ViewStyles()
        {
            Styles = new GenericListModel<BeerStyleModel>();
            InitializeComponent();
        }

        public async void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadColor();
        }

        public async Task LoadColor()
        {
            var styles = await _styleDataManager.GetAll();
            Styles.List = new ObservableCollection<BeerStyleModel>(styles);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            _styleDataManager.Update(Styles.ToModify.Id, Styles.ToModify);
            Styles.Current = Styles.ToModify;


        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            _styleDataManager.DeleteById(Styles.ToModify.Id);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Update_Button.Visibility = Visibility.Visible;
            Create_Button.Visibility = Visibility.Collapsed;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Update_Button.Visibility = Visibility.Collapsed;
            Create_Button.Visibility = Visibility.Visible;
            _newBeer = new BeerStyleModel(string.Empty, string.Empty);
            StyleDetailsComponent.StyleDetails = _newBeer;
        }

        private void StyleDetailsComponent_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
