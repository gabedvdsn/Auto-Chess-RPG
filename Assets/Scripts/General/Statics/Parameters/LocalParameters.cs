using UnityEditorInternal;

namespace AutoChessRPG
{
    public static class LocalParameters
    {
        // Time
        public static float TOTAL_TIME_ELAPSED;
        
        // Difficulty
        public static int DIFFICULTY_FACTOR;
        public static float DIFFICULTY_COEF;

        public static void InitializeDifficulty(int d)
        {
            DIFFICULTY_FACTOR = d;
            DIFFICULTY_COEF = GameParameters.ComputeCoefDifficulty(d);
        }
        
    }
}
