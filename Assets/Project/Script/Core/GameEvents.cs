using System;
using System.Collections;
using System.Collections.Generic;
using Gazeus.DesafioMatch3.Models;
using UnityEngine;

namespace Gazeus.DesafioMatch3
{
    public class GameEvents : MonoBehaviour
    {
        private static GameEvents _instance;
        public static GameEvents Instance
        {
            get { return _instance; }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        public event Action<int> onMatchTilesDone;
        public void MatchedTilesDone(int amountMatched)
        {
            if (onMatchTilesDone != null)
            {
                onMatchTilesDone(amountMatched);
            }
        }

        public event Action<List<Vector2Int>> onSimpleEffectDone;
        public void SimpleEffectDone(List<Vector2Int> matchedTiles)
        {
            if (onSimpleEffectDone != null)
            {
                onSimpleEffectDone(matchedTiles);
            }
        }

        public event Action<List<Vector2Int>> onSecondEffectDone;
        public void SecondEffectDone(List<Vector2Int> matchedTiles)
        {
            if (onSecondEffectDone != null)
            {
                onSecondEffectDone(matchedTiles);
            }
        }

        public event Action onScreenEffectDone;
        public void ScreenEffectDone()
        {
            if (onScreenEffectDone != null)
            {
                onScreenEffectDone();
            }
        }

        public event Action onSelectionEffectDone;
        public void SelectionEffectDone()
        {
            if (onSelectionEffectDone != null)
            {
                onSelectionEffectDone();
            }
        }

        public event Action onSelectionEffectStop;
        public void SelectionEffectStop()
        {
            if (onSelectionEffectStop != null)
            {
                onSelectionEffectStop();
            }
        }
    }
}


