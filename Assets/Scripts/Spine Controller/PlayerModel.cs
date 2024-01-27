using System;
using System.Collections;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo
{
    public class PlayerModel : Model
    {
        [SerializeField] private GameObject player;

        [HeaderAttribute("Animations")] [SerializeField]
        private AnimationReferenceAsset turnLeftAnimation;

        [SerializeField] private AnimationReferenceAsset turnLeftAfterAnimation;

        [SerializeField] private AnimationReferenceAsset turnRightAnimation;
        [SerializeField] private AnimationReferenceAsset turnRightAfterAnimation;

        [SerializeField] private AnimationReferenceAsset goBackwardsAnimation;
        [SerializeField] private AnimationReferenceAsset goBackwardsAfterAnimation;

        [SerializeField] private AnimationReferenceAsset fireAnimation;

        [SerializeField] private AnimationReferenceAsset fullManaAnimation;
        
        const int DefaultFlashCount = 3;

        public int flashCount = DefaultFlashCount;
        public Color flashColor = Color.white;
        [Range(1f / 120f, 1f / 15f)]
        public float interval = 1f / 60f;
        public string fillPhaseProperty = "_FillPhase";
        public string fillColorProperty = "_FillColor";

        MaterialPropertyBlock mpb;
        MeshRenderer meshRenderer;
        Player playerStats;
        
        private void Awake()
        {
            var playerController = player.GetComponent<PlayerController>();
            playerStats = player.GetComponent<Player>();
            var playerWeapon = player.GetComponent<PlayerWeapon>();
            
            playerController.onIdle.AddListener(ResetSideTurn);
            playerController.onIdle.AddListener(ResetBackTurn);
            playerController.onLeftTurn.AddListener(TurnLeft);
            playerController.onRightTurn.AddListener(TurnRight);
            playerController.onBackwardsTurn.AddListener(GoBackward);
            
            playerWeapon.onFire.AddListener(Fire);
            
            // playerStats.onTakingDamage.AddListener(Flash);
            
            //Implement mana later
            // playerStats.onFullMana.AddListener(ShowFullMana);
        }

        private void Update()
        {
            // if (playerStats.manaState != Player.ManaState.NotFull || spineObject.AnimationState.Tracks) ClearFullMana();
        }

        private void TurnLeft()
        {
            ResetSideTurn();
            // spineObject.AnimationState.SetAnimation(1, turnLeftAnimation, false);
            spineObject.AnimationState.SetAnimation(0, turnLeftAfterAnimation, true);
        }

        private void TurnRight()
        {
            ResetSideTurn();
            // spineObject.AnimationState.SetAnimation(1, turnRightAnimation, false);
            spineObject.AnimationState.SetAnimation(0, turnRightAfterAnimation, true);
        }

        private void ResetSideTurn()
        {
            spineObject.AnimationState.AddEmptyAnimation(0, 0 , 0);
            spineObject.AnimationState.AddAnimation(0, idleAnimation, true, 0f);
        }

        private void GoBackward()
        {
            spineObject.AnimationState.SetAnimation(2, goBackwardsAnimation, false);
            spineObject.AnimationState.AddAnimation(2, goBackwardsAfterAnimation, true, 0f);
        }

        private void ResetBackTurn()
        {
            spineObject.AnimationState.ClearTrack(2);
        }

        private void Fire()
        {
            spineObject.AnimationState.SetAnimation(3, fireAnimation, false);
            spineObject.AnimationState.AddEmptyAnimation(3, 0.1f, 0f);
        }

        private void ShowFullMana()
        {
            spineObject.AnimationState.SetAnimation(4, fullManaAnimation, true);
        }

        private void ClearFullMana()
        {
            spineObject.AnimationState.ClearTrack(4);
        }

        // private void Flash () {
        //     if (mpb == null) mpb = new MaterialPropertyBlock();
        //     if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        //     meshRenderer.GetPropertyBlock(mpb);
        //
        //     StartCoroutine(FlashRoutine());
        // }
        //
        // IEnumerator FlashRoutine () {
        //     if (flashCount < 0) flashCount = DefaultFlashCount;
        //     int fillPhase = Shader.PropertyToID(fillPhaseProperty);
        //     int fillColor = Shader.PropertyToID(fillColorProperty);
        //
        //     WaitForSeconds wait = new WaitForSeconds(interval);
        //
        //     for (int i = 0; i < flashCount; i++) {
        //         mpb.SetColor(fillColor, flashColor);
        //         mpb.SetFloat(fillPhase, 1f);
        //         meshRenderer.SetPropertyBlock(mpb);
        //         yield return wait;
        //
        //         mpb.SetFloat(fillPhase, 0f);
        //         meshRenderer.SetPropertyBlock(mpb);
        //         yield return wait;
        //     }
        //
        //     yield return null;
        // }
    }

    public class ShieldModel : Model
    {
        
    }
}