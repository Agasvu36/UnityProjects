using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int levelID;
    public void levelSelect(int SceneID) 
    {
        SceneManager.LoadScene(SceneID);
    }
}
