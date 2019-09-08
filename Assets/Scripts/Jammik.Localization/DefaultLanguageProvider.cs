using Jammik.Localization.Interfaces;
using UnityEngine;

namespace Jammik.Localization
{
    public class DefaultLanguageProvider : ILanguageProvider
    {
        public SystemLanguage GetLanguage()
        {
            return Application.systemLanguage;
        }
    }
}