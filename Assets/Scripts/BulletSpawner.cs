
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private List<Pattern> patterns = new List<Pattern>();
        private CameraBound _cameraBound;
        private MovementStrategy _movementStrategy;
        private EnemyWeapon _weapon;
        private BulletSpawnerMovement _movement;
        private int curPattern;
        private const float MIN_ANGLE = -90;
        private const float MAX_ANGLE = 90f;
        private float _spinningSpeed;

        //Reset position of Spawner to the upper middle of camera
        private Vector3 offsetFromCamCenterAfterReset;
        private Quaternion rotationAfterReset;
        

        private void UsePattern()
        {
            _movement.SetMovementStrategy(patterns[curPattern].movementStrategy);
            _weapon.SetWeaponStrategy(patterns[curPattern].weaponStrategy);
            if (patterns[curPattern].spawnerType == Pattern.SpawnerType.Spinning)
                _spinningSpeed = patterns[curPattern].spinningSpeed;
            StartCoroutine(SwitchPatternAfter(patterns[curPattern].patternLifetime));
        }

        IEnumerator SwitchPatternAfter(float patternLifetime)
        {
            yield return new WaitForSeconds(patternLifetime);
            ResetSpawnerTransform();
            curPattern++;
            if (curPattern == patterns.Count) curPattern = 0;
            UsePattern();
        }

        private void ResetSpawnerTransform()
        {
            transform.position = _cameraBound.mainCamera.transform.position + offsetFromCamCenterAfterReset;
            transform.rotation = rotationAfterReset;
        }

        private void Start()
        {
            _cameraBound = gameObject.GetComponent<CameraBound>();
            _weapon = gameObject.GetComponent<EnemyWeapon>();
            _movement = gameObject.GetComponent<BulletSpawnerMovement>();
            offsetFromCamCenterAfterReset = transform.position - _cameraBound.mainCamera.transform.position;
            rotationAfterReset = transform.rotation;
            UsePattern();
            
        }

        private void Update()
        {
            if (patterns[curPattern].spawnerType == Pattern.SpawnerType.Spinning)
            {
                if (transform.eulerAngles.z <= MIN_ANGLE || transform.eulerAngles.z >= MAX_ANGLE) _spinningSpeed *= -1; 
                gameObject.transform.eulerAngles = new Vector3(0f,0f,transform.eulerAngles.z+_spinningSpeed);
            }
        }
    }

    [System.Serializable]
    public class Pattern
    { 
        public enum SpawnerType
             {
                 Straight,
                 Spinning
             }
        public MovementStrategy movementStrategy;
        public WeaponStrategy weaponStrategy; 
        public SpawnerType spawnerType;
        public float spinningSpeed;
        public float patternLifetime;
    }
}