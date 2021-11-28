using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassScreen : MonoBehaviour
{
    [SerializeField]
    Transform compassBearings;

    public float bearing;

    public void Update()
    {
        compassBearings.localEulerAngles = new Vector3(0, 0, bearing);
        for(int i1 = 0; i1 < compassBearings.childCount;i1++)
        {
            compassBearings.GetChild(i1).transform.eulerAngles = Vector3.zero;
        }
    }

}
