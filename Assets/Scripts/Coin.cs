using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Inventories;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip pickedUpSound;
    [SerializeField] float coinValue = 10f;

    Purse purse;

    private void Awake()
    {
        purse = FindObjectOfType<Purse>();
    }

    private void OnTriggerEnter2D()
    {
        purse.UpdateBalance(coinValue);
        AudioSource.PlayClipAtPoint(pickedUpSound, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
