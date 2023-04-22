using System;
using Game.Interfaces;
using Game.Scripts.L10n;
using Game.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using VContainer;


namespace Game.Scripts
{
    public class SimpleUser : MonoBehaviour
    {
        private const string InteractActionName = "Interact";
        
        [SerializeField] public InputActionAsset _input;
        [SerializeField] public TargetView _targetView;
        [SerializeField] private float _useDistance = 2f;
        [SerializeField] private LayerMask _useMask;

        private Camera _camera;
        private IInteractive _interactive = null;
        private LocalizationService _localization;
        
        [Inject]
        public void InjectParameters(LocalizationService localization)
        {
            _localization = localization;
        }

        private void OnEnable()
        {
            Debug.Assert(_input != null, "Input not initialized.");
            var action = _input[InteractActionName];
            Debug.Assert(action != null, $"Input action asset not contain '{InteractActionName}'.");
            
            action.started += ActionOnPerformed;
            action.Enable();
        }

        private void OnDisable()
        {
            var action = _input[InteractActionName];
            Debug.Assert(action != null, "Input not initialized.");
            action.started -= ActionOnPerformed;
            action.Disable();
        }

        private void ActionOnPerformed(InputAction.CallbackContext obj)
        {
            if (_interactive != null && _targetView.IsActive)
            {
                var res = _interactive.Use(obj);

                if (!res)
                {
                    _targetView.IsActive = res;
                    _targetView.Description = "Вы обосрались";
                }
            }
        }

        private void Update()
        {
            var interactive = FindInteractive();

            if (!ReferenceEquals(_interactive, interactive))
            {
                if (interactive == null)
                {
                    _targetView.Hide();
                }
                else
                {
                    UpdateTargetViewInfo(interactive);
                    _targetView.Show();
                }
            }

            _interactive = interactive;
        }

        private void UpdateTargetViewInfo(IInteractive interactive)
        {
            _targetView.IsActive = interactive.CanUse(this);
            _targetView.Title = _localization.Get(interactive.Title);
            _targetView.Description = _localization.Get(interactive.Description);
        }

        private IInteractive FindInteractive()
        {
            _camera ??= Camera.main;
            var ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));

            if (Physics.Raycast(ray, out RaycastHit hit, _useDistance, _useMask.value))
            {
                var interactive = hit.collider.GetComponent<IInteractive>();
                return interactive;
            }

            return null;
        }

        private void UseAction()
        {
            
        }
    }
}