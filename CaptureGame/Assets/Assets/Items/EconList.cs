using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EconList", menuName = "ScriptableObjects/EconList")]
public class EconList : ScriptableObject
{
    [SerializeField]
    ItemList items;

    public ItemEcon[] obj;

    private void OnValidate()
    {
        if (items != null)
        {
            if (obj.Length != items.obj.Length)
            {
                obj = new ItemEcon[items.obj.Length];
            }
            for (int i1 = 0; i1 < obj.Length; i1++)
            {
                if (obj[i1] == null)
                {
                    obj[i1] = new ItemEcon();
                }
                if (obj[i1].id != items.obj[i1].id)
                {
                    obj[i1] = new ItemEcon();
                }
                obj[i1].id = items.obj[i1].id;
                obj[i1].name = items.obj[i1].name;
                obj[i1].number = items.obj[i1].number;
            }
        }
    }
}
