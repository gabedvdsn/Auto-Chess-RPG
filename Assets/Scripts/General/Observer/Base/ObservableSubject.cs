using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoChessRPG
{
    public class ObservableSubject : MonoBehaviour
    {
        private List<IObserver> observers = new List<IObserver>();
        
        public void AddObserver(IObserver observer) => observers.Add(observer);

        public void RemoveObserver(IObserver observer) => observers.Remove(observer);

        protected void NotifyObservers(IObservableData data)
        {
            foreach (IObserver observer in observers) observer.OnNotify(this, data);
        }
    }
}