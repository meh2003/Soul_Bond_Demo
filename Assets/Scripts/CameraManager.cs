using UnityEngine;

namespace Demo
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; set; }
        [SerializeField] private GameObject mainCam;
        [SerializeField] private GameObject viewCam;

        private static float MaxViewTimer;
        private float viewTimer;
        [SerializeField] private Boss boss;

        private void Start()
        {
            MaxViewTimer = 3f;
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            mainCam.SetActive(true);
            viewCam.SetActive(false);
            viewTimer = MaxViewTimer;
            foreach (var stage in boss.stages)
            {
                stage.onStageCompleted.AddListener(SwitchCamera);
            }
        }

        private void SwitchCamera()
        {
            mainCam.SetActive(!mainCam.activeInHierarchy);
            viewCam.SetActive(!viewCam.activeInHierarchy);
        }

        private void Update()
        {
            if (viewCam.activeInHierarchy)
            {
                Time.timeScale = 0;
                viewTimer -= Time.unscaledDeltaTime;
                if (viewTimer < 0)
                {
                    viewTimer = 0;
                    SwitchCamera();
                }
            }
            else
            {
                Time.timeScale = 1;
                viewTimer = MaxViewTimer;
            }
        }
    }
}