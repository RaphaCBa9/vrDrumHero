using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameplayManager : MonoBehaviour
{
    public int hpCount;
    public float score;
    public static gameplayManager instance;


    public bool gameOver
    {
        get { return hpCount <= 0; }
    }

    public void Start()
    {
        instance = this;
        hpCount = 20;
        score = 0;
    }

    public void hitNote(float value)
    {
        score += value;
        hpCount++;
    }

    public void missNote()
    {
        hpCount--;
        if (gameOver)
        {
            // Handle game over logic here
            Debug.Log("Game Over");

            // take the player to the GameOver Scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("gameOver");
        }
    }


}
