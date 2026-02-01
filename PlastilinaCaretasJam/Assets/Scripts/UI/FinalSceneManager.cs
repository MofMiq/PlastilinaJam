using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalSceneManager : MonoBehaviour
{
    [Header("Animator Final")]
    public Animator finalAnimator;

    [Header("Animators de Finales")]
    public SpriteRenderer finalSpriteRenderer;
    public RuntimeAnimatorController finalBad;       // Score muy bajo
    public Sprite spriteFinalBad;
    public RuntimeAnimatorController finalNormal;    // Score bajo-medio
    public Sprite spriteFinalNormal;
    public RuntimeAnimatorController finalGood;      // Score medio-alto
    public Sprite spriteFinalGood;
    public RuntimeAnimatorController finalPerfect;   // Score máximo
    public Sprite spriteFinalPerfect;

    [Header("Música de Finales")]
    public AudioSource musicSource;
    public AudioClip musicBad;
    public AudioClip musicNormal;
    public AudioClip musicGood;
    public AudioClip musicPerfect;

    [Header("Rangos de Score")]
    public int thresholdNormal = 4;   // >= para normal
    public int thresholdGood = 8;   // >= para bueno
    public int thresholdPerfect = 12; // >= para perfecto

    void Start()
    {
        LoadFinalBasedOnScore();
        StartCoroutine(EnableAnimatorAfterDelay());
    }

    void LoadFinalBasedOnScore()
    {
        int score = 0;

        if (GameManager.Instance != null)
        {
            score = GameManager.Instance.totalScore;
            Debug.Log("Score final: " + score);
        }
        else
        {
            Debug.LogWarning("GameManager no encontrado. Usando score 0.");
        }

        // Seleccionar animator según el score
        RuntimeAnimatorController selectedAnimator = GetFinalAnimator(score);

        if (finalAnimator != null && selectedAnimator != null)
        {
            finalAnimator.runtimeAnimatorController = selectedAnimator;
        }
    }

    void SetScale(float x, float y)
    {
        finalAnimator.transform.localScale = new Vector3(x, y, 1f);
        finalSpriteRenderer.transform.localScale = new Vector3(x, y, 1f);
    }

    void SetPosition(float x, float y)
    {
        finalAnimator.transform.position = new Vector3(x, y, 0f);
        finalSpriteRenderer.transform.position = new Vector3(x, y, 0f);
        
    }

    IEnumerator EnableAnimatorAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        finalAnimator.enabled = true;
    }

    RuntimeAnimatorController GetFinalAnimator(int score)
    {
        if (score >= thresholdPerfect)
        {
            Debug.Log("Final: PERFECTO");
            SetScale(1.06f, 1.06f);
            SetPosition(-4.5f, -0.6f);
            finalSpriteRenderer.sprite = spriteFinalPerfect;
            PlayMusic(musicPerfect);
            return finalPerfect;
        }
        else if (score >= thresholdGood)
        {
            Debug.Log("Final: BUENO");
            SetPosition(-3f, -1f);
            SetScale(0.9f, 0.9f);
            finalSpriteRenderer.sprite = spriteFinalGood;
            PlayMusic(musicGood);
            return finalGood;
        }
        else if (score >= thresholdNormal)
        {
            Debug.Log("Final: NORMAL");
            SetPosition(-3f, -1f);
            SetScale(0.9f, 0.9f);
            finalSpriteRenderer.sprite = spriteFinalNormal;
            PlayMusic(musicNormal);
            return finalNormal;
        }
        else
        {
            Debug.Log("Final: MALO");
            SetPosition(-4.76f, -1.24f);
            SetScale(0.7f, 0.7f);
            finalSpriteRenderer.sprite = spriteFinalBad;
            PlayMusic(musicBad);
            return finalBad;
        }
    }

    void PlayMusic(AudioClip clip)
    {
        if (musicSource != null && clip != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }
}
