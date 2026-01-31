using UnityEngine;

public class NPCMaskReceiver : MonoBehaviour
{
    public MaskItem rewardMask;

    public NPCController npcController;

    // SpriteRenderer del NPC (NO UI)
    public SpriteRenderer npcSpriteRenderer;

    //public bool hasReceivedMask = false;

    public NPCDialog npcDialog;

    public MaskItem ReceiveMask(MaskItem givenMask)
    {
        string npcName = GetComponent<NPCBase>().GetNPCName();

        int value = givenMask.GetValueForNPC(npcName);

        Debug.Log("NPC " + npcName + " recibe " + givenMask.maskName + 
              " → valor: " + value);

        //if (npcBase.hasMask)
        //{
        //    Debug.Log("NPC already received a mask.");
        //     return null;
        // }

        // hasReceivedMask = true;
        //npcBase.SetHasMask(true);

        Debug.Log("NPC received mask: " + givenMask.maskName);

        // Cambiar sprite del NPC
        SetNPCMaskVisual(givenMask);

        Debug.Log("NPC gives back mask: " + rewardMask.maskName);

        GameManager.Instance.AddScore(value);

        npcDialog.StartResponseDialog();

        Debug.Log("npcController es null? " + (npcController == null));
        npcController.OnMaskReceived();

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
