using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
        [Serializable]
        private class ObjectPool
        {
            public string Tag = default;
            public GameObject PoolPrefab = default;
            public int PoolSize = default;
            public bool ExpandPool = default;
        }

        [SerializeField] private List<ObjectPool> poolsToInitialize = new List<ObjectPool>();
        #endregion

        Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();
        Dictionary<string, bool> isPoolExpandable = new Dictionary<string, bool>();
        Dictionary<string, GameObject> poolsPrefap = new Dictionary<string, GameObject>();

        public bool UseRandomStartSearch;

        delegate GameObject PoolSearchMethod(string poolTag);

        PoolSearchMethod poolSearcher;

        // Start is called before the first frame update
        void Start()
        {
            InitialisePools();

            if (UseRandomStartSearch)
            {
                poolSearcher = RandomStartSearch;
            }
            else
            {
                poolSearcher = LinearSearch;
            }
        }

        private void InitialisePools()
        {
            foreach (ObjectPool pool in poolsToInitialize)
            {
                pools.Add(pool.Tag, new List<GameObject>());
                isPoolExpandable.Add(pool.Tag, pool.ExpandPool);
                poolsPrefap.Add(pool.Tag, pool.PoolPrefab);

                for (int i = 0; i < pool.PoolSize; i++)
                {
                    GameObject go = Instantiate(pool.PoolPrefab);
                    go.SetActive(false);
                    pools[pool.Tag].Add(go);
                }
            }
        }

        
        public GameObject InstantiatePooledItem(string poolTag, Vector3 position, Quaternion rotation)
        {
            if (!pools.ContainsKey(poolTag))
            {
                Debug.LogWarning($"Tried getting item from pool with tag '{poolTag}'. No such pool exists");
                return null;
            }

            GameObject poolItem = poolSearcher(poolTag);

            if (poolItem != null)
            {
                poolItem.SetActive(true);
                poolItem.transform.position = position;
                poolItem.transform.rotation = rotation;
                return poolItem;
            }
            else if (isPoolExpandable[poolTag])
            {
                poolItem = Instantiate(poolsPrefap[poolTag], position, rotation);
                pools[poolTag].Add(poolItem);
                return poolItem;
            }
            else
            {
                return null;
            }
        }

        private GameObject LinearSearch(string poolTag)
        {
            for (int i = 0; i < pools[poolTag].Count; i++)
            {
                if (!pools[poolTag][i].activeInHierarchy)
                {
                    return pools[poolTag][i];
                }
            }

            return null;
        }

        private GameObject RandomStartSearch(string poolTag)
        {
            int poolSize = pools[poolTag].Count;
            int randomOffset = Random.Range(0, poolSize);

            for (int i = 0; i < pools[poolTag].Count; i++)
            {
                if (randomOffset + i >= poolSize)
                {
                    randomOffset = 0;
                }

                if (!pools[poolTag][i+randomOffset].activeInHierarchy)
                {
                    return pools[poolTag][i + randomOffset];
                }
            }

            return null;
        }
    }
}
