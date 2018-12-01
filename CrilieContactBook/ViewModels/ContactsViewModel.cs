using CrilieContactBook.Model;
using CrilieContactBook.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

namespace CrilieContactBook.ViewModels
{
    public class ContactsViewModel : VM_Base<Contact>
    {
               
        //Constructor
        public ContactsViewModel()
        {
            ItemsList = DbHandler<Contact>.LoadElements();
            PrepareToAddNewItemCommand = new IntermediaryCommand(PrepareToAddItem);
            PrepareToEditItemCommand = new IntermediaryCommand(PrepareToEditItem);
            PrepareToDeleteItemCommand = new IntermediaryCommand(PrepareToDeleteItem);
            CancelCommand = new IntermediaryCommand(Cancel);
            ConfirmActionVisibility = System.Windows.Visibility.Hidden;
            ButtonFinisherText = "";
        }

    }
}
