using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;


public class pon : MonoBehaviour
{
    
    public Vector3 position;
    public Vector3 _vel;
    public Vector3 _inipos;
    public static Vector3 vel;
    public static Vector3 inipos;
    public Rigidbody _rb;
    private bool _isGhost;
    private Vector3 screencenter  = new Vector3(Screen.width / 2f,Screen.height /2f,0);
    System.Random rnd = new System.Random();
    [SerializeField] private GameObject mark;

    public static Vector3 ponPos;
    // Start is called before the first frame update
    void Start()
    {

        _rb = GetComponent<Rigidbody>();
        _inipos = _rb.position;
        //apply initial velocity
        int a = rnd.Next(5,15);
        _vel = new Vector3(a , 0, 0);
        _rb.velocity = _vel;
        vel = _vel;
        inipos = _inipos;
    }

    public void Init( bool isGhost, Vector3 velocity) 
    {
        
        _isGhost = isGhost;
        _rb.AddForce(velocity,ForceMode.Impulse);

    }
    
    private void Update()
    { 
        position = _rb.position;
        ponPos = Camera.main.WorldToScreenPoint(position); 
        //Debug.Log("ponPos   "+ponPos);
        //Debug.Log("vel x  "+vel);
       
    }
    
    
}
 