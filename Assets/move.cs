using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class move : MonoBehaviour
{
    public Rigidbody rb;
    private float Force;
    private Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(1, 0, 0);
        rb.useGravity = false;
        if(Force == 0){ Force = 1f;}
    }

    // Update is called once per frame
    void Update()
    {
      
            if (Input.GetKeyDown(KeyCode.A))
            {
                rb.AddForce(transform.TransformDirection(direction) * Force, ForceMode.Impulse);
                rb.useGravity =true;
            }
    }
}
