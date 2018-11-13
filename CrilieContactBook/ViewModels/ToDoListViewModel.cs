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

        //The Importance of the Selected Task
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

        //Variable used to change the state of the Selected Task's fields and make them editable or read only
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
            PrepareToAddNewTaskCommand = new IntermediaryCommand(PrepareToAddTask);
            PrepareToEditTaskCommand = new IntermediaryCommand(PrepareToEditTask);
            PrepareToDeleteTaskCommand = new IntermediaryCommand(PrepareToDeleteTask);
            PrepareToCompleteTaskCommand = new IntermediaryCommand(PrepareMarkAsCompleted);
            CancelCommand = new IntermediaryCommand(Cancel);
            FinisherButtonsVisibility = Visibility.Hidden;
            SelectedTaskFilter = TaskListFilter.Active;
        }


        //CRUD COMMANDS
        public IntermediaryCommand PrepareToAddNewTaskCommand { get; private set; }
        public IntermediaryCommand PrepareToEditTaskCommand { get; private set; }
        public IntermediaryCommand PrepareToDeleteTaskCommand { get; private set; }
        public IntermediaryCommand PrepareToCompleteTaskCommand { get; private set; }
        public IntermediaryCommand CancelCommand { get; private set; }

        private ICommand _finisherTaskCommand;
        public ICommand FinisherTaskCommand
        {
            get => _finisherTaskCommand;
            set
            {
                _finisherTaskCommand = value;
                RaisePropertyChanged();
            }
        }



        //Sets up the command and wires it up with the method to add a new Task
        private void PrepareToAddTask()
        {
            SelectedTask = new TaskToComplete();
            SelectedTask.Importance = 3;
            ButtonFinisherText = "Add Task";
            FinisherTaskCommand = new IntermediaryCommand(AddNewTask);
            NotEditable = false;
            FinisherButtonsVisibility = System.Windows.Visibility.Visible;
        }

        //Add new Task to database and resets the Selected task and UI Elements bound to it
        public void AddNewTask()
        {
            TaskToCompleteDbManagement.AddTaskToComplete(SelectedTask);
            SelectedTask = new TaskToComplete();
            NotEditable = true;
            TaskList = TaskToCompleteDbManagement.LoadTasks();
            FinisherButtonsVisibility = System.Windows.Visibility.Hidden;
        }


        //Sets up the command and wires it up with the method to edit/update the Selected Task
        private void PrepareToEditTask()
        {
            ButtonFinisherText = "Edit Task";
            FinisherTaskCommand = new IntermediaryCommand(EditTask);
            NotEditable = false;
            FinisherButtonsVisibility = System.Windows.Visibility.Visible;

        }

        //Edits the Selected Task in the database and resets the UI elements bound to it
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

        //Sets up the command and wires it up with the method to delete the Selected Task
        private void PrepareToDeleteTask()
        {
            ButtonFinisherText = "Delete Task";
            FinisherTaskCommand = new IntermediaryCommand(DeleteTask);
            NotEditable = true;
            FinisherButtonsVisibility = System.Windows.Visibility.Visible;
        }

        //Deletes the Selected Task from the database and resets the UI elements bound to it
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


        //Sets up the command and wires it up with the method to delete the Selected Task
        private void PrepareMarkAsCompleted()
        {
            ButtonFinisherText = "Completed!";
            FinisherTaskCommand = new IntermediaryCommand(MarkAsCompleted);
            NotEditable = true;
            FinisherButtonsVisibility = System.Windows.Visibility.Visible;
        }

        //Marks as completed the Selected Task from the database, and updates it and resets the UI elements bound to it
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

        //Cancel - reset current action
        public void Cancel()
        {
            SelectedTask = new TaskToComplete();
            NotEditable = true;
            FinisherButtonsVisibility = Visibility.Hidden;
        }



    }
}
