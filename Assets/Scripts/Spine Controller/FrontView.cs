using System;
using System.Collections.Generic;
using System.Linq;
using Spine;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Demo
{
    public class FrontView : Model
    {
        [SerializeField] private Boss boss;

        [HeaderAttribute("Animations")] 
        [SerializeField] private AnimationReferenceAsset cloakRemoveAnimation;
        [SerializeField] private AnimationReferenceAsset handMoveAnimation;
        [SerializeField] private AnimationReferenceAsset leftThighHitAnimation;
        [SerializeField] private AnimationReferenceAsset rightThighHitAnimation;
        private void Awake()
        {
            spineObject.AnimationState.AddAnimation(0, idleAnimation, true, 0f);
            //Cloak stage
            boss.stages[0].onStageCompleted.AddListener(RemoveCloak);
            //Elbow stage
            boss.stages[1].onStageCompleted.AddListener(MoveHand);
            //Belt stage
            boss.stages[2].onCrossingDamageThreshold.AddListener(ChangeToNextSkin);
            //Thigh stage
            boss.stages[3].onCrossingDamageThreshold.AddListener(ChangeToNextSkin);
            boss.stages[3].PartSystem[0].onTakingDamage.AddListener(delegate { MoveThighOnHit(leftThighHitAnimation);});
            boss.stages[3].PartSystem[1].onTakingDamage.AddListener(delegate { MoveThighOnHit(rightThighHitAnimation); });
        }

        private void RemoveCloak()
        {
            spineObject.AnimationState.AddAnimation(1, cloakRemoveAnimation, false, 0f);
            // spineObject.AnimationState.AddEmptyAnimation(1, 0.2f, 0f);
        }

        private void MoveHand()
        {
            spineObject.AnimationState.AddAnimation(1, handMoveAnimation, false, 0f);
            // spineObject.AnimationState.AddEmptyAnimation(1, 1f, 0.5f);          
        }

        private void MoveThighOnHit(AnimationReferenceAsset hitAnim)
        { 
            spineObject.AnimationState.AddAnimation(2, hitAnim, false, 0f);
        }
    }
}