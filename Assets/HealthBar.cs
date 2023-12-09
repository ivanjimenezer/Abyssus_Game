using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        public int maxHealth = 100; // Set your max health value here
        public int currentHealth = 100; // Set your initial current health value here
        public float regenerationRate = 1.0f; // Set the rate at which health regenerates per second

        private void Start()
        {
            slider = GetComponent<Slider>();
            SetMaxHealth(maxHealth);
            SetCurrentHealth(currentHealth);
            StartCoroutine(RegenerateHealthOverTime());
        }

        public void SetMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        public void SetCurrentHealth(int currentHealth)
        {
            slider.value = currentHealth;
        }

        private IEnumerator RegenerateHealthOverTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(1.0f); // Adjust the time interval based on your preference

                if (currentHealth < maxHealth)
                {
                    currentHealth += 1; // Adjust the regeneration amount based on your preference
                    SetCurrentHealth(currentHealth);
                }
            }
        }
    }
}
