using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    [TextArea]
    public string[] dialogLines;

    private int currentLine = 0;
    public bool dialogFinished = false;

    public DialogUI dialogUI;

    // NUEVO
    public CanvasGroup inventoryCanvasGroup;

    void Start()
    {
        ShowCurrentLine();

        // BLOQUEAR INVENTARIO AL EMPEZAR
        SetInventoryInteractable(false);
    }

    public void NextLine()
    {
        currentLine++;

        if (currentLine >= dialogLines.Length)
        {
            dialogFinished = true;
            OnDialogFinished();
            return;
        }

        ShowCurrentLine();
    }

    void ShowCurrentLine()
    {
        dialogUI.SetText(dialogLines[currentLine]);
    }

    void OnDialogFinished()
    {
        Debug.Log("Dialog finished");

        // DESBLOQUEAR INVENTARIO
        SetInventoryInteractable(true);
    }

    void SetInventoryInteractable(bool value)
    {
        if (inventoryCanvasGroup == null) return;

        inventoryCanvasGroup.interactable = value;
        inventoryCanvasGroup.blocksRaycasts = value;

        // Opcional: feedback visual
        inventoryCanvasGroup.alpha = value ? 1f : 0.5f;
    }
}
