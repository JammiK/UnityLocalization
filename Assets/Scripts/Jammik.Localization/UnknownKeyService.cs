using System;
using Jammik.Localization.Interfaces;

namespace Jammik.Localization
{
    internal class UnknownKeyService : IUnknownKeyService
    {
        readonly UnknownKeyPolitics _politics;
        readonly string _defaultString;

        public UnknownKeyService(UnknownKeyPolitics politics, string defaultString)
        {
            _politics = politics;
            _defaultString = defaultString;
        }

        public string GetUnknownKeyString(string key)
        {
            switch (_politics)
            {
                case UnknownKeyPolitics.DisplayKey:
                    return key;
                case UnknownKeyPolitics.DisplayDefaultString:
                    return _defaultString;
                case UnknownKeyPolitics.DisplayEmptyString:
                    return string.Empty;
                default:
                    throw new ArgumentException($"Unknown {nameof(UnknownKeyPolitics)} '{_politics}'");
            }
        }
    }
}