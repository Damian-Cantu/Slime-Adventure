using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using TMPro;
public class Player_Controller : MonoBehaviour
{
    bool left = false;
    bool right = false;
    bool up = false;
    bool down = false;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public float moveSpeed = 5f;
    private float savedMoveSpeed;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float potion_count;
    public float potionHealAmount;
    public float coin_count;

    public int playerScore;
    public int lastScore;
    public GameObject playerScoreFloater;
    public TextMeshPro test;

    [Header("UI Elements")]
    public GameObject playerScoreUI;
    private TMP_Text myScoreUI;
    public GameObject potionUI;
    private TMP_Text myPotionsUI;
    public GameObject coinUI;
    private TMP_Text myCoinsUI;
    public GameObject totalTimeUI;
    private TMP_Text myTotalTimeUI;
    public float playTime;

    bool canMove = true;

    public DamageableCharacter damageableCharacter;

    public static Player_Controller Instance;

    //[SerializeField] GameObject Vfx;
    [Header("Visual Effects")]
    public ParticleSystem sword;
    public ParticleSystem fire;
    public Color slowedColor;

    public bool Upgraded = false;

    // Start is called before the first frame update
    void Start()
    {
        fire.Stop();
        savedMoveSpeed = moveSpeed;
        Instance = this;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageableCharacter = GetComponent<DamageableCharacter>();

        string file = @"Assets\Characters\Player\Player_Data.txt";

        // To read a text file line by line
        if (File.Exists(file))
        {
            // Store each line in array of strings
            string[] lines = File.ReadAllLines(file);
            IDamageable damageable = GetComponent<IDamageable>();
            float hlt = (float)Convert.ToDouble(lines[0]);
            float ptns = (float)Convert.ToDouble(lines[1]);
            float cns = (float)Convert.ToDouble(lines[2]);
            float scr = (float)Convert.ToDouble(lines[3]);
            float tm = (float)Convert.ToDouble(lines[4]);
            damageable.Health = hlt;
            potion_count = ptns;
            coin_count = cns;
            playerScore = (int)scr;
            playTime = tm;
        }

        sword.gameObject.SetActive(false);

        //UI Setup
        GameObject myScoreUIElement = Instantiate(playerScoreUI, GameObject.Find("Canvas").GetComponent<Canvas>().transform);
        myScoreUI = myScoreUIElement.GetComponent<TMP_Text>();
        myScoreUI.text = "Score: " + playerScore.ToString();

        GameObject myPotionsUIElement = Instantiate(potionUI, GameObject.Find("Canvas").GetComponent<Canvas>().transform);
        myPotionsUI = myPotionsUIElement.GetComponentInChildren<TMP_Text>();
        myPotionsUI.text = "x " + potion_count.ToString();

        GameObject myCoinsUIElement = Instantiate(coinUI, GameObject.Find("Canvas").GetComponent<Canvas>().transform);
        myCoinsUI = myCoinsUIElement.GetComponentInChildren<TMP_Text>();
        myCoinsUI.text = "x " + coin_count.ToString();

        GameObject myTotalTimeUIElement = Instantiate(totalTimeUI, GameObject.Find("Canvas").GetComponent<Canvas>().transform);
        myTotalTimeUI = myTotalTimeUIElement.GetComponentInChildren<TMP_Text>();
        myTotalTimeUI.text = "00:00:00";

    }

    // Update is called once per frame  
    private void FixedUpdate()
    {
        if(Upgraded)
        {
            swordAttack.damage = 6;
        }
        else
        {
            swordAttack.damage = 3;
        }

        playTime += Time.fixedDeltaTime;
        myTotalTimeUI.text = playTime.ToString();
        int displayHours = (int)playTime / 3600;
        int displayMinutes = (int)playTime / 60;
        int displaySeconds = (int)playTime % 60;

        myTotalTimeUI.text = String.Format("{0:00}:{1:00}:{2:00}", displayHours, displayMinutes, displaySeconds);

        if (canMove)
        {
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.y = Input.GetAxisRaw("Vertical");

            if (movementInput != Vector2.zero)
            {
                animator.SetFloat("moveX", movementInput.x);
                animator.SetFloat("moveY", movementInput.y);
                //bool success = TryMove(movementInput); 
                rb.AddForce(movementInput * moveSpeed * Time.deltaTime);

                if (rb.velocity.magnitude > maxSpeed)
                {
                    float limitedSpeed = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, idleFriction);
                    rb.velocity = rb.velocity.normalized * limitedSpeed;
                }

                animator.SetBool("isMoving", true);
            }
            else
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
                animator.SetBool("isMoving", false);
            }

            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
                left = true;
                right = false;
                down = false;
                up = false;
            }
            if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
                right = true;
                left = false;
                up = false;
                down = false;
            }

            if (movementInput.y < 0)
            {
                right = false;
                left = false;
                up = false;
                down = true;
            }
            if (movementInput.y > 0)
            {
                right = false;
                left = false;
                up = true;
                down = false;
            }
        }
    }
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnAttack()
    {
        animator.SetTrigger("Sword");
        if(Upgraded)
        {
            sword.gameObject.SetActive(true);
            sword.Play();
        }
    }

    void OnHeal()
    {
        print("HEAL PRESSED");
        if (potion_count > 0)
        {
            this.GetComponent<IDamageable>().Heal(potionHealAmount);
            potion_count -= 1;
        }
    }

    public void SwordAttack()
    {
        LockMovement();
        int ranVal = UnityEngine.Random.Range(0,2);

        if(ranVal == 0)
        {
            soundEffectsHandler.Instance.PlaySound("swordSwing");
        }
        else
        {
            soundEffectsHandler.Instance.PlaySound("swordSwingInv");
        }
        

        if (left)
        {
            swordAttack.AttackLeft();
        }
        if (right)
        {
            swordAttack.AttackRight();
        }
        if (up)
        {
            swordAttack.AttackUp();
        }
        if (down)
        {
            swordAttack.AttackDown();
        }
    }

    public void DeathScreen()
    {
        StartCoroutine(WaitFor(2.5f));
        //SceneManager.LoadScene("DeathMenu", LoadSceneMode.Single);
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
        if(Upgraded)
        {
            //Vfx.SetActive(false);
        }
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        swordAttack.StopAttack();
        canMove = true;
    }

    public void updateScore(Camera cam)
    {
        GameObject score = Instantiate(playerScoreFloater, GameObject.Find("Canvas").GetComponent<Canvas>().transform);

        score.transform.position = cam.WorldToScreenPoint(transform.position);

        myScoreUI.text = "Score: " + playerScore.ToString();

        //score.GetComponent<TextMeshPro>().text = playerScore.ToString();
        score.GetComponent<TMP_Text>().text = lastScore.ToString();
       
    }

    public void updateInvtory(string item, int amount)
    {
        switch (item)
        {
            case "coin":
                myCoinsUI.text = "x " + coin_count.ToString();
                soundEffectsHandler.Instance.PlaySound("coinPickup");
                break;

            case "potion":
                myPotionsUI.text = "x " + potion_count.ToString();
                soundEffectsHandler.Instance.PlaySound("coinPickup");
                break;

            default:
                print("Could not find item string");
                break;
        }
    }

    public void StatusEffectsIEnum(string status, float seconds, float tickTime, float damage)
    {
        StartCoroutine(statusEffects(status, seconds, tickTime, damage));
    }

    public IEnumerator statusEffects(string status, float seconds, float tickTime, float damage)
    {
        float tick = 0f;
        //float oldSpeed = moveSpeed;
        float halfSpeed = savedMoveSpeed / 2;
        for (float i = 0f; i <= seconds; i += Time.deltaTime)
        {
            switch (status)
            {
                case "slow":
                    moveSpeed = halfSpeed;
                    spriteRenderer.color = slowedColor;
                    break;

                case "fire":
                    if(fire.isEmitting == false)
                    {
                        fire.Play(true);
                        soundEffectsHandler.Instance.PlaySound("catchFire");
                    }
                    
                    if(tick >= tickTime)
                    {
                        damageableCharacter.OnHit(damage);
                        tick = 0;
                    }
                    else
                    {
                        tick += Time.deltaTime;
                    }
                    //damageableCharacter.OnHit(0.5f);
                    break;

                default:
                    print("Could not find status string");
                    break;
            }
            yield return null;
        }

        if(fire.isEmitting == true)
        {
            fire.Stop(false);
        }
        
        moveSpeed = savedMoveSpeed;
        spriteRenderer.color = Color.white;
    }

    public IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        SceneManager.LoadScene("DeathMenu", LoadSceneMode.Single);
    }
}
