using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Game.UI
{
    public class ItemPanelView : MonoBehaviour
    {
        private Dictionary<string, ItemView> _items;

        private void Start()
        {
            _items = GetComponentsInChildren<ItemView>(true).ToDictionary(x => x.KeyName, x => x);
        }

        public void Added(string item, int count)
        {
            var view = _items[item];
            if (count > 0)
                view.gameObject.SetActive(true);
            view.SetCount(count);
        }

        public void Removed(string item, int count)
        {
            var view = _items[item];
            if (count <= 0)
                view.gameObject.SetActive(false);
            else
                view.SetCount(count);
        }
    }
}

