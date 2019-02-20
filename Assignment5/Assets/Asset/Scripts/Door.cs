using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnim;
    private int points;
    // Start is called before the first frame update
    void Start()
    {
        doorAnim = GetComponent<Animator>();
        doorAnim.Play("Close");
    }


    public void PointCollect(){
        points++;

      
        if (points >= 1)
        {
            doorAnim = GetComponent<Animator>();
            doorAnim.Play("Open");
            doorAnim.SetInteger("index", 1);
            doorAnim.SetTrigger("move");
        }

        else
        {
            doorAnim = GetComponent<Animator>();
            doorAnim.Play("Close");
            doorAnim.SetInteger("index", 2);
            doorAnim.SetTrigger("move");
        }

    }
}
