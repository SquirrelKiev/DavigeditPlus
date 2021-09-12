using System;
using System.Collections.Generic;
using UnityEngine;

namespace DavigeditPlus.Filter
{
    class FilterByWarrior : Filter, IFilterBase
    {
        [SerializeField]
        [Tooltip("Which players will allow the Filter to pass. e.g. if I ticked Element 0, and none others, only the red warrior would trigger. Keep the array at 4!")]
        private bool[] allowedPlayers = new bool[4];

        private GameController gameController;

        private List<Player> players;

        private void Start()
        {
            gameController = FindObjectOfType<GameController>();
            players = gameController.Players;
        }

        public bool CheckFilter(GameObject filterObject)
        {
            for (int i = 0; i < gameController.Players.Count; i++)
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
