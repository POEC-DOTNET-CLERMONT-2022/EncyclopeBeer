using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.Models;
using System.Windows;
using System.Windows.Controls;
using Ipme.WikiBeer.Dtos;

namespace Ipme.WikiBeer.Wpf.UserControls.Components
{
    /// <summary>
    /// Logique d'interaction pour ColorDetailsComponent.xaml
    /// </summary>
    public partial class StyleDetailsComponent : UserControl
    {
        //private IDataManager<BeerStyleModel, BeerStyleDto> _styleDataManager = ((App)Application.Current).StyleDataManager;

        public static readonly DependencyProperty StyleDetailsProperty =
                    DependencyProperty.Register("StyleDetails", typeof(BeerStyleModel), typeof(StyleDetailsComponent));

        public BeerStyleModel StyleDetails
        {
            get { return GetValue(StyleDetailsProperty) as BeerStyleModel; }
            set
            {
                if (value != null)
                {
                    SetValue(StyleDetailsProperty, value);
                }
            }
        }

        public StyleDetailsComponent()
        {
            InitializeComponent();
        }
    }
}
