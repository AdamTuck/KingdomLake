using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class DialogueAnimator : MonoBehaviour
{
    private string currentLine;
    
    [SerializeField] private float textSpeed;
    private int index;
    private DialogueSceneScriptableObject dialogueScript;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI dialogueTextComponent;
    [SerializeField] private TextMeshProUGUI characterNameTextComponent;
    [SerializeField] private Texture2D characterPortrait;

    void Update()
    {
        if (PlayerInput.GetInstance().space)
        {
            if (dialogueTextComponent.text == currentLine)
                NextLine();
            else
            {
                StopAllCoroutines();
                dialogueTextComponent.text = currentLine;
            }
        }
    }

    public void StartDialogue(DialogueSceneScriptableObject _dialogueScript)
    {
        dialogueScript = _dialogueScript;
        
        gameObject.SetActive(true);
        index = 0;

        dialogueTextComponent.text = string.Empty;

        currentLine = dialogueScript.dialogueLines[index].line;
        characterNameTextComponent.text = dialogueScript.dialogueLines[index].character;

        StartCoroutine(TypeDialogueLine());
    }

    private IEnumerator TypeDialogueLine ()
    {
        foreach (char c in currentLine.ToCharArray())
        {
            dialogueTextComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < dialogueScript.dialogueLines.Length -1)
        {
            index++;
            dialogueTextComponent.text = string.Empty;
            currentLine = dialogueScript.dialogueLines[index].line;
            characterNameTextComponent.text = dialogueScript.dialogueLines[index].character;
            StartCoroutine(TypeDialogueLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void CancelDialogue ()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
