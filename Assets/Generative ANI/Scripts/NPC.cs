using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class NPC : MonoBehaviour
{
    public DialogueGeneration _dialogueGeneration;
    private bool _isDialogueActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_isDialogueActive) return;
        _isDialogueActive = true;
        Debug.Log("NPC trigger activated.");
        UniTaskVoid dialogueTask = _dialogueGeneration.StartDialogueGeneration();
    }
}
