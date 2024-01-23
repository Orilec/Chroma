using System.Collections;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    public AudioClip baseMusicClip;
    public AudioClip instrumentsClip;
    public AudioClip drumsClip;

    private AudioSource baseMusic;
    private AudioSource instruments;
    private AudioSource drums;

    // Temps de transition en secondes
    public float transitionTime = 1.0f;


    void Start()
    {
        // Initialise les AudioSources et charger les clips
        baseMusic = gameObject.AddComponent<AudioSource>();
        instruments = gameObject.AddComponent<AudioSource>();
        drums = gameObject.AddComponent<AudioSource>();

        baseMusic.clip = baseMusicClip;
        instruments.clip = instrumentsClip;
        drums.clip = drumsClip;

        // Charge la musique de base
        //baseMusic.Play();
        Debug.Log("Musique de base");

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleTrack(drums);
        }

        else if (Input.GetKeyDown(KeyCode.O))
        {
            ToggleTrack(instruments);
        }

        
    }

    // Ajouter/enlever une track
    public void ToggleTrack(AudioSource track)
    {
        if (track.isPlaying)
        {
            RemoveInstrument(track);
            Debug.Log(track.clip.name + " retirée");

        }
        else
        {
            //AddInstrument(track, baseMusic.time);
            Debug.Log(track.clip.name + " ajoutée");
            Debug.Log("New music starting time : " + track.time);

        }
    }

    public void AddInstrument(AudioSource instrument, float startTime)
    {
        instrument.time = startTime;
        StartCoroutine(FadeIn(instrument));
        instrument.Play();
    }

    public void RemoveInstrument(AudioSource instrument)
    {
        StartCoroutine(FadeOut(instrument));
    }

    IEnumerator FadeIn(AudioSource instrument)
    {
        float elapsedTime = 0;

        while (elapsedTime < transitionTime)
        {
            instrument.volume = Mathf.Lerp(0, 1, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        instrument.volume = 1; 
    }

    IEnumerator FadeOut(AudioSource instrument)
    {
        float elapsedTime = 0;

        while (elapsedTime < transitionTime)
        {
            instrument.volume = Mathf.Lerp(1, 0, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        instrument.volume = 0; 
        instrument.Stop();
    }
}
