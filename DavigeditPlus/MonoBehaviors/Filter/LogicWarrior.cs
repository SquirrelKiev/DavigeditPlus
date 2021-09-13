using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus.Filter
{
    class LogicWarrior : Filter, IFilterBase
    {
        [Header("Settings")]
        [SerializeField]
        [Tooltip("Which players to target. Keep the array at 4!")]
        private bool[] allowedPlayers = new bool[4];

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

                    player.OnDeath += LogicWarrior_OnDeath;
                    player.OnRagdoll += onRagdoll.Invoke;
                }
            }
        }

        private void LogicWarrior_OnDeath(Player obj) { onWarriorDeath.Invoke(); }

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

        public void HurtWarrior()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (allowedPlayers[i])
                {
                    Player player = players[i];

                    // PlayerHealth health = player.GetComponentInParent<PlayerHealth>();
                    SingletonBehaviour<GameController>.Instance.PlayerOutOfBounds(player);
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

                    // TODO: Figure out how to properly ragdoll, and disable input
                    player.ActivateRagdoll(player.GetComponent<SimpleCharacterController>().velocity, null);
                }
            }
        }
        
        public void UnragdollWarrior()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (allowedPlayers[i])
                {
                    Player player = players[i];

                    player.DeactivateRagdoll();
                }
            }
        }

        public bool CheckFilter(GameObject filterObject)
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
