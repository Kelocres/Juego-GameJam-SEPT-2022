using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner  
{
    private ProjectilesConfiguration _projectileConfig;

    public ProjectileSpawner(ProjectilesConfiguration config)
    {
        _projectileConfig = config;
    }

    public Projectile create(string id, Vector3 position, Quaternion rotation)
    {
        var prefab = _projectileConfig.GetProjectileById(id);

        return Object.Instantiate(prefab, position, rotation);
    }
}
