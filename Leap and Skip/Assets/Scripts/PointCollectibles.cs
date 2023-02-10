using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCollectibles : MonoBehaviour
{
    private int points=0;

    [SerializeField] private Text PointText;
    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            points++;
            PointText.text = "Points " + points;
        }
    }
}
