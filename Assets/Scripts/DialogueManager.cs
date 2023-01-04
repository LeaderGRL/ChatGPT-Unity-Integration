using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePannel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private static DialogueManager instance;
    private bool DialogueActive = false;

    public InputAction dialogueAction;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        dialogueAction.performed += DialogueAction;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        dialogueAction.Enable();
    }

    private void OnDisable()
    {
        dialogueAction.Disable();
    }

    public static DialogueManager GetInstance()
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

    public void DialogueAction(CallbackContext ctx)
    {
        if (DialogueActive)
        {
            DisplayNextSentence();
        }
    }

}
