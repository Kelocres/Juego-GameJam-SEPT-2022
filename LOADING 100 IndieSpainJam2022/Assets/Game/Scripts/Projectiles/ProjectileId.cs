using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Create Projectile ID", fileName = "ProjectileId", order = 0)]
public class ProjectileId : ScriptableObject
{
  
    [SerializeField] private string _value;

    public string Value { get => _value; set => _value = value; }     
}
