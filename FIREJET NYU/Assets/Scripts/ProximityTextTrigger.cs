using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProximityTextTrigger : MonoBehaviour
{
    public GameObject textObject;
    public string playerTag = "Player";

    void Start()
    {
        if (textObject != null)
            textObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            textObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            textObject.SetActive(false);
        }
    }
}
