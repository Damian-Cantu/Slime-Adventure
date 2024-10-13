using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLeave : MonoBehaviour
{
    public GameObject bounceHouse;
    public GameObject player;
    public soundEffectsHandler handler;
    public GameObject entrance;

    private bool playerIsClose = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            GetMeOutOFHere();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            //player = null;
        }
    }

    public void GetMeOutOFHere()
    {
        handler.PlayMusic();

        player.transform.position = entrance.transform.position;

        bounceHouse.SetActive(false);

    }
}
