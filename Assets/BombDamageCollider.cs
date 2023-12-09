using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class BombDamageCollider : MonoBehaviour
    {
        Collider damageCollider;
        public int bombDamage = 50; // Adjust the bomb damage as needed

        private void Awake()
        {
            damageCollider = GetComponentInChildren<Collider>();
            // Disable the collider initially
            damageCollider.enabled = true;
        }

        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if the bomb's damage collider interacts with a character
            if (other.CompareTag("Player"))
            {
                Debug.Log("Bomb damaged the player");
                PlayerStats playerStats = other.GetComponent<PlayerStats>();

                if (playerStats != null)
                {
                    playerStats.TakeDamage(bombDamage);
                }
            }
            else if (other.CompareTag("Enemy"))
            {
                Debug.Log("Bomb damaged an enemy");
                EnemyStats enemyStats = other.GetComponent<EnemyStats>();

                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(bombDamage);
                }
            }
        }
    }
}
