using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour {

    public float moveSpeed = 18f;
    private Rigidbody rb;
    private bool canControl;
    public float hitTimer = 0.5f;

    private BoundsHandler _bounds;
    private PlayerState _playerState;

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
            if (Input.mousePosition.y < 0.3f * Screen.height)
            {


                if (Input.GetMouseButton(0))
                {
                    Vector3 vel = rb.velocity;

                    rb.velocity = Vector3.Lerp(new Vector3(vel.x, 0, vel.z),
                        new Vector3(Input.GetAxis("Mouse X") * moveSpeed, 0, Input.GetAxis("Mouse Y") * moveSpeed), moveSpeed * Time.deltaTime);


                }
                else if (rb.velocity != Vector3.zero)
                    rb.velocity = Vector3.zero;
            }
            else if (rb.velocity != Vector3.zero)
                rb.velocity = Vector3.zero;
        }

            else
            {
                hitTimer -= Time.deltaTime;
                if (hitTimer < 0)
                {
                    hitTimer = 0.5f;
                    canControl = true;
                }
            }
        
        
    
    transform.position = new Vector3(Mathf.Clamp(transform.position.x, _bounds.MinCoords.x - 2f, _bounds.MaxCoords.x + 2f), transform.position.y,
            Mathf.Clamp(transform.position.z, _bounds.MinCoords.y - 1f, _bounds.MaxCoords.y + 1));
        
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
