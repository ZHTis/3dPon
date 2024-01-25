using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Fixation : MonoBehaviour
{
    [SerializeField] private string _scene;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.L))
        {
            LoadControl.Load(_scene);
        }
    }
}
