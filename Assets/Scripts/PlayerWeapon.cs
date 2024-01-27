using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Demo
{
    public class PlayerWeapon : Weapon {
        private InputReader _input;
        private float _fireTimer;
        public UnityEvent onFire;

        private const int MaxLevel = 3;
        private int _level;
        

        [SerializeField] private WeaponStrategy level2WeaponStrategy;
        [SerializeField] private WeaponStrategy level3WeaponStrategy;
        
        [SerializeField] private WeaponStrategy secondaryWeaponStrategy;
        private Player _player;
        private UnityEvent onUpgrade;
        void Awake()
        {
            _input = GetComponent<InputReader>();
            _player = GetComponent<Player>();
            _level = 1;
            onUpgrade.AddListener(SwitchWeaponStrategy);
        }

        private void SwitchWeaponStrategy()
        {
            switch (_level)
            {
                case 1:
                    SetWeaponStrategy(weaponStrategy);
                    break;
                case 2:
                    SetWeaponStrategy(level2WeaponStrategy);
                    break;
                case 3:
                    SetWeaponStrategy(level3WeaponStrategy);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        
        void Update() {
            _fireTimer += Time.deltaTime;
            
            if (_input.Fire && _fireTimer >= weaponStrategy.FireRate) {
                weaponStrategy.Fire(firePoint, layer);
                onFire.Invoke();
                _fireTimer = 0f;
            }

            if (_input.SecondaryFire && _player.manaState == Player.ManaState.Full)
            {
                secondaryWeaponStrategy.Fire(firePoint, layer);
                onFire.Invoke();
                _player.SetMana(0);
            }
        }

        public void Upgrade(int amount)
        {
            _level += amount;
            if (_level >= MaxLevel) _level = MaxLevel;
            onUpgrade?.Invoke();
        }
    }
}
