using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBomb : ABomb
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }




    public override void Explode()
    {
        Debug.Log("Exploded");

        RaycastHit[] hit = ExplodeHit(true);

        string hits = "";
        foreach (RaycastHit item in hit)
        {
            hits += item.collider.gameObject.name + " / ";
        }
        if (hits.Length > 0)
            hits = hits.Remove(hits.Length - 3);
        else
            hits = "No hits";

        Debug.Log("Hits: " + hits);
        // Grid scale x ==> left and right
        // Grid scale y ==> up and down
        #region Visualization
        ////up
        //for (int i = 0; i < BombData.ExplosionRadiusUp; i++)
        //{
        //    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    go.transform.position = new Vector3
        //        (
        //        transform.position.x,
        //        transform.position.y,
        //        transform.position.z + (FieldManager.Get.Grid.cellSize.y * (i + 1))
        //        );
        //    go.transform.localScale = FieldManager.Get.Grid.cellSize;
        //}
        ////down
        //for (int i = 0; i < BombData.ExplosionRadiusDown; i++)
        //{
        //    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    go.transform.position = new Vector3
        //        (
        //        transform.position.x,
        //        transform.position.y,
        //        transform.position.z - (FieldManager.Get.Grid.cellSize.y * (i + 1))
        //        );
        //    go.transform.localScale = FieldManager.Get.Grid.cellSize;
        //}
        ////left
        //for (int i = 0; i < BombData.ExplosionRadiusLeft; i++)
        //{
        //    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    go.transform.position = new Vector3
        //        (
        //        transform.position.x - (FieldManager.Get.Grid.cellSize.x * (i + 1)),
        //        transform.position.y,
        //        transform.position.z
        //        );
        //    go.transform.localScale = FieldManager.Get.Grid.cellSize;
        //}
        ////right
        //for (int i = 0; i < BombData.ExplosionRadiusLeft; i++)
        //{
        //    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    go.transform.position = new Vector3
        //        (
        //        transform.position.x + (FieldManager.Get.Grid.cellSize.x * (i + 1)),
        //        transform.position.y,
        //        transform.position.z
        //        );
        //    go.transform.localScale = FieldManager.Get.Grid.cellSize;
        //}
        #endregion


    }
}
