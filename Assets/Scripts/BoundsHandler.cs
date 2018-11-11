using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsHandler : MonoBehaviour {  



    public float enemySpeed;
    private Vector2 _maxCoords;
    private Vector2 _minCoords;
    


    public Vector2 MinCoords
    {
        get
        {
            return _minCoords;
        }

        set
        {
            _minCoords = value;
        }
    }

    public Vector2 MaxCoords
    {
        get
        {
            return _maxCoords;
        }

        set
        {
            _maxCoords = value;
        }
    }

    private void Awake()
    {
        Transform MaxX = GameObject.Find("MaxX").transform;
        Transform MinX = GameObject.Find("MinX").transform;
        Transform MaxZ = GameObject.Find("MaxZ").transform;
        Transform MinZ = GameObject.Find("MinZ").transform;


        _maxCoords = new Vector2(MaxX.position.x - 2.0f, MaxZ.position.z - 1f);
        _minCoords = new Vector2(MinX.position.x + 2.0f, MinZ.position.z + 1f);
    }



    
}
