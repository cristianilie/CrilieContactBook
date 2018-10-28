using CrilieContactBook.Model;
using CrilieContactBook.ViewModels.Commands;
using System.Windows;

namespace CrilieContactBook.ViewModels
{
    public class ContactDisplayInfoViewModel : ViewModelBase
    {
        //ContactsViewModel(parent VM) link used to change the current child view displayed
        public ContactsViewModel ContactsVM { get; set; }

        //Grid visibility property
        public Visibility ShowGridWithContactInfo { get; set; } = Visibility.Visible;

        //Grid visibility property
        public Visibility ShowDeletionDialog { get; set; } = Visibility.Hidden;

        //Constructor(overloaded)
        public ContactDisplayInfoViewModel(ContactsViewModel _contactsVM)
        {
            ContactsVM = _contactsVM;
            SwitchToContactEditViewCommand = new ViewSwitchCommand(DisplayContactEditView);
            SwitchViewToAddNewContactCommand = new ViewSwitchCommand(AddNewContact);
        }

        //Commands used to change the views
        public ViewSwitchCommand SwitchToContactEditViewCommand { get; private set; }
        public ViewSwitchCommand SwitchViewToAddNewContactCommand { get; private set; }
        public ViewSwitchCommand DeleteSelectedContactCommand { get; private set; }


        //Calls the ContactsViewModel methid to change the view into ContactEditView
        public void DisplayContactEditView()
        {
            ContactsVM.SwitchToContactEditView();
        }

        //Add new contact to Contacts list and database
        public void AddNewContact()
        {            
            ContactsVM.SwitchToAddNewContact();
        }


    }
}
