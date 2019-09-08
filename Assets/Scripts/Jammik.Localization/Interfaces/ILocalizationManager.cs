using UnityEngine;

namespace Jammik.Localization.Interfaces
{
    public interface ILocalizationManager
    {
        string Get(string localizationKey);
    }
}