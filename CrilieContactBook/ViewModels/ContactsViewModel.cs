using CrilieContactBook.Model;
using System.Collections.ObjectModel;

namespace CrilieContactBook.ViewModels
{
    public class ContactsViewModel : ViewModelBase
    {
        //The child view displayed - default ContactDisplayInfoView
        //ContactDisplayInfoView - only displays information about a selected contact
        private object selectedContactView;
        public object SelectedContactView
        {
            get { return selectedContactView; }
            set
            {
                selectedContactView = value;
                RaisePropertyChanged();
            }
        }

        //The list of contacts in the database
        private ObservableCollection<Contact> contactsList;
        public ObservableCollection<Contact> ContactsList
        {
            get { return contactsList; }
            set
            {
                contactsList = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<Contact> contactsList2;
        public ObservableCollection<Contact> ContactsList2
        {
            get { return contactsList2; }
            set
            {
                contactsList2 = value;
                RaisePropertyChanged();
            }
        }

        //The selected contact from the Contacts listview
        private Contact selectedContact;
        public Contact SelectedContact
        {
            get { return selectedContact; }
            set
            {
                selectedContact = value;
                RaisePropertyChanged();
            }
        }

        //Constructor
        public ContactsViewModel()
        {
            ContactDisplayInfoVM = new ContactDisplayInfoViewModel(this);
            ContactEditInfoVM = new ContactEditInfoViewModel(this, ContactsManagementState.Default);
            SelectedContactView = ContactDisplayInfoVM;

            ContactsList = ContactBookDbManagement.LoadContacts();
        }

        //Load Contact listfrom our  SQLITE database
        private void LoadContacts()
        {
            ContactsList = ContactBookDbManagement.LoadContacts();
        }

        //Adds a new contact to the database
        public void AddContact(Contact _contact)
        {
            ContactBookDbManagement.AddContact(_contact);
            LoadContacts();
        }

        //Edits a contact's details
        public void EditContact(Contact _contact)
        {
            ContactBookDbManagement.UpdateContact(_contact);
            LoadContacts();
        }

        //Deletes a contact from the database
        public void DeleteContact()
        {
            if (SelectedContact != null)
            {
                ContactBookDbManagement.DeleteContact(SelectedContact);
                LoadContacts();
            }
        }


        //Contacts Child ViewVodel - ContactDisplayInfoViewModel
        private ContactDisplayInfoViewModel contactDisplayInfoVM;
        public ContactDisplayInfoViewModel ContactDisplayInfoVM
        {
            get { return contactDisplayInfoVM; }
            set
            {
                contactDisplayInfoVM = value;
                RaisePropertyChanged();
            }
        }

        //Contacts Child ViewVodel - ContactDisplayInfoViewModel
        private ContactEditInfoViewModel contactEditInfoVM;
        public ContactEditInfoViewModel ContactEditInfoVM
        {
            get { return contactEditInfoVM; }
            set
            {
                contactEditInfoVM = value;
                RaisePropertyChanged();
            }
        }

        //Switch Child View to ContactDisplayInfoView
        public void SwitchToContactDisplayView()
        {
            SelectedContactView = ContactDisplayInfoVM;
        }

        //Switch Child View to ContactEditInfoView to edit the Current/SelectedContact
        public void SwitchToContactEditView()
        {
            SelectedContactView = new ContactEditInfoViewModel(this, ContactsManagementState.Update);
        }

        //Switch Child View to ContactEditInfoView to add a new Contact
        public void SwitchToAddNewContact()
        {
            SelectedContactView = new ContactEditInfoViewModel(this, ContactsManagementState.Create);
        }
    }
}
