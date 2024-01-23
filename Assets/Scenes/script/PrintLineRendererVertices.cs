using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintLineRendererVertices: MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    public Vector3[] positions;
    void Start()
    {
        if (line != null){PrintVetices(line);}
    }

    public void PrintVetices(LineRenderer line)
    {
        int positionCount = line.positionCount;
        positions = new Vector3[positionCount];
        line.GetPositions(positions);
        Debug.Log("Number of vertices:  "+ positions.Length);
       
        for (int i = 0; i < positions.Length; i++)
        {
            Debug.Log("Position " + i + ": " + positions[i]);
        }
    }

}
