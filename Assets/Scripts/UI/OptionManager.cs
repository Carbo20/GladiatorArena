using UnityEngine;
using System.Collections;


using Image = UnityEngine.UI.Image;

public class OptionManager : MonoBehaviour {

    bool soundMute;


    private Sprite soundOff;
    private Sprite soundOn;

    // Use this for initialization
    void Start () {
        soundMute = false;

        soundOff = Resources.Load<Sprite>("SoundOff");
        soundOn = Resources.Load<Sprite>("SounOn");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SoundButton()
    {
        if(soundMute == false)
        {
            AudioListener.volume = 0;
            soundMute = true;
            GameObject.Find("SoundButton").GetComponent<Image>().sprite = soundOff;
        }
        else
        {
            AudioListener.volume = 1;
            soundMute = false;
            GameObject.Find("SoundButton").GetComponent<Image>().sprite = soundOn;
        }
    }

    public void BackButton()
    {
        Application.LoadLevel("OppeningScreen");
    }

}
