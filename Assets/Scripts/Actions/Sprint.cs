using UnityEngine;

[RequireComponent(typeof(Move))]
public class Sprint : MonoBehaviour
{
    private bool isSprinting = false;

    private new Rigidbody2D rigidbody;
    private Controller controller;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller>();
    }

    private void Update()
    {
        isSprinting = controller.input.RetrieveSprintInput();
    }

    private void FixedUpdate()
    {

    }
}
