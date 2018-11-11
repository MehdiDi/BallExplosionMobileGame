using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCircleHandler : MonoBehaviour {

    [SerializeField]
    private List<GameObject> _inCircleBlocks;

    public List<GameObject> InCircleBlocks
    {
        get
        {
            return _inCircleBlocks;
        }

        set
        {
            _inCircleBlocks = value;
        }
    }

    private void Awake()
    {
        _inCircleBlocks = new List<GameObject>();
    }

    void Start()
    {

    }

    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "block")
        {
            _inCircleBlocks.Add(other.gameObject);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "block")
        {
            _inCircleBlocks.Remove(other.gameObject);
        }
    }
}
