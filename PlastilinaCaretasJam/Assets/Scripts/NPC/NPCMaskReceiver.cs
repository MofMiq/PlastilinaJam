using UnityEngine;

public class NPCMaskReceiver : MonoBehaviour
{
    public MaskItem rewardMask;

    // Animator del NPC
    public Animator npcAnimator;

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
        if (rewardMask.maskName == "InvisibleMask")
        {
            Debug.Log("AAAA");
        }

        GameManager.Instance.AddScore(value);

        npcDialog.StartResponseDialog(givenMask);

        return rewardMask;
    }

    void SetNPCMaskVisual(MaskItem mask)
    {
        string npcName = GetComponent<NPCBase>().GetNPCName();
        RuntimeAnimatorController animatorToUse = mask.GetAnimatorForNPC(npcName);

        if (npcAnimator != null && animatorToUse != null)
        {
            npcAnimator.runtimeAnimatorController = animatorToUse;
        }
        else
        {
            Debug.LogWarning("Missing npcAnimator or animator controller for NPC: " + npcName);
        }
    }
}
