using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceHouseEntrance : MonoBehaviour
{
    public GameObject bounceHouse;
    public soundEffectsHandler handler;
    public GameObject dj;

    private bool playerIsClose = false;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            StartCoroutine(countdownToEnter());
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

    public IEnumerator countdownToEnter()
    {
        yield return new WaitForSeconds(5);

        WelcomeToTheBounceHouse();
    }

    public void WelcomeToTheBounceHouse()
    {
        handler.PauseMusic();

        bounceHouse.SetActive(true);

        player.transform.position = dj.transform.position;

    }
}
