using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Enemy : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    public float damage = 1;
    public float knockbackForce = 100f;
    public float moveSpeed = 500f;
    public DetectionZone detectionZone;
    private DamageableCharacter damageableCharacterScript;
    Rigidbody2D rb;

    public enum statusModifiers // your custom enumeration
    {
        none,
        slow,
        fire,
        blind
    };

    [Header("Status Effects")]
    public statusModifiers statusModifier;
    public float statusLength = 1f;
    public float statusTickTime = 0f;
    public float statusDamage = 0f;

    private Transform target;

    public Transform enemy;
    public float nextWaypointDistance = 3f;

    Path path;
    //int currentWaypoint = 0;
    //bool reachedEndOfPath = false;

    Seeker seeker;

    [Header("Dropped Items")]
    public GameObject potion;
    public float potionDropRate = 0.5f;
    public GameObject coin;
    public float coinDropRate = 0.5f;

    void Start()
    {
        damageableCharacterScript = GetComponent<DamageableCharacter>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //seeker = GetComponent<Seeker>();
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        //InvokeRepeating("UpdatePath", 0f, .5f);
    }
    /*void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }*/
    //FixedUpdate
    void FixedUpdate()
    {    
        if(damageableCharacterScript.Health == 0)
        {
            moveSpeed = 0;
            rb.mass = 2;
        }

        //if(path == null)
        //{
        //    return;
       //}
        //if(currentWaypoint >= path.vectorPath.Count)
        //{
          //  reachedEndOfPath = true;
            //return;
        //}
        //else
        //{
          //  reachedEndOfPath = false;
       //}

        /*Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * moveSpeed * Time.deltaTime;
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

            if(direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }*/
        
        if(detectionZone.entered)
        {
            animator.SetBool("isMoving", true);
            Vector2 direction = (detectionZone.detectedObj[0].transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed * Time.deltaTime);


        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void ItemDrops()
    {
        float ranDropRate = Random.Range(0f, 1f);

        if (ranDropRate <= potionDropRate)
        {
            GameObject potionItem = Instantiate(potion);
            potionItem.transform.position = transform.position;

            //score.transform.position = cam.WorldToScreenPoint(transform.position);
        }

        if (ranDropRate <= coinDropRate)
        {
            GameObject coinItem = Instantiate(coin);
            coinItem.transform.position = transform.position;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Collider2D collider = other.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();
        Player_Controller playerController = collider.GetComponent<Player_Controller>();

        if(damageable != null && collider.tag == "Player")
        {
            Vector2 direction = (collider.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;
            damageable.OnHit(damage, knockback);
            //StartCoroutine(playerController.statusEffects(statusModifier.ToString(), 3f));
            playerController.StatusEffectsIEnum(statusModifier.ToString(), statusLength, statusTickTime, statusDamage);
        }
    }
}