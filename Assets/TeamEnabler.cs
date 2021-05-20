using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamEnabler : MonoBehaviour
{
    [SerializeField] PlayerConfigManager _manager;
    public void enableTeams(bool enable)
    {
        _manager._teamsEnabled = enable;
        SceneManager.LoadScene("Final");
    }
}
