using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.Utilities
{
    public interface INavigator
    {
        public ContentControl CurrentContentControl { get; set; }
        public void RegisterView(Control view);
        public void NavigateTo(Type type);
        public void Back();
        public bool CanGoBack();

    }
}
