using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float backgroundScrollSpeed = 0.5f;
    private Material myMaterial;
    private Vector2 offSet;

    private void Awake()
    {
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0, backgroundScrollSpeed);
    }

    void Update()
    {
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}