using SceneLoader;
using UnityEngine;
using Zenject;

namespace UIButtons
{
    public class LoadButton : BaseButton
    {
        [Inject]
        private FadeSceneLoader _fadeSceneLoader = default;

        [SerializeField]
        private SceneName jumpSceneName = default;

        public override void OnClicked()
        {
            base.OnClicked();

            _fadeSceneLoader.JumpSceneLoad(jumpSceneName);
        }
    }
}
