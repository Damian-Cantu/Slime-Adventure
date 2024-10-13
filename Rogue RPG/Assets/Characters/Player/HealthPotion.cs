using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            col.GetComponent<Player_Controller>().potion_count += 1;
            col.GetComponent<Player_Controller>().updateInvtory("potion", 1);
            Destroy(gameObject);
        }
    }
}
