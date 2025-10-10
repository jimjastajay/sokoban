using UnityEngine;

public class Button : MonoBehaviour
{

    [SerializeField]
    GameObject connectedDoor;

    void OnTriggerEnter(Collider collision)
    {
        connectedDoor.GetComponent<Animator>().SetBool("isOpen", true);
    }

    void OnTriggerExit(Collider other)
    {
        connectedDoor.GetComponent<Animator>().SetBool("isOpen", false);
    }
}
