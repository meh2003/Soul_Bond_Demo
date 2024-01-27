using UnityEngine;

namespace Demo
{
    public class ShieldItem : Item
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<Player>().GainShield(1);
            Destroy(gameObject);
        }
    }
}