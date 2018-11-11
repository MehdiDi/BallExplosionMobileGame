using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    private float _moveSpeed;
    [SerializeField]
    private Vector3 _dir;
    private Rigidbody rb;

    private EnemySpawner.Spawn _spawn;

    public float StartMoving = 1f;

    private BoundsHandler _boundsHandler;

    public EnemySpawner.Spawn Spawn
    {
        get
        {
            return _spawn;
        }

        set
        {
            _spawn = value;
        }
    }

    private void Awake()
    {
        
    }

    void Start () {
        _boundsHandler = GameObject.Find("EnemyHandler").GetComponent<BoundsHandler>();
        rb = GetComponent<Rigidbody>();
        

        _moveSpeed = _boundsHandler.enemySpeed;
        switch(Spawn)
        {
            case EnemySpawner.Spawn.TopLeft:
                _dir = new Vector3(Random.Range(3, 0.1f), 0, Random.Range(-3, -0.15f));
                break;
            case EnemySpawner.Spawn.TopRight:
                _dir = new Vector3(Random.Range(-3, -0.1f), 0, Random.Range(-3, -0.15f));
                break;
            case EnemySpawner.Spawn.BottomLeft:
                _dir = new Vector3(Random.Range(3, 0.1f), 0, Random.Range(3, 0.15f));
                break;
            case EnemySpawner.Spawn.BottomRight:
                _dir = new Vector3(Random.Range(-3, -0.1f), 0, Random.Range(3, 0.15f));
                break;
        }

        
    }
	
	void Update () {
        if (StartMoving > 0)
            StartMoving -= Time.deltaTime;

        if(StartMoving < 0)
            transform.position += _dir.normalized * _moveSpeed * Time.deltaTime ;

        CheckPosition();

        if (transform.rotation != Quaternion.Euler(0, 0, 0))
            transform.rotation = Quaternion.Euler(0, 0, 0);

        if (rb.velocity != Vector3.zero)
            rb.velocity = Vector3.zero;
	}

    private void CheckPosition()
    {
        Vector2 maxCoords = _boundsHandler.MaxCoords;
        Vector2 minCoords = _boundsHandler.MinCoords;

        if (transform.position.x >= maxCoords.x || transform.position.x <= minCoords.x)
            ChangeDirection('H');


        if (transform.position.z >= maxCoords.y || transform.position.z <= minCoords.y)
            ChangeDirection('V');

    }

    private void ChangeDirection(char direction)
    {
        switch (direction)
        {
            //Horizontal: H, Vertical: V

            case 'l':
                _dir = new Vector3(-Mathf.Abs(_dir.x), 0, _dir.z);
                break;
            case 'r':
                _dir = new Vector3(Mathf.Abs(_dir.x), 0, -_dir.z);
                break;
            case 'u':
                _dir = new Vector3(_dir.x, 0, Mathf.Abs(_dir.z));
                break;
            case 'd':
                _dir = new Vector3(_dir.x, 0, -Mathf.Abs(_dir.z));
                break;

            case 'H':
                _dir = new Vector3(-_dir.x, 0, _dir.z);
                break;

            case 'V':
                _dir = new Vector3(_dir.x, 0, -_dir.z);
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "player" || other.gameObject.tag == "block")
        {




            Vector3 player = other.gameObject.transform.position;
            if (Mathf.Abs(player.z - transform.position.z) < 0.9f)
            {
                if (player.x > transform.position.x)
                    ChangeDirection('l');
                else
                    ChangeDirection('r');
            }
            else
            {
                if (player.z > transform.position.z)
                    ChangeDirection('d');
                else
                    ChangeDirection('u');
            }
            
        }
    }
}
