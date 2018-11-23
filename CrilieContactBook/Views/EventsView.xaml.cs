using CrilieContactBook.ViewModels;
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

namespace CrilieContactBook.Views
{
    /// <summary>
    /// Interaction logic for EventsView.xaml
    /// </summary>
    public partial class EventsView : UserControl
    {
        public EventsView()
        {
            InitializeComponent();
            DataContext = new EventsViewModel();
        }

        private void btnEventFinisher_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression binding = eventTitleTxtBx.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            binding = eventDescriptionTxtBx.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }
    }
}
