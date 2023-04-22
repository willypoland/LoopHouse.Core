using System;
using System.Collections.Generic;
using System.Linq;
using Game.Interfaces;
using UnityEngine;
using UnityEngine.Events;


namespace Game.Scripts
{
    public class SimpleInteractive : MonoBehaviour, IInteractive
    {
        [Serializable]
        public class RequiredItem
        {
            public string KeyName;
            [Min(1)] public int Count = 1;
        }


        [SerializeField] private string _title;
        [SerializeField] private string _description = string.Empty;
        [SerializeField] private UnityEvent _useEvent;
        [SerializeField] private bool _canUse = true;
        [SerializeField] private bool _resultAfterUse = true;
        [SerializeField] private StringDatabase _database;
        [SerializeField] private List<RequiredItem> _requiredItems = new();
        [SerializeField] private bool _removeRequiredAfterUse = false;

        public string Title => _title;
        public string Description => _description;

        public bool CanUse(object user)
        {
            return ShouldDbUse() ? ValidateDb() : _canUse;
        }

        public bool Use(object user)
        {
            return ShouldDbUse() ? DbUse(user) : SimpleUse(user);
        }

        private bool ValidateDb()
        {
            return _requiredItems.All(item => _database.Check(item.KeyName, item.Count));
        }

        private bool DbUse(object user)
        {
            if (!ValidateDb())
                return false;

            if (_removeRequiredAfterUse)
            {
                foreach (var item in _requiredItems)
                    _database.RemoveMany(item.KeyName, item.Count);
            }

            _useEvent?.Invoke();
            return true;
        }

        private bool ShouldDbUse()
        {
            return _database != null && _requiredItems != null && _requiredItems.Count > 0;
        }

        private bool SimpleUse(object user)
        {
            if (_canUse)
            {
                _useEvent?.Invoke();
                return _resultAfterUse;
            }

            return _resultAfterUse;
        }

        private void Reset()
        {
            _title = name;
            _database = FindObjectOfType<StringDatabase>();
        }
    }
}