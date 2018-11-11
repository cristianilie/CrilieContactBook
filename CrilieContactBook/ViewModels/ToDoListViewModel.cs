using CrilieContactBook.Model;
using CrilieContactBook.ViewModels.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CrilieContactBook.ViewModels
{
    public class ToDoListViewModel : ViewModelBase
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

        //The Importance of the selected Task
        private TaskImportance selectedTaskImportance;
        public TaskImportance SelectedTaskImportance
        {
            get => selectedTaskImportance;
            set
            {
                selectedTaskImportance = value;
                RaisePropertyChanged();
            }
        }

        //Variable used to change the state of the SelectedTask's fields and make them editable or read only
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

        //Variable used to specify the final Add/Update/DeleteConfirm button's content
        private string buttonFinisherText;
        private ICommand _finisherTaskCommand;

        //The text that Appears on the button that fisnishes the CRUD commands on tasks
        public string ButtonFinisherText
        {
            get { return buttonFinisherText; }
            set
            {
                buttonFinisherText = value;
                RaisePropertyChanged();
            }
        }

        //Displays the Task CRUD "finisher" buttons if we show our intent to execute any operation on the database
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

        //Task List Filter - filters the tasklist by TaskListFilters enum and displays All/Active/Completed/Failed tasks
        private TaskListFilter selectedTaskFilter;
        public TaskListFilter SelectedTaskFilter
        {
            get { return selectedTaskFilter; }
            set {
                selectedTaskFilter = value;
                RaisePropertyChanged();

                switch (SelectedTaskFilter)
                {
                    case TaskListFilter.All:
                        TaskList = TaskToCompleteDbManagement.LoadTasks();
                        break;
                    case TaskListFilter.Active:
                        TaskList = new ObservableCollection<TaskToComplete>(TaskToCompleteDbManagement.LoadTasks().Where(x => ((DateTime)x.Deadline.Date >= DateTime.Now) && ((bool)x.Completed == false)));
                        break;
                    case TaskListFilter.Completed:
                        TaskList = new ObservableCollection<TaskToComplete>(TaskToCompleteDbManagement.LoadTasks().Where(x => (bool)x.Completed == true));
                        break;
                    case TaskListFilter.Failed:
                        TaskList = new ObservableCollection<TaskToComplete>(TaskToCompleteDbManagement.LoadTasks().Where(x => ((DateTime)x.Deadline.Date < DateTime.Now) && (bool)x.Completed == false));
                        break;
                    default:
                        break;
                }
            }
        }



        //Default constructor
        public ToDoListViewModel()
        {
            TaskList = TaskToCompleteDbManagement.LoadTasks();
            PrepareToAddNewTaskCommand = new TaskToCompleteCommand(PrepareToAddTask);
            PrepareToEditTaskCommand = new TaskToCompleteCommand(PrepareToEditTask);
            PrepareToDeleteTaskCommand = new TaskToCompleteCommand(PrepareToDeleteTask);
            PrepareToCompleteTaskCommand = new TaskToCompleteCommand(PrepareMarkAsCompleted);
            FinisherButtonsVisibility = Visibility.Hidden;
            SelectedTaskFilter = TaskListFilter.Active;
        }


        //CRUD COMMANDS
        public TaskToCompleteCommand PrepareToAddNewTaskCommand { get; private set; }
        public TaskToCompleteCommand PrepareToEditTaskCommand { get; private set; }
        public TaskToCompleteCommand PrepareToDeleteTaskCommand { get; private set; }
        public TaskToCompleteCommand PrepareToCompleteTaskCommand { get; private set; }

        public ICommand FinisherTaskCommand
        {
            get => _finisherTaskCommand;
            set
            {
                _finisherTaskCommand = value;
                RaisePropertyChanged();
            }
        }



        //Sets up the command and wires it up with the method to add a new task
        private void PrepareToAddTask()
        {
            SelectedTask = new TaskToComplete();
            SelectedTask.Importance = 3;
            ButtonFinisherText = "Add Task";
            FinisherTaskCommand = new TaskToCompleteCommand(AddNewTask);
            NotEditable = false;
            FinisherButtonsVisibility = System.Windows.Visibility.Visible;
        }

        //Add new task to database and resets the Selected task and UI Elements bound to it
        public void AddNewTask()
        {
            TaskToCompleteDbManagement.AddTaskToComplete(SelectedTask);
            SelectedTask = new TaskToComplete();
            NotEditable = true;
            TaskList = TaskToCompleteDbManagement.LoadTasks();
            FinisherButtonsVisibility = System.Windows.Visibility.Hidden;
        }


        //Sets up the command and wires it up with the method to edit/update the selected task
        private void PrepareToEditTask()
        {
            ButtonFinisherText = "Edit Task";
            FinisherTaskCommand = new TaskToCompleteCommand(EditTask);
            NotEditable = false;
            FinisherButtonsVisibility = System.Windows.Visibility.Visible;

        }

        //Edits the Selected task in the database and resets the UI elements bound to it
        public void EditTask()
        {
            if (SelectedTask != null)
            {
                TaskToCompleteDbManagement.UpdateTaskToComplete(SelectedTask);
                SelectedTask = new TaskToComplete();
                NotEditable = true;
                TaskList = TaskToCompleteDbManagement.LoadTasks();
                FinisherButtonsVisibility = System.Windows.Visibility.Hidden;

            }
        }

        //Sets up the command and wires it up with the method to delete the selected task
        private void PrepareToDeleteTask()
        {
            ButtonFinisherText = "Delete Task";
            FinisherTaskCommand = new TaskToCompleteCommand(DeleteTask);
            NotEditable = true;
            FinisherButtonsVisibility = System.Windows.Visibility.Visible;
        }

        //Deletes the Selected task from the database and resets the UI elements bound to it
        public void DeleteTask()
        {
            if (SelectedTask != null)
            {
                TaskToCompleteDbManagement.DeleteTask(SelectedTask);
                SelectedTask = new TaskToComplete();
                TaskList = TaskToCompleteDbManagement.LoadTasks();
                FinisherButtonsVisibility = System.Windows.Visibility.Hidden;
            }
        }


        //Sets up the command and wires it up with the method to delete the selected task
        private void PrepareMarkAsCompleted()
        {
            ButtonFinisherText = "Completed!";
            FinisherTaskCommand = new TaskToCompleteCommand(MarkAsCompleted);
            NotEditable = true;
            FinisherButtonsVisibility = System.Windows.Visibility.Visible;
        }

        //Marks as completed the Selected task from the database, and updates it and resets the UI elements bound to it
        public void MarkAsCompleted()
        {
            if (SelectedTask != null)
            {
                SelectedTask.Completed = true;
                TaskToCompleteDbManagement.UpdateTaskToComplete(SelectedTask);
                SelectedTask = new TaskToComplete();
                TaskList = TaskToCompleteDbManagement.LoadTasks();
                FinisherButtonsVisibility = System.Windows.Visibility.Hidden;
            }
        }




    }
}
