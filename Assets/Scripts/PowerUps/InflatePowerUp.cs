using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflatePowerUp : PowerUp
{
    private List<GameObject> _players;
    private GameObject _selectedPlayer;
    public InflatePowerUp()
    {
        _players = BattleManager.getPlayers();
    }
    public IEnumerator restorePowerUp(GameObject player)
    {
        yield return new WaitForSecondsRealtime(5f);

        if (_players.Count > 1)
        {
            GameObject ballons = _selectedPlayer.transform.Find("Balloons").gameObject;
            int nBallons = ballons.transform.childCount;

            for (int i = 0; i < nBallons; i++)
            {
                ballons.transform.GetChild(i).localScale = ballons.transform.GetChild(i).localScale / 2;
            }
            player.GetComponent<PowerUpManager>().setIsPowerUpRunning(false);
        }
    }

    public bool runPowerUp(GameObject player)
    {
        do
        {
            int number = (int)Random.Range(0f, _players.Count);
            _selectedPlayer = _players[number];
        }
        while (_selectedPlayer.Equals(player) && _players.Count > 1);

        if (_players.Count > 1)
        {
            GameObject ballons = _selectedPlayer.transform.Find("Balloons").gameObject;
            int nBallons = ballons.transform.childCount;

            for (int i = 0; i < nBallons; i++)
            {
                ballons.transform.GetChild(i).localScale = ballons.transform.GetChild(i).localScale * 2;
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}
