using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager weaponSlotManager;
        public WeaponItem rightWeapon;
        public WeaponItem leftWeapon;
        public WeaponItem unarmedWeapon;
        public WeaponItem [] weaponInRightHandSlots = new WeaponItem[1];
        public WeaponItem [] weaponInLeftHandSlots = new WeaponItem[1];

        public int currentRightWeaponIndex = -1;
        public int currentLeftWeaponIndex = -1; 

        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        //    leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
          //  rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
        }
        private void Start ()
        {
           rightWeapon = unarmedWeapon;
           leftWeapon = unarmedWeapon;
           weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
           weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
        }
        public void ChangeRightWeapon()
        {
            /*
             currentRightWeaponIndex = currentRightWeaponIndex + 1;

            if (currentRightWeaponIndex > weaponsInRightHandSlots.Length - 1)
            {
                currentRightWeaponIndex = -1;
                rightWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
            }
            else if (weaponsInRightHandSlots[currentRightWeaponIndex] != null)
            {
                rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[currentRightWeaponIndex], false);
            }
            else
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
            }*/
             currentRightWeaponIndex = currentRightWeaponIndex + 1;
            if (currentRightWeaponIndex ==0 && weaponInRightHandSlots[0] != null)
            {
                rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex], false);
            }
            else if(currentRightWeaponIndex == 0 && weaponInRightHandSlots[0] == null)
            {
                currentRightWeaponIndex = currentRightWeaponIndex +1;
            }
            else if(currentRightWeaponIndex == 1 && weaponInRightHandSlots[1] != null)
            {
                rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex], false);
            }
            else{
                currentRightWeaponIndex = currentRightWeaponIndex +1;
            }
            if(currentRightWeaponIndex > weaponInRightHandSlots.Length - 1)
            {
                currentRightWeaponIndex = -1;
                rightWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false); 
            }
        }

        /*
        public void ChangeLeftWeapon()
        {
            currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
            if (currentLeftWeaponIndex ==0 && weaponInLeftHandSlots[0] != null)
            {
                leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
                Debug.Log("1er - Left");
                weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex], true);
            }
            else if(currentLeftWeaponIndex == 0 && weaponInLeftHandSlots[0] == null)
            {
                currentLeftWeaponIndex = currentLeftWeaponIndex +1;
            }
            else if(currentLeftWeaponIndex == 1 && weaponInLeftHandSlots[1] != null)
            {
                leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
                Debug.Log("2do - Left");
                weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex], true);
            }
            else{
                currentLeftWeaponIndex = currentLeftWeaponIndex +1;
            }
            if(currentLeftWeaponIndex > weaponInLeftHandSlots.Length - 1)
            {
                currentLeftWeaponIndex = -1;
                leftWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);

            }
        }
    */
    public void ChangeLeftWeapon()
        {
            currentLeftWeaponIndex = currentLeftWeaponIndex + 1;

            if (currentLeftWeaponIndex == 0 && weaponInLeftHandSlots[0] != null)
            {
                leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex], true);
            }
            else if (currentLeftWeaponIndex == 0 && weaponInLeftHandSlots[0] == null)
            {
                currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
            }
            else if (currentLeftWeaponIndex == 1 && weaponInLeftHandSlots[1] != null)
            {
                leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex], true);
            }
            else if (currentLeftWeaponIndex == 1 && weaponInLeftHandSlots[1] == null)
            {
                currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
            }

            if (currentLeftWeaponIndex > weaponInLeftHandSlots.Length - 1)
            {
                currentLeftWeaponIndex = -1;
                leftWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, true);
            }
        }
    

    }
}
