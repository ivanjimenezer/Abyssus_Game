using UnityEngine;

namespace SG
{
    public class BarrellExplosion : MonoBehaviour
    {
        public GameObject explosionPrefab; // Assign your particle system prefab in the Inspector

        void OnCollisionEnter(Collision collision)
        {
            // Check if the collision is with the sword (you can adjust the tag as needed)
            if (collision.collider.CompareTag("Sword"))
            {
                Debug.Log("Sword collided with barrel");
                Explode();
            }
        }
        void Explode()
        {
            // Instantiate the particle system at the barrel's position
            if (explosionPrefab == null)
            {
                Debug.LogError("Explosion prefab not assigned!");
                return;
            }

            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Destroy the barrel and the particle system after 2 seconds
            Destroy(explosion, 2f);
            Destroy(gameObject);
        }
    }
}
