using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AiControler : MonoBehaviour
    {
        [SerializeField] float chaseDist = 5f;

        public void Update(){
            GameObject player =  GameObject.FindWithTag("Player");
        }
    }

}