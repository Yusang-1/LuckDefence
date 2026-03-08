using UnityEngine;
using System;
using System.Collections.Generic;

public class EnemyList : MonoBehaviour
{
    public static Action EnemyDied;

    public static List<Entity> Enemies = new List<Entity>();

    private void Start()
    {
        //Enemies = new List<Entity>();
        Enemies.Clear();
    }

    public static void Activated(Entity entity)
    {
        Enemies.Add(entity);
    }

    public static void Deactivated(Entity entity)
    {
        Enemies.Remove(entity);
        EnemyDied?.Invoke();
    }

    public void OnDeactivateAllEnemy()
    {
        foreach(var enemy in Enemies)
        {
            enemy.gameObject.SetActive(false);
        }
        Enemies.Clear();
    }
}
