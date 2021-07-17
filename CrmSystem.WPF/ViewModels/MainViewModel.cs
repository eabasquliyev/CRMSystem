﻿using System;
using System.Windows.Input;
using CrmSystem.Domain;
using CrmSystem.Domain.Models;
using CrmSystem.WPF.Helpers;

namespace CrmSystem.WPF.ViewModels
{
    public class MainViewModel:ObservableObject
    {
        // view models

        private ContactsViewModel _contactsViewModel;
        private AddEditContactViewModel _addEditContactViewModel;
        private SettingsViewModel _settingsViewModel;
        private CompanySetupViewModel _companySetupViewModel;
        private EmployeesViewModel _employeesViewModel;

        public Employee LoggedUser => App.LoggedUser;

        private IUnitOfWork _unitOfWork;

        private ObservableObject _currentViewModel;

        public MainViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _contactsViewModel = new ContactsViewModel(_unitOfWork);
            _contactsViewModel.CreateContactClicked += NavToAddEditContact;
            
            _addEditContactViewModel = new AddEditContactViewModel(_unitOfWork);
            _addEditContactViewModel.SaveOrCancelClicked += NavToContacts;

            _settingsViewModel = new SettingsViewModel();
            _settingsViewModel.UsersButtonClicked += NavToEmployeesView;
            _companySetupViewModel = new CompanySetupViewModel(_unitOfWork);
            _companySetupViewModel.InitialSetupCompleted += OnInitialSetupCompleted;

            _employeesViewModel = new EmployeesViewModel(_unitOfWork);

            ContactsClickCommand = new RelayCommand(NavToContacts);
            SettingsClickCommand = new RelayCommand(NavToSettings);
            LogoutClickCommand = new RelayCommand(Logout);
        }

        private void Logout()
        {
            App.LoggedUser = null;
            App.Company = null;
            LogoutEvent?.Invoke();
        }

        private void NavToEmployeesView()
        {
            CurrentViewModel = _employeesViewModel;
        }

        private void OnInitialSetupCompleted()
        {
            CurrentViewModel = null;
        }

        private void NavToSettings()
        {
            CurrentViewModel = _settingsViewModel;
        }

        private void NavToAddEditContact(object? sender, AddEditContactEventArgs e)
        {
            _addEditContactViewModel.EditMode = e.EditMode;

            _addEditContactViewModel.Contact = e.Contact;


            CurrentViewModel = _addEditContactViewModel;
        }
        public ICommand ContactsClickCommand { get; set; }
        public ICommand SettingsClickCommand { get; set; }
        public ICommand LogoutClickCommand { get; set; }

        public event Action LogoutEvent;
        public ObservableObject CurrentViewModel
        {
            get => _currentViewModel;
            set => base.SetProperty(ref _currentViewModel, value);
        }
        private void NavToContacts()
        {
            CurrentViewModel = _contactsViewModel;
        }

        public void InitialSetup()
        {
            if (App.Company == null)
            {
                CurrentViewModel = _companySetupViewModel;
            }
        }

        public void ViewLoad()
        {
            CurrentViewModel = null;
        }
    }
}