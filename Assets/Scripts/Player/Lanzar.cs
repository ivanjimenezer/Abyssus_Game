using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SG
{
    public class Lanzar : MonoBehaviour
    {
        [Header("References")]
        public Transform cam;
        public Transform attackPoint;
        public GameObject objectToThrow;

        [Header("Settings")]
        public int totalThrows = 30;
        public float throwCooldown = 5;
        public float explosionRadius = 10;
        public float explosionForce = 30;
        public float delayBeforeExplosion = 2;

        [Header("Throwing")]
        public KeyCode throwKey = KeyCode.Mouse0;
        public float throwForce = 30;
        public float throwUpwardForce = 2;

        bool readyToThrow = true;
        private GameObject storedProjectile;

        private int currentAmmo;


        public void Initialize()
        {    
             
            //objectToThrow = FindObjectOfType<BulletBall>();
        
        } 

        public void MasBalas(int ammoAmount)
        {
            // Called when picking up additional bullets
            currentAmmo += ammoAmount;
        }

        private void Start()
        {
            readyToThrow = true;
            currentAmmo = totalThrows;
        }

        private void Update()
        {
            if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
            {
                Throw();
            }
        }

        public void Throw()
        {
            if(currentAmmo > 0 && readyToThrow ){
            readyToThrow = false;

            // instantiate object to throw
            GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);
            Rigidbody projectileRb = projectile.GetComponentInChildren<Rigidbody>();

            // calculate direction
            Vector3 forceDirection = cam.transform.forward;
            RaycastHit hit;

            if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
            {
                forceDirection = (hit.point - attackPoint.position).normalized;
            }

            // add force
            Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
            projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

            // store a reference to the instantiated projectile
            StoreProjectileReference(projectile);

            currentAmmo--;

            // activate trigger after a delay
            Invoke("TriggerProjectileExplosion", delayBeforeExplosion);
            Invoke(nameof(ResetThrow), throwCooldown);}
        }

        private void StoreProjectileReference(GameObject projectile)
        {
            storedProjectile = projectile;
        }

        private void TriggerProjectileExplosion()
        {
            // Check if the stored projectile reference is not null
            if (storedProjectile != null)
            {
                // Find the Projectile component in the stored object
                Projectile projectileScript = storedProjectile.GetComponent<Projectile>();
                if (projectileScript != null)
                {
                    // Call the TriggerExplosion method
                    projectileScript.TriggerExplosion();
                }
            }
        }

        private void ResetThrow()
        {
            readyToThrow = true;
        }
    }
}
