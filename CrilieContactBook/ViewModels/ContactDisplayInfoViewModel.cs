using CrilieContactBook.ViewModels.Commands;
using System.Windows;

namespace CrilieContactBook.ViewModels
{
    public class ContactDisplayInfoViewModel : ViewModelBase
    {
        //Grid visibility property - Shows the grid with the contact info
        private Visibility _showGridWithContactInfo = Visibility.Visible;

        //Grid visibility property - Shows the grid with the contact deletion dialog
        private Visibility _showDeletionDialog = Visibility.Hidden;

        //ContactsViewModel(parent VM) link used to change the current child view displayed
        public ContactsViewModel ContactsVM { get; set; }

        //Grid visibility property - Shows the grid with the contact info
        public Visibility ShowGridWithContactInfo
        {
            get => _showGridWithContactInfo;
            set
            {
                _showGridWithContactInfo = value;
                RaisePropertyChanged();
            }
        }
        //Grid visibility property - Shows the grid with the contact deletion dialog
        public Visibility ShowDeletionDialog
        {
            get => _showDeletionDialog;
            set
            {
                _showDeletionDialog = value;
                RaisePropertyChanged();
            }
        }

        //Constructor(overloaded)
        public ContactDisplayInfoViewModel(ContactsViewModel _contactsVM)
        {
            ContactsVM = _contactsVM;
            
            SwitchToContactEditViewCommand = new ViewSwitchCommand(DisplayContactEditView);
            SwitchViewToAddNewContactCommand = new ViewSwitchCommand(AddNewContact);
            ShowContactDeletionDialogCommand = new ViewSwitchCommand(DisplayDeletionDialogToggle);
            DeleteSelectedContactCommand = new ViewSwitchCommand(DeleteSelectedContact);
        }

        //Commands used to change the views
        public ViewSwitchCommand SwitchToContactEditViewCommand { get; private set; }
        public ViewSwitchCommand SwitchViewToAddNewContactCommand { get; private set; }
        public ViewSwitchCommand ShowContactDeletionDialogCommand { get; private set; }
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

        //Toggles between displaying the contact info and the deletion dialog
        public void DisplayDeletionDialogToggle()
        {
            if (ShowGridWithContactInfo == Visibility.Visible)
            {
                ShowGridWithContactInfo = Visibility.Hidden;
                ShowDeletionDialog = Visibility.Visible;
            }
            else
            {
                ShowGridWithContactInfo = Visibility.Visible;
                ShowDeletionDialog = Visibility.Hidden;
            }
            
        }

        //Deletes the Selected Contact and returns to the previous display area
        public void DeleteSelectedContact()
        {
            ContactsVM.DeleteContact();
            DisplayDeletionDialogToggle();
        }
    }
}
