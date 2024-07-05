using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChibiCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectibleItems"))
        {
            CollectItem(other.gameObject);
        }
    }

    private void CollectItem(GameObject item)
    {
        Instantiate(item.GetComponent<Collectible>().particleEffect, item.transform.position, Quaternion.identity);
        Destroy(item);
    }
}
