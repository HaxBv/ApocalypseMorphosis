using UnityEngine;


[RequireComponent (typeof(AudioSource))]
public class AudioReproducer : MonoBehaviour
{

    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource> ();
    }
    void Start()
    {
       
    }

    void Update()
    {
        
    }


    public void  SetAudio()
    {
        //source.clip.length


        Invoke("DesacticveObj", source.clip.length);
    }

    public void DesactiveObj()
    {
        gameObject.SetActive (false);
    }
}
