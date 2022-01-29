using Ipme.WikiBeer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.Utilities
{
    internal class Navigator: ObservableObject, INavigator // Doit être Observable pour utiliser les binding
    {
        private List<Control> Views { get; set; } = new List<Control>();

        private ContentControl currentContentControl = new ContentControl();

        public Stack<Control> BackStack { get; set; } = new Stack<Control>();

        public ContentControl CurrentContentControl
        {
            get => currentContentControl;
            set
            {
                currentContentControl = value;
                OnNotifyPropertyChanged();
            }
        }

        public void RegisterView(Control view)
        {
            Views.Add(view);
        }

        public void NavigateTo(Type type)
        {
            if (CurrentContentControl == null) return;
            if (CurrentContentControl.Content != null)
            {
                BackStack.Push((Control)CurrentContentControl.Content);
            }
            var view = Views.SingleOrDefault(elt => elt.GetType() == type);
            if (view == null) return;
            CurrentContentControl.Content = view;
        }
        public void Back()
        {
            if (CurrentContentControl == null) return;
            CurrentContentControl.Content = BackStack.Pop();
        }
        public bool CanGoBack()
        {
            return BackStack.TryPeek(out var result);
        }
    }
}
