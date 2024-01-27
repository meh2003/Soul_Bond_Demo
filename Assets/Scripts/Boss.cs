
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Demo
{
    //Control the stages of the boss battle
    public class Boss : MonoBehaviour
    {
        [SerializeField] private InputReader input;
        public List<BossStage> stages;
        private int currentStage = 0;
        public int stageBeforeSwitch;
        public bool canSwitchView;
        public List<GameObject> views;
        private int currentView = 0;
        public UnityEvent onViewSwitch;

        public int CurrentStage => currentStage;

        private int bossMaxHealth;
        private int currentBossHealth;

        public float GetBossHealthNormalized() => currentBossHealth / (float)bossMaxHealth;

        private void DamageBoss(int damage)
        {
            currentBossHealth -= damage;
        }
        
        private void Start()
        {
            foreach (var system in stages.SelectMany(stage => stage.PartSystem))
            {
                system.onSystemDestroyed.AddListener(CheckStageCompleted);
                system.onTakingDamage.AddListener(DamageBoss);
                system.GetComponent<Collider2D>().enabled = true;
                bossMaxHealth += system.GetMaxHealth;
            }

            currentBossHealth = bossMaxHealth;

            foreach (var view in views)
            {
                view.SetActive(false);
            }
            views[currentView].SetActive(true);
            canSwitchView = false;
            stages[stageBeforeSwitch].onStageCompleted.AddListener(EnableViewSwitch);
            InitializeStage();
        }

        private void Update()
        {
            //trigger a transition
            if (input.SwitchView && canSwitchView) SwitchView();
        }

        void CheckStageCompleted()
        {
            if (!stages[currentStage].IsStageCompleted()) return;
            stages[currentStage].onStageCompleted.Invoke();
            
            Debug.Log("Moving to next stage");
            AdvanceToNextStage();
        }

        private void AdvanceToNextStage()
        {
            currentStage++;
            if (currentStage < stages.Count)
            {
                InitializeStage();
            }
        }

        private void InitializeStage()
        {
            stages[currentStage].InitializeStage();
        }

        private void SwitchView()
        {
            //should be on Input Action switchViewAction
            views[currentView].SetActive(false);
            currentView++;
            views[currentView].SetActive(true);
            canSwitchView = false;
            onViewSwitch.Invoke();
            InitializeStage();
        }

        private void EnableViewSwitch()
        {
            canSwitchView = !canSwitchView;
        }
    }
}