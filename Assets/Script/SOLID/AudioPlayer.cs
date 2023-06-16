using UnityEngine;

public class AudioPlayer : Singleton<AudioPlayer>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip soundClip1, soundClip2, soundClip3;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Load audio clips from the "Resources" folder
        soundClip1 = Resources.Load<AudioClip>("Sound/SoundSelectEffect");
        soundClip2 = Resources.Load<AudioClip>("Sound/SoundErrorEffect");
        soundClip3 = Resources.Load<AudioClip>("Sound/SoundFinishEffect");

        PlaySound1();
    }

    public void PlaySound1()
    {
        audioSource.PlayOneShot(soundClip1);
    }

    public void PlaySound2()
    {
        audioSource.PlayOneShot(soundClip2);
    }

    public void PlaySound3()
    {
        audioSource.PlayOneShot(soundClip3);
    }

}
