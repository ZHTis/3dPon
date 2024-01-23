using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadControl : MonoBehaviour
{
    public static int reloadNum = 0;
    
    public static void LoadSelf()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void Load(string scene)
    {
        SceneManager.LoadScene(scene);
        //Debug.Log(reloadNum);
        UpdateloadNum();
    }

    public static void Refix()
    {
        SceneManager.LoadScene("Fixation");
    }

    public static void UpdateloadNum()
    {
        reloadNum++;
        if (reloadNum >= 5)
        {
            reloadNum = reloadNum % 5;
            Refix();
        }
    }
}

