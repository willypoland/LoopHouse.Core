using System;
using TMPro;
using UnityEngine;


namespace Game.Scripts.L10n
{
    public sealed class LocalizedUiTMPText : LocalizedUi
    {
        [SerializeField] private TMP_Text _uiText;

        public override string Text 
        {
            get => _uiText.text;
            set => _uiText.text = value;
        }

        private void Reset()
        {
            _uiText = GetComponent<TMP_Text>();
        }
    }
}