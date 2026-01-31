using UnityEngine;

public class NPCMaskReceiver : MonoBehaviour
{
    public MaskItem rewardMask;

    // SpriteRenderer del NPC (NO UI)
    public SpriteRenderer npcSpriteRenderer;

    private bool hasReceivedMask = false;

    public NPCDialog npcDialog;

    public MaskItem ReceiveMask(MaskItem givenMask)
    {
        string npcName = GetComponent<NPCBase>().GetNPCName();

        int value = givenMask.GetValueForNPC(npcName);

        Debug.Log("NPC " + npcName + " recibe " + givenMask.maskName + 
              " → valor: " + value);

        if (hasReceivedMask)
        {
            Debug.Log("NPC already received a mask.");
            return null;
        }

        hasReceivedMask = true;

        Debug.Log("NPC received mask: " + givenMask.maskName);

        // Cambiar sprite del NPC
        SetNPCMaskVisual(givenMask);

        Debug.Log("NPC gives back mask: " + rewardMask.maskName);

        GameManager.Instance.AddScore(value);

        npcDialog.StartResponseDialog();

        return rewardMask;
    }

    void SetNPCMaskVisual(MaskItem mask)
    {
        if (npcSpriteRenderer != null && mask.npcSprite != null)
        {
            npcSpriteRenderer.sprite = mask.npcSprite;
        }
        else
        {
            Debug.LogWarning("Missing npcSpriteRenderer or npcSprite on MaskItem");
        }
    }
}
