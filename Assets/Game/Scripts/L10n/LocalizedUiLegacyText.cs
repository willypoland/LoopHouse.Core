using UnityEngine;
using UnityEngine.UI;


namespace Game.Scripts.L10n
{
    public sealed class LocalizedUiLegacyText : LocalizedUi
    {
        [SerializeField] private Text _uiText;

        public override string Text
        {
            get => _uiText.text;
            set => _uiText.text = value;
        }
    }
}