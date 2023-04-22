using System;
using Game.Interfaces;
using Game.Scripts.L10n;
using UnityEngine;
using UnityEngine.UI;


namespace Game.UI
{
    public class TargetView : MonoBehaviour, ITargetView
    {
        [SerializeField] private Text _titleText;
        [SerializeField] private Text _descriptionText;
        [SerializeField] private LocalizedUi _titleTextLocalized;
        [SerializeField] private LocalizedUi _descriptionTextLocalized;
        [SerializeField] private GameObject _activeCross;
        [SerializeField] private GameObject _inActiveCross;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;
        
        private bool _isShowed;

        private bool _isActive;

        private void Start()
        {
            Hide();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ITargetView view = this;

                if (view.IsActive) view.Hide(); else view.Show();
            }
        }

        public bool IsShowed => _isShowed;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    var color = value ? _activeColor : _inactiveColor;
                    _titleText.color = color;
                    _descriptionText.color = color;
                }
            }
        }

        public string Title
        {
            get => _titleText.text;
            set => _titleText.text = string.IsNullOrWhiteSpace(value) ? "UNTITLED" : value;
        }

        public string Description
        {
            get => _descriptionText.text;
            set => _descriptionText.text = value ?? string.Empty;
        }

        public void Show()
        {
            if (!_isShowed)
            {
                _titleText.gameObject.SetActive(true);
                _descriptionText.gameObject.SetActive(true);
                _activeCross.SetActive(true);
                _inActiveCross.SetActive(false);
                _isShowed = true;
            }
        }

        public void Hide()
        {
            if (_isShowed)
            {
                _titleText.gameObject.SetActive(false);
                _descriptionText.gameObject.SetActive(false);
                _activeCross.SetActive(false);
                _inActiveCross.SetActive(true);
                _isShowed = false;
            }
        }
    }


}
