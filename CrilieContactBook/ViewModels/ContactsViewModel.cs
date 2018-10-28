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

            ContactsList = new ObservableCollection<Contact>();

            ContactsList.Add(new Contact
            {
                Id = 1,
                FullName = "Fane",
                Information = "Information 1",
                Phone = "23443",
                WhatsApp = "234 fsdsf",
                Skype = "234esddds"
            });
            ContactsList.Add(new Contact
            {
                Id = 2,
                FullName = "Iwan",
                Information = "Information 2",
                Phone = "23w443",
                WhatsApp = "234 fsdsf",
                Skype = "234sddds"
            });
            ContactsList.Add(new Contact
            {
                Id = 3,
                FullName = "Cula1e",
                Information = "Information 3",
                Phone = "23e443",
                WhatsApp = "234 fsdxsf",
                Skype = "234sdxdds"
            });

        }

        //TO DO - Load Contact list(after implementing SQLITE
        private void LoadContacts()
        {

        }

        //Adds a new contact to the database/Contact list
        public void AddContact(Contact _contact)
        {
                ContactsList.Add(_contact);
        }

        //Edits a contact's details
        public void EditContact(Contact _contact)
        {
            ContactsList[ContactsList.IndexOf(_contact)] = _contact;
        }

        //Deletes a contact
        public void DeleteContact(Contact _contact)
        {
            ContactsList.Remove(_contact);
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
