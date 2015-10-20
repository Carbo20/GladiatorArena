using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    void Start ()
    {
        // Authenticate localUser for multi network
        MultiplayerController.Instance.SignInAndStartMPGame();

        
    }

/*TODO
    public void goToOptions()
    {
        Application.LoadLevel("IAProgScene");
    }
*/

}