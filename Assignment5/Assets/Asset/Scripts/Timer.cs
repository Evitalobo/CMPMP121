using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    public float timeLeft = 60.0f;
    public Text timer; 


    void Update()
    {
        timeLeft -= Time.deltaTime;
        timer.text = (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
