using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float SpawnTimer = 3f;
    private float _timer;
    private int count = 0;
    private BoundsHandler _bounds;


    public enum Spawn
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }
    private void Awake()
    {
        _bounds = GetComponent<BoundsHandler>();
    }

    void Start () {
        _timer = SpawnTimer;
	}
	
	
	void Update () {
        _timer -= Time.deltaTime;

        if(_timer < 0)
        {
            
            GameObject newBlock = (GameObject)Instantiate(Resources.Load("prefabs/block" + (int)Random.Range(1, 4)));
            Block blockComp = newBlock.GetComponent<Block>();
            blockComp.Spawn = (Spawn)count++;

            count = count > 3 ? 0 : count;

            switch (blockComp.Spawn)
            {
                case Spawn.TopLeft:
                    newBlock.transform.position = new Vector3(_bounds.MinCoords.x, -0.66f, _bounds.MaxCoords.y - 1.01f);
                    break;

                case Spawn.TopRight:
                    newBlock.transform.position = new Vector3(_bounds.MaxCoords.x  , -0.66f, _bounds.MaxCoords.y - 1.01f);
                    break;
                case Spawn.BottomLeft:
                    newBlock.transform.position = new Vector3(_bounds.MinCoords.x , -0.66f, _bounds.MinCoords.y + 1.01f);
                    break;
                case Spawn.BottomRight:
                    newBlock.transform.position = new Vector3(_bounds.MaxCoords.x, -0.66f, _bounds.MinCoords.y + 1.01f);
                    break;
            }

            _timer = SpawnTimer;
        }
	}

    public void SpawnNew()
    {
        GameObject newBlock = (GameObject)Instantiate(Resources.Load("prefabs/block" + (int)Random.Range(1, 4)));
        Block blockComp = newBlock.GetComponent<Block>();
        blockComp.Spawn = (Spawn)count++;

        count = count > 3 ? 0 : count;

        switch (blockComp.Spawn)
        {
            case Spawn.TopLeft:
                newBlock.transform.position = new Vector3(_bounds.MinCoords.x, -0.66f, _bounds.MaxCoords.y - 1.01f);
                break;

            case Spawn.TopRight:
                newBlock.transform.position = new Vector3(_bounds.MaxCoords.x, -0.66f, _bounds.MaxCoords.y - 1.01f);
                break;
            case Spawn.BottomLeft:
                newBlock.transform.position = new Vector3(_bounds.MinCoords.x, -0.66f, _bounds.MinCoords.y + 1.01f);
                break;
            case Spawn.BottomRight:
                newBlock.transform.position = new Vector3(_bounds.MaxCoords.x, -0.66f, _bounds.MinCoords.y + 1.01f);
                break;
        }
    }
}
