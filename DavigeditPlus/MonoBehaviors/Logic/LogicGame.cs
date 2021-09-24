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

        public void RespawnPlayers(bool invincibleForABit)
        {
            foreach (Player player in gameController.Players)
            {
                player.Respawn(gameController.PlayerSpawnPosition);
                if (invincibleForABit)
                    player.SetInvincibleForDuration(SingletonBehaviour<GameConstants>.Instance.PlayerInvincibilityDuration);
            }
        }
    }
}
