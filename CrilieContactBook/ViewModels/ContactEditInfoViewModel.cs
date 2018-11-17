using CrilieContactBook.Model;
using CrilieContactBook.ViewModels.Commands;

namespace CrilieContactBook.ViewModels
{
    public class ContactEditInfoViewModel : ViewModelBase
    {
        //ContactsViewModel(parent VM) link used to change the current child view displayed
        public ContactsViewModel ContactsVM { get; set; }

        public ContactsManagementState CurrentState { get; set; } = ContactsManagementState.Default;

        //Current selected contact
        private Contact currentContact;
        public Contact CurrentContact
        {
            get { return currentContact; }
            set
            {
                currentContact = value;
                RaisePropertyChanged();
            }
        }


        //Constructor(overloaded)
        public ContactEditInfoViewModel(ContactsViewModel contactsViewModel, ContactsManagementState newState)
        {
            ContactsVM = contactsViewModel;
            if(newState == ContactsManagementState.Create)
            {
                CurrentContact = new Contact() { };
            }
            else if(newState == ContactsManagementState.Update)
            {
                CurrentContact = ContactsVM.SelectedContact;
            }
            else
            {
            
            }


            SwitchToContactDisplayViewCommand = new ViewSwitchCommand(DisplayContactInfoView);
            AddNewContactCommand = new ViewSwitchCommand(FinishAddingNewContact);
            SaveContactChangesCommand = new ViewSwitchCommand(SaveChangesToCurrentContact);

        }



        //Commands
        public ViewSwitchCommand SwitchToContactDisplayViewCommand { get; private set; }
        public ViewSwitchCommand AddNewContactCommand { get; private set; }
        public ViewSwitchCommand SaveContactChangesCommand { get; private set; }


        //Displays/returns to the CondatcDisplayInfo view
        public void DisplayContactInfoView()
        {
            ContactsVM.SwitchToContactDisplayView();
        }

        //Finishes the Contact adding process by caling AddContact method in the ContactsViewModel
        public void FinishAddingNewContact()
        {    
                ContactsVM.AddContact(CurrentContact);
                CurrentContact = new Contact();
        }

        //Finishes the Contact adding process by caling AddContact method in the ContactsViewModel
        public void SaveChangesToCurrentContact()
        {
            ContactsVM.EditContact(CurrentContact);
            CurrentContact = new Contact();
        }

    }
}

