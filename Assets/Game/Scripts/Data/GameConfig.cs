using UnityEngine;


namespace Game.Scripts.L10n
{
    [CreateAssetMenu(menuName = "Game/Config", fileName = "GameConfig")]
    public sealed class GameConfig : ScriptableObject
    {
        [SerializeField] private string _defaultLanguage = "ru";
        [SerializeField] private string _localizationFolder = "Localization/";

        public string DefaultLanguage => _defaultLanguage;
        public string LocalizationFolder => _localizationFolder;
    }
}