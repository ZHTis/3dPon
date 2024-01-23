using System;
using System.Collections;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class testTCP : MonoBehaviour
{
    [Header("Server settings")]
    public string hostname ;
    public int port ;
    
    private TcpListener _server;
    private NetworkStream _stream;
    
    void Start()
    {
        StartCoroutine(SetServer("127.0.0.1", 59250));
        StartCoroutine(ReceiveData());
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Send("A");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Send("L");
        }
    
    }

    private IEnumerator SetServer(string host, int port)
    {
        _server = new TcpListener(IPAddress.Parse(host), port);
        _server.Start();
        print("Server started");

        var handler = _server.AcceptTcpClientAsync();

        print("Connecting...");
        while (!handler.IsCompleted)
        {
            yield return null;
        }
        print("Connected");
        _stream = handler.Result.GetStream();
        Send("start");
    }

 
    private IEnumerator ReceiveData()
    {
        // Waiting for stream creation
        while (_stream == null)
        {
            yield return new WaitForSeconds(1);
            //print("Waiting...");
        }
        
        while (_stream != null)
        {
            var buf = new byte[1024];
            var readHandler = _stream.ReadAsync(buf, 0, buf.Length);
            
            while (!readHandler.IsCompleted)
            {
                yield return null;
            }
            
            print("U got: "+Encoding.UTF8.GetString(buf));
        }
    }

   public void Send(string msg)
    {
        var buf = new byte[1024];
        UnicodeEncoding encoder = new UnicodeEncoding();
        buf = encoder.GetBytes(msg);
        _stream.Write(buf,0,buf.Length);
        Debug.Log("SendFromU: "+msg);
    }
}
