using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
	[SerializeField]
	Dictionary<int, Item> obj = new Dictionary<int, Item>();
	public Inventory(Item[] objs)
	{
		for(int i1 = 0; i1 < objs.Length; i1++)
        {
			this.obj.Add(objs[i1].id, objs[i1].clone());
        }
	}

	public void transfer(Inventory dest, int id, int num)
    {
		if (!obj.ContainsKey(id))
		{
			return;
		}
		if (obj[id].number > num)
		{
			if(!dest.obj.ContainsKey(id))
            {
				dest.obj.Add(id, obj[id].clone());
				dest.obj[id].number = num;
			}
			obj[id].number -= num;
		}
    }
	public void consume(int id, int num)
	{
		if (!obj.ContainsKey(id))
		{
			return;
		}
		if (obj[id].number > num)
		{
			obj[id].number -= num;
		}
		else
        {
			obj[id].number = 0;
		}
	}
	public Item getItem(int id)
    {
		if(obj.ContainsKey(id))
        {
			return obj[id];
		}
		return null;
    }

	public string ToString()
    {
		/*
		int[] keys = (new List<int>(this.obj.Keys)).ToArray();
		for (int i = 0; i < Math.Min(obj.Count, 14); i++)
		{
			returnString += ($"{obj[keys[i]].name}:{obj[keys[i]].number}\n");
		}
		return returnString.Substring(0,returnString.Length - 1);
		*/
		string returnString = "";
		int i1 = 0;
		foreach (KeyValuePair<int, Item> keyValues in obj)
		{
			if(i1 > 14)
            {
				break;
            }
			returnString += $"{keyValues.Value.name}:{keyValues.Value.number}\n";
			i1++;
		}
		return returnString.Substring(0, returnString.Length - 1);
	}
}
