using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour {

    public float DestroyTime = 0.4f;
    
    public BombCircleHandler _bombCircle;
    private GameObject circle;
    private EnemySpawner spawner;
    

  

    void Start () {

        _bombCircle = gameObject.transform.GetChild(0).GetComponent<BombCircleHandler>();
        circle = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        circle.GetComponent<Image>().enabled = true;
        spawner = GameObject.Find("EnemyHandler").GetComponent<EnemySpawner>();

    }

    void Update () {
        circle.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
    private void DestroyInCircle()
    {
        
        List<GameObject> inCircleBlocks = _bombCircle.InCircleBlocks;

        PlayerState _playerState = GameObject.Find("player").GetComponent<PlayerState>();

        _playerState.AddToScore(3);



        for (int i = 0; i < inCircleBlocks.Count; i++)
        {
            if (inCircleBlocks[i] != null)
            {
                GameObject blocExplosion = (GameObject)Instantiate(Resources.Load("prefabs/blockexp"));
                blocExplosion.transform.position = inCircleBlocks[i].transform.position;

                GameObject scoreText = (GameObject)Instantiate(Resources.Load("prefabs/ScorePopUp"));
                scoreText.GetComponent<TextMesh>().text = "3";
                scoreText.transform.position = inCircleBlocks[i].transform.position;

                Destroy(scoreText, 1f);
                Destroy(blocExplosion, 1);
            }

            


            Destroy(inCircleBlocks[i]);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            DestroyInCircle();

            

            GameObject explosion = (GameObject)Instantiate(Resources.Load("prefabs/explosion"));
            explosion.transform.position = transform.position;
            Destroy(explosion, 1);

            gameObject.SetActive(false);
            Destroy(gameObject, DestroyTime);
            circle.GetComponent<Image>().enabled = false;

            spawner.SpawnNew();
        }

    }

  


}
