using UnityEngine;

public class Shredder : MonoBehaviour
{
    [SerializeField] private GameObject shreddingTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}