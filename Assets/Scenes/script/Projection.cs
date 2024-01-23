using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class Projection : MonoBehaviour {
    [SerializeField] private LineRenderer _line;
    [SerializeField] private int _maxPhysicsFrameIterations = 1000;
    [SerializeField] private Transform _obstaclesParent;
    [SerializeField] public GameObject targetx;
    [SerializeField] public GameObject target2;
    [SerializeField] private Camera _camera3;

    private List<Vector3> points = new List<Vector3>();
    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    private readonly Dictionary<Transform, Transform> _spawnedObjects = new Dictionary<Transform, Transform>();
    System.Random rnd = new System.Random();
    
    public static Vector3 distance;
    [SerializeField] private float stance = 2;

    public static Vector3 target1Pos;
    public static Vector3 target2Pos;
    
    
    private void Start() {
        CreatePhysicsScene();
        target_ini();
        setCam();
    }

    private void Update()
    {
        setTarget();
    }

    private void setCam()
    {
        
        Vector3 vec = new Vector3(-15, 0, 12);
        if (distance.x > 0)
        {
            _camera3.transform.SetParent(targetx.transform);
            _camera3.transform.position = vec;
            _camera3.transform.rotation = Quaternion.Euler(-25,90,0);
        }
        if (distance.x < 0)
        {
            _camera3.transform.SetParent(target2.transform);
            _camera3.transform.position = vec;
        }
    }
    private void CreatePhysicsScene() 
    {
        _simulationScene = SceneManager.CreateScene("Simulate", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();
        foreach (Transform obj in _obstaclesParent) 
        {
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            ghostObj.GetComponent<Renderer>().enabled = true;
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
            
        }
        
    }

    public void SimulateTrajectory(pon prefab, Vector3 pos, Vector3 vel) 
    {
        var ghostObj = Instantiate(prefab, pos, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);
        ghostObj.Init(true,vel);
        _line.positionCount = _maxPhysicsFrameIterations;

        for (var i = 0; i < _maxPhysicsFrameIterations; i++) 
        {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(i, ghostObj.transform.position);
            _line.enabled = false;
        }

        Destroy(ghostObj.gameObject);
        getPoints();
    }
    
    public void getPoints()
    { 
        int positionCount = _line.positionCount; 
        Vector3[] positions = new Vector3[positionCount];
        _line.GetPositions(positions);
        for (int i = 1; i < positions.Length; i++)
        {
            if (points.Count < 100)
            {
                //Debug.Log("logging " + i + ": " + positions[i].y);
                points.Add(positions[i]);
                //Debug.Log(points.Count());
            }
        }
    }

    public void setTarget()
    {
        if (points.Count() >= 100)
        {
            bool escape = false;
            for (int i = 3; i < 80; i++)
            {
                float delta = points[i].y - points[i - 1].y; 
                if (Math.Abs(delta) < 0.01 && escape == false)  
                {   
                    targetx.transform.position = points[i];
                    target2.transform.position = targetx.transform.position + distance;
                    target1Pos = Camera.main.WorldToScreenPoint(targetx.transform.position);
                    target2Pos = Camera.main.WorldToScreenPoint(target2.transform.position);
                    escape = true;
                }
            }
            
        }
    }

    void target_ini()
    {
        int seed = rnd.Next(1,3);
        //Debug.Log("targetRESET  " + seed);
        if (seed %2 == 1)
        {
            distance = Vector3.left*stance;
        }

        if (seed % 2 == 0)
        {
            distance = Vector3.right*stance;
        }

    }
}
