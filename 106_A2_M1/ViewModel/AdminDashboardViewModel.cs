﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using _106_A2_M1.Model;
using _106_A2_M1.Services;
using _106_A2_M1.View;
using _106_A2_M1.View.AdminFrames;
using _106_A2_M1.View.Pages;

namespace _106_A2_M1.ViewModel
{
    public class AdminDashboardViewModel : ViewModelBase
    {
        private ICommand _userSearchCommand;
        public ICommand LogoutCommand { get; }
        public ICommand NavQRQueueCommand { get; }
        public ICommand NavUserDirectoryCommand { get; }
        public ICommand NavIssueQueueCommand { get; }
        public ICommand MarkAsResolvedCommand { get; }

        private Admin _admin; // Declare an instance of the Admin class MODEL to ViewModel Pipeline
        private ObservableCollection<UserDB> _userList; // Change the type to ObservableCollection<User>
        private ObservableCollection<Issue> _issueList;
        
        

        public ICommand UserSearchCommand
        {
            get
            {
                if (_userSearchCommand == null)
                    _userSearchCommand = new RelayCommand(param => UserDirSearch(), null);

                return _userSearchCommand;
            }
        }
        public ObservableCollection<UserDB> UserList   //MODEL to ViewModel Pipeline
        {
            get => _userList; // Access the ObservableCollection
            set
            {
                _userList = value;
                OnPropertyChanged(nameof(UserList)); // Notify property changed to update the UI
            }
        }
        public ObservableCollection<Issue> IssueList   //MODEL to ViewModel Pipeline
        {
            get => _issueList;  // Access the ObservableCollection
            set
            {
                _issueList = value;
                OnPropertyChanged(nameof(IssueList)); // Notify property changed to update the UI
            }
        }
        

        public AdminDashboardViewModel()
        {
            _admin = new Admin(); // Initialize the Admin instance MODEL to ViewModel Pipeline
            UpdateUserListAsync();
            IssueList = new ObservableCollection<Issue>(_admin.issue_list); // Initialize IssueList
            
            // Set the default frame to UserDirectoryFrame
            FrameTitle = "User Directory";
            CurrentDisplayFrame = new AdminUserDirectoryFrame();
            CurrentDisplayFrame.DataContext = this;

            LogoutCommand = new RelayCommand(x => NavigateToPage(new LoginPage()));
            NavQRQueueCommand = new RelayCommand(x => 
                { 
                    FrameTitle = "QR Code Approval Queue";
                    NavigateToFrame(new AdminQRQueueFrame()); 
                });
            NavUserDirectoryCommand = new RelayCommand(x =>
            {
                FrameTitle = "User Directory";
                NavigateToFrame(new AdminUserDirectoryFrame());
            });
            NavIssueQueueCommand = new RelayCommand(x =>
            {
                FrameTitle = "Issue Report Management";
                NavigateToFrame(new AdminIssueManagementFrame());
            });
            MarkAsResolvedCommand = new RelayCommand(x => MarkAsResolved(x));
        }

        
        private void NavigateToPage(Page destinationPage)
        {
            MainWindowVM mainVM = (MainWindowVM)Application.Current.MainWindow.DataContext;
            mainVM.CurrentDisplayPage = destinationPage;
        }

        private void NavigateToFrame(UserControl destinationFrame)
        {
            CurrentDisplayFrame = destinationFrame;
            CurrentDisplayFrame.DataContext = this;
        }

        public async Task UpdateUserListAsync()
        {
            try
            {
                // Use SingletonClient to get the list of users with a search query through a GET request
                List<UserDB> listOfUsers = await SingletonClient.Instance.GetListOfUsersAsync();

                if (listOfUsers != null && listOfUsers.Count > 0)
                {
                    // Assign the list to the UserList property
                    UserList = new ObservableCollection<UserDB>(listOfUsers);
                }
                else
                {
                    Console.WriteLine("Failed to retrieve the list of users from the backend.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public void UserDirSearch()
        {

        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessageBox.Show("Data Context Working!");
            }
        }

        public void MarkAsResolved(object parameter)
        {
            if (parameter is Issue issue)
            {
                issue.resolve = true;

                OnPropertyChanged(nameof(IssueList));
                NavigateToFrame(new AdminIssueManagementFrame());
            }
        }
    }

}
