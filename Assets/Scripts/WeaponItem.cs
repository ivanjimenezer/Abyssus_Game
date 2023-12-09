using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(menuName = "Item/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed; 
        public bool isEnemy;
        [Header("One Handed Attack Animations")]
        public string Attack01;
        //public string[] HeavyAttacks_List= { "OH_Heavy_Attack_01", "SwingAttack_OneHand_01"};
        //public string[] LightAttacks_List= {"HeavyAttack_OneHand_02","SideAttack_OneHand_01" };  
        public string Attack02;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;


    }
}

