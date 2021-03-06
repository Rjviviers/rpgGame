﻿using UnityEngine;

namespace RPG.Core
{
    public class health : MonoBehaviour
    {
        
        [SerializeField] float HP = 100f;
        bool isDead = false;
        public bool IsDead(){
            return isDead;
        }
        public void takeDmg(float dmg)
        {
            if(isDead) return;
            if(HP > 0){
            HP = Mathf.Max(HP - dmg,0);
            print(" hp :"+HP);
            }
            else
            {
                die();
            }

            
        }
        private void die()
        {   
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
    
}