using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABomb : MonoBehaviour
{
    protected Collider BombCollider { get; private set; }

    public BasePlayerController Owner { get; set; }
    [SerializeField]
    private SO_Bomb m_BombData;
    public SO_Bomb BombData { get => m_BombData; }
    protected float tickingTime;
    public bool Exploded { get; protected set; }

    [SerializeField]
    private string m_BlockTag = "";

    #region Layer
    protected int BreakableLayer { get; private set; }
    protected int UnbreakableLayer { get; private set; }
    protected int WallLayer { get; private set; }

    private int BombHitLayermask { get; set; }
    #endregion
    // Start is called before the first frame update
    protected virtual void Start()
    {
        // layer
        BreakableLayer = LayerMask.GetMask("Breakable");
        UnbreakableLayer = LayerMask.GetMask("Unbreakable");
        WallLayer = LayerMask.GetMask("Wall");
        BombHitLayermask = LayerMask.GetMask("Player", "Unbreakable", "Breakable", "Wall");

        // collider
        BombCollider = GetComponent<Collider>();
#if UNITY_EDITOR
        if (BombCollider == null)
        {
            Debug.LogWarning("Bomb has no collider.", this.gameObject);
            Debug.Break();
        }
#endif

        // bomb data
#if UNITY_EDITOR
        if (BombData == null)
        {
            Debug.LogWarning("Bombdata is null.", this.gameObject);
            Debug.Break();
        }
#endif
        tickingTime = BombData.ExplosionTimer;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        tickingTime -= Time.deltaTime;
        if (tickingTime <= 0 && !Exploded)
        {
            Explode();
            Exploded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BombCollider.isTrigger = false;
    }

    public abstract void Explode();

    protected RaycastHit[] ExplodeHit(bool _logic = true)
    {
        #region Collision detection
        // up
        RaycastHit[] HitUp = Physics.BoxCastAll
            (
            this.gameObject.transform.position,
            FieldManager.Get.Grid.cellSize / 2.01f,
            Vector3.forward,
            this.gameObject.transform.rotation,
            FieldManager.Get.Grid.cellSize.y * BombData.ExplosionRadiusUp,
            BombHitLayermask
            );
        // down
        RaycastHit[] HitDown = Physics.BoxCastAll
            (
            this.gameObject.transform.position,
            FieldManager.Get.Grid.cellSize / 2.01f,
            Vector3.back,
            this.gameObject.transform.rotation,
            FieldManager.Get.Grid.cellSize.y * BombData.ExplosionRadiusDown,
            BombHitLayermask
            );
        // left
        RaycastHit[] HitLeft = Physics.BoxCastAll
            (
            this.gameObject.transform.position,
            FieldManager.Get.Grid.cellSize / 2.01f,
            Vector3.left,
            this.gameObject.transform.rotation,
            FieldManager.Get.Grid.cellSize.y * BombData.ExplosionRadiusLeft,
            BombHitLayermask
            );
        // right
        RaycastHit[] HitRight = Physics.BoxCastAll
            (
            this.gameObject.transform.position,
            FieldManager.Get.Grid.cellSize / 2.01f,
            Vector3.right,
            this.gameObject.transform.rotation,
            FieldManager.Get.Grid.cellSize.y * BombData.ExplosionRadiusRight,
            BombHitLayermask
            );
        #endregion

        if (!_logic)
        {
            // returns every hit. Has no logic
            return MakeManyArraysToOne(HitUp, HitDown, HitLeft, HitRight);
        }
        // sort by distance
        SortByDistance(HitUp);
        SortByDistance(HitDown);
        SortByDistance(HitLeft);
        SortByDistance(HitRight);

        // unbreakable and breakable blocks block ray
        RayCastList ActualHit = new RayCastList();

        //include logic
        ActualHit.AddRange(SortLogicExplode(HitUp));
        ActualHit.AddRange(SortLogicExplode(HitDown));
        ActualHit.AddRange(SortLogicExplode(HitLeft));
        ActualHit.AddRange(SortLogicExplode(HitRight));

        return ActualHit.ToArray();
    }

    /// <summary>
    /// Checks every hit for something that blocks the explosion.
    /// </summary>
    /// <param name="_hit">all hit</param>
    /// <returns></returns>
    private RaycastHit[] SortLogicExplode(RaycastHit[] _hit)
    {
        List<RaycastHit> tR = new List<RaycastHit>();
        foreach (RaycastHit item in _hit)
        {
            // Breakable hit -> Add to list and stop
            if (item.collider.gameObject.tag == "Block")
            {
                // if block is destroyable add to hit list, else don't
                if (item.collider.gameObject.GetComponent<Blocks>().IsDestroyable)
                {
                    tR.Add(item);
                }
                break;
            }
            // anything else -> add to list and continue (like Player)
            else
            {
                tR.Add(item);
            }
        }

        return tR.ToArray();
    }

    private void SortByDistance(RaycastHit[] _array)
    {
        List<RaycastHit> tR = new List<RaycastHit>(_array);
        tR.Sort(delegate (RaycastHit x, RaycastHit y)
        {
            return x.distance.CompareTo(y.distance);
        }
        );
        tR.CopyTo(_array);
    }
    private T[] MakeManyArraysToOne<T>(params T[][] _values)
    {
        List<T> tR = new List<T>();
        foreach (T[] item in _values)
        {
        tR.AddRange(item);
        }

        return tR.ToArray();
    }

    private T[] HashSetToArray<T>(HashSet<T> _set)
    {
        T[] tR = new T[_set.Count];
        _set.CopyTo(tR);

        return tR;
    }

    protected virtual void CheckCollisionForBlockAndDamageThem(IEnumerable<RaycastHit> _hits)
    {
        foreach (RaycastHit hit in _hits)
        {
            if(hit.collider.gameObject.tag == "Block") { }
        }
    }
    protected virtual void DamageBlock(Blocks _block)
    {

    }
}
