using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInit : MonoBehaviour {

  public GameObject playerPrefab;

	void Start ()
  {
    var nbPlayer = GameObject.Find("GameSessionManager").GetComponent<GameSessionManager>().NbPlayer;

    // Instantiate the selected amount of player
    for (int i = 1; i <= nbPlayer; i++)
    {
      // Need a way determine which control the user will use

      // Will get the next spawn available 
      // and instantiate the player on it
      Instantiate(
        playerPrefab,
        GameObject.Find("Spawn" + i).transform.position,
        new Quaternion());
    }
  }
}
