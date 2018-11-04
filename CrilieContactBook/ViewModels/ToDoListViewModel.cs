using CrilieContactBook.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrilieContactBook.ViewModels
{
    public class ToDoListViewModel: ViewModelBase
    {
        //The list of tasks in the database
        private ObservableCollection<TaskToComplete> taskList;
        public ObservableCollection<TaskToComplete> TaskList
        {
            get { return taskList; }
            set
            {
                taskList = value;
                RaisePropertyChanged();
            }
        }

        //The selected task from the TaskList listview
        private TaskToComplete selectedTask;
        public TaskToComplete SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                RaisePropertyChanged();
            }
        }

        public ToDoListViewModel()
        {

        }
    }
}
