using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class StaminaBar : MonoBehaviour
    {
        public Slider slider;
        public int maxStamina = 100; // Set your max stamina value here
        public int currentStamina = 100; // Set your initial current stamina value here
        public float refillRate = 1.0f; // Set the rate at which stamina refills per second

        private void Start()
        {
            slider = GetComponent<Slider>();
            SetMaxStamina(maxStamina);
            SetCurrentStamina(currentStamina);
            StartCoroutine(RefillStaminaOverTime());
        }

        public void SetMaxStamina(int maxStamina)
        {
            slider.maxValue = maxStamina;
            slider.value = maxStamina;
        }

        public void SetCurrentStamina(int currentStamina)
        {
            slider.value = currentStamina;
        }

        private IEnumerator RefillStaminaOverTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(1.0f); // Adjust the time interval based on your preference

                if (currentStamina < maxStamina)
                {
                    currentStamina += 10; // Adjust the refill amount based on your preference
                    SetCurrentStamina(currentStamina);
                }
            }
        }
    }
}
