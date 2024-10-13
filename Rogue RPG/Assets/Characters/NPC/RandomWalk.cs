using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public float _randomWalkInterval = 0;
    public float _randomWalkTimer = 0;
    public float _randomWalkSpeed = 1.0f;
    private Vector2 npc_velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // set interval
        _randomWalkInterval = Random.Range(1, 3);

        // set movement
        updateMovement();

    }

    // Update is called once per frame
    void Update()
    {
        // update timer
        _randomWalkTimer += Time.deltaTime;

        // check for grounded
        if (_randomWalkTimer >= _randomWalkInterval)
        {
            updateMovement();

            // reset timer
            _randomWalkTimer = 0;

            // reset speed
            if (Random.Range(0, 100) >= 20)
            {
                _randomWalkSpeed = 1.0f;
            }
            else
            {
                _randomWalkSpeed = 0;
            }

        }
        // if timer is up change direction

        // else keep going in same direction
        //npc_cc.Move(npc_velocity * Time.deltaTime);
    }

    void updateMovement()
    {
        // pick random direction
        Vector2 _direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

        // set velocity
        npc_velocity = _direction.normalized * _randomWalkSpeed;

        // reset interval
        _randomWalkInterval = Random.Range(4, 10);
    }
}