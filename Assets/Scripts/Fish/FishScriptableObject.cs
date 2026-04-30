using UnityEngine;

[CreateAssetMenu(fileName = "FishObject", menuName = "Scriptable Objects/FishObject")]
public class FishScriptableObject : ScriptableObject
{
    public DialogueLine[] dialogueLines;
}