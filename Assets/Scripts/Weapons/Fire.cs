using UnityEngine;

public class Fire : Projectile
{
    [SerializeField] private int damage = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == PlayerManager.instance.player.name)
        {
            PlayerStats.playerStats.HealDamage(damage*-1);
        }
    }
}
