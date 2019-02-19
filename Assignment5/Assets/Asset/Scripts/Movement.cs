
using UnityEngine;
using UnityEngine.UI;


public class Movement : MonoBehaviour
{
    public Text HappyText;
    private int points = 0;
  //  private Animator m_animator;


    private void Start()
    {
        /*
        string tag = "ParticleEffect";
        ParticleSystem Burst = GetComponent<ParticleSystem>();
        var allColliderGameObjects = GameObject.FindGameObjectsWithTag(tag);

        m_animator = GetComponent<Animator>();
        m_animator.Play("Close");
    */

    }

    //void OnParticleTrigger()
    // {
    //       Burst = GetComponent<ParticleSystem>();


    //  Burst.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    //   }


    void Update()
    {
        /*
        if (points >= 1)
        {
            m_animator = GetComponent<Animator>();
            m_animator.Play("Open");
            m_animator.SetInteger("AnimIndex", 1);
            m_animator.SetTrigger("Next");
        }
        else
        {
            m_animator = GetComponent<Animator>();
            m_animator.Play("Close");
            m_animator.SetInteger("AnimIndex", 2);
            m_animator.SetTrigger("Next");
        }
        */
    }



        //Happiness sphere collider
        void OnTriggerEnter(Collider other)
        {
            //BurstMode();
            points++;
            HappyText.text = ("Happiness: " + points.ToString());
            Destroy(other.gameObject);
            //  Burst.Play();


        }

}
   