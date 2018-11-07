using CrilieContactBook.Model;
using CrilieContactBook.ViewModels.Commands;
using System.Collections.ObjectModel;
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

        private TaskImportance selectedTaskImportance;

        public TaskImportance SelectedTaskImportance
        {
            get { return selectedTaskImportance; }
            set { selectedTaskImportance = value; }
        }

        //Variable used to change the state of the SelectedTask's fields and make them editable or read only
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
        private bool _notEditable = true;
        private ICommand _finisherTaskCommand;

        public string ButtonFinisherText
        {
            get { return buttonFinisherText; }
            set
            {
                buttonFinisherText = value;
                RaisePropertyChanged();
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
        }



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
        }

        //Add new task to database and resets the Selected task and UI Elements bound to it
        public void AddNewTask()
        {
            TaskToCompleteDbManagement.AddTaskToComplete(SelectedTask);
            SelectedTask = new TaskToComplete();
            NotEditable = true;
            TaskList = TaskToCompleteDbManagement.LoadTasks();
        }


        //Sets up the command and wires it up with the method to edit/update the selected task
        private void PrepareToEditTask()
        {         
            ButtonFinisherText = "Edit Task";
            FinisherTaskCommand = new TaskToCompleteCommand(EditTask);
            NotEditable = false;
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
            }
        }

        //Sets up the command and wires it up with the method to delete the selected task
        private void PrepareToDeleteTask()
        {
            ButtonFinisherText = "Delete Task";
            FinisherTaskCommand = new TaskToCompleteCommand(DeleteTask);
            NotEditable = true;
        }

        //Deletes the Selected task from the database and resets the UI elements bound to it
        public void DeleteTask()
        {
            if (SelectedTask != null)
            {
                TaskToCompleteDbManagement.DeleteTask(SelectedTask);
                SelectedTask = new TaskToComplete();
                TaskList = TaskToCompleteDbManagement.LoadTasks();
            }
        }


        //Sets up the command and wires it up with the method to delete the selected task
        private void PrepareMarkAsCompleted()
        {
            ButtonFinisherText = "Completed!";
            FinisherTaskCommand = new TaskToCompleteCommand(MarkAsCompleted);
            NotEditable = true;
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
            }
        }

    }
}
