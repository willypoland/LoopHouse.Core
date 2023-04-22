using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using VContainer.Unity;


namespace Game.Scripts.L10n
{
    public class LocalizationService
    {
        private readonly GameConfig _config;
        private Dictionary<string, string> _dictionary;
        private string _language;

        public LocalizationService(GameConfig config)
        {
            _config = config;
        }

        public string Language => _language;

        public string Get(string key)
        {
            if (_language == null || _dictionary == null)
                throw new Exception("Localized text dictionary not loaded");

            if (!_dictionary.TryGetValue(key, out var text))
                text = "KEY_NOT_EXIST:" + key;

            return text;
        }

        public string Get(string key, params object[] args)
        {
            var text = Get(key);
            return string.Format(CultureInfo.InvariantCulture, text, args);
        }

        public void ChangeLanguage(string language)
        {
            Dictionary<string, string> newDictionary;
            string newLanguage;
            try
            {
                var json = File.ReadAllText(GetPath(language));
                newDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                newLanguage = language;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                newLanguage = _language;
                newDictionary = _dictionary;
            }
            
            _language = newLanguage;
            _dictionary = newDictionary;
        }
        
        private string GetPath(string lang)
        {
            var file = Path.ChangeExtension(lang, ".json");
            return Path.Combine(Application.dataPath, _config.LocalizationFolder, file);
        }
    }
}