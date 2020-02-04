using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AiControler : MonoBehaviour
    {
        [SerializeField] float chaseDist = 5f;
        [SerializeField] float susTime =3f;//suspision time
        [SerializeField] Patrol patrolPath;
        [SerializeField] float WaypointTolarance = 1f;

        Fighter figter;
        health hp; 
        GameObject player;
        Vector3 guardPosition ;
        mover Movement;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        int currentWaypointIndex = 0;
        public void Start(){
            figter = GetComponent<Fighter>();
            hp = GetComponent<health>();
            player = GameObject.FindWithTag("Player");
            guardPosition = transform.position;
            Movement = GetComponent<mover>();
        }
        public void Update()
        {
            if(hp.IsDead()) return;

            if(inRangeOfPlayer()  && figter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AtackBehevior();
            }
            else if(timeSinceLastSawPlayer< susTime)
            {

                SusBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPos = guardPosition;
            if (patrolPath != null)
            {
                if(AtWaypoint()){
                    CycleWaypoint();
                }
                nextPos = GetCurrentWaypoint();
            }
            Movement.StartMoveAction(nextPos);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position,GetCurrentWaypoint());
            return distanceToWaypoint < WaypointTolarance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextWaypoint(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void SusBehaviour()
        {
            
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AtackBehevior()
        {
            figter.Attack(player);
        }

        private bool inRangeOfPlayer()
        {
            float distancePlayer = Vector3.Distance(player.transform.position, transform.position);
            return distancePlayer < chaseDist ;
        }
        //called by unity
        private void OnDrawGizmos(){
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,chaseDist);
        }
        
    }

}