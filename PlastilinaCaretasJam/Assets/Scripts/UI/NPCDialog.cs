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
    public int flag = 0;
    public int flag1 = 0;

    private void Start()
    {
        dialogUI.OnDialogFinished += OnDialogFinished;

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
            if (sceneName == "0B.INTRO")
            {
                TransitionManager.LoadTransition("Scenes/01.SceneOruga");
            }
            else if (sceneName == "01.SceneOruga")
            {
                TransitionManager.LoadTransition("Scenes/02.SceneCune");
            }
            else if (sceneName == "02.SceneCune")
            {
                TransitionManager.LoadTransition("Scenes/03.ScenePescao");
            }
            else if (sceneName == "03.ScenePescao")
            {
                TransitionManager.LoadTransition("Scenes/04.SceneFinal");
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
    }
}
