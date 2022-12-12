using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle" || collision.gameObject.tag == "enemy")
            Destroy(gameObject);
        if (collision.gameObject.name == "bullet(Clone)")
            Destroy(gameObject);
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
