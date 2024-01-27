using UnityEngine;

namespace Demo
{
    /// <summary>
    /// Wind Orb is the basic, straight moving player projectile
    /// </summary>
    public class WindOrb : FriendlyProjectile
    {
        public override void OnFrame()
        {
            transform.position += transform.up * (speed * Time.deltaTime);
        }
    }
}