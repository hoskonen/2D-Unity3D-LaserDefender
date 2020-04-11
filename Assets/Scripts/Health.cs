using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    private TextMeshProUGUI health;
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        health = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        health.text = player.GetHealth().ToString();
    }
}