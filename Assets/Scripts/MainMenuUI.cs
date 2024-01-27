using Eflatun.SceneReference;
using UnityEngine;
using Utilities;
using Button = UnityEngine.UI.Button;

namespace Demo
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] SceneReference startingLevel;
        [SerializeField] Button playButton;
        [SerializeField] private Button settingButton;
        [SerializeField] Button quitButton;

        void Awake()
        {
            playButton.onClick.AddListener(LoadStartingLevel);
            quitButton.onClick.AddListener(QuitGame);
            Time.timeScale = 1f;
        }

        public void LoadStartingLevel()
        {
            Loader.Load(startingLevel);
        }

        public void QuitGame()
        {
            Helpers.QuitGame();
        }
    }
}