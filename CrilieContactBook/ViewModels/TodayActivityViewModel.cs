using CrilieContactBook.Model;
using CrilieContactBook.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CrilieContactBook.ViewModels
{
    public class TodayActivityViewModel : INotifyPropertyChanged
    {
        //Item list - Events/Tasks that have the date/deadline Today
        private ObservableCollection<IActivityEntity> itemsList;
        public ObservableCollection<IActivityEntity> ItemsList
        {
            get { return itemsList; }
            set
            {
                itemsList = value;
                RaisePropertyChanged();
            }
        }

        //Commands to view selected item / or ignore it
        public ViewSwitchCommand IgnoreCommand { get; set; }
        public ViewSwitchCommand DisplayActivityCommand { get; set; }

        //Reference to the Main View Model that helps us navigating between the views
        public MainViewModel MainVM { get; set; }

        //Default Constructor
        public TodayActivityViewModel()
        {
            GetTodayEvents();
            GetTodayTasks();
        }

        //Overloaded Constructor
        //It has a reference to the main VM that helps to navigate to the desired views
        public TodayActivityViewModel(MainViewModel mwVM)
        {
            GetTodayEvents();
            GetTodayTasks();
            MainVM = mwVM;
            DisplayActivityCommand = new ViewSwitchCommand(SwitchViewAndSeeSelectedActivity);

            //Calling the method to navidate to the Contact view 
            IgnoreCommand = new ViewSwitchCommand(() => { MainVM.DisplayContactView(); });
        }

        //Displays the selected activity in his "native" view, in a more detailed form
        public void SwitchViewAndSeeSelectedActivity()
        {
            MainVM.SeeDetailedActivity(selectedActivity);
        }

        //The selected T(TaskToComplete/Event) item  from item list
        private IActivityEntity selectedActivity;
        public IActivityEntity SelectedActivity
        {
            get { return selectedActivity; }
            set
            {
                selectedActivity = value;
                RaisePropertyChanged();
            }
        }

        //Retrieves the Events that are scheduled for today and adds them to the existing items(if any) in the Items list
        private void GetTodayEvents()
        {
            List<Event> todayEvents = DbHandler<Event>.LoadElements().Where(x => x.ScheduledDate.Date == DateTime.Today.Date).ToList();
            if (ItemsList == null)
            {
                ItemsList = new ObservableCollection<IActivityEntity>(todayEvents);
            }
            else
            {
                if (todayEvents.Count>0)
                {
                    for (int i = 0; i < todayEvents.Count; i++)
                    {
                        ItemsList.Add(todayEvents[i]);
                    }
                }
            }
        }

        //Retrieves the Tasks that are scheduled for today and adds them to the existing items(if any) in the Items list
        private void GetTodayTasks()
        {
            List<TaskToComplete> todayTasks = DbHandler<TaskToComplete>.LoadElements().Where(x => x.Deadline.Date == DateTime.Today.Date).ToList();
            if (ItemsList.Count < 1)
            {
                ItemsList = new ObservableCollection<IActivityEntity>(todayTasks);
            }
            else
            {
                if (todayTasks.Count > 0)
                {
                    for (int i = 0; i < todayTasks.Count; i++)
                    {
                        ItemsList.Add(todayTasks[i]);
                    }
                }
            }
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
