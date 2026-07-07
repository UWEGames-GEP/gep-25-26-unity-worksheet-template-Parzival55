using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private AudioClip pickupSound;

    private bool playerInRange;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Inventory.Instance.AddItem(item);

            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}