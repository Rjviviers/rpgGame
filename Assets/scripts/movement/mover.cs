using UnityEngine;
using UnityEngine.AI;    
using RPG.Core;

namespace RPG.Movement
{
    public class mover : MonoBehaviour , IAction
    {
        // serialize    
        [SerializeField] Transform target;
        //Ray lastRay; //last ray we shot at the screen
        NavMeshAgent navMeshAgent;
        // Update is called once per frame
        void Start(){
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            UpdateAnimator();
        }
        public void StartMoveAction(Vector3 destination){
            GetComponent<ActionScheduler>().StartAction(this);
            moveTo(destination);
        }
        public void moveTo(Vector3 destination){
            //GetComponent<ActionScheduler>().StartAction(this);
            GetComponent<NavMeshAgent>().destination = destination;
            navMeshAgent.isStopped = false;
        }
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
        private void UpdateAnimator()// updates speed of animation from idle to walk to run 
        {
            Vector3 velocity =  GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelo = transform.InverseTransformDirection(velocity);
            float speed = localVelo.z;
            //transform.LookAt(target);
            GetComponent<Animator>().SetFloat("forwardspeed",speed);
        }

        
        
    }
}