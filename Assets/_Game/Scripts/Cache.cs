using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;


public class Cache
{

    //Cache for Coroutine

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

    //Cache for String

    private static Dictionary<string, string> dict_string = new Dictionary<string, string>();

    public static string GetString(string key)
    {
        if (!dict_string.ContainsKey(key))
        {
            dict_string[key] = new string(key);
        }

        return dict_string[key];
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

    private static Dictionary<Collider, Obstacle> obstacles = new Dictionary<Collider, Obstacle>();

    public static Obstacle GetObstacle(Collider collider)
    {
        if (!obstacles.ContainsKey(collider))
        {
            obstacles.Add(collider, collider.GetComponent<Obstacle>());
        }

        return obstacles[collider];
    }
}

