using UnityEngine;

public class CustomPlayAtOneShot : MonoBehaviour
{
    public static AudioSource PlayClipAt(AudioClip _clip, Vector3 _position, float _volume)
    {
        GameObject tempGO = new GameObject("Audio One Shot");
        tempGO.transform.position = _position;

        AudioSource source = tempGO.AddComponent<AudioSource>();
        source.clip = _clip;
        source.volume = _volume;
        source.Play();

        Destroy(tempGO, _clip.length);
        return source;
    }
}
