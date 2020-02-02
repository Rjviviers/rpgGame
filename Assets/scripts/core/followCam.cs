using UnityEngine;


namespace RPG.Core
{
    public class followCam : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }
    [SerializeField] Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    // Update is called once per frame
    void LateUpdate()
    {   
        // if(Input.GetMouseButtonDown(0)){
        //     GetComponent<NavMeshAgent>().destination = target.position;
        // } wrong
        Vector3 desiredPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position,desiredPos,smoothSpeed);
        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}

}