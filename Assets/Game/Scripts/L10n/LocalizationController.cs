using UnityEngine;
using VContainer;


namespace Game.Scripts.L10n
{
    public class LocalizationController : MonoBehaviour
    {
        private LocalizationService _service;
        private LocalizedUi[] _texts;
        private GameConfig _config;

        [Inject]
        public void InjectParameters(LocalizationService service, GameConfig config)
        {
            _service = service;
            _config = config;
        }

        private void Start()
        {
            _texts = FindObjectsOfType<LocalizedUi>(true);
            ChangeLanguage(_config.DefaultLanguage);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha1))
                ChangeLanguage("ru");
            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha2))
                ChangeLanguage("en");
        }

        private void ChangeLanguage(string lang)
        {
            _service.ChangeLanguage(lang);

            foreach (var text in _texts)
                text.Text = _service.Get(text.Key);
            
            Debug.Log($"Language changed -> {lang}");
        }
    }
}