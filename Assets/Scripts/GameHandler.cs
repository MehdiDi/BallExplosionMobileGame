using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

    public void RestartGame()
    {
        initializeClassVariables();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public static void initializeClassVariables()
    {
        EnemySpawner.blocksCount = 0;
        EnemySpawner.DestroyedBlocksCount = 0;
    }
}
