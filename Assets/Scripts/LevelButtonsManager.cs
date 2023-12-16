using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;


public class LevelButtonsManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int levelButtonId;

    private Button bt;
    void Start()
    {
        bt = GetComponent<Button>();
        if (levelButtonId > Progress.Instance.PlayerInfo.CurrentLevel)
        {
            bt.interactable = false;
        }

    }
    private void Update()
    {
    }

    // Update is called once per frame
}
