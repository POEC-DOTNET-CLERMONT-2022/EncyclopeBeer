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
    /// Logique d'interaction pour ViewCountries.xaml
    /// </summary>
    public partial class ViewCountries : UserControl
    {
        private IDataManager<CountryModel, CountryDto> _countryDataManager = ((App)Application.Current).CountryDataManager;

        public IGenericListModel<CountryModel> Styles { get; }

        public ViewCountries()
        {
            Styles = new GenericListModel<CountryModel>();
            InitializeComponent();
        }

        private async void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCountries();
        }

        private async Task LoadCountries()
        {
            var countries = await _countryDataManager.GetAll();
            Styles.List = new ObservableCollection<CountryModel>(countries);
        }

        private void AddCountry()
        {
            CountryModel NewCountry = new CountryModel(string.Empty);
        }
    }
}
