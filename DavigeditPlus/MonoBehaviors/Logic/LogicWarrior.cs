using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus.Logic
{
    public class LogicWarrior : Filter.Filter
    {
        [Header("Settings")]
        [SerializeField]
        [Tooltip("Which players to target. Keep the array at 4!")]
        private bool[] allowedPlayers = new bool[4] { true, true, true, true };

        [Header("Events")]
        [SerializeField]
        private UnityEvent onWarriorDeath = new UnityEvent();
        [SerializeField]
        private UnityEvent onRagdoll = new UnityEvent();

        private List<Player> players;

        private void Start()
        {
            players = SingletonBehaviour<GameController>.Instance.Players;

            for (int i = 0; i < players.Count; i++)
            {
                if (allowedPlayers[i])
                {
                    Player player = players[i];

                    player.OnDeath += (Player j) => onWarriorDeath.Invoke();
                    player.OnRagdoll += onRagdoll.Invoke;
                }
            }
        }

        public void KillWarrior()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (allowedPlayers[i])
                {
                    Player player = players[i];
                    player.Kill();
                }
            }
        }

        public void TeleportWarriors(GameObject location)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if(allowedPlayers[i])
                {
                    Player player = players[i];
                    player.ResetPosition(location.transform.position);
                }
            }
        }

        public void HurtWarrior(float damageToDeal)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (allowedPlayers[i])
                {
                    Player player = players[i];

                    PlayerHealth health = player.GetComponentInChildren<PlayerHealth>();
                    health.TakeDamage(damageToDeal, true);
                }
            }
        }

        public void RagdollWarrior()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (allowedPlayers[i])
                {
                    Player player = players[i];

                    player.GetComponentInParent<PlayerPhysicsController>().EnterRagdollState(player.Velocity);
                }
            }
        }

        public override bool CheckFilter(GameObject filterObject)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (allowedPlayers[i] && players[i] == filterObject.GetComponentInParent<Player>())
                {
                    onPass.Invoke();
                    return !reverseOutcome;
                }
            }
            return reverseOutcome;
        }

        public void RespawnPlayers(bool invincibleForABit)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (allowedPlayers[i])
                {
                    players[i].Respawn(SingletonBehaviour<GameController>.Instance.PlayerSpawnPosition);
                    if (invincibleForABit)
                        players[i].SetInvincibleForDuration(SingletonBehaviour<GameConstants>.Instance.PlayerInvincibilityDuration);
                }
            }
        }

        void OnValidate()
        {
            if (allowedPlayers.Length != 4)
            {
                Debug.LogWarning("Keep at 4! thats how many players Davigo supports!");
                Array.Resize(ref allowedPlayers, 4);
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "DavigeditPlus/Logic/Logic_warrior.png", true);
        }
    }
}
