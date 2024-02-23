using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCameraController : MonoBehaviour
{
    [Header("Inputs")] 
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform target;
    
    [Space]
    
    [SerializeField] private RTSCameraFollowStyle followStyle;

    [Space] 
    
    [SerializeField] private float tilt;
    [SerializeField] private float rotation;
    [SerializeField] private float zoom;
    [SerializeField] private float height;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum RTSCameraFollowStyle
    {
        FollowTarget,
        FollowInput
    }
}
