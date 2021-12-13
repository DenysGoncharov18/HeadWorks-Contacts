using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Contacts.Services.ServiceSettings
{
    public class SettingsManager : ISettingsManager
    {
        public int Count
        {
            get => Preferences.Get(nameof(Count),5);
            set => Preferences.Set(nameof(Count), value);
        }
    }
}
