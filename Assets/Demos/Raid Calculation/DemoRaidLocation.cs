using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using AutoChessRPG;
using UnityEngine;

public class DemoRaidLocation : MonoBehaviour
{
    [SerializeField] private float r;

    private float c;
    private float timeSinceRaided = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float R() => r;

    public float C() => c;
    
    public float TimeSinceRaided() => timeSinceRaided;

    public void SendRaidOdds(float _c)
    {
        c = _c;
    }

    private void OnDrawGizmos()
    {
        GUIManager.DrawString($"C: {c.ToString(CultureInfo.InvariantCulture)}", transform.position, Color.black, Vector2.up * 4);
        GUIManager.DrawString($"R: {r.ToString(CultureInfo.InvariantCulture)}", transform.position, Color.black, Vector2.up * 2);
        // GUIManager.DrawString(timeSinceRaided.ToString(CultureInfo.InvariantCulture), transform.position, Color.black, Vector2.zero);
    }
}
