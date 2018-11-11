using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour {
    private int _score;
    private int _lifes = 3;
    private GameObject life1;
    private GameObject life2;
    private GameObject life3;
    private Text ScoreText;

    public int Score { get { return _score; } set { _score = value; } }
    public int Lifes { get { return _lifes; } set { _lifes = value; } }

    private void Awake()
    {
        life1 = GameObject.Find("life1");
        life2 = GameObject.Find("life2");
        life3 = GameObject.Find("life3");

        ScoreText = GameObject.Find("Score").GetComponent<Text>();
    }

    public void AddToScore(int points)
    {
        _score += points;

        ScoreText.text = _score.ToString();
        
    }

    public void removeLife()
    {

        if (--_lifes == 2)
            life1.GetComponent<Image>().enabled = false;
        else if(_lifes == 1)
            life2.GetComponent<Image>().enabled = false;
        else
        {
            life3.GetComponent<Image>().enabled = false;
            Destroy(gameObject);
            ScoreScreen();
        }


    }

    private void ScoreScreen()
    {
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        GameObject goCanvas = GameObject.Find("GameOverCanvas");
        goCanvas.GetComponent<Canvas>().enabled = true;

        goCanvas.transform.GetChild(1).GetComponent<Text>().text = _score.ToString();


    }
}
