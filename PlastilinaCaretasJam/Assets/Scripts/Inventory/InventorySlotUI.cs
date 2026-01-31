using UnityEngine;

public class InventorySlotDebug : MonoBehaviour
{
    public MaskItem maskItem;
    public NPCMaskReceiver npcReceiver;

    public void OnSlotClicked()
    {
        if (maskItem == null || npcReceiver == null)
        {
            Debug.LogWarning("Slot not configured!");
            return;
        }

        Debug.Log("Giving mask: " + maskItem.maskName);
        npcReceiver.ReceiveMask(maskItem);
    }
}
