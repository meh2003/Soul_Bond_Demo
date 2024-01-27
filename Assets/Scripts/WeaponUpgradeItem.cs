using UnityEngine;

namespace Demo
{
    public class WeaponUpgradeItem : Item
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<PlayerWeapon>().Upgrade(1);
            Destroy(gameObject);
        }
    }
}