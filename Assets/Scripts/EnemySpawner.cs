using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float SpawnTimer;
    private float _timer;
    private int count = 0;
    private BoundsHandler _bounds;
    public static int blocksCount = 0;
    public static int DestroyedBlocksCount = 0;

    [SerializeField]
    private int _maxSpawnedEnemies = 3;

    public float blockSpeed = 3;

    public Vector2 spawnOffset;
    
 
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
        
        _timer = 0.3f;
	}
	
	
	void Update () {
        _timer -= Time.deltaTime;

        if(_timer < 0)
        {
            spawnBlock();
                
            _timer = SpawnTimer;
            Debug.Log(blocksCount + " max: " + MaxSpawnedEnemies);
        }
}
    public void spawnBlock()
    {
        if (blocksCount >= MaxSpawnedEnemies)
            return;

        int val = (int)Random.Range(1, 3);

        GameObject newBlock = (GameObject)Instantiate(Resources.Load("prefabs/blocks/blue/block" + val));
        Block blockComp = newBlock.GetComponent<Block>();

        blockComp.Spawn = (Spawn)count++;

        count = count > 3 ? 0 : count;
        

        Vector2 offset = val == 1 ? spawnOffset : new Vector2(.12f, .12f);


        blockComp.MoveSpeed = blockSpeed;


        switch (blockComp.Spawn)
        {
            case Spawn.TopLeft:
                newBlock.transform.position = new Vector3(_bounds.MinCoords.x + offset.x, -0.66f, _bounds.MaxCoords.y - offset.y);
                break;

            case Spawn.TopRight:
                newBlock.transform.position = new Vector3(_bounds.MaxCoords.x - offset.x, -0.66f, _bounds.MaxCoords.y - offset.y);
                break;
            case Spawn.BottomLeft:
                newBlock.transform.position = new Vector3(_bounds.MinCoords.x + offset.x, -0.66f, _bounds.MinCoords.y + offset.y);
                break;
            case Spawn.BottomRight:
                newBlock.transform.position = new Vector3(_bounds.MaxCoords.x - offset.x, -0.66f, _bounds.MinCoords.y + offset.y);
                break;
        }

        blocksCount++;


    }

    //public void SpawnNew()
    //{
    //    if (blocksCount >= MaxSpawnedEnemies)
    //        return;

    //    int val = (int)Random.Range(1, 3);

    //    GameObject newBlock = (GameObject)Instantiate(Resources.Load("prefabs/blocks/blue/block" + val));
    //    Block blockComp = newBlock.GetComponent<Block>();
    //    blockComp.Spawn = (Spawn)count++;

    //    count = count > 3 ? 0 : count;

    //    blockComp.MoveSpeed = blockSpeed;

    //    float offset = val == 2 ? 0: xOffset;
    //    newBlock.GetComponent<Block>().xOffset = offset;

    //    switch (blockComp.Spawn)
    //    {
    //        case Spawn.TopLeft:
    //            newBlock.transform.position = new Vector3(_bounds.MinCoords.x + offset, -0.66f, _bounds.MaxCoords.y - 1.01f);
    //            break;

    //        case Spawn.TopRight:
    //            newBlock.transform.position = new Vector3(_bounds.MaxCoords.x - offset, -0.66f, _bounds.MaxCoords.y - 1.01f);
    //            break;
    //        case Spawn.BottomLeft:
    //            newBlock.transform.position = new Vector3(_bounds.MinCoords.x + offset, -0.66f, _bounds.MinCoords.y + 1.01f);
    //            break;
    //        case Spawn.BottomRight:
    //            newBlock.transform.position = new Vector3(_bounds.MaxCoords.x - offset, -0.66f, _bounds.MinCoords.y + 1.01f);
    //            break;
    //    }

    //    blocksCount++;
    //}

    public void SetSpeed(float speed)
    {
        blockSpeed = speed;
    }

    public void AddToMaxEnemies(int n)
    {
        _maxSpawnedEnemies += n;
    }

    public void substractFromSpawnTimer(float n)
    {
        SpawnTimer -= n;
        Debug.Log("Substracted!");
    }

    #region Properties
    public int MaxSpawnedEnemies
    {
        get
        {
            return _maxSpawnedEnemies;
        }

        set
        {
            _maxSpawnedEnemies = value;
        }
    }


    #endregion
}
