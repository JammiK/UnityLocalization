using UnityEngine;

namespace Jammik.Localization.Interfaces
{
    public interface ILanguageProvider
    {
        SystemLanguage GetLanguage();
    }
}