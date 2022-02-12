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
        //private StyleDataManager _styleDataManager = (StyleDataManager)((App)Application.Current).StyleDataManager;

        public IGenericListModel<BeerStyleModel> Styles { get; }

        //private BeerStyleModel _newBeer;

        public ViewStyles()
        {
            Styles = new GenericListModel<BeerStyleModel>();
            InitializeComponent();
        }

        public async void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadStyles();
        }

        public async Task LoadStyles()
        {
            var styles = await _styleDataManager.GetAll();
            Styles.List = new ObservableCollection<BeerStyleModel>(styles);
        }

        private async void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            await _styleDataManager.Update(Styles.ToModify.Id, Styles.ToModify);
            var index = Styles.List.IndexOf(Styles.Current);
            Styles.List[index] = Styles.ToModify.DeepClone();
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            await _styleDataManager.DeleteById(Styles.ToModify.Id);
            Styles.List.Remove(Styles.Current);
        }

        private async void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            var newStyle = await _styleDataManager.Add(Styles.ToModify);
            Styles.List.Add(newStyle);            
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            Update_Button.Visibility = Visibility.Visible;
            Create_Button.Visibility = Visibility.Collapsed;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            Update_Button.Visibility = Visibility.Collapsed;
            Create_Button.Visibility = Visibility.Visible;
            Styles.ToModify = new BeerStyleModel(string.Empty, string.Empty);
        }

        private void StyleDetailsComponent_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
