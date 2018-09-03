using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Atotupe.Tools
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        private const string CurrenciesMode = "currencies_mode_key";
        private static readonly string CurrenciesModeDefault = "usd";

        public static string GeneralSettings
        {
            get => AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(SettingsKey, value);
        }

        public static string CurrenciesModeSettings
        {
            get => AppSettings.GetValueOrDefault(CurrenciesMode, CurrenciesModeDefault);
            set => AppSettings.AddOrUpdateValue(CurrenciesMode, value);
        }
    }
}
