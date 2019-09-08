using System.Collections.Generic;

namespace Jammik.Localization.Interfaces
{
    public interface ILocalizationDictionaryParser
    {
        IDictionary<string, string> Parse(string value);
    }
}