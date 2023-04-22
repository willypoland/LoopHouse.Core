using TMPro;
using UnityEngine;


namespace Game.UI
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private string _keyName;
        [SerializeField] private TMP_Text _cuntText; 

        public string KeyName => _keyName;
        
        public void SetCount(int count)
        {
            _cuntText.text = count.ToString();
        }
    }
}