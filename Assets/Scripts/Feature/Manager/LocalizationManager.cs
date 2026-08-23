using UnityEngine.Localization.Settings;

namespace Jing.Tools.Localization
{
    public static class LocalizationManager
    {
        public static string GetLocalization(string table, string key)
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString(table, key);
        }

    }
}
