using System;
using System.Collections.Generic;
using AutoChessRPG.Entity.Character;

namespace AutoChessRPG
{
    public class ActiveEncounterObserver : Observer
    {
        private Dictionary<ObservableSubject, List<IObservableData>> record;

        private void Start()
        {
            record = new Dictionary<ObservableSubject, List<IObservableData>>();
        }

        public override void OnNotify(ObservableSubject subject, IObservableData data)
        {
            if (record.TryGetValue(subject, out List<IObservableData> dataRecord)) dataRecord.Add(data);
            else record[subject] = new List<IObservableData>() { data };
        }

        public Dictionary<ObservableSubject, List<IObservableData>> GetRecord() => record;
    }
}