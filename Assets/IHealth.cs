using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth 
{
    public void ResetHealth();
    public void TakeDamage(float damage);
}
