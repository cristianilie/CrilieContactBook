using CrilieContactBook.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieContactBook.ViewModels
{
    public class MainViewModel : ViewModelBase
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



        public ViewSwitchCommand ContactsViewSwitchCommand { get; private set; }
        public ViewSwitchCommand EventsViewSwitchCommand { get; private set; }
        public ViewSwitchCommand ToDoListViewSwitchCommand { get; private set; }


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


        private void DisplayContactView() => CurrentView = ContactVM;
        private void DisplayEventsView() => CurrentView = EventsVM;
        private void DisplayToDoListtView() => CurrentView = ToDoListVM;
    }
}
