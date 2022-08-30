using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //Script
    public DialogueTrigger trigger;

    private Queue<string> sentences;
    public Image avatarImage; 
    public TMP_Text avatarNameText;                             //Nome dell'avatar
    public TMP_Text dialogueText;                               //Testo dialogo
    private bool start;                                         //Controllo primo dialogo

    private void Start()
    {
        sentences = new Queue<string>();
        start = true;
        StartCoroutine(StartFirstDialogue());
    }

    IEnumerator StartFirstDialogue()
    {
        yield return new WaitForSeconds(1);
        if (start)                                              //Se il tutorial � appena iniziato fa partire subito il dialogo
        {
            trigger.TriggerDialogue();                          //Trigger per iniziare il dialogo
            start = false;                                      //indico che non siamo pi� in fase di start
            
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        avatarNameText.text = dialogue.avatarName;
        avatarImage.sprite = dialogue.avatarImage;

        if(avatarImage.sprite.name == "jiggly_solo1024")
        {
            avatarImage.rectTransform.sizeDelta = new Vector2(83, 56);
        }
        else
        {
            avatarImage.rectTransform.sizeDelta = new Vector2(77, 70);
        }

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    //Mostra le frasi
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //Animazione di typing
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    
    //Fine dialogo
    public void EndDialogue()
    {
        trigger.dialogueCanva.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sentences.Clear();
    }
}
