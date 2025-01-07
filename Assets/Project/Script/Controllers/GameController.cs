using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Gazeus.DesafioMatch3.Core;
using Gazeus.DesafioMatch3.Models;
using Gazeus.DesafioMatch3.Views;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gazeus.DesafioMatch3.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private BoardView _boardView;
        [SerializeField] private int _boardHeight = 10;
        [SerializeField] private int _boardWidth = 10;
        [SerializeField] GameObject _SelectedEffect;
        [SerializeField] Animator _Animator;

        [SerializeField] float _GameScore = 0;
        [SerializeField] float _GameScoreToWin = 750; // It's set to 750 for testing purposes 
        public Slider _ScoreSlider;

        private GameService _gameEngine;
        private bool _isAnimating;
        private int _selectedX = -1;
        private int _selectedY = -1;

        #region Unity
        private void Awake()
        {
            _gameEngine = new GameService();
            _boardView.TileClicked += OnTileClick;
        }

        private void OnDestroy()
        {
            _boardView.TileClicked -= OnTileClick;
        }

        private void Start()
        {
            List<List<Tile>> board = _gameEngine.StartGame(_boardWidth, _boardHeight);
            _boardView.CreateBoard(board);
        }
        private void Update()
        {
            if (_ScoreSlider != null)
            {
                if (_GameScore > 0) _ScoreSlider.value = (float)(_GameScore / _GameScoreToWin) * 100;
                else _ScoreSlider.value = 0;
            }
        }
        #endregion

        private void AnimateBoard(List<BoardSequence> boardSequences, int index, Action onComplete)
        {
            BoardSequence boardSequence = boardSequences[index];

            PlayEffects(boardSequence);

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_boardView.DestroyTiles(boardSequence.MatchedPosition));
            sequence.AppendInterval(1);
            sequence.Append(_boardView.MoveTiles(boardSequence.MovedTiles));
            sequence.Append(_boardView.CreateTile(boardSequence.AddedTiles));

            index += 1;
            if (index < boardSequences.Count)
            {
                sequence.onComplete += () => AnimateBoard(boardSequences, index, onComplete);
            }
            else
            {
                sequence.onComplete += () => onComplete();
            }
        }

        private void PlayEffects(BoardSequence boardSequence)
        {
            List<List<Vector2Int>> separateMatches = _gameEngine.IdentifyIndependentMatches(boardSequence.MatchedPosition);

            // Process each independent match set individually
            int matchesScore = 0;
            foreach (var matchGroup in separateMatches)
            {
                matchesScore += CalculateMatchesScore(matchGroup.Count);
                if (matchGroup.Count == 3)
                {
                    GameEvents.Instance.SimpleEffectDone(matchGroup);
                }
                else if (matchGroup.Count > 3)
                {
                    GameEvents.Instance.SecondEffectDone(matchGroup);
                }
            }

            _GameScore += matchesScore; // Update the game score bar

            // Play the third effect for super matches
            if (boardSequence.MatchedPosition.Count > 6)
            {
                Debug.Log($"7 or more MATCHED!!!");
                // Call Screen Effect
                GameEvents.Instance.ScreenEffectDone();
            }

            GameEvents.Instance.MatchedTilesDone(boardSequence.MatchedPosition.Count);
        }

        private int CalculateMatchesScore(int amountMatched)
        {
            // Some simple score system
            switch (amountMatched)
            {
                case 3:
                    return 10;
                case 4:
                    return 25;
                case 5:
                    return 50;
                case 6:
                    return 75;
                case 7:
                    return 125;
                default:
                    return 125;
            }
        }

        private void OnTileClick(int x, int y)
        {
            if (_isAnimating) return;

            if (_selectedX > -1 && _selectedY > -1)
            {
                if (Mathf.Abs(_selectedX - x) + Mathf.Abs(_selectedY - y) > 1)
                {
                    _selectedX = -1;
                    _selectedY = -1;
                    GameEvents.Instance.SelectionEffectStop();
                }
                else
                {
                    _isAnimating = true;
                    _boardView.SwapTiles(_selectedX, _selectedY, x, y).onComplete += () =>
                    {
                        bool isValid = _gameEngine.IsValidMovement(_selectedX, _selectedY, x, y);
                        if (isValid)
                        {
                            List<BoardSequence> swapResult = _gameEngine.SwapTile(_selectedX, _selectedY, x, y);
                            AnimateBoard(swapResult, 0, () =>
                            {
                                _isAnimating = false;
                                if (_GameScore > _GameScoreToWin)
                                {
                                    LoadMenuScene();
                                }
                            });
                        }
                        else
                        {
                            _boardView.SwapTiles(x, y, _selectedX, _selectedY).onComplete += () => _isAnimating = false;
                        }
                        _selectedX = -1;
                        _selectedY = -1;
                        GameEvents.Instance.SelectionEffectStop();
                    };
                }
            }
            else
            {
                _selectedX = x;
                _selectedY = y;
                GameEvents.Instance.SelectionEffectStop();
            }

            if (_selectedX > -1 && _selectedY > -1 && _boardView.GetTileAtPosition(x, y) != null)
            {
                Vector3 tilePos = _boardView.GetTileWorldPosition(x, y);
                _SelectedEffect.transform.position = tilePos;
                GameEvents.Instance.SelectionEffectDone();
            }
            else
            {
                GameEvents.Instance.SelectionEffectStop();
            }
        }

        public void LoadMenuScene()
        {
            DOTween.Clear();
            _Animator.SetTrigger("CloseGame");
            StartCoroutine(FinishGame());
        }

        IEnumerator FinishGame()
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(0);
        }
    }
}