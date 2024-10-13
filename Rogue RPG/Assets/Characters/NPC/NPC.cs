using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;

    public List<GameObject> buttonsToUse;

    public float wordSpeed;
    public bool playerIsClose;

    public GameObject npcCharacter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                if(buttonsToUse.Count > 0)
                {
                    foreach (GameObject btn in buttonsToUse)
                    {
                        btn.SetActive(true);
                    }
                }
                StartCoroutine(Typing());
            }
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);

        if (buttonsToUse.Count > 0)
        {
            foreach (GameObject btn in buttonsToUse)
            {
                btn.SetActive(false);
            }
        }
    }

    IEnumerator Typing()
    {
        index = 0;
        foreach(char letter in dialogue[index].ToCharArray()) 
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else 
        {
            zeroText();
        }
    }

    public void BuyPotion()
    {
        Player_Controller playerScript = Player_Controller.Instance;
        if(playerScript.coin_count >= 50)
        {
            playerScript.potion_count += 1;
            playerScript.coin_count -= 50;
            playerScript.updateInvtory("potion", 1);
            playerScript.updateInvtory("coin", -50);
            zeroText();
        }
        
    }

    public void BuySword()
    {
        Player_Controller playerScript = Player_Controller.Instance;

        if(playerScript.coin_count >= 100)
        {
            playerScript.Upgraded = true;
            playerScript.coin_count -= 100;
            playerScript.updateInvtory("coin", -100);
            zeroText();
        }
    }

    public void Bribe()
    {
        Player_Controller playerScript = Player_Controller.Instance;

        if(playerScript.coin_count >= 300)
        {
            playerScript.coin_count -= 300;
            playerScript.updateInvtory("coin", -300);
            //Destroy(npcCharacter);
            StartCoroutine(MoveUp(10f));
            NextLine();
        }
    }

    IEnumerator MoveUp(float moveDistance)
    {
        Vector3 targetPosition = transform.position + Vector3.up * moveDistance;
        float moveSpeed = 5f;
        while (targetPosition != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        Destroy(gameObject);
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}
