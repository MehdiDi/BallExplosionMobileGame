using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    private float _moveSpeed = 3;
    [SerializeField]
    private Vector3 _dir;
    private Rigidbody rb;

    private EnemySpawner.Spawn _spawn;

    public float StartMoving = 1f;
    private Vector2 _offset;
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

    private Vector2 maxCoords;
    private Vector2 minCoords;
    private bool _canChange = true;



    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    public Vector2 Offset
    {
        get
        {
            return _offset;
        }

        set
        {
            _offset = value;
        }
    }

    void Start () {
        _boundsHandler = GameObject.Find("EnemyHandler").GetComponent<BoundsHandler>();
        rb = GetComponent<Rigidbody>();

        _dir = getDirection(Spawn);

        maxCoords = _boundsHandler.MaxCoords;
        minCoords = _boundsHandler.MinCoords;

        if (gameObject.name.Contains("1"))
        {

            minCoords += Offset - new Vector2(.05f, .05f);
            maxCoords -= Offset + new Vector2(.05f, .05f);
            if (gameObject.name.Contains("1"))
                minCoords += new Vector2(0.25f, 0);
        }
        StartCoroutine(CollisionTime(StartMoving));
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
        if (!_canChange)
            return;

        if (transform.position.x >= maxCoords.x || transform.position.x <= minCoords.x)
            ChangeDirection('H');


        if (transform.position.z >= maxCoords.y || transform.position.z <= minCoords.y)
            ChangeDirection('V');

        StartCoroutine(changeTimer(0.15f));

    }

    private Vector3 getDirection(EnemySpawner.Spawn spawn)
    {
        Vector3 direction = Vector3.zero;
        switch (spawn)
        {
            case EnemySpawner.Spawn.TopLeft:
                direction = new Vector3(Random.Range(3, 0.1f), 0, Random.Range(-3, -0.15f));
                break;
            case EnemySpawner.Spawn.TopRight:
                direction = new Vector3(Random.Range(-3, -0.1f), 0, Random.Range(-3, -0.15f));
                break;
            case EnemySpawner.Spawn.BottomLeft:
                direction = new Vector3(Random.Range(3, 0.1f), 0, Random.Range(3, 0.15f));
                break;
            case EnemySpawner.Spawn.BottomRight:
                direction = new Vector3(Random.Range(-3, -0.1f), 0, Random.Range(3, 0.15f));
                break;
            
        }
        return direction;
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

    private void SetColor()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "player" || other.gameObject.tag == "block")
        {
            
            Vector3 player = other.gameObject.transform.position;
            if (Mathf.Abs(player.z - transform.position.z) < 0.5f)
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

    private IEnumerator CollisionTime(float waitTime)
    {
        float colTimer = 0;
        do
        {
            colTimer += Time.deltaTime;
            yield return null;
        } while (colTimer < waitTime);

        gameObject.layer = 8;
        
    }

    private IEnumerator changeTimer(float t)
    {
        _canChange = false;
        float colTimer = 0;

        do
        {
            colTimer += Time.deltaTime;
            yield return null;
        } while (colTimer > t);
        _canChange = true;
    }
}
