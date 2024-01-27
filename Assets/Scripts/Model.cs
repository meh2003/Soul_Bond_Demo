using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Demo
{
    public class Model : MonoBehaviour
    {
        public SkeletonAnimation spineObject;
        [SerializeField] protected AnimationReferenceAsset idleAnimation;

        [SerializeField] private List<string> skinNames;
        private int skinIndex = 0;
        // Start is called before the first frame update
        private void Awake()
        {
            if (skinNames.Count > 0)
                spineObject.skeleton.SetSkin(spineObject.skeleton.Data.FindSkin(skinNames[skinIndex]));
            spineObject.AnimationState.AddAnimation(0, idleAnimation, true, 0f);
        }

        private void OnEnable()
        {
            skinIndex = 0;
        }

        protected void ChangeToNextSkin()
        {
            skinIndex++;
            if (skinIndex < skinNames.Count) spineObject.skeleton.SetSkin(spineObject.skeleton.Data.FindSkin(skinNames[skinIndex]));
        }
    }
}
