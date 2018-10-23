using CrilieContactBook.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieContactBook.ViewModels
{
    public class ContactsViewModel : ViewModelBase
    {
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

        private ObservableCollection<Contact> contactsList;

        public ObservableCollection<Contact> ContactsList
        {
            get { return contactsList; }
            set { contactsList = value;
                RaisePropertyChanged();
            }
        }


        private Contact selectedContact;

        public Contact SelectedContact
        {
            get { return selectedContact; }
            set {
                selectedContact = value;
                RaisePropertyChanged();
                }
        }

        public ContactsViewModel()
        {
            ContactDisplayInfoVM = new ContactDisplayInfoViewModel();
            SelectedContactView = ContactDisplayInfoVM;

            ContactsList = new ObservableCollection<Contact>();

            ContactsList.Add(new Contact{Id = 1,
                    FullName = "Fane",
                    Information = "Information 1",
                    Phone = "23443",
                    WhatsApp = "234 fsdsf",
                    Skype = "234esddds" });
            ContactsList.Add(new Contact{Id = 2,
                    FullName = "Iwan",
                    Information = "Information 2",
                    Phone = "23w443",
                    WhatsApp = "234 fsdsf",
                    Skype = "234sddds" });
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

        private void LoadContacts()
        {

        }

        private void AddContact(Contact _contact)
        {
            ContactsList.Add(_contact);
        }

        private void EditContact(Contact _contact)
        {

        }

        private void DeleteContact(Contact _contact)
        {
            ContactsList.Remove(_contact);
        }

        private ContactDisplayInfoViewModel contactDisplayInfoVM;

        public ContactDisplayInfoViewModel ContactDisplayInfoVM
        {
            get { return contactDisplayInfoVM; }
            set { contactDisplayInfoVM = value;
                RaisePropertyChanged();
            }
        }

        private ContactEditInfoViewModel contactEditInfoVM;

        public ContactEditInfoViewModel ContactEditInfoVM
        {
            get { return contactEditInfoVM; }
            set { contactEditInfoVM = value;
                RaisePropertyChanged();
            }
        }
    }
}
