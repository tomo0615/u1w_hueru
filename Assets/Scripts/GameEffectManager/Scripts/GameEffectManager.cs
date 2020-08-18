using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GameEffectManager.Scripts
{
    public class GameEffectManager : MonoBehaviour
    {
        #region particle用
        private readonly Dictionary<EffectType ,EffectPool> _effectPool = new Dictionary<EffectType, EffectPool>();

        [SerializeField, Header("EffectTableを設定後アタッチ")]
        private EffectsTable effectsTable = null;
        #endregion

        private Transform _myTransform;

        private void Start()
        {
            _myTransform = GetComponent<Transform>();

            InitializeEffectList();
        }

        private void InitializeEffectList()
        {
            //すべてのエフェクトをディクショナリに格納
            for (int i = 0; i < effectsTable.gameEffectList.Count; i++)
            {
                _effectPool.Add((EffectType)i, new EffectPool(_myTransform, effectsTable.gameEffectList[i])); ;
            }

            //オブジェクトが破棄されたときにプールを破棄できるようにする
            foreach (var value in _effectPool.Values)
            {
                this.OnDestroyAsObservable().Subscribe(_ => value.Dispose());
            }
        }
        public void OnGenelateEffect(Vector3 position, EffectType type)
        {
            //poolから借りて終わったら返す
            var gameObj = _effectPool[type].Rent();

            gameObj.PlayEffect(position)
                .Subscribe(_=>
                {
                    _effectPool[type].Return(gameObj);
                });
        }
    }
}

