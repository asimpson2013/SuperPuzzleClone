using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Moves the button up and down when the mouse is hovering on the button
/// </summary>
public class ButtonBobbing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Whether or not the button is being selected
    /// </summary>
    bool selected = false;
    /// <summary>
    /// The velocity the button is moving up and down
    /// </summary>
    public float velocity = 1;
    /// <summary>
    /// The height that the button moves up and down
    /// </summary>
    public float height = 5;
    /// <summary>
    /// The speed that the button moves up and down
    /// </summary>
    float speed = 0;
    /// <summary>
    /// The placement the button is at originally
    /// </summary>
    Vector3 OriginalPlacement;
    
    /// <summary>
    /// Sets the original placement to the current placement
    /// </summary>
    void Start()
    {
        OriginalPlacement = transform.position;
    }

    /// <summary>
    /// Moves the button up and down when selected is true and sets the button back to the original position when selected is false
    /// </summary>
    void Update()
    {
        if (selected)
        {
            speed += velocity * Time.deltaTime;
            transform.position += new Vector3(0, height * Mathf.Cos(speed), 0);
        }
        else
        {
            transform.position = OriginalPlacement;
        }
    }

    /// <summary>
    /// Sets selected to true when the mouse is over the button
    /// </summary>
    /// <param name="eventData">The event data, it's not used in the method</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        selected = true;
    }

    /// <summary>
    /// Sets selected to false when the mouse is moved from the button
    /// </summary>
    /// <param name="eventData">The event data, it's not used in the method</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        selected = false;
    }
}
