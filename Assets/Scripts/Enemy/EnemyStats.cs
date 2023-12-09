using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyStats : CharacterStats
    {
        Animator animator;
        public bool playerIsDead = false;
        //Rigidbody rigidbody;
        string[] deathAnimations = { "Death01" };
        public HealthBar healthbar;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            //rigidbody = GetComponent<Rigidbody>();
        }

        // Start is called before the first frame update
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthbar.SetMaxHealth(maxHealth);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (isDead)
                return;

            currentHealth -= damage;
            healthbar.SetCurrentHealth(currentHealth);
            animator.Play("Harmed");

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Death01");
                isDead = true;

                // Destroy the enemy game object after 10 seconds
                Invoke("DestroyEnemy", 10f);
            }
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }
    }
}
