using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Old
/*
 
public class UniqueList<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection, 
{
    private List<T> m_List;

    public UniqueList()
    {
        m_List = new List<T>();
    }
    public UniqueList(int _capacity)
    {
        m_List = new List<T>(_capacity);
    }
    public UniqueList(IEnumerable<T> _collection)
    {
        m_List = new List<T>(_collection);
    }

    public T this[int index] { get => m_List[index]; }
    object IList.this[int index]
    {
        get => m_List[index];
        set
        {
            if (!CanCastToItem(value))
                throw new TypeAccessException("Wrong variable type");
            m_List[index] = (T)value;
        }
    }

    T IList<T>.this[int index] { get => m_List[index]; set => m_List[index] = value; }

    public int Count => m_List.Count;

    public bool IsReadOnly => false;

    public bool IsSynchronized => false;

    public object SyncRoot => null;

    public bool IsFixedSize => false;

    public void Add(T item)
    {
        m_List.Add(item);
    }

    public int Add(object value)
    {
        m_List.Add((T)value);
        return m_List.Count - 1;
    }

    public void Clear()
    {
        m_List.Clear();
    }

    public bool Contains(T item)
    {
        return m_List.Contains(item);
    }

    public bool Contains(object value)
    {
        if (!CanCastToItem(value))
            return false;

        return m_List.Contains((T)value);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        m_List.CopyTo(array, arrayIndex);
    }

    public void CopyTo(Array array, int index)
    {
        if (!CanCastToItem(array, true))
            throw new InvalidOperationException("Type of array was invalid.");

        m_List.CopyTo((T[])array, index);
    }

    public void CopyTo(int index, T[] array, int arrayIndex, int count)
    {
        if (!CanCastToItem(array, true))
            throw new InvalidOperationException("Type of array was invalid.");

        m_List.CopyTo(index, array, arrayIndex, count);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return m_List.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        return m_List.IndexOf(item);
    }

    public int IndexOf(object value)
    {
        if (!CanCastToItem(value))
            return -1;
        return m_List.IndexOf((T)value);
    }

    public void Insert(int index, T item)
    {
        m_List.Insert(index, item);
    }

    public void Insert(int index, object value)
    {
        if (!CanCastToItem(value))
            throw new InvalidOperationException("Item has wrong type.");

        m_List.Insert(index, (T)value);
    }

    public bool Remove(T item)
    {
        return m_List.Remove(item);
    }

    public void Remove(object value)
    {
        if (!CanCastToItem(value))
            throw new InvalidOperationException("Item has wrong type.");

        m_List.Remove((T)value);
    }

    public void RemoveAt(int index)
    {
        m_List.RemoveAt(index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return m_List.GetEnumerator();
    }

    protected bool CanCastToItem(object _value, bool _isarray = false)
    {
        // is no array
        if (!_isarray)
        {
            try
            {
                T temp = (T)_value;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        // is array
        else
        {
            try
            {
                T[] temp = (T[])_value;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}

*/
#endregion
public class UniqueList<T> : List<T>
{
    public new void Add(T item)
    {
        if (!base.Contains(item))
            base.Add(item);
    }

    public new void AddRange(IEnumerable<T> collection)
    {
        foreach (T item in collection)
        {
            if (!base.Contains(item))
                base.Add(item);
        }
    }
}