using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Bomb", order = 1)]
public class SO_Bomb : ScriptableObject
{
    /// <summary>Damage of explosion</summary>
    public int ExplosionDamage;
 
    /// <summary>Explosion radius in every direction. Gets multiplied with gridsize. X:up; Y:right; Z:down; W:left</summary>
    public Vector4Int ExplosionRadius { get => new Vector4Int(ExplosionRadiusUp, ExplosionRadiusRight, ExplosionRadiusDown, ExplosionRadiusLeft); }

    /// <summary>Explosion radius up</summary>
    public int ExplosionRadiusUp;
    /// <summary>Explosion radius down</summary>
    public int ExplosionRadiusDown;
    /// <summary>Explosion radius left</summary>
    public int ExplosionRadiusLeft;
    /// <summary>Explosion radius right</summary>
    public int ExplosionRadiusRight;

    /// <summary>Start time till explosion</summary>
    public float ExplosionTimer;
}
