using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisLoad : MonoBehaviour
{
    public GameObject prefix;
    void Awake()
    {
        DontDestroyOnLoad(this.prefix);
    }
}
