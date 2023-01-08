using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePannel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;


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
        //Get the text component of the choices
        choicesText = new TextMeshProUGUI[choices.Length];
        for (int i = 0; i < choices.Length; i++)
        {
            choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }
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
        DialogueActive = true;
        dialogueText.text = dialogue.GetSentence(0);
    }

    public void EndDialogue()
    {
        dialoguePannel.SetActive(false);
        DialogueActive = false;
    }

    public void DisplayNextSentence()
    {
        Debug.Log("Next Sentence");
        dialogueText.text = "Next Sentence";
    }

    public void DialogueAction(CallbackContext ctx)
    {
        if (DialogueActive)
        {
            Debug.Log("Dialogue Action");
            ClientChatGPT.GetInstance().ReadContext(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text);
            //DisplayNextSentence();
        }
    }
    
    public void DisplayChoices(string[] text)
    {
        for (int i = 0; i < choices.Length; i++)
        {
            this.choices[i].SetActive(true);
            choicesText[i].text = text[i+1];
        }

        Debug.Log(choices.Length);

        StartCoroutine(SelectFirstChoice());
    }

    public void HideChoices()
    {
        for (int i = 0; i < choices.Length; i++)
        {
            this.choices[i].SetActive(false);
        }
    }

    private IEnumerator SelectFirstChoice()
    {
        //Clear first then select the first choice
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
        EventSystem.current.SetSelectedGameObject(choices[0]);
    }

}
