using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [System.Serializable]
    public class SoundItem
    {
        public string Name_Sound;           // nombre para el diccionario
        public AudioClip clip;       // sonido
        public float volume = 1f;    // volumen individual
    }

    public List<SoundItem> sounds = new List<SoundItem>();
    private Dictionary<string, SoundItem> soundDictionary;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();

        soundDictionary = new Dictionary<string, SoundItem>();

        foreach (var s in sounds)
        {
            if (!soundDictionary.ContainsKey(s.Name_Sound))
                soundDictionary.Add(s.Name_Sound, s);
        }
    }

    // Reproduce un sonido por nombre
    public void Play(string key)
    {
        if (soundDictionary.ContainsKey(key))
        {
            SoundItem sound = soundDictionary[key];
            audioSource.PlayOneShot(sound.clip, sound.volume);
        }
        else
        {
            Debug.LogWarning("El sonido '" + key + "' no existe en el diccionario.");
        }
    }
}