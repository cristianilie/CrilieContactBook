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
    /// Interaction logic for ToDoListView.xaml
    /// </summary>
    public partial class ToDoListView : UserControl
    {
        public ToDoListView()
        {
            InitializeComponent();
            DataContext = new ToDoListViewModel();
        }

        private void btnTaskFinisher_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression binding = taskTitleTxtBx.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            binding = taskDescriptionTxtBx.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            binding = taskImportanceCbx.GetBindingExpression(ComboBox.SelectedItemProperty);
            binding.UpdateSource();
        }
    }
}
