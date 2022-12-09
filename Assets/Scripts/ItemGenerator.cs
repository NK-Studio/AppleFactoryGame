using System.Collections.Generic;
using Data;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemGenerator : MonoBehaviour
{
    private float _span = 1.0f;
    private float _delta;
    
    [SerializeField] private List<ItemData> ItemDatas = new();

    private void Update()
    {
        OnGenerate();
    }

    private void OnGenerate()
    {
        _delta += Time.deltaTime;
        bool isOverTime = _delta >= _span;

        if (isOverTime)
        {
            _delta = 0;
            
            int randomItemIndex = Random.Range(0, ItemDatas.Count);
            GameObject randomObject = ItemDatas[randomItemIndex].Prefab;
            GameObject item = Instantiate(randomObject);

            float x = Random.Range(-1, 2);
            float z = Random.Range(-1, 2);

            item.transform.position = new Vector3(x, 4, z);


            float dropSpeed = ItemDatas[randomItemIndex].DropSpeed;
            item.GetComponent<ItemController>().Speed = dropSpeed;
        }
    }
}