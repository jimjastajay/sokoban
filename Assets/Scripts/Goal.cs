using UnityEngine;

public class Goal : MonoBehaviour
{

    Renderer myRenderer;

    [SerializeField]
    Color emptyColor, fullColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        emptyColor = myRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("block"))
        {
            myRenderer.material.color = fullColor;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("block"))
        {
            myRenderer.material.color = emptyColor;
        }
    }
}
