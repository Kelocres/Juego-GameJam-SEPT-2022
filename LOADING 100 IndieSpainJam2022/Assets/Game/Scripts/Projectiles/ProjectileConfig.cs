using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Create ProjectilesConfiguration",
                 fileName = "ProjectilesConfiguration",
                    order = 0)]
public class ProjectileConfig : ScriptableObject
{
    [SerializeField] private Projectile[] _projectilePrefabs;
 
    private Dictionary<string, Projectile> _idToProjectilePrefab;

    private void Awake()
    { 
        _idToProjectilePrefab = new Dictionary<string, Projectile>();
        foreach (var projectile in _projectilePrefabs)
        {
            _idToProjectilePrefab.Add(projectile.Id, projectile);//
        }
    }

    public Projectile GetProjectileById(string id)
    {

        if (!_idToProjectilePrefab.TryGetValue(id, out var projectile))
        {
            throw new System.Exception($"Projectile {id} not found");
        }

        return projectile;
    }
}