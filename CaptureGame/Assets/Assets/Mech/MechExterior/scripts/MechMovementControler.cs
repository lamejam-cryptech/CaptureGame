using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMovementControler : MonoBehaviour
{
    [SerializeField]
    private MechState state = MechState.STOP;

    private const float arrivalThreshold = 1;

    //settings
    [SerializeField]
    private float mechSpeed = 10;
    [SerializeField]
    private float mechRotationSpeed = 10;
    [SerializeField]
    private float camRotationSpeed = 10;

    //temporay variables
    [SerializeField]
    private float distTravel = 0;
    [SerializeField]
    private float distGoal = 0;

    [SerializeField]
    private float rotatedTravel = 0;
    [SerializeField]
    private float rotatedGoal = 0;

    public void Update()
    {
        switch(state)
        {
            case MechState.MOVE:
                moveDist();
                break;
            case MechState.ROTATE:
                rotate();
                break;
            case MechState.STOP:
                //get new command
                break;
        }

        //camera command
    }

    //rotate functions
    public void setRot(float goal)
    {
        rotatedGoal = (goal % 360);
        state = MechState.ROTATE;
    }

    private void rotate()
    {
        Vector3 tmp = this.transform.eulerAngles;

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
        this.transform.eulerAngles = tmp;
    }

    //distance travel functions
    public void setDist(float goal)
    {
        distGoal = goal;
        this.state = MechState.MOVE;
    }

    private void moveDist()
    {
        bool hit = Physics.CheckSphere
            (
                this.transform.position + this.transform.forward * mechSpeed * Time.deltaTime,
                2.0f,
                2147482111
            );

        if (!hit)
        {
            if (Mathf.Abs(distTravel - distGoal) > arrivalThreshold)
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
        else
        {
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

    public MechState getState()
    {
        return this.state;
    }

}
