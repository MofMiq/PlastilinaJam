using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterText : MonoBehaviour
{
    [Header("Texto")]
    [TextArea(3, 10)]
    public string fullText;

    [Header("Configuración")]
    [Tooltip("Caracteres por segundo")]
    public float typewriterSpeed = 30f;

    [Tooltip("Empezar automáticamente al iniciar")]
    public bool playOnStart = true;

    [Header("Sonido")]
    [Tooltip("Sonido que se reproduce mientras se escribe el texto")]
    public AudioClip typewriterSound;

    [Tooltip("Aleatorizar el pitch del sonido")]
    public bool randomizePitch = false;

    [Tooltip("Rango de pitch aleatorio (mínimo y máximo)")]
    public Vector2 pitchRange = new Vector2(0.8f, 1.2f);

    [Tooltip("Frecuencia de cambio de pitch (en segundos)")]
    public float pitchChangeInterval = 0.05f;

    private TextMeshProUGUI textComponent;
    private AudioSource audioSource;
    private Coroutine typewriterCoroutine;
    private bool isTyping = false;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();

        if (textComponent == null)
        {
            Debug.LogError("TypewriterText: No se encontró componente TextMeshProUGUI en este GameObject.");
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        if (string.IsNullOrEmpty(fullText))
        {
            fullText = textComponent.text;
        }

        if (playOnStart)
        {
            StartTypewriter();
        }
    }

    public void StartTypewriter()
    {
        if (textComponent == null) return;

        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        typewriterCoroutine = StartCoroutine(TypewriterEffect());
    }

    public void CompleteText()
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

        textComponent.text = fullText;
        isTyping = false;
    }

    IEnumerator TypewriterEffect()
    {
        isTyping = true;
        textComponent.text = "";

        if (typewriterSound != null && audioSource != null)
        {
            audioSource.clip = typewriterSound;
            audioSource.Play();
        }

        // Iniciar coroutine para randomizar el pitch continuamente
        Coroutine pitchCoroutine = null;
        if (randomizePitch && audioSource != null)
        {
            pitchCoroutine = StartCoroutine(RandomizePitchContinuously());
        }

        int i = 0;
        while (i < fullText.Length)
        {
            if (fullText[i] == '<')
            {
                int closeIndex = fullText.IndexOf('>', i);
                if (closeIndex != -1)
                {
                    textComponent.text += fullText.Substring(i, closeIndex - i + 1);
                    i = closeIndex + 1;
                    continue;
                }
            }

            textComponent.text += fullText[i];
            i++;
            yield return new WaitForSeconds(1f / typewriterSpeed);
        }

        // Detener la randomización del pitch
        if (pitchCoroutine != null)
        {
            StopCoroutine(pitchCoroutine);
        }

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.pitch = 1f;
        }

        isTyping = false;
    }

    IEnumerator RandomizePitchContinuously()
    {
        while (isTyping)
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                float newPitch = UnityEngine.Random.Range(pitchRange.x, pitchRange.y);
                audioSource.pitch = newPitch;
                Debug.Log($"Pitch changed to: {newPitch}");
            }
            yield return new WaitForSeconds(pitchChangeInterval);
        }
    }

    public bool IsTyping()
    {
        return isTyping;
    }
}
