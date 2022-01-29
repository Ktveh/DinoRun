using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(GameObject prefab)
    {
        GameObject spawned = Instantiate(prefab, transform.position, Quaternion.identity);
        spawned.SetActive(false);

        _pool.Add(spawned);
    }

    protected bool TryGetObject(out GameObject result)
    {
        Shuffle(_pool);
        result = _pool.First(p => p.activeSelf == false);

        return result != null;
    }

    private void Shuffle(List<GameObject> pool)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            int randonElement = Random.Range(i, pool.Count);
            GameObject temp = pool[randonElement];
            pool[randonElement] = pool[i];
            pool[i] = temp;
        }
    }
}
