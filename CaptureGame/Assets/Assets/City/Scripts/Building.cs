using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingList", menuName = "ScriptableObjects/Building")]
public class Building : ScriptableObject
{
    public GameObject[] BuildingPrefabs;
}
