using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

    [SerializeField]
    Transform newCamPos;

    [SerializeField]
    float smoothVal;
    Vector3 velocity = Vector3.zero;

    Vector3 startPos;

    Transform cam;

    void Start()
    {
        cam = Camera.main.transform;       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MoveCamera());
        }
    }

    IEnumerator MoveCamera(){
        while(Vector3.Distance(cam.position, newCamPos.position) > 0.1f)
        {
            cam.position = Vector3.SmoothDamp(cam.position, newCamPos.position, ref velocity, smoothVal);
            yield return null;
        }
        cam.position = newCamPos.position;
        yield return null;
    }
}
