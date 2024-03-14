using System;
using System.Collections;
using System.Collections.Generic;
using AutoChessRPG;
using Unity.VisualScripting;
using UnityEngine;

public class DemoRaidManager : MonoBehaviour
{
    [SerializeField] private DemoRaidLocation[] locations;

    private DemoRaidLocation target;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateRaidOddsForLocations();
    }

    void CalculateRaidOddsForLocations()
    {
        float sumDist = 0f;
        float sumR = 0f;

        foreach (DemoRaidLocation loc in locations)
        {
            if (!loc.gameObject.activeSelf) continue;
            sumDist += Vector3.Distance(transform.position, loc.transform.position);
            sumR += loc.R();
        }

        foreach (DemoRaidLocation loc in locations)
        {
            if (!loc.gameObject.activeSelf) continue;
            float c = Mathf.Pow((sumDist - Vector3.Distance(transform.position, loc.transform.position)) / sumDist, 2) * Mathf.Pow(loc.R(), 1.25f);
            loc.SendRaidOdds(c);

            if (target is null) target = loc;
            else if (c > target.C()) target = loc;
        }
    }

    private void OnDrawGizmos()
    {
        if (target is not null) GUIManager.DrawLine(transform, target.transform);
    }

}

public struct DemoRaidPacket
{
    private GameObject originBastion;
    private DemoRaidLocation raidLocation;
    private float timeToRaid;
    private float c;
    private bool priority;

    private float armyPower;

    public DemoRaidPacket(GameObject _originBastion, DemoRaidLocation _raidLocation, float _timeToRaid, float _c, bool _priority)
    {
        originBastion = _originBastion;
        raidLocation = _raidLocation;
        timeToRaid = _timeToRaid;
        c = _c;
        priority = _priority;

        armyPower = 0f;
    }

    public string GetInfo()
    {
        return $"Raid from {originBastion.name} => {raidLocation.name} at {timeToRaid}\nC => {c} Priority => {priority} Army Power => {armyPower}";
    }
    
}
