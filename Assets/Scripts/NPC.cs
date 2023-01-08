using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Dialogue grannyDialogue;

    string context = "Were playing role play game where you had to give me a dialogue and 4 options every time. You are a Grandma who need help to cross the road in a weird world ! Add a line break after each line. ";
    string grannySentence;
    string[] grannyChoices;
    // Start is called before the first frame update
    void Start()
    {
        //grannyDialogue = GetComponent<Dialogue>();
        //ClientChatGPT.GetInstance().ReadContext(context);
        //grannySentence = ClientChatGPT.GetInstance().GetNPCAnswer();
        //grannyDialogue.AddSentence(grannySentence);
        //grannyChoices = ClientChatGPT.GetInstance().GetChoices();
    }

    // Update is called once per frame
    void Update()
    {
        grannyDialogue = GetComponent<Dialogue>();
        ClientChatGPT.GetInstance().ReadContext(context);
        grannySentence = ClientChatGPT.GetInstance().GetNPCAnswer();
        grannyDialogue.AddSentence(grannySentence);
        grannyChoices = ClientChatGPT.GetInstance().GetChoices();
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
