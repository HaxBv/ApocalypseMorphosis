using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioClip JumpClip;
    public AudioClip AttackClip;

    private GameObject AudioReproducer;


    public Dictionary <string, AudioClip> musicData = new();


    public GameObject AudioReproducerPrefab;

    public int PoolSize = 10;


    public List<GameObject> AudioPool = new();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        

        for (int i = 0; i < PoolSize; i++)
        {


            GameObject obj = Instantiate(AudioReproducerPrefab);


            AudioPool.Add(obj);




        }





    }
    void Start()
    {
        musicData.Add("JumpSFX", JumpClip);
        musicData.Add("AttackSFX", JumpClip);



        //PlaySound("JumpSFX", 10);
        //PlaySound("AttackSFX", 10);
        /*if(musicData.TryGetValue("JumpSFX", out AudioClip clip))
        {
            print(clip.name);
        }
        else
        {
            print("no existe");
        }

            print(musicData["JumpSFX"].name);*/
    }

    public void PlaySound(string musicName, float volume)
    {

        
        if (musicData.TryGetValue(musicName, out AudioClip clip))
        {
            
            print(clip.name);



            AudioSource audioSource = GetAvaliableSoundReproducer().GetComponent<AudioSource>();


            audioSource.clip = clip;
            audioSource.volume = volume;

            
            
            audioSource.gameObject.SetActive(true);


            
            audioSource.GetComponent<AudioReproducer>().SetAudio();



           

           


        }
        else
        {
            print("no existe");
        }

        print(musicData[musicName].name);
    }


    public GameObject GetAvaliableSoundReproducer()
    {
        foreach(var item in AudioPool)
        {
            if (item.activeSelf == false)
            {
                return item;
            }
            
        }
        return null;
    }

}
