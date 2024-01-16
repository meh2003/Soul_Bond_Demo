using System;
using System.Collections.Generic;
using System.Linq;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;

namespace Demo
{
    public class BossStage : MonoBehaviour
    {
        [SerializeField] private List<BossPart> partSystem;
        private int stageHealth;
        public List<BossPart> PartSystem => partSystem;
        public UnityEvent onCrossingDamageThreshold;
        [SerializeField] private int damageStages;

        private int healthPerStage;
        private int healthOfLastFrame;
        private int currentHealth;
        
        //Move to next stage, trigger corresponding animation
        public UnityEvent onStageCompleted;
        void Awake()
        {
            foreach (var system in partSystem)
            { 
                stageHealth += system.GetMaxHealth;
                system.gameObject.SetActive(false);
                healthOfLastFrame = stageHealth;
                system.onTakingDamage.AddListener(CheckDamageThreshold);
            }

            currentHealth = stageHealth;
            Debug.Log(stageHealth);
        }

        private void Start()
        {
            healthPerStage = stageHealth / damageStages;
        }

        private void CheckDamageThreshold(int damage)
        {
            healthOfLastFrame = currentHealth;
            currentHealth -= damage;

            if (healthOfLastFrame / (stageHealth / damageStages) > currentHealth / (stageHealth / damageStages))
            {
                onCrossingDamageThreshold?.Invoke();
            }
        }

        public void InitializeStage()
        {
            foreach (var system in partSystem)
            {
                system.gameObject.SetActive(true);
            }
        }

        public bool IsStageCompleted()
        {
            return partSystem.All(system => system == null || !(system.GetHealthNormalized() > 0));
        }
    }
}