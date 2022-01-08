using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void PlaceBomb()
    {
        BombManager.Get.PlaceDefaultBomb(this.gameObject.transform.position, this);
    }

    public virtual void GetDamage(int _damage)
    {
        Debug.Log("Player dead");
    }
}
