using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;
using Logger = Components.Logger;

public class TestSocket : MonoBehaviour
{
    private byte[] buffer;
    private string msg;
    void Start()
    {
        buffer = new byte[1024];
        SocketComCenter.Instance.SetServer("127.0.0.1",59250 , buffer);
        msg = "Trail start-----" + Logger._Trialid;
        SocketComCenter.Instance.SendStringAsync(msg);
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            msg = "A   "+ Logger._Trialid;
            SocketComCenter.Instance.SendStringAsync(msg);
            
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            msg = "L   "+Logger._Trialid;
            SocketComCenter.Instance.SendStringAsync(msg);
          
        }
    }
    
    private void OnDestroy()
    {
        SocketComCenter.Instance.CloseServer();
        
    }

    
}
