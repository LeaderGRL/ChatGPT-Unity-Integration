using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePannel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private static DialogueManager instance;
    private bool DialogueActive = false;

    private 

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!DialogueActive)
        {
            return;
        }

        
    }

    DialogueManager GetInstance()
    {
        return instance;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialoguePannel.SetActive(true);
        dialogueText.text = dialogue.sentences[0];
    }

    public void EndDialogue()
    {
        dialoguePannel.SetActive(false);
    }

    public void DisplayNextSentence()
    {
        dialogueText.text = "Next Sentence";
    }

}
