using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{

    public GameObject _hands;

    protected override void setupMovementModule()
    {
        movementModule = new SimpleInputMovementModule(this);
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



