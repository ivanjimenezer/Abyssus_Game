using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{

    public class Explosion : MonoBehaviour
    {
        public float explosionForce;
        public float explosionRadius;
        public LayerMask affectedLayers;

        void Start()
        {
            Explode();
        }

        void Explode()
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

            // Optional: You can add additional effects or sound here

            // Destroy the explosion object after the blast
            Destroy(gameObject);
        }
    }

}
