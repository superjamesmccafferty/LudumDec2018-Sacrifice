﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

  public GameSessionManager GSManager;

  void Start()
  {
    GSManager = GameObject.Find("GameSessionManager").GetComponent<GameSessionManager>();
  }

  public void StartSession(int nbPlayer)
  {
    // Show menu to set character name, color and set Keybinds
    GSManager.StartNewSession(nbPlayer);
  }
}
