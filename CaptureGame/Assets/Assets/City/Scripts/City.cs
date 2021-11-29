using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]

public class City : MonoBehaviour
{
    [Header("Building Templates")]
    [SerializeField]
    Building b;

    [Header("Building Generation")]
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

    [Header("Economy")]
    [SerializeField]
    int pop = 100;
    [SerializeField]
    EconList tradables;

    [SerializeField]
    Inventory localEcon;

    [SerializeField]
    private float updateTime = 60;
    private float currentTime = 0;

    [SerializeField]
    GameObject player;
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
        

        localEcon = new Inventory((Item[]) tradables.obj);
        ItemEcon tmp;
        for (int i1 = 0; i1 < tradables.obj.Length; i1++)
        {
            tmp = (ItemEcon) localEcon.getItem(tradables.obj[i1].id);
            
            tmp.consumeRate += rand.Next(0, 100);
            tmp.productionRate += rand.Next(0, 100);
            tmp.number = rand.Next(0, 10) * pop + pop;
        }
        float scaleFactor = 1;
        if ((this.transform.position - player.transform.position).magnitude > 20f)
        {
            scaleFactor = 200f / ((this.transform.position - player.transform.position).magnitude - 19);
        }
            
        scaleFactor = Mathf.Clamp(scaleFactor, 0, 1);
        //this.transform.localScale = Vector3.one * scaleFactor;
        //for(int i1 = 0; i1 < this.transform.childCount; i1++)
        //{
        //    this.transform.GetChild(i1).transform.localScale = Vector3.one * scaleFactor;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime > updateTime)
        {
            currentTime = 0;
            inventoryUpdate();
        }
    }

    void inventoryUpdate()
    {
        float demand;
        float produce;
        for(int i1 = 0; i1 < tradables.obj.Length; i1++)
        {
            ItemEcon tmp = (ItemEcon) localEcon.getItem(tradables.obj[i1].id);

            demand = tmp.consumeRate * pop;
            produce = tmp.productionRate * pop;

            tmp.price = Mathf.Clamp(Mathf.Exp(-1 * (localEcon.getItem(tradables.obj[i1].id).number + produce - demand - 5) / 100) + 1, 0, 1000);

            localEcon.consume(i1,  -1*(Mathf.FloorToInt(produce - demand)));
        }
    }

    public string getMarket()
    {
        string returnString = "";
        ItemEcon tmp;
        int i2 = 0;
        for (int i1 = 0; i1 < tradables.obj.Length; i1++)
        {
            if(i2 > 14)
            {
                break;
            }
            tmp = (ItemEcon)localEcon.getItem(tradables.obj[i1].id);
            returnString += $"{tmp.id}:{tmp.name}:{tmp.number}:{tmp.price} Credits\n";
        }
        return returnString.Substring(0, returnString.Length);
    }

    public bool buy(int id, ref float credits, int number , Inventory player)
    {
        ItemEcon tmp = (ItemEcon) localEcon.getItem(id);
        if(credits < tmp.price * number)
        {
            return false;
        }
        if(number > tmp.number)
        {
            return false;
        }
        tmp.number -= number;//local
        player.getItem(id).number += number;//player
        credits -= number * ((ItemEcon)localEcon.getItem(id)).price;
        //localEcon.transfer(player, id, number);
        return true;
    }
    public bool sell(int id, ref float credits, int number, Inventory player)
    {
        Item tmp = player.getItem(id);
        if (number > tmp.number)
        {
            return false;
        }
        tmp.number -= number;//player
        localEcon.getItem(id).number += number;//local
        credits += number * ((ItemEcon)localEcon.getItem(id)).price;
        return true;
    }
}
