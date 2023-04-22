using UnityEngine;
using UnityEngine.Rendering;


namespace Game.Scripts
{
    public class VSyncSwitch : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                QualitySettings.vSyncCount = QualitySettings.vSyncCount == 0 ? 1 : 0;
            }
        }
    }
}
