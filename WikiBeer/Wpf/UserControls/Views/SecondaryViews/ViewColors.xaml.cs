using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }
}
