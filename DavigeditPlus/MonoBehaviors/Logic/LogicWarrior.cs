using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DavigeditPlus.Filter;

namespace DavigeditPlus.Logic
{
    class LogicWarrior : Filter.Filter
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
                if (allowedPlayers[i] && players[i].transform.root.gameObject == filterObject.transform.root.gameObject)
                {
                    onPass.Invoke();
                    return !reverseOutcome;
                }
            }
            return reverseOutcome;
        }

        void OnValidate()
        {
            if (allowedPlayers.Length != 4)
            {
                Debug.LogWarning("Keep at 4! thats how many players Davigo supports!");
                Array.Resize(ref allowedPlayers, 4);
            }
        }
    }
}
