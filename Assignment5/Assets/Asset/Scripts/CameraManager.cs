using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera Camera1;
    public Camera Camera2;
    public Camera Camera3;
    GameObject Room1;
    GameObject Room2;
    GameObject Room3;
  
    // Start is called before the first frame update
    void Start()
    {


    Camera1.GetComponent<Camera>().enabled = false;
        Camera2.GetComponent<Camera>().enabled = true;
        Camera3.GetComponent<Camera>().enabled = false;


        ShowCamera2();
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag =="Room1")
        {
           
            ShowCamera1();
            Debug.Log(other.gameObject);
        }
        if (other.tag == "Room2")
        {
           
            ShowCamera2();
            Debug.Log(other.gameObject);
        }
        if (other.tag == "Room3")
        {
          
            ShowCamera3();
           Debug.Log(other.gameObject);

        }

    }


    public void ShowCamera1()
    {
       
        Camera1.enabled = true;
        Camera2.enabled = false;
        Camera3.enabled = false;
       
    }
    public void ShowCamera2()
    {
      
        Camera1.enabled = false;
        Camera2.enabled = true;
        Camera3.enabled = false;
    }
    public void ShowCamera3()
    {
        Camera1.enabled = false;
        Camera2.enabled = false;
        Camera3.enabled = true;
    }
}
