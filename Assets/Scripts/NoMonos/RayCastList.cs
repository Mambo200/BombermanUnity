using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RayCastList : List<RaycastHit>
{
    #region Constructor
    public RayCastList() : base()
    {
    }

    public RayCastList(IEnumerable<RaycastHit> collection) : base(collection)
    {
    }

    public RayCastList(int capacity) : base(capacity)
    {
    }
    #endregion

    public new void Add(RaycastHit item)
    {
        for (int i = 0; i < base.Count; i++)
        {
            if (Compare(base[i], item))
                return;
        }
            base.Add(item);
    }

    public new void AddRange(IEnumerable<RaycastHit> collection)
    {
        foreach (RaycastHit item in collection)
        {
            Add(item);
        }
    }

    /// <summary>
    /// Compares two <see cref="RaycastHit"/>s gameobjects.
    /// </summary>
    /// <param name="_left">First <see cref="RaycastHit"/></param>
    /// <param name="_right">Second <see cref="RaycastHit"/></param>
    /// <returns>true: identical gameobject || false: not identical gameobject</returns>
    private bool Compare(RaycastHit _left, RaycastHit _right)
    {
        return _left.collider.gameObject == _right.collider.gameObject;
    }
}
