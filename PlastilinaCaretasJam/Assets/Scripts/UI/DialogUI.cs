using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class DialogUI : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    [Header("Efecto de Escritura")]
    [Tooltip("Caracteres por segundo")]
    public float typewriterSpeed = 30f;

    [Header("Sonido")]
    [Tooltip("Sonido que se reproduce mientras se escribe el texto")]
    public AudioClip typewriterSound;

    [Tooltip("Aleatorizar el pitch del sonido")]
    public bool randomizePitch = false;

    [Tooltip("Rango de pitch aleatorio (mínimo y máximo)")]
    public Vector2 pitchRange = new Vector2(0.9f, 1.1f);

    private AudioSource audioSource;
    private string[] currentLines;
    private int currentIndex;
    private bool isTyping = false;
    private Coroutine typewriterCoroutine;

    public Action OnDialogFinished;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    public void StartDialog(string[] lines)
    {   
        if (lines == null || lines.Length == 0)
        {
            FinishDialog();
            return;
        }

        currentLines = lines;
        currentIndex = 0;
        ShowCurrentLine();
    }

    public void NextLine()
    {
        if (isTyping)
        {
            CompleteCurrentLine();
            return;
        }

        currentIndex++;

        if (currentIndex >= currentLines.Length)
        {
            FinishDialog();
            return;
        }

        ShowCurrentLine();
    }

    void ShowCurrentLine()
    {
        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        typewriterCoroutine = StartCoroutine(TypewriterEffect(currentLines[currentIndex]));
    }

    void CompleteCurrentLine()
    {
        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.pitch = 1f;
        }

        dialogText.text = currentLines[currentIndex];
        isTyping = false;
    }

    IEnumerator TypewriterEffect(string line)
    {
        isTyping = true;
        dialogText.text = "";

        if (typewriterSound != null && audioSource != null)
        {
            audioSource.clip = typewriterSound;
            audioSource.Play();
        }

        int i = 0;
        while (i < line.Length)
        {
            if (line[i] == '<')
            {
                int closeIndex = line.IndexOf('>', i);
                if (closeIndex != -1)
                {
                    dialogText.text += line.Substring(i, closeIndex - i + 1);
                    i = closeIndex + 1;
                    continue;
                }
            }

            dialogText.text += line[i];
            
            if (randomizePitch && audioSource != null && audioSource.isPlaying)
            {
                audioSource.pitch = UnityEngine.Random.Range(pitchRange.x, pitchRange.y);
            }
            
            i++;
            yield return new WaitForSeconds(1f / typewriterSpeed);
        }

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.pitch = 1f;
        }

        isTyping = false;
    }

    void FinishDialog()
    {
        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.pitch = 1f;
        }

        dialogText.text = "";
        isTyping = false;
        OnDialogFinished?.Invoke();
    }
}
