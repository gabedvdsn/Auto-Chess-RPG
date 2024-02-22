namespace AutoChessRPG
{
    public interface IUniqueArea
    {
        public void OnAreaEnter();
        
        public void OnAreaFailExit();
        
        public void OnAreaSuccessExit();
    }
}