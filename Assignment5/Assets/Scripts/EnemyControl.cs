using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyControl : MonoBehaviour
{
  
    public Transform player;
    private Animator tigreAnim;

    // Start is called before the first frame update
    void Start()
    {
       tigreAnim = GetComponent<Animator>();
       tigreAnim.Play("idle");
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(player.position, this.transform.position) < 10)
        {
            tigreAnim.Play("idle");
        }
        else if (Vector3.Distance(player.position, this.transform.position) < 70)
        {

            tigreAnim.Play("walk");
            Vector3 direction = (player.position - this.transform.position);
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.2f);
           // Debug.Log(direction.magnitude);
            if(direction.magnitude > 8){
                this.transform.Translate(0,0, 0.2f);
            }
        }
      
    }
   
}
