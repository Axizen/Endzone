using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgent : Agent
{
    private Rigidbody enemyAgentRb;
    public Rigidbody enemyAgentRigidbody { get => enemyAgentRb; }

    [HideInInspector]
    public float speed;

    private float baseValue = -0.75f;

    // Start is called before the first frame update
    void Start()
    {
        enemyAgentRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Prevent opponents to bounce
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, baseValue, transform.position.z);
        }
    }
}
