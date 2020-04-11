using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    [SerializeField] private AudioClip menuMusic;

    private void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        MusicManager.Instance.PlayMusic(menuMusic);
    }
}