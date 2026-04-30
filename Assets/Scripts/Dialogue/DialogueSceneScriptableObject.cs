using UnityEngine;

[CreateAssetMenu(fileName = "DialogueScene", menuName = "Scriptable Objects/DialogueScene")]
public class DialogueSceneScriptableObject : ScriptableObject
{
    public DialogueLine[] dialogueLines;
}