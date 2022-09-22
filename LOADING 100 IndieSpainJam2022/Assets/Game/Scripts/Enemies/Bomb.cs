using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.EmptyBar(damage);
        GameManager.instance.UpdateLoading();
    }
}
