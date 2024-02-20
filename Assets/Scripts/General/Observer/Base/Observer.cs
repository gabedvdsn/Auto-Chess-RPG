using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoChessRPG
{
    public class Observer : MonoBehaviour, IObserver
    {
        [SerializeField] private List<ObservableSubject> subjects;
        
        public virtual void OnNotify(ObservableSubject subject, IObservableData data)
        {
            throw new NotImplementedException();
        }

        public virtual void OnEnable()
        {
            foreach (ObservableSubject subject in subjects) subject.AddObserver(this);
        }

        public virtual void OnDisable()
        {
            foreach (ObservableSubject subject in subjects) subject.RemoveObserver(this);
        }
    }
}