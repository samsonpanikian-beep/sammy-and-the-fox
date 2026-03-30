using System.Transactions;
using UnityEngine;

public class ClimbMechanic : MonoBehaviour
{
    public bool canMountDismount = true;
    public bool foxIsMounted;

    [SerializeField] Transform mountPoint;
    [SerializeField] CapsuleCollider playerCollider;
    [SerializeField] Transform playerTransform;

    [SerializeField] float dismountForce;

    private void OnTriggerEnter(Collider other)
    {
        if (canMountDismount && other.CompareTag("Player"))
        {
            foxIsMounted = true;

            playerTransform.GetComponent<Rigidbody>().isKinematic = true;
            playerTransform.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;

            playerTransform.SetParent(mountPoint, false);
            playerTransform.position = mountPoint.position;

            playerCollider.enabled = false;
            playerTransform.rotation = transform.rotation;
        }
    }

    private void Update()
    {
        if (foxIsMounted && Input.GetKeyDown(KeyCode.L))
        {
            playerTransform.GetComponent<Rigidbody>().isKinematic = false;
            playerTransform.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

            playerTransform.SetParent(null, true);

            playerCollider.enabled = true;
            
            playerTransform.GetComponent<Rigidbody>().AddForce(playerTransform.right * dismountForce + playerTransform.up * dismountForce / 4f);

            foxIsMounted = false;
        }
    }
}
