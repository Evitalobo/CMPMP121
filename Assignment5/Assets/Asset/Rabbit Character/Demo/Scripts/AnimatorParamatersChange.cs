using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiveRabbitsDemo
{
    public class AnimatorParamatersChange : MonoBehaviour
    {
        [SerializeField]
        [Range(1, 20)]
        private float speed = 18;
        private Vector2 rotation = Vector2.zero;
        public float mouse = 10;
        private string[] states = new string[] { "Idle", "Run" };

        private Animator RabAnim;

        // Use this for initialization
        void Start()
        {
         
            RabAnim = GetComponent<Animator>();
            RabAnim.Play("Idle");
        }

        // Update is called once per frame
        void Update()
        {

            //movement- Use arrow keys
            Vector3 direction = Vector3.zero;


            if (Input.GetKey(KeyCode.UpArrow))
            {
                direction.z = 1;
                RabAnim.Play("Run");
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                direction.z = -1;
                RabAnim.Play("Run");
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction.x = -1;
                RabAnim.Play("Run");
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction.x = 1;
                RabAnim.Play("Run");
            }

            if (Input.anyKey ==false){
                RabAnim.Play("Idle");
            }

            transform.Translate(speed * direction.normalized * Time.deltaTime);


            MouseMove();

        }


            
            //Mouse rotation/follow
            private void MouseMove()
              {
                rotation.x += -Input.GetAxis("Mouse Y");
                rotation.y += Input.GetAxis("Mouse X");
                rotation.x = Mathf.Clamp(rotation.x, -10f, 10f);

                transform.eulerAngles = (mouse * new Vector2(0, rotation.y));
                Camera.main.transform.localRotation = Quaternion.Euler(mouse * rotation.x, 0, 0);
             }


    }

}
