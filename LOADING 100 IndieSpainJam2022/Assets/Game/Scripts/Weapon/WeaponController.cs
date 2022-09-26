using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static WeaponController;
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
    private float _normalfireRateInSeconds, _especialfireRateInSeconds;

    public float RemainingSecondsToBeAbleToShoot { get => _remainingSecondsToBeAbleToShoot; set => _remainingSecondsToBeAbleToShoot = value; }
    public string ActiveProjectile { get => _activeProjectile; set => _activeProjectile = value; }

    //public float FireRateInSeconds { get => _fireRateInSeconds; set => _fireRateInSeconds = value; }
    public enum ProjectileT
    {
        Normal,
        Especial
    }
    ProjectileT actualProjectile;
    private void Awake()
    {
        
        ActiveProjectile = _defaultProjectId.Value;
        ProjectilesConfiguration _projectileConfig = Instantiate(projectileConfig);
        _projectileSpawner = new ProjectileSpawner(_projectileConfig);
        _normalfireRateInSeconds = _fireRateInSeconds;
        _especialfireRateInSeconds = _normalfireRateInSeconds / 2;
    }

    public void ChangeProjectileType(ProjectileT  pType) {
       // if (RemainingSecondsToBeAbleToShoot >= 0)
         //   return;
            if (pType == ProjectileT.Especial)
            {
             _fireRateInSeconds = _especialfireRateInSeconds;
                ActiveProjectile = "Especial";

            }
            else
            {
                _fireRateInSeconds = _normalfireRateInSeconds;
                 ActiveProjectile = "Normal";
             }
            
    }

     
    public void TryShoot(Quaternion rotation)
    {
        RemainingSecondsToBeAbleToShoot -= Time.deltaTime;
     
        if (RemainingSecondsToBeAbleToShoot > 0)
        {
            return;
        }       
        Shoot(rotation);
    }
    private void Shoot(Quaternion rotation)
    {
        var projectile=_projectileSpawner.create(ActiveProjectile,
                                 _projectileSpawnPosition.position,
                                 Quaternion.identity);
        projectile.transform.localRotation = rotation;

         
        RemainingSecondsToBeAbleToShoot = _fireRateInSeconds;
       
    }
}
