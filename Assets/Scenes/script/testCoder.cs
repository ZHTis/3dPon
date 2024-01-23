using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testCoder : MonoBehaviour
{
    public static string recoderName ;
    private string[] savedData;
    public static Data2Save _data2Save;
    
    void Start()
    {
        DateTime t = DateTime.Now;
        recoderName = "Trials";// + t.ToString();
        Debug.Log("NAME:  "+recoderName);
        setSaveFormat();
        Components.RecordCenter.Instance.AddRecorder(recoderName, "MonkeyX", savedData);
    }

   
    void Update()
    {
        
    }
    
    void setSaveFormat()
    {
        savedData = new string[25];
        savedData[0] = "number";
        savedData[1] = "ini_velX";savedData[2] = "ini_velY";savedData[3] = "ini_velZ";
        savedData[4] = "ini_posX";savedData[5] = "ini_posY";savedData[6] = "ini_posZ";
        savedData[7] = "trialID";
        savedData[8] = "_isCorrect";
        savedData[9] = "ponPosX";savedData[10] = "ponPosY";savedData[11] = "ponPosZ";
        savedData[12] = "timeSinceEngineStart";
        
        savedData[13] = "camID";
        savedData[14] = "camShelf_h";
        savedData[15] = "camShelf_num";
        savedData[16] = "camPosX";savedData[17] = "camPosY"; savedData[18] = "camPosZ";

        savedData[19] = "1targetX";savedData[20] = "1targetX";savedData[21] = "1targetX";
        savedData[22] = "1targetX";savedData[23] = "1targetX";savedData[24] = "1targetX";
    }
    public struct Data2Save : IEnumerable
    {
        public int trialNumber;
        public float ini_velX; public float ini_velY; public float ini_velZ;
        public float ini_posX; public float ini_posY; public float ini_posZ;
        public String trialID;
        public bool _isCorrect;
        public float ponPosX; public float ponPosY;  public float ponPosZ;
        public float timeSinceEngineStart;

        public int camID;
        public float camShelf_h;
        public int camShelf_num;
        public float camPosX;public float camPosY;public float camPosZ;
        
        public float target1X; public float target1Y; public float target1Z;
        public float target2X; public float taregt2Y; public float target2Z;
        
        public IEnumerator GetEnumerator()
        {
            yield return trialNumber;
            yield return ini_velX; yield return ini_velY; yield return ini_velZ;
            yield return ini_posX;
            yield return ini_posY;
            yield return ini_posZ;
            yield return trialID;
            yield return _isCorrect;
            yield return ponPosX;
            yield return ponPosY;
            yield return ponPosZ;
            yield return timeSinceEngineStart;
            yield return camID;
            yield return camShelf_h;
            yield return camShelf_num;
            yield return camPosX;
            yield return camPosY; yield return camPosZ;
            yield return target1X; yield return target1Y; yield return target1Z;
            yield return target2X; yield return taregt2Y; yield return target2Z;
        }
    }

    private void OnDestroy()
    {
        Components.RecordCenter.Instance.Close(recoderName);
    }
}

       

      