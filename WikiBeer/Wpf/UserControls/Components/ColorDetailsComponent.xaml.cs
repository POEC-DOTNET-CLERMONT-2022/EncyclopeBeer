﻿using Ipme.WikiBeer.Models;
using System;
using System.Collections.Generic;
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

namespace Ipme.WikiBeer.Wpf.UserControls.Components
{
    /// <summary>
    /// Logique d'interaction pour ColorDetailsComponent.xaml
    /// </summary>
    public partial class ColorDetailsComponent : UserControl
    {
        public static readonly DependencyProperty ColorToModifyProperty =
                    DependencyProperty.Register("ColorToModify", typeof(BeerColorModel), typeof(ColorDetailsComponent));

        public BreweryModel ColorToModify
        {
            get { return GetValue(ColorToModifyProperty) as BreweryModel; }
            set
            {
                if (value != null)
                {
                    SetValue(ColorToModifyProperty, value);
                }
            }
        }

        public ColorDetailsComponent()
        {
            InitializeComponent();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}