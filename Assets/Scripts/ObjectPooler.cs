using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class ObjectPooler : MonoBehaviour
    {
        #region Simple Singleton
        public static ObjectPooler Instance;

        private void Awake()
        {
            Instance = this;
        }
        #endregion

        #region Pools to initialise
        [System.Serializable]
        private class ObjectPool
        {
            public string Tag;
            public GameObject PoolPrefab;
            public int PoolSize;
            public bool ExpandPool;
        }

        [SerializeField] private List<ObjectPool> poolsToInitialize = new List<ObjectPool>();
        #endregion

        Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();
        Dictionary<string, bool> isPoolExpandable = new Dictionary<string, bool>();

        // Start is called before the first frame update
        void Start()
        {
            InitialisePools();
        }

        private void InitialisePools()
        {
            foreach (ObjectPool pool in poolsToInitialize)
            {
                pools.Add(pool.Tag, new List<GameObject>());
                isPoolExpandable.Add(pool.Tag, pool.ExpandPool);

                for (int i = 0; i < pool.PoolSize; i++)
                {
                    GameObject go = Instantiate(pool.PoolPrefab);
                    go.SetActive(false);
                    pools[pool.Tag].Add(go);
                }
            }
        }


    }
}
