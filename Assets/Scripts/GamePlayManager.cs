using System;
using System.Collections;
using UnityEngine;
using Eflatun.SceneReference;
using UnityEngine.Events;

namespace Demo
{
    // public enum GameState
    // {
    //     MainMenu,
    //     InGame,
    //     Paused,
    //     Victory,
    //     Defeat
    // }

    public class GamePlayManager : MonoBehaviour
    {
        [SerializeField] private SceneReference mainMenuScene;
        private GameObject gameOverUI;
        private float restartTimer = 5f;
        // public static event Action<GameState> OnGameStateChanged;
        public static GamePlayManager Instance { get; private set; }
        
        // private GameState _gameState;
        public Player Player { get; private set; }
        public Boss Boss { get; private set; }
        public static Camera MainCamera { get; private set; }
        public void AddScore(int amount) => Score += amount;
        public int Score { get; private set; }

        public bool IsGameOver() => Player.GetHealthNormalized() <= 0 || Boss.GetBossHealthNormalized() <= 0;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
                // DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }

            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            Boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
            gameOverUI = GameObject.Find("GameOver");
            gameOverUI.SetActive(false);
            if (Boss != null)
            {
                foreach (var stage in Boss.stages)
                {
                    stage.onStageCompleted.AddListener(DestroyProjectiles);
                }
            }

            MainCamera = Camera.main;

            //Set the game state to main menu on start
            // UpdateGameState(GameState.MainMenu);
        }

        private void Update()
        {
            if (IsGameOver())
            {
                restartTimer -= Time.deltaTime;
                // UpdateGameState(GameState.MainMenu);
                if (gameOverUI.activeSelf == false)
                {
                    gameOverUI.SetActive(true);
                }

                StartCoroutine(BackToMainMenu());
            }
        }

        IEnumerator BackToMainMenu()
        {
            yield return new WaitForSeconds(restartTimer);

            DestroyProjectiles();
            Loader.Load(mainMenuScene);
            this.enabled = false;
        }

        public static void DestroyProjectiles()
        {
            var projectiles = GameObject.FindGameObjectsWithTag("Projectile");
            foreach (var projectile in projectiles)
            {
                Destroy(projectile);
            }
        }
    }
}