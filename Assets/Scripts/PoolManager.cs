//using System.Collections.Generic;
//using UnityEngine;
//using WeaponSystem;

//public class PoolManager : MonoBehaviour
//{
//    private static PoolManager _instance;

//    public static PoolManager Instance
//    {
//        get
//        {
//            if (_instance == null)
//            {
//                _instance = FindObjectOfType<PoolManager>();
//            }

//            return _instance;
//        }
//    }

//    private IDictionary<int, Queue<BulletInstance>> objectPool = new Dictionary<int, Queue<BulletInstance>>();

//    public void CreatePool(BulletInstance bulInst, int poolSize)
//    {
//        int poolKey = bulInst.GetInstanceID();

//        GameObject poolHolder = new GameObject(bulInst.name + " Pool");
//        poolHolder.transform.parent = transform;

//        if (!objectPool.ContainsKey(poolKey))
//        {
//            objectPool.Add(poolKey, new Queue<BulletInstance>());

//            for (int i = 0; i < poolSize; i++)
//            {
//                BulletInstance bulletInst = new BulletInstance(Instantiate(bulInst.objectPrefab) as GameObject);
//                objectPool[poolKey].Enqueue(bulletInst);
//                bulletInst.SetParent(poolHolder.transform);
//            }
//        }
//    }

//    public void SpawnBullet(BulletInstance prefab, Vector3 position, Quaternion rotation)
//    {
//        int poolKey = prefab.GetInstanceID();

//        if (objectPool.ContainsKey(poolKey))
//        {
//            BulletInstance bullInst = objectPool[poolKey].Dequeue();
//            objectPool[poolKey].Enqueue(bullInst);

//            bullInst.ReuseBullet(position, rotation);
//        }
//    }


//    public void SpawnBullet(BulletInstance prefab, Transform _transform)
//    {
//        int poolKey = prefab.GetInstanceID();

//        if (objectPool.ContainsKey(poolKey))
//        {
//            BulletInstance bullInst = objectPool[poolKey].Dequeue();
//            objectPool[poolKey].Enqueue(bullInst);

//            bullInst.ReuseBullet(_transform.position, _transform.rotation);
//        }
//    }
//}