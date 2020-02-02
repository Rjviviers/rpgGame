using UnityEngine;

namespace RPG.Combat
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
                isDead = true;
                die();
            }

            void die()
            {
                GetComponent<Animator>().SetTrigger("die");
            }
        }

    }
    
}