using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreTxt;
    public SceneTransitionManager stm;


    // Update is called once per frame
    void Update()
    {
        scoreTxt.text = GameManager.score.ToString();
        if (GameManager.gameOver)
        {
            Debug.Log(GameManager.endOfSong);
            Debug.Log(GameManager.life);
            stm.GoToSceneAsync(0);
            Destroy(gameObject);
        }
    }
}
