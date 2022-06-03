using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/**
 * Adapted from "Introduction to AUDIO in Unity" by Brackeys:
 * https://www.youtube.com/watch?v=6OT43pvUyfY
 *
 * Adapted from Josh McCoy by James Jasper Fadden O'Roarke.
 * 
 * All 3 music tracks used in-game are original collaborated compositions created through OpenAI's MuseNet.
 * https://openai.com/blog/musenet/
 * 
 * CC0 SFX from "rubberduck" on OpenGameArt
 * 
 * The soundfont used for the 3 songs that are used can be found here:
 * https://musical-artifacts.com/artifacts/23
 */

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    private bool playTitleMusic;

    [SerializeField]
    private AudioMixerGroup musicMixerGroup;

    [SerializeField]
    private AudioMixerGroup sfxMixerGroup;

    [SerializeField]
    private List<SoundClip> musicTracks;

    [SerializeField]
    private List<SoundClip> sfxClips;


    // Enable this on the main game sound manager before publishing the game
    // This is intended to prevent there from being two sound managers at once when one survives from the title screen.
    [SerializeField]

    private SoundClip trackPlaying;
    private SoundClip trackFading;
    private SoundClip sfxPlaying;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (var track in this.musicTracks)
        {
            track.audioSource = this.gameObject.AddComponent<AudioSource>();
            track.audioSource.clip = track.clip;
            track.audioSource.volume = track.volume;
            track.audioSource.pitch = track.pitch;
            track.audioSource.loop = track.loop;
            track.audioSource.outputAudioMixerGroup = this.musicMixerGroup;
        }

        foreach (var clip in this.sfxClips)
        {
            clip.audioSource = this.gameObject.AddComponent<AudioSource>();
            clip.audioSource.clip = clip.clip;
            clip.audioSource.volume = clip.volume;
            clip.audioSource.pitch = clip.pitch;
            clip.audioSource.loop = clip.loop;
            clip.audioSource.outputAudioMixerGroup = this.sfxMixerGroup;
        }

        // play initial track
        this.trackPlaying = null;

        if (playTitleMusic)
        {
            this.PlayMusicTrack("Altar");
        }
        // If not, then either this is a sound manager to be destroyed or the title was bypassed, and this manager is instead used.

        // When switching between scenes, the audio controller is not destroyed.
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusicTrack(string title)
    {
        var track = this.musicTracks.Find(track => track.title == title);

        if (null == track)
        {
            Debug.Log("Sound track not found: " + title);
            return;
        }

        if (null != this.trackPlaying)
        {
            this.trackPlaying.audioSource.Stop();
        }

        track.audioSource.Play();

        this.trackPlaying = track;
    }


    public void PlaySoundEffect(string title)
    {
        var track = this.sfxClips.Find(track => track.title == title);

        if (null == track)
        {
            Debug.Log("Sound track not found: " + title);
            return;
        }

        track.audioSource.Play();
    }

    public void StopSoundEffect(string title)
    {
        var track = this.sfxClips.Find(track => track.title == title);

        if (null == track)
        {
            Debug.Log("Sound track not found: " + title);
            return;
        }

        track.audioSource.Stop();
    }
}
