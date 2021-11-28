using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMovementControler : MonoBehaviour
{
    [SerializeField]
    private MechState state = MechState.STOP;

    [SerializeField]
    private GameObject cameras;

    private const float arrivalThreshold = 1;

    //settings
    [SerializeField]
    private float mechSpeed = 10;
    [SerializeField]
    private float mechRotationSpeed = 10;
    [SerializeField]
    private float camRotationSpeed = 10;

    //temporay variables
    private float distTravel = 0;
    private float distGoal = 0;

    private float rotatedTravel = 0;
    private float rotatedGoal = 0;

    public void Start()
    {
    }

    public void Update()
    {
        switch(state)
        {
            case MechState.MOVE:
                moveDist();
                break;
            case MechState.ROTATE:
                rotate(this.cameras.transform);
                break;
            case MechState.STOP:
                //get new command
                break;
        }

        //camera command
    }

    //rotate functions
    private void setRot(float goal)
    {
        rotatedGoal = goal % 360;
        state = MechState.ROTATE;
    }

    private void rotate(Transform target)
    {
        Vector3 tmp = target.transform.eulerAngles;

        if (Mathf.Abs(rotatedTravel - rotatedGoal) > arrivalThreshold)
        {
            rotatedTravel += mechRotationSpeed * getDir(rotatedTravel, rotatedGoal) * Time.deltaTime;
            tmp.y += mechRotationSpeed * getDir(rotatedTravel, rotatedGoal) * Time.deltaTime;
        }
        else
        {
            tmp.y += getDir(rotatedTravel, rotatedGoal) * Mathf.Abs(rotatedTravel - rotatedGoal);
            rotatedGoal = 0;
            rotatedTravel = 0;
            this.state = MechState.STOP;
        }
        target.transform.eulerAngles = tmp;
    }

    //distance travel functions
    private void setDist(float goal)
    {
        distGoal = goal;
        this.state = MechState.MOVE;
    }
    private void moveDist()
    {
        if(Mathf.Abs(distTravel - distGoal) > arrivalThreshold)
        {
            distTravel += mechSpeed * getDir(distTravel, distGoal) * Time.deltaTime;
            this.transform.position += this.transform.forward * getDir(distTravel, distGoal) * mechSpeed * Time.deltaTime;
        }
        else
        {
            this.transform.position += this.transform.forward * getDir(distTravel, distGoal) * Mathf.Abs(distTravel - distGoal);
            distGoal = 0;
            distTravel = 0;
            this.state = MechState.STOP;
        }
    }

    private float getDir(float cur, float goal)
    {
        if(goal > cur)
        {
            return 1;
        }
        return -1;
    }
}
