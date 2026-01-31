using UnityEngine;

public class NPCMaskReceiver : MonoBehaviour
{
    public MaskItem rewardMask;
    private bool hasReceivedMask = false;

    public MaskItem ReceiveMask(MaskItem givenMask)
    {
        if (hasReceivedMask)
        {
            Debug.Log("NPC already received a mask!");
            return null;
        }

        hasReceivedMask = true;

        Debug.Log("NPC received mask: " + givenMask.maskName);
        Debug.Log("NPC gives back mask: " + rewardMask.maskName);

        return rewardMask;   // 👈 DEVUELVE LA MÁSCARA
    }
}
