
using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField]
    public string name;
    public int id;
    public int number;

    public virtual Item clone()
    {
        Item tmp = new Item();
        tmp.name = this.name;
        tmp.id = this.id;
        tmp.number = this.number;
        return tmp;
    }
}

[System.Serializable]
public class ItemEcon : Item
{
    [SerializeField]
    public float consumeRate;
    [SerializeField]
    public float productionRate;
    [SerializeField]
    public float price;

    public override Item clone()
    {
        ItemEcon tmp = new ItemEcon();
        tmp.name = this.name;
        tmp.id = this.id;
        tmp.number = this.number;
        tmp.consumeRate = this.consumeRate;
        tmp.productionRate = this.productionRate;
        tmp.price = this.price;
        return tmp;
    }
}
