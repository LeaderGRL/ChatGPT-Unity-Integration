using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Dialogue grannyDialogue = new Dialogue();
    // Start is called before the first frame update
    void Start()
    {
        grannyDialogue.sentences = new string[2];
        grannyDialogue.sentences[0] = "Hello there!";
        grannyDialogue.sentences[1] = "How are you?";
        grannyDialogue.sentences[2] = "Goodbye!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if ( other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            DialogueManager.GetInstance().StartDialogue(grannyDialogue);
        }

    }
}
