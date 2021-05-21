using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamEnabler : MonoBehaviour
{
    public void loadTeamsScene()
    {
        SceneManager.LoadScene("PickCharacterTeams");
    }

    public void loadSoloScene()
    {
        SceneManager.LoadScene("PickCharacterSolo");
    }
}
