using UnityEngine;

namespace Jammik.Localization.Interfaces
{
    public interface ILocalizationManager
    {
        void ChangeLanguage(SystemLanguage language);
        string Get(string localizationKey);
    }
}