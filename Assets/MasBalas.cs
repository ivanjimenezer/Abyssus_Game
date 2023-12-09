using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class MasBalas : MonoBehaviour
    {
        public int ammoAmount = 10; // Adjust the amount of ammo provided by the pickup

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Lanzar lanzar = other.GetComponent<Lanzar>();
                if (lanzar != null)
                {
                    lanzar.MasBalas(ammoAmount);
                    Destroy(gameObject); // Destroy the pickup item after collecting ammo
                }
            }
        }
    }
}
