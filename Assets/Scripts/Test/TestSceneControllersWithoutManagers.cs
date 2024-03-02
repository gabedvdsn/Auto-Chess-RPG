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
    
    [SerializeField] private SerializedDictionary<Affiliation, GameObject[]> controllersByAffiliation;

    private void Start()
    {
        InitializeCharacters();
        InitializeControllers();
    }

    private void InitializeCharacters()
    {
        foreach (Affiliation affiliation in controllersByAffiliation.Keys)
        {
            foreach (GameObject go in controllersByAffiliation[affiliation])
            {
                Character character = go.GetComponent<Character>();
                character.Initialize(affiliation);
            }
        }
    }
    
    private void InitializeControllers()
    {
        foreach (GameObject[] gos in controllersByAffiliation.Values)
        {
            foreach (GameObject go in gos)
            {
                EncounterAutoCharacterController controller = go.GetComponent<EncounterAutoCharacterController>();
                controller.Initialize(new EncounterPreferencesPacket());
            }
        }
    }
}
