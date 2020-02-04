using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
namespace RPG.Control
{
    public class playerControler : MonoBehaviour
    {   
        health hp;
        private void Start(){
            hp = GetComponent<health>();
        }
        private void Update()
        {
            if(hp.IsDead()) return;
            if(interactWithCombat()) return;
            if(interactWithMovement()) return;
        }

        private bool interactWithCombat()
        {
           RaycastHit[] hits =  Physics.RaycastAll(GetMouseRay());
           foreach (RaycastHit hit in hits)
           {    
                combatTarget target = hit.transform.GetComponent<combatTarget>();
                if(target == null)continue;
               
                if(!GetComponent<Fighter>().CanAttack(target.gameObject))continue;

                if(Input.GetMouseButtonDown(0)){
                    GetComponent<Fighter>().Attack(target.gameObject); 
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