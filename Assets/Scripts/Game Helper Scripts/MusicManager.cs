using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource audioSource;
    public AudioClip[] musicTracks;
    public bool playOnStart = true;
    public bool loop = true;

    private int currentTrackIndex = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (playOnStart && musicTracks.Length > 0)
        {
            PlayTrack(currentTrackIndex);
        }
    }

    public void PlayTrack(int index)
    {
        if (index >= 0 && index < musicTracks.Length)
        {
            currentTrackIndex = index;
            audioSource.clip = musicTracks[index];
            audioSource.loop = loop;
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
            PlayTrack(0);
        else if (scene.name == "MainGame")
            PlayTrack(1);
    }

public void ToggleMute()
    {
        audioSource.mute = !audioSource.mute;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }
}
