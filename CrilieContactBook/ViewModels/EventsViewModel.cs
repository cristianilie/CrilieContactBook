using CrilieContactBook.Model;
using CrilieContactBook.ViewModels.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CrilieContactBook.ViewModels
{
    public class EventsViewModel : VM_Base<Event>
    {
        //Event List Filter - filters the event list by EventListFilter enum and displays All/Active/Completed/Failed tasks
        private EventListFilter selectedItemFilter;
        public EventListFilter SelectedItemFilter
        {
            get { return selectedItemFilter; }
            set
            {
                selectedItemFilter = value;
                RaisePropertyChanged();

                switch (selectedItemFilter)
                {
                    case EventListFilter.Today:
                        ItemsList = new ObservableCollection<Event>(DbHandler<Event>.LoadElements().Where(x => (x.ScheduledDate.Date == DateTime.Now.Date) && (x.Finished == false)));
                        break;
                    case EventListFilter.On_3_Days:
                        ItemsList = new ObservableCollection<Event>(DbHandler<Event>.LoadElements().Where(x => (x.ScheduledDate.Date >= DateTime.Now.Date) && x.ScheduledDate.Date <= DateTime.Now.Date.AddDays(3) && (x.Finished == false)));
                        break;
                    case EventListFilter.On_7_Days:
                        ItemsList = new ObservableCollection<Event>(DbHandler<Event>.LoadElements().Where(x => (x.ScheduledDate.Date >= DateTime.Now.Date) && x.ScheduledDate.Date <= DateTime.Now.Date.AddDays(7) && (x.Finished == false)));
                        break;
                    case EventListFilter.On_14_Days:
                        ItemsList = new ObservableCollection<Event>(DbHandler<Event>.LoadElements().Where(x => (x.ScheduledDate.Date >= DateTime.Now.Date) && x.ScheduledDate.Date <= DateTime.Now.Date.AddDays(14) && (x.Finished == false)));
                        break;
                    case EventListFilter.On_30_Days:
                        ItemsList = new ObservableCollection<Event>(DbHandler<Event>.LoadElements().Where(x => (x.ScheduledDate.Date >= DateTime.Now.Date) && x.ScheduledDate.Date <= DateTime.Now.Date.AddDays(30) && (x.Finished == false)));
                        break;
                    case EventListFilter.All_Active:
                        ItemsList = new ObservableCollection<Event>(DbHandler<Event>.LoadElements().Where(x => (x.ScheduledDate.Date >= DateTime.Now.Date) && x.Finished == false));
                        break;
                    case EventListFilter.InactiveEvents:
                        ItemsList = new ObservableCollection<Event>(DbHandler<Event>.LoadElements().Where(x=>(x.Finished == true)));
                        break;
                    default:
                        break;
                }

            }
        }


        //Default Constructor
        public EventsViewModel()
        {
            SelectedItemFilter = EventListFilter.All_Active;
            PrepareToAddNewItemCommand = new IntermediaryCommand(PrepareToAddItem);
            PrepareToEditItemCommand = new IntermediaryCommand(PrepareToEditItem);
            PrepareToDeleteItemCommand = new IntermediaryCommand(PrepareToDeleteItem);
            PrepareToFinishActionCommand = new IntermediaryCommand(PrepareToFinish);
            CancelCommand = new IntermediaryCommand(Cancel);
            ConfirmActionVisibility = Visibility.Hidden;
        }

        //Sets up the command and wires it up with the method to add a new Item(Event) to the Event db table
        public override void PrepareToAddItem()
        {
            SelectedItem = new Event()
            {
                ScheduledDate = DateTime.Now
            };
            ButtonFinisherText = "Add Event";
            FinisherCommand = new IntermediaryCommand(AddItem);
            NotEditable = false;
            ConfirmActionVisibility = System.Windows.Visibility.Visible;
        }

        //Add new event to database and resets the Selected Item(Event) and UI Elements bound to it
        public override void AddItem()
        {
            if (CheckDate(SelectedItem.ScheduledDate))
            {
                SelectedItem.Finished = false;
                DbHandler<Event>.AddItem(SelectedItem);
            }

            SelectedItem = new Event()
            {
                ScheduledDate = DateTime.Now
            };
            NotEditable = true;
            ItemsList = DbHandler<Event>.LoadElements();
            SelectedItemFilter = EventListFilter.All_Active;
        }

        //Sets up the command and wires it up with the method to edit/update the Selected Item(Event)
        public override void PrepareToEditItem()
        {
            ButtonFinisherText = "Edit Event";
            FinisherCommand = new IntermediaryCommand(EditItem);
            NotEditable = false;
            ConfirmActionVisibility = System.Windows.Visibility.Visible;
        }

        //Edits the Selected Item(Event) in the database and resets the UI elements bound to it
        public override void EditItem()
        {
            if (SelectedItem != null)
            {
                if (CheckDate(SelectedItem.ScheduledDate))
                {
                    DbHandler<Event>.UpdateItem(SelectedItem);

                    SelectedItem = new Event()
                    {
                        ScheduledDate = DateTime.Now
                    };
                    NotEditable = true;
                    ItemsList = DbHandler<Event>.LoadElements();
                    SelectedItemFilter = EventListFilter.All_Active;
                    ConfirmActionVisibility = System.Windows.Visibility.Hidden;
                }
            }
        }
               
        //Sets up the command and wires it up with the method to delete the Selected Item(Event)
        public override void PrepareToDeleteItem()
        {
            ButtonFinisherText = "Delete Event";
            FinisherCommand = new IntermediaryCommand(DeleteItem);
            NotEditable = true;
            ConfirmActionVisibility = System.Windows.Visibility.Visible;
        }

        //Deletes the Selected Event from the database and resets the UI elements bound to it
        public override void DeleteItem()
        {
            if (SelectedItem != null)
            {
                DbHandler<Event>.DeleteItem(SelectedItem);
                SelectedItem = new Event();
                ItemsList = DbHandler<Event>.LoadElements();
                SelectedItemFilter = EventListFilter.All_Active;
                ConfirmActionVisibility = System.Windows.Visibility.Hidden;
            }
        }


        //Sets up the command and wires it up with the method to finish the Selected Item(Event)
        private void PrepareToFinish()
        {
            ButtonFinisherText = "Finish!";
            FinisherCommand = new IntermediaryCommand(FinishEvent);
            NotEditable = true;
            ConfirmActionVisibility = System.Windows.Visibility.Visible;
        }

        //Marks as completed the Selected Item(Event) from the database, and updates it and resets the UI elements bound to it
        public void FinishEvent()
        {
            if (SelectedItem != null)
            {
                SelectedItem.Finished = true;
                DbHandler<Event>.UpdateItem(SelectedItem);
                SelectedItem = new Event();
                ItemsList = DbHandler<Event>.LoadElements();
                SelectedItemFilter = EventListFilter.All_Active;
                ConfirmActionVisibility = System.Windows.Visibility.Hidden;
            }
        }

      

        //Checks if the current selected Date in the datetime picker is valid
        private bool CheckDate(DateTime dateToCkeck)
        {
            if (ButtonFinisherText.Equals("Add Event") && dateToCkeck.Date >= DateTime.Now.Date)
            {
                ConfirmActionVisibility = System.Windows.Visibility.Visible;
                return true;
            }

            if (ButtonFinisherText.Equals("Edit Event") && dateToCkeck.Date >= DateTime.Now.Date && dateToCkeck.Date <= (DateTime.Now.AddYears(100).Date))
            {
                ConfirmActionVisibility = System.Windows.Visibility.Visible;
                return true;
            }

            ConfirmActionVisibility = System.Windows.Visibility.Hidden;
            return false;
        }
    }

}
