using UnityEngine;
using UnityEngine.UI;

public class FinalSceneManager : MonoBehaviour
{
    [Header("Animator Final")]
    public Animator finalAnimator;

    [Header("Animators de Finales")]
    public RuntimeAnimatorController finalBad;       // Score muy bajo
    public RuntimeAnimatorController finalNormal;    // Score bajo-medio
    public RuntimeAnimatorController finalGood;      // Score medio-alto
    public RuntimeAnimatorController finalPerfect;   // Score máximo

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

    RuntimeAnimatorController GetFinalAnimator(int score)
    {
        if (score >= thresholdPerfect)
        {
            Debug.Log("Final: PERFECTO");
            PlayMusic(musicPerfect);
            return finalPerfect;
        }
        else if (score >= thresholdGood)
        {
            Debug.Log("Final: BUENO");
            PlayMusic(musicGood);
            return finalGood;
        }
        else if (score >= thresholdNormal)
        {
            Debug.Log("Final: NORMAL");
            PlayMusic(musicNormal);
            return finalNormal;
        }
        else
        {
            Debug.Log("Final: MALO");
            PlayMusic(musicBad);
            return finalBad;
        }
    }

    void PlayMusic(AudioClip clip)
    {
        if (musicSource != null && clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}
