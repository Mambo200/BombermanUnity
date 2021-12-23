using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldManager : MonoBehaviour
{
    private static FieldManager m_Instance;
    public static FieldManager Get { get => m_Instance; }

#if UNITY_EDITOR
    [SerializeField]
    [Tooltip("Editor will be paused if an error with singleton occurs")]
    private bool m_PauseOnError = false;
#endif

    [SerializeField]
    private Tilemap m_Tilemap;
    public Tilemap Tilemap { get => m_Tilemap; }
    [SerializeField]
    private UnityEngine.Grid m_Grid;
    public UnityEngine.Grid Grid { get => m_Grid; }

    void Awake()
    {
        if (m_Instance != null)
#if UNITY_EDITOR
        {
            Debug.LogWarning($"It already exists an Instance of {nameof(FieldManager)}. Editor will be paused", this.gameObject);
            Debug.Break();
            Destroy(this.gameObject);
        }

#else
        {
            Debug.LogWarning($"It already exists an Instance of {nameof(FieldManager)}. This will be destroyed", this.gameObject);
            Destroy(this.gameObject);
        }
#endif

        m_Instance = GameObject.FindObjectOfType<FieldManager>();
        if (m_Instance == null)
        {
            Debug.LogWarning($"There is no Instance of {nameof(FieldManager)} in the current scene. Its not possible!", this.gameObject);
#if UNITY_EDITOR
            Debug.Break();
#endif
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 GetTileCenterPosition(Vector3 _position)
    {
        Vector3 tr = m_Grid.GetCellCenterWorld(m_Grid.LocalToCell(_position));
        //Vector3 tr = m_Grid.GetCellCenterWorld(ToVector(_position));
        //Vector3 tr = m_Tilemap.GetCellCenterWorld(ToVector(_position));
        return tr;
    }
    public Vector3 GetTileCenterPosition(Vector3Int _position)
    {
        Vector3 tr = m_Grid.GetCellCenterWorld(m_Grid.LocalToCell(_position));
        //Vector3 tr = m_Grid.GetCellCenterWorld(_position);
        //Vector3 tr = m_Tilemap.GetCellCenterWorld(_position);
        return tr;
    }

    #region Converter
    private Vector3 ToVector(Vector3Int _position)
    {
        Vector3 tr = new Vector3(
            _position.x,
            // We need to swap Z- and Y-axis to make the position work
            _position.y,
            _position.z
            );
        return tr;
    }
    private Vector3Int ToVector(Vector3 _position)
    {
        Vector3Int tr = new Vector3Int(
            (int)_position.x,
            // We need to swap Z- and Y-axis to make the position work
            (int)_position.z,
            (int)_position.y
            );
        return tr;
    }
    #endregion
}
