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
    /// Logique d'interaction pour ViewColors.xaml
    /// </summary>
    public partial class ViewColors : UserControl
    {
        private IDataManager<BeerColorModel, BeerColorDto> _colorDataManager = ((App)Application.Current).ColorDataManager;

        public IGenericListModel<BeerColorModel> Colors { get; }

        public ViewColors()
        {
            Colors = new GenericListModel<BeerColorModel>();
            InitializeComponent();
        }

        public async void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadColor();
        }

        public async Task LoadColor()
        {
            var colors = await _colorDataManager.GetAll();
            Colors.List = new ObservableCollection<BeerColorModel>(colors);
        }

        private async void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            var newColor = await _colorDataManager.Add(Colors.ToModify);
            Colors.List.Add(newColor);
            Colors.ToModify = null;

            Update_Button.Visibility = Visibility.Visible;
            Delete_Button.Visibility = Visibility.Visible;
            Create_Button.Visibility = Visibility.Collapsed;
            ListOverlay.Visibility = Visibility.Collapsed;
        }

        private async void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Colors.ToModify != null)
            {
                await _colorDataManager.Update(Colors.ToModify.Id, Colors.ToModify);
                var index = Colors.List.IndexOf(Colors.Current);
                Colors.List[index] = Colors.ToModify.DeepClone();
            }

        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Colors.ToModify != null)
            {
                await _colorDataManager.DeleteById(Colors.ToModify.Id);
                Colors.List.Remove(Colors.Current);
                Colors.ToModify = null;
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
            Colors.ToModify = new BeerColorModel(string.Empty);
        }
    }
}
