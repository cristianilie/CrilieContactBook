using CrilieContactBook.Model;
using CrilieContactBook.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CrilieContactBook.ViewModels
{
    public class VM_Base<T> : INotifyPropertyChanged
        where  T : I_DB_Query, new()
    {
        //Item list of generic type T
        private ObservableCollection<T> itemsList;
        public ObservableCollection<T> ItemsList
        {
            get { return itemsList; }
            set
            {
                itemsList = value;
                RaisePropertyChanged();
            }
        }

        //The selected T item  from item list
        private T selectedItem;
        public virtual T SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisePropertyChanged();
            }
        }


        //Variable used to change the state of the Selected T item's fields and make them editable or read only
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

        //The text that appears on the button that fisnishes the CRUD commands on T objects
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

        //Displays the T View&ViewModel  CRUD "finisher" buttons if we show our intent to execute any operation on the database
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


        //CRUD COMMANDS
        public IntermediaryCommand PrepareToAddNewItemCommand { get; set; }
        public IntermediaryCommand PrepareToEditItemCommand { get; set; }
        public IntermediaryCommand PrepareToDeleteItemCommand { get; set; }
        public IntermediaryCommand PrepareToFinishActionCommand { get; set; }
        public IntermediaryCommand CancelCommand { get; set; }

        private ICommand finisherCommand;
        public ICommand FinisherCommand
        {
            get => finisherCommand;
            set
            {
                finisherCommand = value;
                RaisePropertyChanged();
            }
        }


        //Sets up the command and wires it up with the method to add a new T Item
        public virtual void PrepareToAddItem()
        {
            SelectedItem = new T();
            ButtonFinisherText = $"Add {typeof(T).Name}";
            FinisherCommand = new IntermediaryCommand(AddItem);
            NotEditable = false;
            ConfirmActionVisibility = System.Windows.Visibility.Visible;
        }

        //Sets up the command and wires it up with the method to edit/update the Selected Item
        public virtual void PrepareToEditItem()
        {
            ButtonFinisherText = $"Edit {typeof(T).Name}";
            FinisherCommand = new IntermediaryCommand(EditItem);
            NotEditable = false;
            ConfirmActionVisibility = System.Windows.Visibility.Visible;
        }

        //Sets up the command and wires it up with the method to delete the Selected Item
        public virtual void PrepareToDeleteItem()
        {
            ButtonFinisherText = "Delete";
            FinisherCommand = new IntermediaryCommand(DeleteItem);
            NotEditable = true;
            ConfirmActionVisibility = System.Windows.Visibility.Visible;
        }


        //Adds a new contact to the database
        public virtual void AddItem()
        {
            if (SelectedItem != null)
            {
                DbHandler<T>.AddItem(SelectedItem);
                SelectedItem = new T();
                NotEditable = true;
                ConfirmActionVisibility = System.Windows.Visibility.Hidden;
                ItemsList = DbHandler<T>.LoadElements();
            }
        }

        //Edits a contact's details
        public virtual void EditItem()
        {
            if (SelectedItem != null)
            {
                DbHandler<T>.UpdateItem(SelectedItem);
                SelectedItem = new T();
                NotEditable = true;
                ConfirmActionVisibility = System.Windows.Visibility.Hidden;

                ItemsList = DbHandler<T>.LoadElements();
            }
        }

        //Deletes a contact from the database
        public virtual void DeleteItem()
        {
            if (SelectedItem != null)
            {
                DbHandler<T>.DeleteItem(SelectedItem);
                SelectedItem = new T();
                NotEditable = true;
                ConfirmActionVisibility = System.Windows.Visibility.Hidden;
                ItemsList = DbHandler<T>.LoadElements();
            }
        }

        //Cancel - reset current action
        public void Cancel()
        {
            SelectedItem = new T();
            NotEditable = true;
            ConfirmActionVisibility = Visibility.Hidden;
        }

        /// <summary>
        /// INotifyPropertyChanged implementation
        /// </summary>
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var hndlr = PropertyChanged;
            if (hndlr != null)
            {
                hndlr(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
