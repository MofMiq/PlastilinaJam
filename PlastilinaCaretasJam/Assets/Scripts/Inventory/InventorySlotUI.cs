using UnityEngine;

public class InventorySlotDebug : MonoBehaviour
{
    public string slotName;

    public void OnSlotClicked()
    {
        Debug.Log("Clicked on slot: " + slotName);
    }
}
