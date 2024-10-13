using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    public Animator animator;
    public bool disableSimulation = false;
    Rigidbody2D rb;
    Collider2D physicsCollider;
    public float Health
    {
        set
        {
            if(value < _health)
            {
                if(!forPlayer)
                {
                    animator.SetTrigger("hit");
                }
            }

            _health = value;

            if(_health <= 0)
            {
                
                animator.SetTrigger("death");
                Targetable = false;

                if (forPlayer && isDead == false)
                {
                    soundEffectsHandler.Instance.PauseMusic();
                    soundEffectsHandler.Instance.PlaySound("playerDeath");

                    isDead = true;
                }
            }

            if(value > maxhealth)
            {
                _health = maxhealth;
            }
        }
        get
        {
            return _health;
        }
    }
    public float maxhealth;

    public GameObject healthBar;
    public bool forPlayer = false;
    public GameObject myHealthBar;
    public HealthBar healthBarScript;
    private GameObject container;

    public Vector3 healthBarOffset;

    public int killScore;

    public Camera cam;

    private bool isDead = false;

    public bool Targetable { get{return _targetable;} 
    set{
        _targetable = value;

        if(disableSimulation)
        {
            rb.simulated = false;
        }

        physicsCollider.enabled = value;
    } }

    public float _health;

    public bool _targetable = true;

    public void Start()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();

        cam = Camera.main;

        //Instantiate(new gameObject holder, GameObject.Find("Canvas").GetComponent<Canvas>().transform);
        if (!forPlayer)
        {
            if (!GameObject.Find("enemyHealthBars"))
            {
                container = new GameObject("enemyHealthBars");
                container.transform.SetParent(GameObject.Find("Canvas").GetComponent<Canvas>().transform);
                container.transform.SetAsFirstSibling();
            }
            else
            {
                container = GameObject.Find("enemyHealthBars");
            }
            myHealthBar = Instantiate(healthBar, container.transform);
        }
        else
        {
            myHealthBar = Instantiate(healthBar, GameObject.Find("Canvas").GetComponent<Canvas>().transform);
        }
        
        
        
        healthBarScript = myHealthBar.GetComponent<HealthBar>();
        healthBarScript.setMaxHealth(Health);
        healthBarScript.setHealth(Health);
    }

    public void Update()
    {
        if (!forPlayer)
        {
            myHealthBar.transform.position = cam.WorldToScreenPoint(transform.position) + healthBarOffset;
            healthBarScript.setMaxHealth(maxhealth);
            healthBarScript.setHealth(Health);
        }
        else
        {
             healthBarScript.setMaxHealth(maxhealth);
             healthBarScript.setHealth(Health);

             /*Vector2 newvect = new Vector2(0,0);
             Vector3 newvect2 = new Vector3(2,2,2);
             myHealthBar.GetComponent<RectTransform>().pivot = newvect;
             myHealthBar.GetComponent<RectTransform>().localScale = newvect2;*/
        }
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        rb.AddForce(knockback);

        healthBarScript.setHealth(Health);

        if(!isDead)
        soundEffectsHandler.Instance.PlaySound("takeDamage");
    }

    public void OnHit(float damage)
    {
        Health -= damage;

        healthBarScript.setHealth(Health);

        if(!isDead)
        soundEffectsHandler.Instance.PlaySound("takeDamage");
    }

    public void Heal(float boost)
    {
        Health += boost;
        healthBarScript.setHealth(Health);
        Player_Controller.Instance.potion_count -= 1;
        Player_Controller.Instance.updateInvtory("potion", 1);
    }

    public void OnObjectDestroyed()
    {
        if (!forPlayer)
        {
            Player_Controller.Instance.playerScore += killScore;
            Player_Controller.Instance.lastScore = killScore;
            Player_Controller.Instance.updateScore(cam);

            Enemy enemy = GetComponent<Enemy>();

            if(enemy != null)
            {
                enemy.ItemDrops();
            }
            else
            {
                Skeleton skeleton = GetComponent<Skeleton>();
                if(skeleton != null)
                {
                    skeleton.ItemDrops();
                }
            }
        }

        Destroy(gameObject);
        Destroy(myHealthBar);
    }

    public float getMaxhealth()
    {
        return maxhealth;
    }
}
