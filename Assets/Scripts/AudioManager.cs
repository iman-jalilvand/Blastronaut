using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton instance

    [Header("Audio Sources")]
    public AudioSource sfxSource; // For shooting and explosion sounds
    public AudioSource ambientSource; // For background music
    public AudioSource rocketSource; // For rocket engine sound

    [Header("Audio Clips")]
    public AudioClip shootClip; // For shooting sound
    public AudioClip explosionClip; // For explosion sound
    public AudioClip ambientClip; // For background music
    public AudioClip rocketClip; // For rocket engine sound
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        // Play ambient sound in a loop
        PlayAmbientSound();
    }

    public void PlayAmbientSound()
    {
        if (ambientClip != null && ambientSource != null)
        {
            ambientSource.clip = ambientClip;
            ambientSource.loop = true; // looping the sound
            ambientSource.Play();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
    
    public void PlayMovementSound()
    {
        if (rocketSource != null && !rocketSource.isPlaying)
        {
            rocketSource.clip = rocketClip;
            rocketSource.loop = true; // looping the sound
            rocketSource.Play();
        }
    }

    public void StopMovementSound()
    {
        if (rocketSource != null && rocketSource.isPlaying)
        {
            rocketSource.Stop();
        }
    }
}
