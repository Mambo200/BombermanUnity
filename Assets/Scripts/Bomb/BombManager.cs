using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    private static BombManager m_Instance;
    public static BombManager Get { get => m_Instance; }

    [SerializeField]
    private GameObject m_DefaultBombAsset;
    public GameObject DefaultBombAsset { get => m_DefaultBombAsset; }

    void Awake()
    {
        if (m_Instance != null)
#if UNITY_EDITOR
        {
            Debug.LogWarning($"It already exists an Instance of {nameof(BombManager)}. Editor will be paused", this.gameObject);
            Debug.Break();
            Destroy(this.gameObject);
        }

#else
        {
            Debug.LogWarning($"It already exists an Instance of {nameof(FieldManager)}. This will be destroyed", this.gameObject);
            Destroy(this.gameObject);
        }
#endif

        m_Instance = GameObject.FindObjectOfType<BombManager>();
        if (m_Instance == null)
        {
            Debug.LogWarning($"There is no Instance of {nameof(BombManager)} in the current scene. Its not possible!", this.gameObject);
#if UNITY_EDITOR
            Debug.Break();
#endif
        }

    }

    public ABomb PlaceDefaultBomb(Vector3 _pos, BasePlayerController _player)
    {
        GameObject go = GameObject.Instantiate(DefaultBombAsset);
        DefaultBomb b = go.GetComponent<DefaultBomb>();
#if UNITY_EDITOR
        if(b == null)
        {
            Debug.LogError("Object has no Bomb script", this.gameObject);
            Debug.Break();
        }
#endif
        b.Owner = _player;
        Vector3 newPos = FieldManager.Get.GetTileCenterPosition(_pos);
        go.transform.position = newPos;

        Debug.Log($"{_pos} --> {newPos}");
        return b;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
