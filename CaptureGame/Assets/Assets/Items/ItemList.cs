using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemList", menuName = "ScriptableObjects/ItemList")]
public class ItemList : ScriptableObject
{
	public Item[] obj;

	public int getIdbyName(string name)
    {
        for(int i1 = 0; i1 < obj.Length; i1++)
        {
            if(obj[i1].name == name)
            {
                return obj[i1].id;
            }
        }
        return -1;
    }
}

