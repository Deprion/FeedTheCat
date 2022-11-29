using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private PlayerManager pm;
    private Animation anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerManager>();
        anim = GetComponent<Animation>();
    }

    public void Movement(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            rb.velocity = speed * Time.fixedDeltaTime * ctx.ReadValue<Vector2>();

            if (rb.velocity.x > 0) anim.ChangeDirRight();
            else if (rb.velocity.x < 0) anim.ChangeDirLeft();
        }
        else if (ctx.canceled) rb.velocity = Vector2.zero;
    }
    public void Dash(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            rb.AddForce(rb.velocity, ForceMode2D.Impulse);
            pm.SubEnergy(25);
        }
    }
    private void OnDestroy()
    {
        rb.simulated = false;
    }
}
