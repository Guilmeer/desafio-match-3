using System.Collections;
using System.Collections.Generic;
using Gazeus.DesafioMatch3.Views;
using UnityEngine;

namespace Gazeus.DesafioMatch3
{
    public class GameEffectsController : MonoBehaviour
    {
        [SerializeField] private BoardView _boardView;
        [SerializeField] PoolManager _PoolManager;

        [SerializeField] GameObject _SparkEffect;
        [SerializeField] GameObject _ChainEffect;
        [SerializeField] GameObject _SelectedEffect;
        [SerializeField] Animator _SuperEffectAnimator;

        [SerializeField] int _PoolStartSize = 5;

        #region Unity
        private void OnDisable()
        {
            GameEvents.Instance.onSimpleEffectDone -= SimplePopEffect;
            GameEvents.Instance.onSecondEffectDone -= SecondEffect;
            GameEvents.Instance.onScreenEffectDone -= PlayScreenEffect;
            GameEvents.Instance.onSelectionEffectDone -= PlaySelectionEffect;
            GameEvents.Instance.onSelectionEffectStop -= StopSelectionEffect;

        }

        private void Start()
        {
            _PoolManager.CreatePool("SparkEffect", _SparkEffect, _PoolStartSize);
            _PoolManager.CreatePool("ChainEffect", _ChainEffect, _PoolStartSize);

            #region Subscribes
            GameEvents.Instance.onSimpleEffectDone += SimplePopEffect;
            GameEvents.Instance.onSecondEffectDone += SecondEffect;
            GameEvents.Instance.onScreenEffectDone += PlayScreenEffect;
            GameEvents.Instance.onSelectionEffectDone += PlaySelectionEffect;
            GameEvents.Instance.onSelectionEffectStop += StopSelectionEffect;
            #endregion
        }
        #endregion

        public void SimplePopEffect(List<Vector2Int> matchedTiles)
        {
            for (int i = 0; i < matchedTiles.Count; i++)
            {
                Vector2Int position = matchedTiles[i];
                if (_boardView.GetTileAtPosition(position.x, position.y) != null)
                {
                    GameObject sparkEffect = _PoolManager.GetObject("SparkEffect");
                    Vector3 tilePos = _boardView.GetTileWorldPosition(position.x, position.y);
                    sparkEffect.transform.position = tilePos;
                }
            }
        }

        public void SecondEffect(List<Vector2Int> matchedTiles)
        {
            foreach (var position in matchedTiles)
            {
                if (_boardView.GetTileAtPosition(position.x, position.y) != null)
                {
                    GameObject sparkEffect = _PoolManager.GetObject("ChainEffect");
                    Vector3 tilePos = _boardView.GetTileWorldPosition(position.x, position.y);
                    sparkEffect.transform.position = tilePos;
                }
            }
        }

        public void PlayScreenEffect()
        {
            if (_SuperEffectAnimator != null) _SuperEffectAnimator.SetTrigger("ScreenEffect");
        }

        public void PositionSelectionEffect(Vector3 tilePos)
        {
            _SelectedEffect.transform.position = tilePos;
        }

        public void PlaySelectionEffect()
        {
            _SelectedEffect.GetComponent<ParticleSystem>().Play();
        }
        public void StopSelectionEffect()
        {
            Debug.Log($"Stoped the selected effect");
            _SelectedEffect.GetComponent<ParticleSystem>().Stop();
        }
    }
}
