using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using AutoChessRPG;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class TestSceneControllersWithoutManagers : MonoBehaviour
{
    [Description("This script allows for testing of controller behaviours without relying on any managerial scripts")] 
    
    [SerializeField] private bool autoInitialize;
    
    [SerializeField] private SerializedDictionary<Affiliation, GameObject[]> controllersByAffiliation;
    
    [SerializeField] private SerializedDictionary<Affiliation, Material> materialsByAffiliation;


    private void Awake()
    {
        InitializeControllers();
    }
    
    private void InitializeControllers()
    {
        if (autoInitialize && controllersByAffiliation.Count == 0)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Character");
            controllersByAffiliation[Affiliation.Ally] = new GameObject[gos.Length];

            for (int i = 0; i < gos.Length; i++)
            {
                controllersByAffiliation[Affiliation.Ally][i] = gos[i];
            }
        }
        
        foreach (Affiliation affiliation in controllersByAffiliation.Keys)
        {
            for (int i = 0; i < controllersByAffiliation[affiliation].Length; i++)
            {
                GameObject go = controllersByAffiliation[affiliation][i];
                
                EncounterAutoCharacterController controller = go.GetComponent<EncounterAutoCharacterController>();
                controller.Initialize(affiliation);

                go.GetComponent<MeshRenderer>().material = materialsByAffiliation[affiliation];
                
                Debug.Log($"Initialized {go} with {affiliation}");
            }
        }
    }
    
}
