using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
	public string GamePlay;
	
	public void LoadGamePlay()
	{
		SceneManager.LoadScene(GamePlay);
	}
}
