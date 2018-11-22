using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour {

    public float DestroyTime = 0.4f;
    
    private BombCircleHandler _bombCircle;
    private EnemySpawner spawner;
    private GameObject _circleDraw;

    private void Awake()
    {
        _circleDraw = GameObject.Find("line");

    }
    void Start () {


        spawner = GameObject.Find("EnemyHandler").GetComponent<EnemySpawner>();
        

        _circleDraw = (GameObject)Instantiate(Resources.Load("prefabs/line"));

        _circleDraw.GetComponent<LineRenderer>().enabled = true;
        _circleDraw.transform.position = transform.position;
        _bombCircle = GameObject.Find("line(Clone)").GetComponent<BombCircleHandler>();

    }

    void Update () {
    }
    private void DestroyInCircle()
    {
        if (_bombCircle == null)
        {
            Debug.Log("null");
            return;
        }

        List<GameObject> inCircleBlocks = _bombCircle.InCircleBlocks;

        PlayerState _playerState = GameObject.Find("player").GetComponent<PlayerState>();

        _playerState.AddToScore(3);



        for (int i = 0; i < inCircleBlocks.Count; i++)
        {
            EnemySpawner.blocksCount--;

            if (inCircleBlocks[i] != null)
            {
                GameObject blocExplosion = (GameObject)Instantiate(Resources.Load("prefabs/blockexp"));
                blocExplosion.transform.position = inCircleBlocks[i].transform.position;

                
                
                Destroy(blocExplosion, 1);
            }
            
            Destroy(inCircleBlocks[i]);

            EnemySpawner.DestroyedBlocksCount++;
            
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
            _circleDraw.GetComponent<LineRenderer>().enabled = false;

            


            Destroy(_circleDraw);

            spawner.spawnBlock();

        }
    }

  


}
