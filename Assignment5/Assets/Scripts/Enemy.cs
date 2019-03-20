using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public int kills=0;
    public float health = 50f;
    public ParticleSystem blood;
    public ParticleSystem splatter;
   // public GameObject bloodPool;

    public void TakeDamage (float amount){
        health -= amount;
        if(health <= 0f){
            blood.transform.position = this.transform.position;
            splatter.transform.position = this.transform.position;
            splatter.Play();
            blood.Play();
            killCt.kills += 1;
            Die();
        }
    }

    void Die(){
        Destroy(gameObject);
    }
}

