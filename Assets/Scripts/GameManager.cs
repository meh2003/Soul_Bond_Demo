using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

namespace Demo
{
    public class NewBehaviourScript : MonoBehaviour
    {
        public enum GameState
        {
            Battle,
            Intermission,
            End
        }
        
        [SerializeField] public float patternCooldown;
        [SerializeField] public Camera cam;
        [SerializeField] private Player player;
        [SerializeField] private float cameraSmoothTime = 0.25f;
        [SerializeField] private float cameraVelocity = 0f;

        // Save bullet pattern
        private List<List<string[]>> _bulletPattern;
        private List<string[]> _currentBulletPattern;
        
        private float _zoom;
        private float _timer;
        
        // [SerializeField] private Boss boss;
        private GameState _gameState;

        private void Awake()
        {
            //bulletPattern = GetPattern("boss");
        }

        // Start is called before the first frame update
        void Start()
        {
            _gameState = GameState.Intermission;
            _zoom = cam.orthographicSize;
            _timer = 0;
            _bulletPattern = new List<List<string[]>>();
            _currentBulletPattern = new List<string[]>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void ActiveState()
        {
            switch (_gameState)
            {
                case GameState.Battle:
                    SpawnSnowflake();
                    if (player.GetHealth <= 0) _gameState = GameState.End;
                    break;
                
                case GameState.Intermission:
                    break;
                
                case GameState.End:
                    break;
            }
        }

        void ChangeState()
        {
            
        }

        void SpawnSnowflake()
        {
            _timer += Time.deltaTime;
            // Spawn all the snowflakes 
            while (float.Parse(_currentBulletPattern[0][0]) > _timer)
            {
                // TODO: Spawn bullet on the playfield
                
                // Pop the latest snowflake
                _bulletPattern.RemoveAt(0);
            }
        }

        void CameraZoom(float zoomValue, float zoomLevel, float zoomSpeed)
        {
            _zoom -= zoomSpeed * zoomValue;
            _zoom = Mathf.Clamp(_zoom, 0, zoomLevel);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, _zoom, ref cameraVelocity, cameraSmoothTime);
        }

        void CameraMove()
        {
            
        }

        /// <summary>
        /// Get boss pattern from text file
        /// </summary>
        /// <param name="txtName">text file name</param>
        /// <returns>boss' bullet pattern</returns>
        List<string[]> GetPattern(string txtName)
        {
            string path = "Assets/BossPattern/" + txtName + ".txt";
            var temp = File.ReadAllLines(path).ToList();
            
            // Separate each pattern
            

            // Structure of a line: <time>,<x-coord>,<y-coord>
            List<string[]> pattern = new List<string[]>();
            foreach (string l in temp)
            {
                var line = l.Split(',');
                pattern.Add(line);
            }

            return pattern;
        }
    }
}
