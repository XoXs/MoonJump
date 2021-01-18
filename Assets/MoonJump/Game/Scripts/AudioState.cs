using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioState : MonoBehaviour {

    public AudioSource audi;
    float camerasource = 0.37f;
    float sfxVolume = 0.25f;
    void Start()
    {

        if (this.gameObject.tag == "Player")
            stateManager(1);

            stateManager(0);
      
    }
   public void stateManager(int scene)
    {
        audi = GetComponent<AudioSource>();
        if (scene == 1)
        {
            int sfx = PlayerPrefs.GetInt("audiosfx");
            if (sfx == 1)
            {

                audi.volume = sfxVolume;
            }
            else
            {
                audi.volume = 0f;


                sfxVolume = 0f;
                AudioListener.pause = true;


                AudioListener.volume = 0;

            }

                return;
        }
     

        if (audi == null)
            return;
        int onOff = PlayerPrefs.GetInt("audio");
        if (onOff == 1)
        {
            
            audi.volume = camerasource;
        }
        else
        {
            if (this.gameObject.name.Contains("Camera"))
            {
                audi.volume = 0f;
            }
           
        }

    }

   void GamePlayState()
    {
        audi = GetComponent<AudioSource>();

    }
}
