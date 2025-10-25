using UnityEngine;

public class ObjectFlipper
{
    private Transform objectTransform;

    private Vector3 currentRotation;

    private Vector3 flippedRotation = new Vector3(0, -180f, 0);
    private Vector3 defaultRotation = new Vector3(0, 0, 0);

    public ObjectFlipper(Transform objectTransform)
    {
        this.objectTransform = objectTransform;
    }

    public void Flip(float normilizeAxis)
    {
        if (normilizeAxis != 0)
        {
            if (normilizeAxis < 0)
            {
                currentRotation = flippedRotation;
            }
            else
            {
                currentRotation = defaultRotation;
            }
        }

        objectTransform.eulerAngles = currentRotation;
    }
}