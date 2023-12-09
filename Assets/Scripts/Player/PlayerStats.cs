 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SG
{
    public class PlayerStats : CharacterStats
{


 public bool playerIsDead = false;
    public HealthBar healthbar;
    public StaminaBar staminaBar;
    AnimatorHandler animatorHandler;
    //Rigidbody rigidbody;
    string[] deathAnimations = { "Death01", "Death02" };


    private void Awake()
    {   
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        staminaBar = FindObjectOfType<StaminaBar>();
        healthbar = FindObjectOfType<HealthBar>();
        //rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    { 
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

        maxStamina = SetMaxStaminaFromStaminaLevel();
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    private int SetMaxStaminaFromStaminaLevel()
    {
        maxStamina=staminaLevel * 10;
        return maxStamina;
    }


    public void TakeDamage(int damage)
    {   // modificado 

        if (isDead)
            return;
 
            currentHealth = currentHealth-damage;
            healthbar.SetCurrentHealth(currentHealth);

            animatorHandler.PlayTargetAnimation("Harmed", true);
            //rigidbody.Velocity= Vector3.zero;

            if (currentHealth <= 0)
            {   
                currentHealth = 0; 
                // Randomly choose a death animation
                string randomDeathAnimation = deathAnimations[Random.Range(0, deathAnimations.Length)];
                Debug.Log(randomDeathAnimation);
                animatorHandler.PlayTargetAnimation(randomDeathAnimation, true);
                isDead = true;
               SceneManager.LoadScene("Creditos");
            }
 
        
    }

    public void TakeStaminaDamage(int Damage)
    {
        currentStamina = currentStamina - Damage;
        staminaBar.SetCurrentStamina(currentStamina);
        //Set Bar value
    }

    public void EmpezarNivel(string NombreNivel)
    {
        SceneManager.LoadScene(NombreNivel);
    }

 
}
}
