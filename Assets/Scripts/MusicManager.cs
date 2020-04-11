using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    public static MusicManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MusicManager>();

                if (instance == null)
                {
                    instance = new GameObject("MusicManager", typeof(MusicManager)).GetComponent<MusicManager>();
                }
                else
                {
                    Destroy(instance);
                }
            }

            return instance;
        }

        private set { instance = value; }
    }

    private AudioSource musicSource;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
    }

    public void PlayMusic(AudioClip music)
    {
        var activeSource = musicSource;
        activeSource.clip = music;
        activeSource.volume = 0.1f;
        activeSource.Play();
    }
}