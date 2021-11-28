using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    [SerializeField]
    Building b;

    [SerializeField]
    Vector2 startPos;
    [SerializeField]
    Vector2 endPos;
    [SerializeField]
    Vector2 delta;
    [SerializeField]
    Vector2 offset;

    [SerializeField]
    int key;

    System.Random rand;

    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random(key);
        for(float x = startPos.x; x <= endPos.x; x+=delta.x)
        {
            for(float y = startPos.y; y <= endPos.y; y+= delta.y)
            {
                Instantiate(
                    b.BuildingPrefabs[rand.Next(0, b.BuildingPrefabs.Length)],
                    new Vector3(x + offset.x, 0, y + offset.y),
                    Quaternion.Euler(-90f, rand.Next(0,4) * 90f, 0)
                    , this.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
