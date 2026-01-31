using UnityEngine;

public class DialogButton : MonoBehaviour
{
    public NPCDialog npcDialog;

    public void OnNextPressed()
    {
        npcDialog.NextLine();
    }
}
