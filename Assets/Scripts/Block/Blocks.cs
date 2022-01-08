using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    [SerializeField]
    [Tooltip("If true, this block can be destroyed")]
    private bool m_IsDestroyable = true;
    public bool IsDestroyable { get => m_IsDestroyable; }
    public bool IsDestroyed { get; protected set; }

    [SerializeField]
    [Tooltip("Material applied at start if block is destroyable")]
    private Material m_MaterialDestroyable;
    [SerializeField]
    [Tooltip("Material applied at start if block is not destroyable")]
    private Material m_MaterialNotDestroyable;

    // Start is called before the first frame update
    void Start()
    {
        ChangeMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDestroyed)
            DestroyBlock();
    }

    /// <summary>
    /// Change status of <see cref="IsDestroyable"/>
    /// </summary>
    /// <param name="_newStatus">new Value</param>
    public void ChangeDestroyableStatus(bool _newStatus)
    {
        if (m_IsDestroyable != _newStatus)
        {
            // status got changed
            m_IsDestroyable = _newStatus;

            // change material of block
            ChangeMaterial();
        }
    }

    /// <summary>
    /// Call this method if a Block got hit by a Bomb or whatever
    /// </summary>
    /// <param name="_damage">Damage of hit (In most cases, this is not relevant)</param>
    public virtual void BlockHit(int _damage)
    {
        if (!IsDestroyable)
            return;

        IsDestroyed = true;
    }

    /// <summary>
    /// Block dies
    /// </summary>
    protected virtual void DestroyBlock()
    {
        Object.Destroy(this.gameObject);
    }

    #region Change material
    private void ChangeMaterial()
    {
        ChangeMaterial(m_IsDestroyable);
    }

    private void ChangeMaterial(bool _isDestroyable)
    {
        if (_isDestroyable)
            ChangeMaterial(m_MaterialDestroyable);
        else
            ChangeMaterial(m_MaterialNotDestroyable);
    }

    private void ChangeMaterial(Material _material)
    {
        GetComponent<MeshRenderer>().material = _material;
    }
    #endregion
}
