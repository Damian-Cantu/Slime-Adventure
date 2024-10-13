using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
        private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            col.GetComponent<Player_Controller>().coin_count += 10;
            col.GetComponent<Player_Controller>().updateInvtory("coin", 10);
            Destroy(gameObject);
        }
    }
}
