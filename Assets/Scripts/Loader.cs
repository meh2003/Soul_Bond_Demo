using System.Collections.Generic;
using Eflatun.SceneReference;
using MEC;
using UnityEngine.SceneManagement;

namespace Demo {
    public static class Loader {
        static readonly SceneReference LoadingScene = new (SceneGuidToPathMapProvider.ScenePathToGuidMap["Assets/Scenes/LoadingScene.unity"]);
        static SceneReference TargetScene;

        public static void Load(SceneReference scene) {
            TargetScene = scene;
            
            SceneManager.LoadScene(TargetScene.Name);
            // Timing.RunCoroutine(LoadTargetScene());
        }

        // static IEnumerator<float> LoadTargetScene() {
        //     yield return Timing.WaitForOneFrame;
        //     SceneManager.LoadScene(TargetScene.Name);
        // }
    }
}