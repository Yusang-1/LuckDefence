using UnityEngine;
using System.Collections.Generic;

public class EnemyList : MonoBehaviour
{
    public static List<Entity> Enemies;

    private void Start()
    {
        Enemies = new List<Entity>();
    }

    public static void Activated(Entity entity)
    {
        Enemies.Add(entity);
    }

    public static void Deactivated(Entity entity)
    {
        Enemies.Remove(entity);
    }
}
