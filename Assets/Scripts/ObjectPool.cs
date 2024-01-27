using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo
{
    //TODO: Implement with projectiles and potentially enemies
    public class ObjectPool : MonoBehaviour
    {
        public static List<ObjectPool> instance;

        public List<GameObject> pooledObjects;
        public GameObject objectToPool;
        public bool canExpand = true;
        public int countToPool;
        [SerializeField] private string _tag;

        void Awake()
        {
            instance ??= new List<ObjectPool>();
            _tag = objectToPool.tag;
            var tagExists = instance.Exists((ob) => ob.objectToPool.CompareTag(_tag));
            if (tagExists)
            {
                Debug.Log("Object pool for " + _tag + " already exists!");
                Destroy(this);
                return;
            }

            pooledObjects = new List<GameObject>();
            instance.Add(this);
            for (var i = 0; i < countToPool; i++)
            {
                var obj = Instantiate(objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }

        public static GameObject GetPooledObject(string tagToFind)
        {
            var tagExists = instance.Exists((ob) => ob.objectToPool.CompareTag(tagToFind));
            Debug.Log("Trying to shoot " + tagToFind + " but " + tagExists);
            if (!tagExists) return null;
            var pool = instance.Find((list) => list._tag == tagToFind);
            foreach (var t in pool.pooledObjects.Where(t => !t.activeInHierarchy))
            {
                return t;
            }

            if (!pool.canExpand) return null;
            var obj = Instantiate(pool.objectToPool);
            obj.SetActive(false);
            pool.pooledObjects.Add(obj);
            return obj;
        }
    }
}