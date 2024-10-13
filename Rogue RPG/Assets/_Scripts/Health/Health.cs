using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int totalHealth;
    public int currentHealth;

    public GameObject healthBar;
    private GameObject myHealthBar;
    private HealthBar healthBarScript;

    public Vector3 healthBarOffset;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        myHealthBar = Instantiate(healthBar, GameObject.Find("Canvas").GetComponent<Canvas>().transform);
        healthBarScript = myHealthBar.GetComponent<HealthBar>();
        healthBarScript.setMaxHealth(totalHealth);
        healthBarScript.setHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        myHealthBar.transform.position = cam.WorldToScreenPoint(transform.position) + healthBarOffset;
        //debug
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Heal(25);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBarScript.setHealth(currentHealth);

        if(currentHealth <= 0)
        {
            Destroy(myHealthBar);
            Destroy(gameObject);

            //It may be better performance wise to hide an object instead of destroy and then recreate again. For now we can just distroy.
            /*myHealthBar.SetActive(false);
            gameObject.SetActive(false);*/
        }
    }

    public void Heal(int heal)
    {
        if(currentHealth < totalHealth)
        {
            currentHealth += heal;
            if(currentHealth > totalHealth)
            {
                currentHealth = totalHealth;
            }
            healthBarScript.setHealth(currentHealth);
        }
        
    }
}
