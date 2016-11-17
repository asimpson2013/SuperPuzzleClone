using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls which character is selected 
/// </summary>
public class CharSelectControl:MonoBehaviour 
{
    /// <summary>
    /// The Characters that can be chosen
    /// </summary>
    public enum ChosenCharacter
    {
        Character1,
        Character2,
        Character3
    }

    /// <summary>
    /// Controls what happens when button for character 1 is hit
    /// </summary>
    public void SelectCharacter1()
    {
        gameController.character = ChosenCharacter.Character1;
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Controls what happens when button for character 2 is hit
    /// </summary>
    public void SelectCharacter2()
    {
        gameController.character = ChosenCharacter.Character2;
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Controls what happens when button for character 3 is hit
    /// </summary>
    public void SelectCharacter3()
    {
        gameController.character = ChosenCharacter.Character3;
        SceneManager.LoadScene(1);
    }
}
