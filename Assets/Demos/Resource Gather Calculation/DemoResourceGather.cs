using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using AutoChessRPG;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class DemoResourceGather : MonoBehaviour
{
    [SerializeField] private int itemRarity;
    
    [SerializeField] private float c;
    [SerializeField] private float o;

    [SerializeField] private SerializedDictionary<int, float> minionLevelProficiency;

    private float output;

    private string minionString = "";
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (int level in minionLevelProficiency.Keys)
        {
            minionString += $"( {level}, {minionLevelProficiency[level]} ) ";
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateOutput();
    }

    void CalculateOutput()
    {
        int index = 0;
        output = 0;
        foreach (int level in minionLevelProficiency.Keys)
        {
            output += (2 * c * level) / (itemRarity * (c + o)) * minionLevelProficiency[level];
        }

        output *= o;
    }

    private void OnDrawGizmos()
    {
        GUIManager.DrawString($"C: {c} | O: {o} | R: {itemRarity}", transform.position, Color.black, Vector2.zero);
        GUIManager.DrawString($"M: {minionString}", transform.position, Color.black, Vector2.up * 2);
        GUIManager.DrawString($"Output: {output.ToString(CultureInfo.InvariantCulture)} items / min", transform.position, Color.black, Vector2.up * 4);
        GUIManager.DrawString($"Output: 1 item / {60 / output} sec", transform.position, Color.black, Vector2.up * 6);
    }
}
