using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ponSimulator : MonoBehaviour
{
    [SerializeField] private Projection _projection;
    [SerializeField] private pon _ballPrefab;
    

    private void Update()
    {
        _projection.SimulateTrajectory(_ballPrefab, _ballPrefab._inipos , _ballPrefab._vel);
    }

   
}