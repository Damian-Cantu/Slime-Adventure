using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;
    public Transform player;

    [Header("Dropped Items")]
    public GameObject potion;
    public float potionDropRate = 0.5f;
    public GameObject coin;
    public float coinDropRate = 0.5f;

    private DamageableCharacter damageableCharacterScript;
    // Start is called before the first frame update
    void Start()
    {
        damageableCharacterScript = GetComponent<DamageableCharacter>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }

            if (timeBtwShots <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
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
}
