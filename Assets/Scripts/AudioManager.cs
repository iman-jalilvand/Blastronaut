using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton instance

    [Header("Audio Sources")]
    public AudioSource sfxSource; // For shooting sound
    public AudioSource ambientSource; // For background music
    public AudioSource rocketSource; // For rocket engine sound
    public AudioSource explosionSource; // 🔊 Dedicated explosion sound channel


    [Header("Audio Clips")]
    public AudioClip shootClip; // For shooting sound
    public AudioClip explosionClip; // For explosion sound
    public AudioClip ambientClip; // For background music
    public AudioClip rocketClip; // For rocket engine sound
    public AudioClip lockOnClip;    // For lock-on sound


    
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
        if (clip == explosionClip && explosionSource != null)
        {
            explosionSource.PlayOneShot(clip); // ✅ Use separate channel
        }
        else if (sfxSource != null)
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

    public void PlayLockOnSound()
    {
        if (lockOnClip != null)
            sfxSource.PlayOneShot(lockOnClip);
    }

    public void StopLockOnSound()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }

}
