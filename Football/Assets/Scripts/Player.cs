using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    //Player object 
    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody { get => _rigidbody; }

    public GameObject _hands;



    private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"{_hands.name} is colliding with {other.collider.name}");

        if (other.gameObject.tag == "Ball") 
         {
            other.transform.parent = _hands.transform;
        }
    }

}



