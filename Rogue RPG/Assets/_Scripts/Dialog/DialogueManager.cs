using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogPanel;
    public Text nameText;
    public Text dialogueText;

    private Queue<string> sentences;

    public static DialogueManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogPanel.SetActive(true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        dialogPanel.SetActive(false);
        
    }
}
