using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Private Variable
    public float translationSpeed = 20.0f;
    public float rotationSpeed = 25.0f;
    private float horizontalInput;
    private float forwardInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Player input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Move player forward
        transform.Translate(Vector3.forward * Time.deltaTime * translationSpeed * forwardInput);
        // Rotate player
        transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime);
    }
}
