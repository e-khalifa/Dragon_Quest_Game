using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;



    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (instance != null && instance != this)
            Destroy(gameObject);

        ChangeSoundVolume(0);
        ChangeMusicVolume(0);
    }

    public void PlaySound(AudioClip sound)
    {
        soundSource.PlayOneShot(sound);
    }

    private void ChangeSourceVolume(string volumeName, float change, AudioSource source)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;
        source.volume = currentVolume;
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }


    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(
         "soundVolume", _change, soundSource
        );
    }
    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(
         "musicVolume", _change, musicSource
        );
    }
}
