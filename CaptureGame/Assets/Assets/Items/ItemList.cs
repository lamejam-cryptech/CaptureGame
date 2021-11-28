using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemList", menuName = "ScriptableObjects/ItemList")]
public class ItemList : ScriptableObject
{
	public Item[] obj;
}

