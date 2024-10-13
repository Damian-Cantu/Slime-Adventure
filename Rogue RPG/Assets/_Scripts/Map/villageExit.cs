using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
public class villageExit : MonoBehaviour
{
    public int nextRoom = 0;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            IDamageable damageable = col.GetComponent<IDamageable>();
            string health = damageable.Health.ToString();
            string ptns = col.GetComponent<Player_Controller>().potion_count.ToString();
            string cns = col.GetComponent<Player_Controller>().coin_count.ToString();
            string file = @"Assets\Characters\Player\Player_Data.txt";
            string[] textLines1 = {health, ptns, cns};
  
            File.WriteAllLines(file, textLines1);
            
            // pick the next room
            switch(nextRoom){
                case 1: 
                    SceneManager.LoadScene("Village", LoadSceneMode.Single); 
                    break;
                case 2: 
                    SceneManager.LoadScene("Plains", LoadSceneMode.Single); 
                    break;
                case 3: 
                    SceneManager.LoadScene("Ice", LoadSceneMode.Single); 
                    break;
                case 4: 
                    SceneManager.LoadScene("Lava", LoadSceneMode.Single); 
                    break;
            }
            
        }
    }
}
