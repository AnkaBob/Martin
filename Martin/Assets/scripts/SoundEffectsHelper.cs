using UnityEngine;
using System.Collections;

/// <summary>
/// Création d'effets sonores en toute simplicité
/// </summary>
public class SoundEffectsHelper : MonoBehaviour
{

    /// <summary>
    /// Singleton
    /// </summary>
    public static SoundEffectsHelper Instance;

    public AudioClip Death;
    public AudioClip Grab;
    public AudioClip Jump_1;
    public AudioClip Jump_Trap;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundEffectsHelper!");
        }
        Instance = this;
    }

    public void MakeDeathSound()
    {
        MakeSound(Death);
    }

    public void MakeJump1Sound()
    {
        MakeSound(Jump_1);
    }

    public void MakeJump2Sound()
    {
        MakeSound(Jump_Trap);
    }

    public void MakeGrabSound()
    {
        MakeSound(Grab);
    }

    /// <summary>
    /// Lance la lecture d'un son
    /// </summary>
    /// <param name="originalClip"></param>
    private void MakeSound(AudioClip originalClip)
    {
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}