using CrilieContactBook.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CrilieContactBook.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object currentView;

        public object CurrentView
        {
            get { return currentView; }
            set { currentView = value;
                RaisePropertyChanged();
            }
        }

        private ContactsViewModel contactVM;

        public ContactsViewModel ContactVM
        {
            get { return contactVM; }
            set { contactVM = value;
                RaisePropertyChanged();
            }
        }

        private EventsViewModel eventsVM;

        public EventsViewModel EventsVM
        {
            get { return eventsVM; }
            set {
                eventsVM = value;
                RaisePropertyChanged();
            }
        }

        private ToDoListViewModel toDoListVM;

        public ToDoListViewModel ToDoListVM
        {
            get { return toDoListVM; }
            set {
                toDoListVM = value;
                RaisePropertyChanged();
            }
        }


        //Commands
        public ViewSwitchCommand ContactsViewSwitchCommand { get; private set; }
        public ViewSwitchCommand EventsViewSwitchCommand { get; private set; }
        public ViewSwitchCommand ToDoListViewSwitchCommand { get; private set; }

        //Constructor
        public MainViewModel()
        {
            ContactVM = new ContactsViewModel();
            EventsVM = new EventsViewModel();
            ToDoListVM = new ToDoListViewModel();
            CurrentView = ContactVM;

            ContactsViewSwitchCommand = new ViewSwitchCommand(DisplayContactView);
            EventsViewSwitchCommand = new ViewSwitchCommand(DisplayEventsView);
            ToDoListViewSwitchCommand = new ViewSwitchCommand(DisplayToDoListtView);
        }

        //Methods to change the current (child)view
        private void DisplayContactView() => CurrentView = ContactVM;
        private void DisplayEventsView() => CurrentView = EventsVM;
        private void DisplayToDoListtView() => CurrentView = ToDoListVM;

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
