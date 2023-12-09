using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class Projectile : MonoBehaviour
    {
        public LayerMask affectedLayers; // Layers to be affected by the explosion
        public GameObject explosionPrefab; // Prefab for explosion effect

        public int damage;
        public float explosionForce;
        public float explosionRadius;

        private bool targetHit;
        private float timeBeforeExplosion = 2.0f; // Adjust the time before explosion as needed
        private float currentTime = 0f;

        private Rigidbody rb;
        private ParticleSystem explosionParticleSystem;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            explosionParticleSystem = GetComponentInChildren<ParticleSystem>();

            // Disable the particle system initially
            if (explosionParticleSystem != null)
            {
                explosionParticleSystem.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            // Update the timer
            currentTime += Time.deltaTime;

            // Check if it's time to trigger the explosion
            if (currentTime >= timeBeforeExplosion)
            {
           //  TriggerExplosion();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
             

            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, affectedLayers);

            foreach (Collider hitCollider in colliders)
            {
                Rigidbody rb = hitCollider.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    // Apply explosion force to push characters away
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }

            Debug.Log("TOCO AL ENEMIGO EL PROYECTIL");
            EnemyStats enemyStats = collision.collider.gameObject.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                // Trigger the explosion, and let the parent handle it
                rb.isKinematic = true;
                transform.SetParent(collision.transform);
                ActivateParticleSystem();

                // Uncomment the line below if you want to disable the collider again
                // DisableExplosionCollider();

                enemyStats.TakeDamage(damage);
            }
        }

        public void TriggerExplosion()
        {
            Debug.Log("LLEG� Al  PROJECTILE en TRIGGER EXPLOTION");
            ActivateParticleSystem();
            EnableExplosionCollider();
            Explode();
        }

        private void Explode()
        {
            ActivateParticleSystem();
            // Do any other explosion-related actions
            Destroy(gameObject);
        }

        private void ActivateParticleSystem()
        {
            // Check if the particle system component is not null
            if (explosionParticleSystem != null)
            {
                // Set the particle system to be visible
                explosionParticleSystem.gameObject.SetActive(true);
                Debug.Log("se activ� el sistema de parituca");
                // Play the particle effect
                explosionParticleSystem.Play();
                Invoke("DeactivateParticleSystem", 1f); // Adjust delay as needed
            }
        }

        private void DeactivateParticleSystem()
        {
            Debug.Log("se desactivo");
            // Check if the particle system component is not null
            if (explosionParticleSystem != null)
            {
                // Stop the particle effect
                explosionParticleSystem.Stop();

                // Set the particle system to be invisible
                explosionParticleSystem.gameObject.SetActive(false);
            }
        }

        private void EnableExplosionCollider()
        {
            // Enable the Sphere Collider in the parent object
            Debug.Log("obtenemos el collider");
            SphereCollider explosionCollider = GetComponent<SphereCollider>();
            if (explosionCollider != null)
            {
                explosionCollider.enabled = true;
            }
        }
    }
}
