using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Game.Scripts
{
    public class StringDatabase : MonoBehaviour
    {
        private Dictionary<string, int> _items = new();

        [SerializeField] private UnityEvent<string, int> _added;
        [SerializeField] private UnityEvent<string, int> _removed;

        public void Add(string item)
        {
            AddMany(item, 1);
        }

        public void Remove(string item)
        {
            RemoveMany(item, 1);
        }

        public void AddMany(string item, int count)
        {
            if (_items.TryGetValue(item, out int value))
            {
                int total = value + count;
                _items[item] = total;
                _added?.Invoke(item, total);
            }
            else
            {
                _items.Add(item, count);
                _added?.Invoke(item, count);
            }

        }

        public void RemoveMany(string item, int count)
        {
            if (_items.TryGetValue(item, out int value))
            {
                int newValue = value - count;

                if (newValue > 0)
                {
                    _items[item] = newValue;
                    _removed?.Invoke(item, newValue);
                }
                else
                {
                    _items.Remove(item);
                    _removed?.Invoke(item, 0);
                }
            }
        }

        public int Get(string item)
        {
            if (!_items.TryGetValue(item, out int value))
                value = 0;

            return value;
        }

        public bool Check(string item, int count)
        {
            return Get(item) >= count;
        }
    }
}