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
}
