using UnityEngine;

public class DialogButton : MonoBehaviour
{
    public DialogUI npcDialog;

    public void OnNextPressed()
    {
        npcDialog.NextLine();
    }
}
