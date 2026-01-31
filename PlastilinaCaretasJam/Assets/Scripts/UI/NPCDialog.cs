using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    [TextArea]
    public string[] dialogLines;

    private int currentLine = 0;

    public DialogUI dialogUI;

    void Start()
    {
        ShowCurrentLine();
    }

    public void NextLine()
    {
        currentLine++;

        if (currentLine >= dialogLines.Length)
        {
            Debug.Log("Dialog finished");
            return;
        }

        ShowCurrentLine();
    }

    void ShowCurrentLine()
    {
        dialogUI.SetText(dialogLines[currentLine]);
    }
}
