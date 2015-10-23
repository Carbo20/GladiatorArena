using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private float ready;

    void Start ()
    {
        // Authenticate localUser for multi network
        
        ready = 0;

    }

    void Update()
    {
        /*if (ready < 1f)
            ready += Time.deltaTime;
        else
            MultiplayerController.Instance.SignInAndStartMPGame();*/
    }

    public void MultiLauncher()
    {
        MultiplayerController.Instance.SignInAndStartMPGame();
    }

}