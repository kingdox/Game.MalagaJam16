using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LanguageData
{
    private const SystemLanguage DEFAULT_LANGUAGE = SystemLanguage.Spanish;
    private static readonly Dictionary<SystemLanguage, LanguageEnum> DIC_LANGUAGES = new Dictionary<SystemLanguage, LanguageEnum>()
    {
        [SystemLanguage.English] = LanguageEnum.English,
        [SystemLanguage.Spanish] = LanguageEnum.Spanish,
    };
    public static LanguageEnum GET_LOCALE(this SystemLanguage systemLanguage) => DIC_LANGUAGES.TryGetValue(systemLanguage, out LanguageEnum val) ? val : DIC_LANGUAGES[DEFAULT_LANGUAGE];
}