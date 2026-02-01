using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCDialog : MonoBehaviour
{
    public DialogUI dialogUI;

    [Header("Dialogo Inicial")]
    public string[] initialDialogLines;

    [Header("Inventario")]
    public CanvasGroup inventoryCanvasGroup;

    private bool responsiveDialog = false;
    private MaskItem selectedMask;

    private void Start()
    {
        // Escuchamos cuando termina cualquier diálogo
        dialogUI.OnDialogFinished += OnDialogFinished;

        // Al empezar: diálogo inicial + inventario bloqueado
        SetInventoryInteractable(false);
        dialogUI.StartDialog(initialDialogLines);
    }

    public void StartResponseDialog(MaskItem mask)
    {
        responsiveDialog = true;
        selectedMask = mask;
        SetInventoryInteractable(false);
        
        string npcName = GetCurrentNPCName();
        string response = mask.GetResponseForNPC(npcName);
        dialogUI.StartDialog(new string[] { response });
    }

    private string GetCurrentNPCName()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        
        if (sceneName.Contains("Oruga"))
            return "Oruga";
        else if (sceneName.Contains("Cune"))
            return "Cuneiforme";
        else if (sceneName.Contains("Pescao"))
            return "Pescao";
        
        return "";
    }

    void OnDialogFinished()
    {
        if (responsiveDialog)
        {
            string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            Debug.Log("Current scene: " + sceneName);
            if (sceneName == "SceneOruga")
            {
                SceneManager.LoadScene("Scenes/SceneCune");
            }
            else if (sceneName == "SceneCune")
            {
                SceneManager.LoadScene("Scenes/ScenePescao");
            }
            else if (sceneName == "ScenePescao")
            {
                SceneManager.LoadScene("Scenes/SceneFinal");
            }
        }
        else
            SetInventoryInteractable(true);
    }

    void SetInventoryInteractable(bool value)
    {
        if (inventoryCanvasGroup == null) return;

        inventoryCanvasGroup.interactable = value;
        inventoryCanvasGroup.blocksRaycasts = value;
        inventoryCanvasGroup.alpha = value ? 1f : 0.5f;
    }
}
