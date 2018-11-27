using CrilieContactBook.Model;
using CrilieContactBook.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

namespace CrilieContactBook.ViewModels
{
    public class ContactsViewModel : ViewModelBase
    {
        //CRUD Contact database table queries
        private string addToDb = "Insert into Contact(FullName, Information, Phone, Skype, WhatsApp) values(@FullName, @Information, @Phone, @Skype, @WhatsApp)";
        private string editToDb = "Update Contact Set FullName=@FullName, Information=@Information, Phone=@Phone, Skype=@Skype, WhatsApp=@WhatsApp Where Id=@Id";

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

        //Variable used to change the state of the SelectedContact's fields and make them editable or read only
        private bool _notEditable = true;
        public bool NotEditable
        {
            get => _notEditable;
            set
            {
                _notEditable = value;
                RaisePropertyChanged();
            }
        }


        //The text that appears on the button that fisnishes the CRUD commands on tasks
        private string buttonFinisherText;
        public string ButtonFinisherText
        {
            get { return buttonFinisherText; }
            set
            {
                buttonFinisherText = value;
                RaisePropertyChanged();
            }
        }

        //Displays the Confirmation buttons to finish a CRUD action
        private Visibility confirmActionVisibility;
        public Visibility ConfirmActionVisibility
        {
            get { return confirmActionVisibility; }
            set
            {
                confirmActionVisibility = value;
                RaisePropertyChanged();
            }
        }

        //Displays the Contact CRUD buttons that we use to add/edit/delete a contact in the database
        private Visibility _CRUDButtonsVisibility;
        public Visibility CRUDButtonsVisibility
        {
            get { return _CRUDButtonsVisibility; }
            set
            {
                _CRUDButtonsVisibility = value;
                RaisePropertyChanged();
            }
        }


        //Constructor
        public ContactsViewModel()
        {
            ContactsList = DbHandler<Contact>.LoadElements();
            PrepareToAddNewContactCommand = new IntermediaryCommand(PrepareToAddContact);
            PrepareToEditContactCommand = new IntermediaryCommand(PrepareToEditContact);
            PrepareToDeleteContactCommand = new IntermediaryCommand(PrepareToDeleteContact);
            CancelCommand = new IntermediaryCommand(Cancel);
            CRUDButtonsVisibility = Visibility.Visible;
            ConfirmActionVisibility = System.Windows.Visibility.Hidden;
            ButtonFinisherText = "";
        }


        //CRUD COMMANDS
        public IntermediaryCommand PrepareToAddNewContactCommand { get; private set; }
        public IntermediaryCommand PrepareToEditContactCommand { get; private set; }
        public IntermediaryCommand PrepareToDeleteContactCommand { get; private set; }
        public IntermediaryCommand CancelCommand { get; private set; }

        private ICommand _finisherContactCommand;
        public ICommand FinisherContactCommand
        {
            get => _finisherContactCommand;
            set
            {
                _finisherContactCommand = value;
                RaisePropertyChanged();
            }
        }



        //Sets up the command and wires it up with the method to add a new Contact
        private void PrepareToAddContact()
        {
            SelectedContact = new Contact();
            ButtonFinisherText = "Add Contact";
            FinisherContactCommand = new IntermediaryCommand(AddContact);
            NotEditable = false;
            CRUDButtonsVisibility = Visibility.Hidden;
            ConfirmActionVisibility = System.Windows.Visibility.Visible;
        }

        //Sets up the command and wires it up with the method to edit/update the selected Contact
        private void PrepareToEditContact()
        {
            ButtonFinisherText = "Edit Contact";
            FinisherContactCommand = new IntermediaryCommand(EditContact);
            NotEditable = false;
            CRUDButtonsVisibility = Visibility.Hidden;
            ConfirmActionVisibility = System.Windows.Visibility.Visible;
        }

        //Sets up the command and wires it up with the method to delete the Selected Contact
        private void PrepareToDeleteContact()
        {
            ButtonFinisherText = "Delete";
            FinisherContactCommand = new IntermediaryCommand(DeleteContact);
            NotEditable = true;
            CRUDButtonsVisibility = Visibility.Hidden;
            ConfirmActionVisibility = System.Windows.Visibility.Visible;
        }
        

        //Adds a new contact to the database
        public void AddContact()
        {
            if (SelectedContact != null)
            {
                DbHandler<Contact>.AddItem(SelectedContact,addToDb);
                SelectedContact = new Contact();
                NotEditable = true;
                ConfirmActionVisibility = System.Windows.Visibility.Hidden;
                CRUDButtonsVisibility = System.Windows.Visibility.Visible;
                ContactsList = DbHandler<Contact>.LoadElements();
            }
        }

        //Edits a contact's details
        public void EditContact()
        {
            if (SelectedContact != null)
            {
                DbHandler<Contact>.UpdateItem(SelectedContact,editToDb);
                SelectedContact = new Contact();
                NotEditable = true;
                ConfirmActionVisibility = System.Windows.Visibility.Hidden;
                CRUDButtonsVisibility = System.Windows.Visibility.Visible;

                ContactsList = DbHandler<Contact>.LoadElements();
            }
        }

        //Deletes a contact from the database
        public void DeleteContact()
        {
            if (SelectedContact != null)
            {
                DbHandler<Contact>.DeleteItem(SelectedContact);
                SelectedContact = new Contact();
                NotEditable = true;
                ConfirmActionVisibility = System.Windows.Visibility.Hidden;
                CRUDButtonsVisibility = System.Windows.Visibility.Visible;
                ContactsList = DbHandler<Contact>.LoadElements();
            }
        }

        //Cancel - reset current action
        public void Cancel()
        {
            SelectedContact = new Contact();
            NotEditable = true;
            ConfirmActionVisibility = Visibility.Hidden;
            CRUDButtonsVisibility = Visibility.Visible;
        }
    }
}
