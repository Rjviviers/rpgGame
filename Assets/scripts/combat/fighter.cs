﻿using UnityEngine;
using RPG.Movement;
using RPG.Core;
namespace RPG.Combat
{
    public class fighter : MonoBehaviour , IAction
    {   
        [SerializeField] float weaponRange = 3f; 
        [SerializeField] float TimebetweenAttacks = 0.6f ;
        health target; 
        [SerializeField] float weaponDmg = 5;
        float timeSinceLastAttk = 0;
        
        public void Update()
        {
            timeSinceLastAttk += Time.deltaTime;
            //bool isInRange = GetIsInRange();
            if (target == null) return;
            if(target.IsDead()) return;
            if (!GetIsInRange())
            {
                GetComponent<mover>().moveTo(target.transform.position);
            }
            else
            {
                GetComponent<mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttk > TimebetweenAttacks){
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttk = 0f;
                //triger hit event
            }
            
        }
        //animation Event
        void Hit(){
            target.takeDmg(weaponDmg);
        }
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(combatTarget CombatTarget){
            GetComponent<ActionScheduler>().StartAction(this);
            target = CombatTarget.GetComponent<health>() ; 
        }
        public void Cancel(){
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }

    }
}