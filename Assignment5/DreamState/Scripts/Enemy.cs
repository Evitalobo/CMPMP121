using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public Text kills;
    public float deaths;
    public float health = 50f;
    public ParticleSystem blood;

    public void TakeDamage (float amount){
        health -= amount;
        if(health <= 0f){
            //need to instantiate effects and enemies at some point- hella inefficient code currently
            blood.Play();
            deaths++;

            kills.text = (deaths).ToString("Kills: " + deaths);

      
            Die();
        }
    }

    void Die(){
        Destroy(gameObject);
    }
}
