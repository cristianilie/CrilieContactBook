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
    /// Interaction logic for ContactsView.xaml
    /// </summary>
    public partial class ContactsView : UserControl
    {
        public static BindingExpression bindingExp;

        public ContactsView()
        {
            InitializeComponent();
            DataContext = new ContactsViewModel();
        }

        private void BtnFinisher_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression binding = contactFullNameTxtBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            binding = contactInfoTxtBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            binding = contactFullNameTxtBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            binding = contactPhoneTxtBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            binding = contactSkypeTxtBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            binding = contactWhatsAppTxtBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }
    }
}
