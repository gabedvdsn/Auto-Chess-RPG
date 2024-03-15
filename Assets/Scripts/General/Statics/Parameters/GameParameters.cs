using UnityEngine;

namespace AutoChessRPG
{
    public static class GameParameters
    {
        
        // General stuff
        public const int MIN_LOCAL_GAME_DIFFICULTY = 1;
        public const int MAX_LOCAL_GAME_DIFFICULTY = 5;
        
        // Difficulty Coefficients
        
        /* Defined as => g(0)/h(0)
         *
         * g(x) = d / Sqrt(2 * PI)
         * h(x) = MAX_LOCAL_GAME_DIFFICULTY - g(0)
         *
         * g(x) is the normal distribution (stdev = 1.0, mean = 0.0) multiplied by the difficulty factor (MAX_LOCAL_GAME_DIFFICULTY)
         * h(x) is the ratio ceiling
         */
        
        public const float COEF_DIFFICULTY_1 = 0.0867066454529f;
        public const float COEF_DIFFICULTY_2 = 0.189876878051f;
        public const float COEF_DIFFICULTY_3 = 0.314691651217f;
        public const float COEF_DIFFICULTY_4 = 0.468760544925f;
        public const float COEF_DIFFICULTY_5 = 0.66373372705f;

        public static float ComputeDifficultyG(int d) => d / Mathf.Sqrt(2 * Mathf.PI);
        public static float ComputeDifficultyH(int d) => 5 - ComputeDifficultyG(d);
        public static float ComputeCoefDifficulty(int d) => ComputeDifficultyG(d) / ComputeDifficultyH(d);
        
        // Raids
        public const float DEFAULT_TIME_BETWEEN_RAIDS = 1200f;
        public const float DEFAULT_TIME_BETWEEN_RAIDS_OFFSET = 400f;
        
        // Different health thresholds
        public const float HIGH_HEALTH_UPPER_THRESHOLD = 1.0f;
        public const float HIGH_HEALTH_LOWER_THRESHOLD = .7f;
        public const float MED_HEALTH_UPPER_THRESHOLD = 0.7f;
        public const float MED_HEALTH_LOWER_THRESHOLD = 0.35f;
        public const float LOW_HEALTH_UPPER_THRESHOLD = 0.35f;
        public const float LOW_HEALTH_LOWER_THRESHOLD = 0f;
        
        // Power related
        public const int WHAT_IS_LOW_COOLDOWN_LOWER = -1;
        public const int WHAT_IS_LOW_COOLDOWN_UPPER = 5;
        public const int WHAT_IS_MED_COOLDOWN_LOWER = 5;
        public const int WHAT_IS_MED_COOLDOWN_UPPER = 20;
        public const int WHAT_IS_HIGH_COOLDOWN_LOWER = 20;
        public const int WHAT_IS_HIGH_COOLDOWN_UPPER = 99999;
    }
}
