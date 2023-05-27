using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;


public class Cache
{

    private static Dictionary<float, WaitForSeconds> m_WFS = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWFS(float key)
    {
        if (!m_WFS.ContainsKey(key))
        {
            m_WFS[key] = new WaitForSeconds(key);
        }

        return m_WFS[key];
    }

    //------------------------------------------------------------------------------------------------------------


    private static Dictionary<Collider, PlayerBody> characters = new Dictionary<Collider, PlayerBody>();

    public static PlayerBody GetPlayerBody(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<PlayerBody>());
        }

        return characters[collider];
    }

    private static Dictionary<Collider, Weapon> weapons = new Dictionary<Collider, Weapon>();

    public static Weapon GetWeapon(Collider collider)
    {
        if (!weapons.ContainsKey(collider))
        {
            weapons.Add(collider, collider.GetComponent<Weapon>());
        }

        return weapons[collider];
    }
}

