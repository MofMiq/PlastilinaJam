using UnityEngine;
using TMPro;
using System;

public class DialogUI : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    private string[] currentLines;
    private int currentIndex;

    public Action OnDialogFinished;

    public void StartDialog(string[] lines)
    {
        if (lines == null || lines.Length == 0)
        {
            FinishDialog();
            return;
        }

        currentLines = lines;
        currentIndex = 0;
        dialogText.text = currentLines[currentIndex];
    }

    public void NextLine()
    {
        currentIndex++;

        if (currentIndex >= currentLines.Length)
        {
            FinishDialog();
            return;
        }

        dialogText.text = currentLines[currentIndex];
    }

    void FinishDialog()
    {
        dialogText.text = "";
        OnDialogFinished?.Invoke();
    }
}
