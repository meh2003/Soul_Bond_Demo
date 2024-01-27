using System;
using UnityEngine;

namespace Demo
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Path path;
        [SerializeField] private Boss boss;
        
        [SerializeField] private Transform startPoint;
        [SerializeField] private InputReader input;
        void Start()
        {
            gameObject.transform.position = startPoint.position;
            path = gameObject.GetComponent<Path>();
            foreach (var stage in boss.stages)
            {
                stage.onStageCompleted.AddListener(path.MoveToNextPoint);
            }
            boss.onViewSwitch.AddListener(path.ResetPositionAfterViewSwitch);
            boss.onViewSwitch.AddListener(path.StartMoving);
            boss.stages[boss.stageBeforeSwitch].onStageCompleted.AddListener(path.StopMoving);
        }

        private void Update()
        { 
            
        }
    }
}