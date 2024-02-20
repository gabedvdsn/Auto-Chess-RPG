namespace AutoChessRPG
{
    public interface IObserver
    {
        public void OnNotify(ObservableSubject subject, IObservableData data);
    }
}