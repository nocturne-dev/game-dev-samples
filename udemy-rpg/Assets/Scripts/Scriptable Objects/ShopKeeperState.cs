using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShopKeeperState")]
public class ShopKeeperState : ScriptableObject
{
    [SerializeField] string[] shopInventory;

    public string[] GetShopInventory()
    {
        return shopInventory;
    }

}
