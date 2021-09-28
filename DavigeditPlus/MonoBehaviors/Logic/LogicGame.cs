using System;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;

namespace DavigeditPlus.Logic
{
    public class LogicGame : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField]
        private UnityEvent onScoreChanged = new UnityEvent();

        private GameController gameController;

        private void Start()
        {
            gameController = SingletonBehaviour<GameController>.Instance;
            gameController.OnScoreChanged += (GameState state) => { onScoreChanged.Invoke(); };
        }

        public void WarriorWin()
        {
            PropertyInfo property = typeof(GameState).GetProperty("GameOutcome");
            property.SetValue(gameController.State, GameState.Outcome.WarriorVictory);
        }

        public void GiantWin()
        {
            PropertyInfo property = typeof(GameState).GetProperty("GameOutcome");
            property.SetValue(gameController.State, GameState.Outcome.GiantVictory);
        }

        public void Tie()
        {
            PropertyInfo property = typeof(GameState).GetProperty("GameOutcome");
            property.SetValue(gameController.State, GameState.Outcome.Tie);
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "DavigeditPlus/Logic/Logic_game.png", true);
        }
    }
}
