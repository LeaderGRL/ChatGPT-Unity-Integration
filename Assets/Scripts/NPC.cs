using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Dialogue grannyDialogue;

    string context = "Were playing role play game where you had to give me a dialogue and 4 options every time. You are a Grandma who need help to cross the road in a weird world !";
    string grannySentence;
    string[] grannyChoices;
    // Start is called before the first frame update
    void Start()
    {
        grannyDialogue = GetComponent<Dialogue>();
        ClientChatGPT.GetInstance().ReadContext(context);
        grannySentence = ClientChatGPT.GetInstance().GetNPCAnswer();
        grannyDialogue.AddSentence(grannySentence);
        grannyChoices = ClientChatGPT.GetInstance().GetChoices();
        //grannyDialogue.sentences = new string[3];
        //grannyDialogue.sentences[0] = "Hello there!";
        //grannyDialogue.sentences[1] = "How are you?";
        //grannyDialogue.sentences[2] = "Goodbye!";

        //grannyChoices = new string[4];
        //grannyChoices[0] = "Good";
        //grannyChoices[1] = "Bad";
        //grannyChoices[2] = "Okay";
        //grannyChoices[3] = "I don't know";
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
            DialogueManager.GetInstance().DisplayChoices(grannyChoices);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left trigger");
            DialogueManager.GetInstance().EndDialogue();
        }
    }
}
