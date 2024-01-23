using System;
using System.Collections.Generic;
using UnityEngine;

public class CamShelf : MonoBehaviour
{
    [SerializeField] private GameObject marker1;
    [SerializeField] private GameObject marker2;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject cenCam;
    private Vector3 _centCam;
    private Vector3 _posCam;
    public static int _camNum;
    private Camera _c;
    public static float _h = 7;
    List<Vector3> _camerapos = new List<Vector3>();
    [SerializeField] public int camID = 2;
    public static int CamID;
    
    void Start()
    {
        CamID = 0;
        _camNum = 12;
        BuildShelf();
        SetCamat(camID);
     
    }

    void Update()
    {
        SetCamat(camID);
    }

    void BuildShelf()
    {
        Vector3 p1 = marker1.transform.position;
        Vector3 p2 = marker2.transform.position;
        _centCam = (p2+p1)/2;
        cenCam.transform.position = _centCam;
        CreateCamList(false); 
    }

    void CreateCamList(bool debug)
    {
        float pi = (float)Math.PI;
        float radius = 15;
        List<float> x = new List<float>();
        List<float> y = new List<float>();
        for (int i = 0; i <= _camNum; i++)
        { 
            x.Add(Mathf.Cos((i*2 * pi)/ _camNum) * radius);
            y.Add(Mathf.Sin((i*2 * pi)/ _camNum) * radius);
            _camerapos.Add(new Vector3(x[i], _h, y[i]));  
            if (debug == true)
            {
                Debug.Log(x[i]);
                GameObject prefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
                GameObject cube = Instantiate(prefab, _camerapos[i],Quaternion.identity);
                cube.transform.SetParent(cenCam.transform,false);
            }
            if(debug == false){}
        }
    }
    void SetCamat(int i)
    {
        if(_c != null){Destroy(_c);}
        // lookRotation
        Vector3 relativePos = _camerapos[i]*(-1);
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        //
        _c = Instantiate(_mainCamera, _camerapos[i], rotation);
        _c.transform.SetParent(cenCam.transform,false);
        marker2.SetActive(false);

        CamID = camID;
    }
}
