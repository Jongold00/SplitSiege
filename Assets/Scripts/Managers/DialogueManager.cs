using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;


// SOURCE https://github.com/markv12/VertexTextAnimationDemo/blob/master/Assets/Scripts/DialogueManager.cs#L26
public class DialogueManager : MonoBehaviour
{
    public TMPro.TMP_FontAsset fontToUse;
    public TMP_Text textBox;

    [TextArea(0, 5)]
    public string[] dialogueSet;
    private int currentDialogueIndex = 0;

    private Coroutine dialogueRoutine;
    private DialogueVertexAnimator dialogueVertexAnimator;
    public void Awake()
    {
        dialogueVertexAnimator = new DialogueVertexAnimator(textBox);
        textBox.font = fontToUse;
    }
    public void PlayNextDialogue()
    {
        if (dialogueRoutine != null)
        {
            StopCoroutine(dialogueRoutine);
            dialogueRoutine = null;
        }

        if (currentDialogueIndex >= dialogueSet.Length)
        {
            EventsManager.instance.GameStateChange(GameStateManager.GameState.Building);
            return;
        }

        List<DialogueUtility.DialogueCommand> commands = DialogueUtility.ProcessInputString(dialogueSet[currentDialogueIndex], out string totalTextMessage);
        currentDialogueIndex++;
        dialogueRoutine = StartCoroutine(dialogueVertexAnimator.AnimateTextIn(commands, totalTextMessage, null));
    }
    
}
