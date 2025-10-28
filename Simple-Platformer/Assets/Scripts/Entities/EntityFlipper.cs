using UnityEngine;

public class EntityFlipper
{
    private Transform objectTransform;

    private Vector3 currentRotation;

    private Vector3 flippedRotation = new Vector3(0, -180f, 0);
    private Vector3 defaultRotation = new Vector3(0, 0, 0);

    public EntityFlipper(Transform objectTransform)
    {
        this.objectTransform = objectTransform;
    }

    public void PhysicsFlip(float normilizeAxis)
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

    public void SimpleFlip(bool isRight)
    {
        if (isRight)
        {
            currentRotation = defaultRotation;
        }
        else
        {
            currentRotation = flippedRotation;
        }

        objectTransform.eulerAngles = currentRotation;
    }
}