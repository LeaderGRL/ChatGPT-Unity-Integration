using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private List<string> sentences; 
    
    // Start is called before the first frame update
    void Start()
    {
        sentences = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSentence(string sentence)
    {
        sentences.Add(sentence);
    }

    public void RemoveSentence(string sentence)
    {
        sentences.Remove(sentence);
    }

    public void ClearSentences()
    {
        sentences.Clear();
    }

    public string GetSentence(int index)
    {
        return sentences[index];
    }
}
