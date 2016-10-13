using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.LogicSystem
{
    public class ExplorationResult
    {
        private static ExplorationResult _instance;
        public static ExplorationResult Instance {
            get
            {
                if (_instance == null)
                    _instance = new ExplorationResult();
                return _instance;
            }
        }

        private ExplorationResult() { }
        

        public Scripts.GameResult GameResult = Scripts.GameResult.None;
        public int BattlesFought = 0;
        public int WoolGathered = 0;
        public LevelData LevelPlayed = null;


        public static void Reset()
        {
            _instance = new ExplorationResult();
        }
    }
}
