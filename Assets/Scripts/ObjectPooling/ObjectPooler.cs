using System;
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
            public bool UseRandomStartSearch;
        }

        [SerializeField] private List<ObjectPool> poolsToInitialize = new List<ObjectPool>();

        #endregion

        private Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();
        private Dictionary<string, bool> isPoolExpandable = new Dictionary<string, bool>();
        private Dictionary<string, GameObject> poolsPrefap = new Dictionary<string, GameObject>();
        private Dictionary<string, PoolSearchMethod> poolSearch = new Dictionary<string, PoolSearchMethod>();

        private delegate GameObject PoolSearchMethod(string poolTag);


        private void Start()
        {
            InitialisePools();
        }


        private void InitialisePools()
        {
            foreach (ObjectPool pool in poolsToInitialize)
            {
                ConfiguratePool(pool);
                FillPool(pool);
            }
        }


        private void ConfiguratePool(ObjectPool pool)
        {
            pools.Add(pool.Tag, new List<GameObject>());
            isPoolExpandable.Add(pool.Tag, pool.ExpandPool);
            poolsPrefap.Add(pool.Tag, pool.PoolPrefab);

            PoolSearchMethod poolSearcher = pool.UseRandomStartSearch ? (PoolSearchMethod)RandomStartSearch : LinearSearch;
            poolSearch.Add(pool.Tag, poolSearcher);
        }


        private void FillPool(ObjectPool pool)
        {
            for (int i = 0; i < pool.PoolSize; i++)
            {
                GameObject go = Instantiate(pool.PoolPrefab);
                go.SetActive(false);
                pools[pool.Tag].Add(go);
            }
        }

        
        public GameObject InstantiatePooledItem(string poolTag, Vector3 position, Quaternion rotation)
        {
            if (!pools.ContainsKey(poolTag))
            {
                Debug.LogWarning($"Tried getting item from pool with tag '{poolTag}'. No such pool exists");
                return null;
            }

            GameObject poolItem = poolSearch[poolTag](poolTag);

            if (poolItem != null)
            {
                poolItem.SetActive(true);
                poolItem.transform.position = position;
                poolItem.transform.rotation = rotation;
                return poolItem;
            }
            else if (isPoolExpandable[poolTag])
            {
                // Instantiate new pool object
                poolItem = Instantiate(poolsPrefap[poolTag], position, rotation);
                pools[poolTag].Add(poolItem);
                return poolItem;
            }
            else
            {
                return null;
            }
        }


        #region Pool Search Methods

        private GameObject LinearSearch(string poolTag)
        {
            for (int i = 0; i < pools[poolTag].Count; i++)
            {
                if (IsReadyToBePooled(pools[poolTag][i]))
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

                if (IsReadyToBePooled(pools[poolTag][i+randomOffset]))
                {
                    return pools[poolTag][i + randomOffset];
                }
            }

            return null;
        }


        private bool IsReadyToBePooled(GameObject poolObject)
        {
            return !poolObject.activeInHierarchy;
        }

        #endregion
    }
}
