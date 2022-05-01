using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGrab : MonoBehaviour
{
    // Reference to the character camera.
    [SerializeField]
    private Player _player;
    // Reference to the slot for holding picked item.
    [SerializeField]
    private Transform _hands;
    // Reference to the currently held item.
    private PickableItem pickedItem;
    /// <summary>
    /// Method called very frame.
    /// </summary>
    private void Update()
    {
        // Execute logic only on button pressed
        if (Input.GetButtonDown("Grab"))
        {
            // Check if player picked some item already
            if (pickedItem)
            {
                // If yes, drop picked item
                DropItem(pickedItem);
            }
            else
            {
                // If no, try to pick item in front of the player

                Vector3 fwd = _player.transform.TransformDirection(Vector3.forward);
                Debug.DrawRay(_player.transform.position, fwd * 30, Color.blue);
                  
                RaycastHit hit;
                // Shot ray to find object to pick
                if (Physics.Raycast(_player.transform.position, fwd, out hit, 3.0f))
                {
                    // Check if object is pickable
                    var pickable = hit.transform.GetComponent<PickableItem>();
                    // If object has PickableItem class
                    if (pickable)
                    {
                        // Pick it
                        PickItem(pickable);
                    }
                }
            }
        }
    }
    /// <summary>
    /// Method for picking up item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void PickItem(PickableItem item)
    {
        // Assign reference
        pickedItem = item;
        // Disable rigidbody and reset velocities
        item.Rb.isKinematic = true;
        item.Rb.velocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;
        // Set Slot as a parent
        item.transform.SetParent(_hands);
        // Reset position and rotation
        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
    }
    /// <summary>
    /// Method for dropping item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void DropItem(PickableItem item)
    {
        // Remove reference
        pickedItem = null;
        // Remove parent
        item.transform.SetParent(null);
        // Enable rigidbody
        item.Rb.isKinematic = false;
        // Add force to throw item a little bit
        item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);
    }
}
