using System;
using Contacts.Services.Repository;
using Contacts.Services.ServiceSettings;
using Contacts.ViewModels;
using Contacts.Views;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts
{
    public partial class App : PrismApplication
    {
        public App()
        {
        }

        #region ---Overrides---

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());

            //Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync($"{nameof(MainPage)}");
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

    

        protected override void OnResume()
        {
        }
        #endregion
    }
}
