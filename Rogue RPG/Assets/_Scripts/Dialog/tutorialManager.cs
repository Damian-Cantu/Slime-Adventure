using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class tutorialManager : MonoBehaviour
{
    public bool isNewPlayer = false;
    public DialogueTrigger tutorialTrigger;

    // Start is called before the first frame update
    void Start()
    {
        string file = @"Assets\Characters\Player\Player_Data.txt";

        // To read a text file line by line
        if (File.Exists(file) == false)
        {
            isNewPlayer = true;
        }

        if (SceneManager.GetActiveScene().name == "Plains" && isNewPlayer)
        {
            tutorialTrigger.TriggerDialogue();
        }
    }
}
