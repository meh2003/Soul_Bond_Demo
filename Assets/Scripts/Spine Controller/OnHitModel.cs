namespace Demo
{
    public class OnHitModel : Model
    {
        private void Awake()
        {
            spineObject.AnimationState.SetAnimation(0, idleAnimation, false);
        }
    }
}