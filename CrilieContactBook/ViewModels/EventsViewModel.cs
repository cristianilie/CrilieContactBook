using CrilieContactBook.Model;
using CrilieContactBook.ViewModels.Commands;
using CrilieContactBook.ViewModels.Enums;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CrilieContactBook.ViewModels
{
    public class EventsViewModel : ViewModelBase
    {

        //The list of events in the database
        private ObservableCollection<Event> eventList;
        public ObservableCollection<Event> EventList
        {
            get { return eventList; }
            set
            {
                eventList = value;
                RaisePropertyChanged();
            }
        }

        //The selected event from the EventList listview
        private Event selectedEvent;
        public Event SelectedEvent
        {
            get { return selectedEvent; }
            set
            {
                selectedEvent = value;
                RaisePropertyChanged();
            }
        }



        //Variable used to change the state of the SelectedEvent's fields and make them editable or read only
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

        //Displays the Event CRUD "finisher" buttons if we show our intent to execute any operation on the database
        private Visibility finisherButtonsVisibility;
        public Visibility FinisherButtonsVisibility
        {
            get { return finisherButtonsVisibility; }
            set
            {
                finisherButtonsVisibility = value;
                RaisePropertyChanged();
            }
        }

        //Event List Filter - filters the event list by EventListFilter enum and displays All/Active/Completed/Failed tasks
        private EventListFilter selectedEventFilter;
        public EventListFilter SelectedEventFilter
        {
            get { return selectedEventFilter; }
            set
            {
                selectedEventFilter = value;
                RaisePropertyChanged();

                //switch (SelectedEventFilter)
                //{
                //    case EventListFilter.All:
                //        EventList = TaskToCompleteDbManagement.LoadTasks();
                //        break;
                //    case EventListFilter.Active:
                //        TaskList = new ObservableCollection<TaskToComplete>(TaskToCompleteDbManagement.LoadTasks().Where(x => ((DateTime)x.Deadline.Date >= DateTime.Now) && ((bool)x.Completed == false)));
                //        break;
                //    case TaskListFilter.Completed:
                //        TaskList = new ObservableCollection<TaskToComplete>(TaskToCompleteDbManagement.LoadTasks().Where(x => (bool)x.Completed == true));
                //        break;
                //    case TaskListFilter.Failed:
                //        TaskList = new ObservableCollection<TaskToComplete>(TaskToCompleteDbManagement.LoadTasks().Where(x => ((DateTime)x.Deadline.Date < DateTime.Now) && (bool)x.Completed == false));
                //        break;
                //    default:
                //        break;
                //}
            }
        }


        //Default Constructor
        public EventsViewModel()
        {
            EventList = EventsDbManagement.LoadEvents();
            PrepareToAddNewEventCommand = new IntermediaryCommand(PrepareToAddEvent);
            PrepareToEditEventCommand = new IntermediaryCommand(PrepareToEditEvent);
            PrepareToDeleteEventCommand = new IntermediaryCommand(PrepareToDeleteEvent);
            PrepareToCompleteEventCommand = new IntermediaryCommand(PrepareToFinish);
            CancelCommand = new IntermediaryCommand(Cancel);
            FinisherButtonsVisibility = Visibility.Hidden;
        }

        //CRUD COMMANDS
        public IntermediaryCommand PrepareToAddNewEventCommand { get; private set; }
        public IntermediaryCommand PrepareToEditEventCommand { get; private set; }
        public IntermediaryCommand PrepareToDeleteEventCommand { get; private set; }
        public IntermediaryCommand PrepareToCompleteEventCommand { get; private set; }
        public IntermediaryCommand CancelCommand { get; private set; }

        private ICommand _finisherEventCommand;
        public ICommand FinisherEventCommand
        {
            get => _finisherEventCommand;
            set
            {
                _finisherEventCommand = value;
                RaisePropertyChanged();
            }
        }




        //Sets up the command and wires it up with the method to add a new event
        private void PrepareToAddEvent()
        {
            SelectedEvent = new Event();
            ButtonFinisherText = "Add Event";
            FinisherEventCommand = new IntermediaryCommand(AddNewEvent);
            NotEditable = false;
            FinisherButtonsVisibility = System.Windows.Visibility.Visible;
        }

        //Add new event to database and resets the Selected Event and UI Elements bound to it
        public void AddNewEvent()
        {
            EventsDbManagement.AddEvent(SelectedEvent);
            SelectedEvent = new Event();
            SelectedEvent.Finished = false;
            NotEditable = true;
            EventList = EventsDbManagement.LoadEvents();
            FinisherButtonsVisibility = System.Windows.Visibility.Hidden;
        }


        //Sets up the command and wires it up with the method to edit/update the selected task
        private void PrepareToEditEvent()
        {
            ButtonFinisherText = "Edit Event";
            FinisherEventCommand = new IntermediaryCommand(EditEvent);
            NotEditable = false;
            FinisherButtonsVisibility = System.Windows.Visibility.Visible;
        }

        //Edits the Selected Event in the database and resets the UI elements bound to it
        public void EditEvent()
        {
            if (SelectedEvent != null)
            {
                EventsDbManagement.UpdateEvent(SelectedEvent);
                SelectedEvent = new Event();
                NotEditable = true;
                EventList = EventsDbManagement.LoadEvents();
                FinisherButtonsVisibility = System.Windows.Visibility.Hidden;

            }
        }

        //Sets up the command and wires it up with the method to delete the Selected Event
        private void PrepareToDeleteEvent()
        {
            ButtonFinisherText = "Delete Event";
            FinisherEventCommand = new IntermediaryCommand(DeleteEvent);
            NotEditable = true;
            FinisherButtonsVisibility = System.Windows.Visibility.Visible;
        }

        //Deletes the Selected Event from the database and resets the UI elements bound to it
        public void DeleteEvent()
        {
            if (SelectedEvent != null)
            {
                EventsDbManagement.DeleteEvent(SelectedEvent);
                SelectedEvent = new Event();
                EventList = EventsDbManagement.LoadEvents();
                FinisherButtonsVisibility = System.Windows.Visibility.Hidden;
            }
        }


        //Sets up the command and wires it up with the method to finish the Selected Event
        private void PrepareToFinish()
        {
            ButtonFinisherText = "Finish!";
            FinisherEventCommand = new IntermediaryCommand(FinishEvent);
            NotEditable = true;
            FinisherButtonsVisibility = System.Windows.Visibility.Visible;
        }

        //Marks as completed the Selected Event from the database, and updates it and resets the UI elements bound to it
        public void FinishEvent()
        {
            if (SelectedEvent != null)
            {
                SelectedEvent.Finished = true;
                EventsDbManagement.UpdateEvent(SelectedEvent);
                SelectedEvent = new Event();
                EventList = EventsDbManagement.LoadEvents();
                FinisherButtonsVisibility = System.Windows.Visibility.Hidden;
            }
        }

        //Cancel - reset current action
        public void Cancel()
        {
            SelectedEvent = new Event();
            NotEditable = true;
            FinisherButtonsVisibility = Visibility.Hidden;
        }
    }

}
