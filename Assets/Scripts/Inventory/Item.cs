
using UnityEngine;


[CreateAssetMenu]
public class Item : ScriptableObject
{
    //Item is a scriptable inventory object. 
    public string itemName;
    public Sprite icon;
    public bool canEquip;
    public bool canUse;
    public int uses;
    public bool isCooldownBased;
    public float cooldown;
    public string description;
}
