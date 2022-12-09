using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "New ItemData",menuName = "Data/ItemData",order = 0)]
    public class ItemData : ScriptableObject
    {
        public GameObject Prefab;
        public float DropSpeed;
    }
}
