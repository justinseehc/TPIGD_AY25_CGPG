using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour
{
    public int numBlades = 3;
    List<Transform> blades = new List<Transform>();

    void Start()
    {
        // This is but one way of doing it! There are other ways as well!
        const float BLADE_LENGTH = 7.0f;
        float stepAngle = Mathf.PI * 2 / numBlades;
        for (int iBlade = 0; iBlade < numBlades; iBlade++)
        {
            float curAngle = iBlade * stepAngle;

            // Compute the location of the blade in world space.
            Vector3 spawnLocation = new Vector3(
                Mathf.Cos(curAngle) * BLADE_LENGTH / 2f,
                Mathf.Sin(curAngle) * BLADE_LENGTH / 2f,
                0f);
            spawnLocation += this.transform.position;
            spawnLocation -= Vector3.forward * 0.75f;

            // Instantiate the new blade.
            Transform newBlade = GameObject.CreatePrimitive(PrimitiveType.Cylinder).transform;
            blades.Add(newBlade);

            // Set the scale (local), position (world), and rotation (world).
            newBlade.localScale = new Vector3(0.7f, BLADE_LENGTH / 2f, 0.7f);
            newBlade.position = spawnLocation;
            newBlade.Rotate(0, 0, (curAngle + (Mathf.PI / 2f)) * Mathf.Rad2Deg);

            // Parent to the rotor.
            newBlade.SetParent(this.transform, worldPositionStays: true);
        }
    }

    void Update()
    {
        // Use this because of gimbal lock. Good introduction to concept.
        const float ROTATE_SPEED = 50.0f;
        transform.RotateAround(transform.position, -Vector3.forward, ROTATE_SPEED * Time.deltaTime);
    }
}
