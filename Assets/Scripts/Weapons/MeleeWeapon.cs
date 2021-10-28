using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == PlayerManager.instance.player.name)
        {
            PlayerStats.playerStats.HealDamage(damage*-1);
        }
    }
}
