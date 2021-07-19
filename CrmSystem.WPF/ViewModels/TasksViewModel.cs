﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows.Input;
using CrmSystem.Domain;
using CrmSystem.Domain.Models;
using CrmSystem.WPF.Helpers;

namespace CrmSystem.WPF.ViewModels
{
    public class AddEditTaskEventArgs : EventArgs
    {
        public bool EditMode { get; set; }
        public BaseTask Task { get; set; }
    }

    public class TasksViewModel:ObservableObject
    {
        private readonly IUnitOfWork _unitOfWork;
        private BaseTask _selectedTask;
        private ObservableCollection<BaseTask> _tasks;

        public TasksViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CreateTaskClickCommand = new RelayCommand(CreateTaskClick);
            EditContactClickCommand = new RelayCommand(EditTaskClicked);
        }
        
        private void CreateTaskClick()
        {
            CreateTasksClicked?.Invoke(this, new AddEditTaskEventArgs()
            {
                EditMode = false,
                Task = new ContactTask()
            });
        }

        private void EditTaskClicked()
        {
            CreateTasksClicked?.Invoke(this, new AddEditTaskEventArgs()
            {
                EditMode = true,
                Task = SelectedTask
            });
        }

        public BaseTask SelectedTask
        {
            get => _selectedTask;
            set => base.SetProperty(ref _selectedTask, value);
        }

        public ICommand CreateTaskClickCommand { get; set; }
        public ICommand EditContactClickCommand { get; set; }
        public ICommand ContactInfoClickCommand { get; set; }

        public event EventHandler<AddEditTaskEventArgs> CreateTasksClicked;

        public ObservableCollection<BaseTask> Tasks
        {
            get => _tasks;
            set => base.SetProperty(ref _tasks, value);
        }


        public void LoadTasks()
        {
            Tasks = new ObservableCollection<BaseTask>(_unitOfWork.ContactTasks.Find(ct => ct.Contact.Company.Id == App.Company.Id).ToList());
        }
    }
}