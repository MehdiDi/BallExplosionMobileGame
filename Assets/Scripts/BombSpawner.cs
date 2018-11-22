using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour {

    private GameObject _bomb;

    private Vector2 _minCoords;
    private Vector2 _maxCoords;
    private Transform player;
    public float minDistance = 6;
    private Object _bombPrefab;
    private Object _circlePrefab;

    private void Awake()
    {
        _bombPrefab = Resources.Load("prefabs/bomb");

    }
    void Start () {

        BoundsHandler boundsHandler = GameObject.Find("EnemyHandler").GetComponent<BoundsHandler>();
        _minCoords = boundsHandler.MinCoords;
        _maxCoords = boundsHandler.MaxCoords;
        player = GameObject.Find("player").transform;

    }
	
	
	void Update () {
		if(_bomb == null)
        {
            _bomb = (GameObject)Instantiate(_bombPrefab);

            Vector3 randomPos = new Vector3(Random.Range(_minCoords.x, _maxCoords.x), -0.6f, Random.Range(_minCoords.y, _maxCoords.y));
            if (player == null)
                return;

            while (Vector3.Distance(player.position, randomPos) < minDistance)
            {
                randomPos = new Vector3(Random.Range(_minCoords.x, _maxCoords.x), -0.6f, Random.Range(_minCoords.y, _maxCoords.y));

            }

            _bomb.GetComponent<MeshRenderer>().enabled = true;
            _bomb.transform.position = new Vector3(Random.Range(_minCoords.x, _maxCoords.x), -0.6f, Random.Range(_minCoords.y, _maxCoords.y));

        }
	}
}
