using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    

public class Logger : MonoBehaviour
{
    private float time;
    private bool correct;
    
    public static string _Trialid;

    private testCoder.Data2Save _data2Save = testCoder._data2Save;
    private string recoderName = testCoder.recoderName;
    public int trialNumber;
    
    void Start()
    {
        trialNumber = 0;
        Timeout();
        GetGuid();
        getter();
        Components.RecordCenter.Instance.Write(recoderName, ref _data2Save);
    }
    
    void Update()
    {
        Action();
    }

    void getter()
    {
        _data2Save.trialNumber = trialNumber;
        getPos(pon.vel,out _data2Save.ini_velX,out _data2Save.ini_velY,out _data2Save.ini_velZ);
        getPos(pon.inipos,out _data2Save.ini_posX,out _data2Save.ini_posY,out _data2Save.ini_posZ);
        getPos(pon.ponPos,out _data2Save.ponPosX,out _data2Save.ponPosY,out _data2Save.ponPosZ);
        _data2Save.trialID = _Trialid;
        _data2Save._isCorrect = correct;
        _data2Save.timeSinceEngineStart = Time.realtimeSinceStartup;

        _data2Save.camID = CamShelf.CamID;
        _data2Save.camShelf_h = CamShelf._h;
        _data2Save.camShelf_num = CamShelf._camNum;
        getPos(Camera.main.transform.position,out _data2Save.camPosX,out _data2Save.camPosY,out _data2Save.camPosZ);
        getPos(Projection.target1Pos,out _data2Save.target1X ,out _data2Save.target1Y,out _data2Save.target1Z);
        getPos(Projection.target2Pos,out _data2Save.target2X,out _data2Save.taregt2Y,out _data2Save.target2Z);
        //Debug.Log("ponPos  "+ pon.inipos);
    }
    
    
    void getPos(Vector3 v,out float x, out float y, out float z)
    {
        x = v.x;
        y = v.y;
        z = v.z;
        
    }
    
    void GetGuid()
    {
        _Trialid = System.Guid.NewGuid().ToString("N");
    }
    
    void Action()
    {
        
        if (Input.GetKeyDown(KeyCode.A)|Input.GetKeyDown(KeyCode.L))
        {
            Judge();
            
        }
        SaveNow();
    }

    void SaveNow()
    {
     getter();
     Components.RecordCenter.Instance.Write(recoderName, ref _data2Save);
     Debug.Log("correct  "+ correct);
    }
    

    void Judge()
    {
        if (Projection.distance.x > 0)
        {
            //pressing L is correct, target is the left one
            if (Input.GetKeyDown(KeyCode.A))
            {
                correct = false;
             
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                correct = true;
           
            }
        }
        if (Projection.distance.x < 0)
        {
            //pressing A is correct , target is the right one
            if (Input.GetKeyDown(KeyCode.L))
            {
                correct = false;
          
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                correct = true;
            }
        }
    }

    void Timeout()
    {
        
    }
}
}
