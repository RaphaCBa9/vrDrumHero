using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static float score = 0;
    public static int life = 20;
    public static bool endOfSong = false;

    public static void Init()
    {
        score = 0f;
        life = 20;
    }

    public static bool gameOver
    {
        get
        {
            return ((life <= 0) || (endOfSong));
        }
    }

    public static void hitNote()
    {
        if (life == 20)
        {
            return;
        }
        life++;
    }

    public static void missNote()
    {
        life -= 2;
    }

    public static void endGame()
    {
        endOfSong = true;
    }

}
