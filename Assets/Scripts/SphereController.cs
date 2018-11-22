using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour {

    public float moveSpeed = 18f;
    private Rigidbody rb;
    private bool canControl;
    public float hitTimer = 0.5f;
    private Vector2 touchPos;
    private float deltaX;
    private float deltaZ;
    
    private BoundsHandler _bounds;
    private PlayerState _playerState;
    public bool _pc = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _bounds = GameObject.Find("EnemyHandler").GetComponent<BoundsHandler>();
        _playerState = GetComponent<PlayerState>();
        
    }
    private void Start()
    {
        canControl = true;
    }

    void FixedUpdate () {
        
        if (canControl)
        {

            if (_pc)
            {
                if (Input.GetMouseButton(0))
                    rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y")), moveSpeed * Time.deltaTime);
            }
            else
            {
                if (Input.touchCount > 0)
                {


                    Touch touch = Input.GetTouch(0);
                    Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            
                            break;

                        case TouchPhase.Moved:
                            deltaX = touch.deltaPosition.x;
                            deltaZ = touch.deltaPosition.y;

                            rb.velocity = new Vector3(deltaX, 0, deltaZ) * moveSpeed * Time.deltaTime;

                            break;

                        case TouchPhase.Ended:
                            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, moveSpeed * Time.deltaTime);
                            break;
                    }
                }
                else
                {
                    if (rb.velocity != Vector3.zero)
                        rb.velocity = Vector3.zero;
                }
                
            }
        }

        else
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer < 0)
            {
                hitTimer = 0.5f;
                rb.velocity = Vector3.zero;
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    deltaX = touchPos.x - transform.position.x;
                    deltaZ = touchPos.z - transform.position.z;
                }

                canControl = true;

            }
        }
        
        
    
    transform.position = new Vector3(Mathf.Clamp(transform.position.x, _bounds.MinCoords.x , _bounds.MaxCoords.x), transform.position.y,
            Mathf.Clamp(transform.position.z, _bounds.MinCoords.y , _bounds.MaxCoords.y));
        
    }
  

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "block")
        {
            Vector3 block = other.gameObject.transform.position;
            Vector3 forceDir;

            if (Mathf.Abs(block.z - transform.position.z) < 0.9f)
            {
                if (block.x > transform.position.x)
                    forceDir = Vector3.left;

                else
                    forceDir = Vector3.right;
            }
            else
            {
                if (block.z > transform.position.z)
                    forceDir = new Vector3(0, 0, -1);
                else
                    forceDir = new Vector3(0, 0, 1);
            }
            

            canControl = false;

            rb.velocity = Vector3.zero;
            rb.AddForce( forceDir * 5f, ForceMode.Impulse);

            _playerState.removeLife();
        }
    }
}
