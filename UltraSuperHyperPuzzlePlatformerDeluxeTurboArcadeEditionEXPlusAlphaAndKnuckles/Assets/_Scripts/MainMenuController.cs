using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// The controller for the buttons of the main menu
/// </summary>
public class MainMenuController : MonoBehaviour 
{
    /// <summary>
    /// When the play button is pressed
    /// </summary>
	public void OnPlay()
    {
        SceneManager.LoadScene(1);
    }
}
