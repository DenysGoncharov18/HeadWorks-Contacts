using Prism.Mvvm;
using Prism.Navigation;
using Contacts.Models;
using Contacts.Services.Repository;
using Contacts.Services.ServiceSettings;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using System.ComponentModel;
using Xamarin.Forms;


namespace Contacts.ViewModels
{
    class MainPageViewModel : BindableBase, IInitializeAsync    
    {
        private readonly ISettingsManager _settingsManager;
        private IRepository _repository;

        public MainPageViewModel(ISettingsManager settingsManager, IRepository repository)
        {
            _settingsManager = settingsManager;
            _repository = repository;
           
        }

        #region ---Public Properties---

        public ICommand AddButtonTapComand => new Command(OnButtonTap);
        public ICommand DeleteTapComand => new Command(OnDeleteTap);
        public ICommand UpdateTapComand => new Command(OnUpdateTap);
       
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private ProfileModel _selelctedItem;
        public ProfileModel SelectedItem
        {
            get => _selelctedItem;
            set => SetProperty(ref _selelctedItem, value);
        }

        public ObservableCollection<ProfileModel> _profileList;

        public ObservableCollection<ProfileModel> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
        }

        #endregion

        #region ---Public Methods--- 

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            var profileList = await _repository.GetAllAsync<ProfileModel>();
            ProfileList = new ObservableCollection<ProfileModel>(profileList);
        }

        #endregion

        #region ---Overrides---

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
                if (args.PropertyName == nameof(SelectedItem)) 
            {
                FirstName = SelectedItem.FisrtName;
                LastName = SelectedItem.LastName;
            }
        }

        #endregion

        #region ---Private Helpers---

        private async void OnButtonTap(object obj)
        {
            var profile = new ProfileModel()
            {
                FisrtName = FirstName,
                LastName = LastName,
                CreationTime = DateTime.Now
            };
            var id = await _repository.InsertAsync(profile);
            profile.Id = id;

            ProfileList.Add(profile);
        }
        private async void OnDeleteTap()
        {
            if (SelectedItem != null)
            {
                var confirmConfig = new ConfirmConfig()
                {
                    Message = "Are You really want to delete Profile?",
                    OkText = "Deleting complit",
                    CancelText = "Cancel"
                };

                var confirm = await UserDialogs.Instance.ConfirmAsync(confirmConfig);

                if (confirm)
                {
                    await _repository.DeleteAsync(SelectedItem);
                    ProfileList.Remove(SelectedItem);
                }
            }

        }

        private async void OnUpdateTap()
        {
            if (SelectedItem != null)
            {
                var profile = new ProfileModel()
                {
                    Id = SelectedItem.Id,
                    FisrtName = FirstName,
                    LastName = LastName,
                    CreationTime = DateTime.Now
                };
                var index = ProfileList.IndexOf(SelectedItem);
                ProfileList.Remove(SelectedItem);

                await _repository.UpdateAsync(profile);
                ProfileList.Insert(index, profile);                
            }
        }
        #endregion

    }
}
