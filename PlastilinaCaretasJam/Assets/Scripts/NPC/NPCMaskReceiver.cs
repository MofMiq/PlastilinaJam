using UnityEngine;

public class NPCMaskReceiver : MonoBehaviour
{
    public MaskItem rewardMask;   // La máscara que devuelve
    private bool hasReceivedMask = false;

    public void ReceiveMask(MaskItem givenMask)
    {
        if (hasReceivedMask)
        {
            Debug.Log("NPC already received a mask!");
            return;
        }

        hasReceivedMask = true;

        Debug.Log("NPC received mask: " + givenMask.maskName);
        Debug.Log("NPC gives back mask: " + rewardMask.maskName);

        // Aquí luego actualizaremos el inventario de verdad
    }
}
