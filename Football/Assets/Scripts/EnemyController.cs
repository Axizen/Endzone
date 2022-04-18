using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    private Rigidbody enemyRb;
    public Rigidbody enemyRigidbody { get => enemyRb; }

    public Player _player;

    [HideInInspector]
    public float speed;

    private float baseValue = -0.75f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
         //Prevent opponents to bounce
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, baseValue, transform.position.z);
        }

        //Get position of player
       // Vector3 ballPosition = new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z);

        //Get direction of player
        Vector3 lookDirection = (_player.transform.position - this.transform.position).normalized;
        lookDirection.y = 0;

        //Move the enemy towards the ball
     //   transform.position = Vector3.MoveTowards(transform.position, ballPosition, speed * Time.deltaTime);

        //Make the enemy look towards the player
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
