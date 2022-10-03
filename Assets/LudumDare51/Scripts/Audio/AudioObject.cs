using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioObject : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource _source;

    private Coroutine _current = null;

    public void Play(AudioClip clip, AudioMixerGroup mixerGroup)
    {
        if (_current != null)
            StopCoroutine(_current);
        _current = StartCoroutine(AudioRoutine(clip, mixerGroup));
    }

    private IEnumerator AudioRoutine(AudioClip clip, AudioMixerGroup mixerGroup)
    {
        _source.Stop();
        _source.outputAudioMixerGroup = mixerGroup;
        _source.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        gameObject.SetActive(false);
        _current = null;
    }
}