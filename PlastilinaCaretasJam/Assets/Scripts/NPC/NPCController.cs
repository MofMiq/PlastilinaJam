using UnityEngine;

public class NPCController : MonoBehaviour
{
    public NPCDialog npcDialog;
    public NPCMaskReceiver maskReceiver;

    private bool completed = false;

    public void ActivateNPC()
    {
        //maskReceiver.hasReceivedMask = false;
        gameObject.SetActive(true);
        npcDialog.StartResponseDialog();

    }

    public void OnMaskReceived()
    {
        if (completed) return;

        completed = true;

        npcDialog.StartResponseDialog();

        // Avisar al GameManager tras el diálogo
        Invoke(nameof(NotifyGameManager), 0.2f);
    }

    void NotifyGameManager()
    {
        GameManager.Instance.OnNPCCompleted();
    }
}
