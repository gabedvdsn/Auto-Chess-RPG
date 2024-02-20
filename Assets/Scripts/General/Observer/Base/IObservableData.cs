using System.Collections.Generic;

namespace AutoChessRPG
{
    public interface IObservableData
    {
        public IObservableData OnObserve();

        public Dictionary<string, string> FormatForObservance();
    }
}