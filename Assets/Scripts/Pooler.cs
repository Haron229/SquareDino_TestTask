using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int poolSize;
    [SerializeField]
    private bool isExpandable;
    private List<GameObject> freeList;
    private List<GameObject> usedList;

    private void Awake()
    {
        freeList = new List<GameObject>();
        usedList = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewPoolObject();
        }
    }

    private void CreateNewPoolObject()
    {
        GameObject go = Instantiate(prefab);
        go.transform.parent = this.transform;
        go.SetActive(false);
        freeList.Add(go);
    }

    public GameObject GetPoolObject()
    {
        int listSize = freeList.Count;

        if (freeList.Count == 0 && !isExpandable)
            return null;
        if (freeList.Count == 0)
            CreateNewPoolObject();

        GameObject go = freeList[listSize - 1];
        freeList.RemoveAt(listSize - 1);
        usedList.Add(go);
        return go;
    }

    public void ReturnPoolObject(GameObject obj)
    {
        if (!usedList.Contains(obj))
            Debug.Log("List doesn't contains {0}", obj);

        obj.SetActive(false);
        usedList.Remove(obj);
        freeList.Add(obj);
    }
}
