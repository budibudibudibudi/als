using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class enemyscript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(gameObject);
        }
    }
}
