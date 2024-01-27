using Spine.Unity;
using UnityEngine;

namespace Demo
{
    public class BackView : Model
    {
        [SerializeField] private Boss boss;

        [HeaderAttribute("Animations")] 
        [SerializeField] private AnimationReferenceAsset forearmMoveAnimation;
        [SerializeField] private AnimationReferenceAsset forearmAfterMoveAnimation;
        [SerializeField] private AnimationReferenceAsset tailMoveAnimation;
        [SerializeField] private AnimationReferenceAsset tailAfterMoveAnimation;
        [SerializeField] private AnimationReferenceAsset buttockHitAnimation;
        [SerializeField] private AnimationReferenceAsset legMoveAnimation;
        [SerializeField] private AnimationReferenceAsset legAfterMoveAnimation;
        [SerializeField] private AnimationReferenceAsset legHitAnimation;
        
        
        private void Awake()
        {
            spineObject.AnimationState.AddAnimation(0, idleAnimation, true, 0f);
            //Forearm stage
            boss.stages[4].onStageCompleted.AddListener(MoveForearm);
            //Bra stage
            boss.stages[5].onCrossingDamageThreshold.AddListener(ChangeToNextSkin);
            //Tail stage
            boss.stages[6].onStageCompleted.AddListener(MoveTail);
            //Buttock stage
            boss.stages[7].onCrossingDamageThreshold.AddListener(ChangeToNextSkin);
            boss.stages[7].PartSystem[0].onTakingDamage.AddListener(delegate { MoveButtockOnHit(buttockHitAnimation); });
            //Leg stage
            boss.stages[8].onStageCompleted.AddListener(MoveLeg);
            boss.stages[8].PartSystem[0].onTakingDamage.AddListener(delegate { MoveLegOnHit(legHitAnimation); });
        }

        private void MoveTail()
        {
            spineObject.AnimationState.AddAnimation(2, tailMoveAnimation, false, 0f);
            spineObject.AnimationState.AddAnimation(2, tailAfterMoveAnimation, true, 0f);
            // spineObject.AnimationState.AddEmptyAnimation(1, 0.2f, 0f);
        }

        private void MoveForearm()
        {
            spineObject.AnimationState.AddAnimation(1, forearmMoveAnimation, false, 0f);
            spineObject.AnimationState.AddAnimation(1, forearmAfterMoveAnimation, true, 0f);
            // spineObject.AnimationState.AddEmptyAnimation(1, 1f, 0.5f);          
        }
        
        private void MoveLeg()
        {
            spineObject.AnimationState.AddAnimation(3, legMoveAnimation, false, 0f);
            spineObject.AnimationState.AddAnimation(3, legAfterMoveAnimation, true, 0f);

            // spineObject.AnimationState.AddEmptyAnimation(1, 1f, 0.5f);          
        }
        
        private void MoveButtockOnHit(AnimationReferenceAsset hitAnim)
        { 
            spineObject.AnimationState.AddAnimation(4, hitAnim, false, 0f);
        }
        
        private void MoveLegOnHit(AnimationReferenceAsset hitAnim)
        { 
            spineObject.AnimationState.AddAnimation(4, hitAnim, false, 0f);
        }
    }
}