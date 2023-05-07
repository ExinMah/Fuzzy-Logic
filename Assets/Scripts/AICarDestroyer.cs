using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarDestroyer : MonoBehaviour
{
    void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.tag == "AI Car")
        {
            Destroy(collision.gameObject);
        }
    }

}
