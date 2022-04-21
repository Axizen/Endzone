using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    protected PlayerInputHandler _playerInputHandler;

    //Player object 
    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody { get => _rigidbody; }

    public GameObject _hands;



    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{_hands.name} is colliding with {other.name}");

        if (other.gameObject.tag == "Ball") 
         {
            other.transform.parent = _hands.transform;
        }
    }

}



