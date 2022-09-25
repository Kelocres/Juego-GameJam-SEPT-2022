using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float _fireRateInSeconds;

    [SerializeField] private Transform _projectileSpawnPosition;

    [SerializeField] private float _remainingSecondsToBeAbleToShoot;
 
    private string _activeProjectile;

    private ProjectileSpawner _projectileSpawner;
    [SerializeField] private ProjectileId _defaultProjectId; 
    [SerializeField] private ProjectilesConfiguration projectileConfig;
    private void Awake()
    {
        
        _activeProjectile = _defaultProjectId.Value;
        ProjectilesConfiguration _projectileConfig = Instantiate(projectileConfig);
        _projectileSpawner = new ProjectileSpawner(_projectileConfig);
    }
     

    public void TryShoot(Quaternion rotation)
    {
        _remainingSecondsToBeAbleToShoot -= Time.deltaTime;
     
        if (_remainingSecondsToBeAbleToShoot > 0)
        {
            return;
        }
        Debug.Log(rotation);
        Shoot(rotation);
    }
    private void Shoot(Quaternion rotation)
    {
        var projectile=_projectileSpawner.create(_activeProjectile,
                                 _projectileSpawnPosition.position,
                                 Quaternion.identity);
        projectile.transform.localRotation = rotation;
      _remainingSecondsToBeAbleToShoot = _fireRateInSeconds;
       
    }
}
