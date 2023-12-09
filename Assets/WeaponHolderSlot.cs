using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
        public class WeaponHolderSlot : MonoBehaviour
    {
        public Transform parentOverride;
        public bool isLeftHandSlot;
        public bool isRightHandSlot;

        public GameObject currentWeaponModel;

        public void UnloadWeapon(){
            if(currentWeaponModel != null)
            {
                currentWeaponModel.SetActive(false);
            }
        }

        public void UnloadWeaponAndDestroy()
        {
            
            if (currentWeaponModel != null)
            {
                Destroy(currentWeaponModel);
            }
        }


        public void LoadWeaponModel(WeaponItem weaponItem)
        {
            //Unload weapon destroy
            UnloadWeaponAndDestroy();
            if(weaponItem == null)
            {
                //Unload Weapon Model
                UnloadWeapon();
                return;
            }

            GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;

            if(model != null)
            {   if(parentOverride != null)
                {

                    model.transform.parent = parentOverride; 
                    Debug.Log("Parent: " + parentOverride.position);
                }
                else
                { 
                    model.transform.parent = transform;
                    Debug.Log("Self: " + model.transform.parent.position);
                }
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one; 
            }
            currentWeaponModel = model;
        }

        
    }
}
