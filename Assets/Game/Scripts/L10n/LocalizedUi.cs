using System;
using UnityEngine;
using UnityEngine.Events;


namespace Game.Scripts.L10n
{
    public abstract class LocalizedUi : MonoBehaviour
    {
        [SerializeField] protected string _key = "";

        public string Key => _key;

        public abstract string Text { get; set; }
    }
}