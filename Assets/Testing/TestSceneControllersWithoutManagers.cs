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
            foreach (GameObject go in controllersByAffiliation[affiliation])
            {
                EncounterAutoCharacterController controller = go.GetComponent<EncounterAutoCharacterController>();
                controller.Initialize(affiliation, new EncounterPreferencesPacket());

                go.GetComponent<MeshRenderer>().material = materialsByAffiliation[affiliation];
                
                Debug.Log($"Initialized {go} with {affiliation}");
            }
        }
    }
    
}
