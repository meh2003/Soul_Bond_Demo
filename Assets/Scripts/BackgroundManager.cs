using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    //Can be merged with game manager
    public class BackgroundManager : MonoBehaviour
    {
        public static BackgroundManager Instance { get; set; }
        [SerializeField] private List<GameObject> backgrounds;
        private int currentBackgroundIndex = 0;
        [SerializeField] private Boss boss;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            foreach (var background in backgrounds)
            {
                background.SetActive(false);
            }
            backgrounds[currentBackgroundIndex].SetActive(true);
            boss.onViewSwitch.AddListener(SwitchBackground);
        }

        private void SwitchBackground()
        {
            backgrounds[currentBackgroundIndex].SetActive(false);
            currentBackgroundIndex++;
            backgrounds[currentBackgroundIndex].SetActive(true);
        }
    }
}