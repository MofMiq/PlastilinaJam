using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventorySlotDebug : MonoBehaviour
{
    public MaskItem maskItem;
    public NPCMaskReceiver npcReceiver;

    public Image iconImage;
    private Button button;
    public int slotIndex;

    void Awake()
    {
        button = GetComponent<Button>();
        LoadFromGameManager();
        RefreshVisual();
    }

    void LoadFromGameManager()
    {
        if (GameManager.Instance != null && GameManager.Instance.savedInventory.Count > slotIndex)
        {
            maskItem = GameManager.Instance.savedInventory[slotIndex];
        }
    }

    void RefreshVisual()
    {
        if (maskItem != null && iconImage != null)
        {
            iconImage.sprite = maskItem.icon;
            iconImage.enabled = true;
        }
        if (maskItem.name == "InvisibleMask")
        {
            iconImage.enabled = false;
        }
    }

    public void OnSlotClicked()
    {
        if (maskItem == null || npcReceiver == null)
            return;

        MaskItem reward = npcReceiver.ReceiveMask(maskItem);

        if (reward != null)
        {
            Debug.Log("Slot swapped to new mask: " + reward.maskName);

            maskItem = reward;
            RefreshVisual();
        }

        DisableAllSlots();
    }

    void DisableAllSlots()
    {
        InventorySlotDebug[] allSlots = FindObjectsOfType<InventorySlotDebug>();
        foreach (var slot in allSlots)
        {
            if (slot.button != null)
                slot.button.interactable = false;
            Debug.Log("Disabled slot with mask: " + (slot.maskItem != null ? slot.maskItem.maskName : "None"));
        }
        Debug.Log("All inventory slots disabled.");

        SaveInventoryToGameManager();
    }

    void SaveInventoryToGameManager()
    {
        if (GameManager.Instance == null) return;

        InventorySlotDebug[] allSlots = FindObjectsOfType<InventorySlotDebug>();
        List<MaskItem> masks = new List<MaskItem>();

        System.Array.Sort(allSlots, (a, b) => a.slotIndex.CompareTo(b.slotIndex));

        foreach (var slot in allSlots)
        {
            masks.Add(slot.maskItem);
        }

        GameManager.Instance.SaveInventory(masks);
    }
}
