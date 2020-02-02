using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using System;

namespace RPG.Control
{
    public class playerControler : MonoBehaviour
    {
        private void Update()
        {
            if(interactWithCombat()) return;
            if(interactWithMovement()) return;
            // print("noting to do");
            // interactWithCombat();
        }

        private bool interactWithCombat()
        {
           RaycastHit[] hits =  Physics.RaycastAll(GetMouseRay());
           foreach (RaycastHit hit in hits)
           {
               combatTarget target = hit.transform.GetComponent<combatTarget>();
               if(target == null ) continue ; 
               if(Input.GetMouseButtonDown(0)){
                   GetComponent<fighter>().Attack(target);  
               }
               return true;
           }
           return false;
        }

        private bool interactWithMovement()
        {

            RaycastHit hit;

            bool hashit = Physics.Raycast(GetMouseRay(), out hit);

            if (hashit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<mover>().StartMoveAction(hit.point);
                }
                return true;

            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}