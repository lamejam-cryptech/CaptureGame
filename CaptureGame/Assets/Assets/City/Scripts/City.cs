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

    // Start is called before the first frame update
    void Start()
    {
        for(float x = startPos.x; x <= endPos.x; x+=delta.x)
        {
            for(float y = 0; y <= startPos.y; y+= delta.y)
            {
                Instantiate(
                    b.BuildingPrefabs[0],
                    new Vector3(x + offset.x, 0, y + offset.y),
                    Quaternion.Euler(0, 0, 0)
                    , this.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
