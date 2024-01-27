using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Demo
{
    public class Player : Damageable, IDamageable
    {
        [SerializeField] private int maxShield;
        private int shield;
        private InputReader input;
        [SerializeField] private Collider2D playerCollider;
        private const float Iframe = 2f;
        
         [SerializeField] private int maxMana;
         [SerializeField] private int manaRegenRate;
         private int mana;
         
         public enum ManaState
         {
             Full,
             NotFull
         }
         public float GetManaNormalized() => mana / (float)maxMana;
         public void SetMana(int amount) => mana = amount;
         public ManaState manaState;
         private const double Tolerance = 0.01;
         public UnityEvent onFullMana;
         public UnityEvent onDefeat;

         protected override void Die()
         {
             
         }

        public void GainShield(int amount)
        {
            shield += amount;
            if (shield >= maxShield) shield = maxShield;
        }

        private void UseShield()
        {
            if (shield > 0)
            {
                shield--;
                TriggerIframe(0);
            }
        }

        IEnumerator DisableIframe()
        {
            yield return new WaitForSeconds(Iframe);
            playerCollider.enabled = true;
        }

        private void TriggerIframe(int damage)
        {
            playerCollider.enabled = false;
            StartCoroutine(DisableIframe());
        }

        private void Start()
        {
            input = gameObject.GetComponent<InputReader>();
            onTakingDamage.AddListener(TriggerIframe);
            mana = 0;
            StartCoroutine(GainMana());
        }

        public void Update()
        {
            if (input.Shield) UseShield();
            manaState = Math.Abs(mana - maxMana) < Tolerance ? ManaState.Full : ManaState.NotFull;
            if (manaState == ManaState.Full) onFullMana.Invoke();
        }

        IEnumerator GainMana()
        {
            while (true)
            {
                mana += manaRegenRate;
                if (mana > maxMana) mana = maxMana;
                yield return new WaitForSeconds(1f);
            }
        }
    }
}