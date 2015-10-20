using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    public void goToMainScene()
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