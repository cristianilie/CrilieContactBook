using CrilieContactBook.Model;
using CrilieContactBook.ViewModels.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CrilieContactBook.ViewModels
{
    public class ToDoListViewModel : VM_Base<TaskToComplete>
    {
        public override TaskToComplete SelectedItem
        { get => base.SelectedItem;
            set
            {
                base.SelectedItem = value;
                if(SelectedItem != null)
                    SelectedItemImportance = (TaskImportance)SelectedItem.Importance;
            }
        }

        //The Importance of the Selected Task
        private TaskImportance selectedItemImportance;
        public TaskImportance SelectedItemImportance
        {
            get => selectedItemImportance;
            set
            {
                selectedItemImportance = value;
                RaisePropertyChanged();
            }
        }


        //Task List Filter - filters the tasklist by TaskListFilters enum and displays All/Active/Completed/Failed tasks
        private TaskListFilter selectedItemFilter;
        public TaskListFilter SelectedItemFilter
        {
            get { return selectedItemFilter; }
            set {
                selectedItemFilter = value;
                RaisePropertyChanged();

                switch (SelectedItemFilter)
                {
                    case TaskListFilter.All:
                        ItemsList = DbHandler<TaskToComplete>.LoadElements();
                        break;
                    case TaskListFilter.Active:
                        ItemsList = new ObservableCollection<TaskToComplete>(DbHandler<TaskToComplete>.LoadElements().Where(x => ((DateTime)x.Deadline.Date >= DateTime.Now.Date) && ((bool)x.Completed == false)));
                        break;
                    case TaskListFilter.Completed:
                        ItemsList = new ObservableCollection<TaskToComplete>(DbHandler<TaskToComplete>.LoadElements().Where(x => (bool)x.Completed == true));
                        break;
                    case TaskListFilter.Failed:
                        ItemsList = new ObservableCollection<TaskToComplete>(DbHandler<TaskToComplete>.LoadElements().Where(x => ((DateTime)x.Deadline.Date < DateTime.Now.Date) && (bool)x.Completed == false));
                        break;
                    default:
                        break;
                }
            }
        }


        //Default constructor
        public ToDoListViewModel()
        {
            ItemsList = DbHandler<TaskToComplete>.LoadElements();
            PrepareToAddNewItemCommand = new IntermediaryCommand(PrepareToAddItem);
            PrepareToEditItemCommand = new IntermediaryCommand(PrepareToEditItem);
            PrepareToDeleteItemCommand = new IntermediaryCommand(PrepareToDeleteItem);
            PrepareToFinishActionCommand = new IntermediaryCommand(PrepareMarkAsCompleted);
            CancelCommand = new IntermediaryCommand(Cancel);
            ConfirmActionVisibility = Visibility.Hidden;
            SelectedItemFilter = TaskListFilter.Active;
            SelectedItemImportance = TaskImportance.Average;
        }


        //Sets up the command and wires it up with the method to add a new item to the TaskToComplete db table
        public override void PrepareToAddItem()
        {
            SelectedItem = new TaskToComplete();
            //SelectedItem.Importance = 3;
            ButtonFinisherText = "Add Task";
            FinisherCommand = new IntermediaryCommand(AddItem);
            NotEditable = false;
            ConfirmActionVisibility = System.Windows.Visibility.Visible;
            SelectedItem = new TaskToComplete()
            {
                Deadline = DateTime.Now
            };
        }

        //Adds a new item to the TaskToComplete db table
        public override void AddItem()
        {
            if (CheckDate(SelectedItem.Deadline))
            {
                SelectedItem.Importance = (int)SelectedItemImportance;
                SelectedItem.Completed = false;
                DbHandler<TaskToComplete>.AddItem(SelectedItem);
            }

            SelectedItem = new TaskToComplete()
            {
                Deadline = DateTime.Now
            };

            ItemsList = DbHandler<TaskToComplete>.LoadElements();
            SelectedItemFilter = TaskListFilter.Active;
            NotEditable = true;
            ConfirmActionVisibility = System.Windows.Visibility.Hidden;
        }

        //Sets up the command and wires it up with the method to edit/update the Selected Item
        public override void PrepareToEditItem()
        {
            ButtonFinisherText = "Edit Task";
            FinisherCommand = new IntermediaryCommand(EditItem);
            NotEditable = false;
            ConfirmActionVisibility = System.Windows.Visibility.Visible;
        }

        //Edits/updates the Selected Item in the TaskToComplete db table
        public override void EditItem()
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.Importance != (int)SelectedItemImportance)
                    SelectedItem.Importance = (int)SelectedItemImportance;

                DbHandler<TaskToComplete>.UpdateItem(SelectedItem);
                SelectedItem = new TaskToComplete();
                NotEditable = true;
                ItemsList = DbHandler<TaskToComplete>.LoadElements();
                SelectedItemFilter = TaskListFilter.Active;
                ConfirmActionVisibility = System.Windows.Visibility.Hidden;
            }
        }

        //Sets up the command and wires it up with the method to delete the Selected Item
        public override void PrepareToDeleteItem()
        {
            if (SelectedItem != null)
            {
                ButtonFinisherText = "Delete Task";
                FinisherCommand = new IntermediaryCommand(DeleteItem);
                NotEditable = true;
                ConfirmActionVisibility = System.Windows.Visibility.Visible;
            }
        }

        //Deletes the Selected Item(the selected TaskToComplete) from the TaskToComplete db table
        public override void DeleteItem()
        {
            if (SelectedItem != null)
            {
                DbHandler<TaskToComplete>.DeleteItem(SelectedItem);
                SelectedItem = new TaskToComplete();
                ItemsList = DbHandler<TaskToComplete>.LoadElements();
                SelectedItemFilter = TaskListFilter.Active;
                ConfirmActionVisibility = System.Windows.Visibility.Hidden;
            }
        }


        //Sets up the command and wires it up with the method to "mark as completed" the Selected Item
        private void PrepareMarkAsCompleted()
        {
            ButtonFinisherText = "Completed!";
            FinisherCommand = new IntermediaryCommand(MarkAsCompleted);
            NotEditable = true;
            ConfirmActionVisibility = System.Windows.Visibility.Visible;
        }

        //Marks as completed the Selected Item(TaskToComplete), updates it in the database and resets the UI elements bound to it
        public void MarkAsCompleted()
        {
            if (SelectedItem != null)
            {
                SelectedItem.Completed = true;
                DbHandler<TaskToComplete>.UpdateItem(SelectedItem);
                SelectedItem = new TaskToComplete();
                ItemsList = DbHandler<TaskToComplete>.LoadElements();
                SelectedItemFilter = TaskListFilter.Active;
                ConfirmActionVisibility = System.Windows.Visibility.Hidden;
            }
        }



        //Checks if the current selected Date in the datetime picker is valid
        private bool CheckDate(DateTime dateToCkeck)
        {
            if (ButtonFinisherText.Equals("Add Task") && dateToCkeck.Date >= DateTime.Now.Date)
            {
                ConfirmActionVisibility = System.Windows.Visibility.Visible;
                return true;
            }

            if (ButtonFinisherText.Equals("Edit Task") 
                && dateToCkeck.Date >= DateTime.Now.Date 
                && dateToCkeck.Date <= (DateTime.Now.AddYears(100).Date))
                {
                    ConfirmActionVisibility = System.Windows.Visibility.Visible;
                    return true;
                } 

            ConfirmActionVisibility = System.Windows.Visibility.Hidden;
            return false;
        }

    }
}
