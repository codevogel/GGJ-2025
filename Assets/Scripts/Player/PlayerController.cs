using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Rigidbody RigidBody => rigidBody;
    private Rigidbody rigidBody;

    private Vector2 movementInput = new Vector2();
    public Vector2 MovementInput { get { return movementInput; } private set { } }
    
    [SerializeField] private float moveForce;
    public float MoveForce { get { return moveForce; } }

    [Space]
    [SerializeField] private float jumpForce;
    [SerializeField] private float floorRayLength;
    [SerializeField] private LayerMask groundLayer;
    private bool isJumping = false;

    [Space]
    [SerializeField] private float dashForce;
    public float DashForce { get { return dashForce; } }

    [SerializeField] private float dashCooldown;
    [SerializeField] private UnityEvent onDash;
    private bool isDashing;
    private Coroutine dashCooldownCoroutine;

    public float DashCooldown { get { return dashCooldown; } }

    [SerializeField] private bool isAlive = true;
    public bool IsAlive { get { return isAlive;} set { isAlive = value; } }

    public int PlayerNumber { get; set; }

    private Vector3 bufferedVelocity;
    public Vector3 BufferedVelocity { get { return bufferedVelocity; } }

    [SerializeField] public GameObject wizardModel;
    private Animator wizardAnimator;
   private Vector3 lastForward;

    private void Awake()
    {
      lastForward= transform.forward;
        wizardAnimator = wizardModel.GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
        rigidBody = GetComponent<Rigidbody>();
    }

   //private void Start()
   //{
   //    StartCoroutine(DoBufferVelocity());
   //}

   private void Update()
   {
      bool isMoving = movementInput.magnitude > 0.1;
      if (isMoving)
      {
         lastForward = -new Vector3(movementInput.x, 0, movementInput.y);
      } 
      wizardModel.transform.rotation = Quaternion.LookRotation(lastForward, Vector3.up);
      wizardAnimator.SetBool("Moving", isMoving);

      //align wizard with ground
      RaycastHit hit;
      if (Physics.Raycast(transform.position, Vector3.down,out hit, floorRayLength, groundLayer))
      {
         wizardModel.transform.position = hit.point;
      }
      else
      {
         wizardModel.transform.position = transform.position - (Vector3.up * 1);
      }
   }

   public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    //public void OnJump(InputAction.CallbackContext context)
    //{
    //    isJumping = context.action.triggered;
    //}

    //public void OnDash(InputAction.CallbackContext context)
    //{
    //    isDashing = context.action.triggered;
    //}

    private void FixedUpdate()
    {
        if (!isAlive)
        {
            // Attempt at fixing the jump bug that can occur at the start of the game
            isJumping = false;
            isDashing = false;
            return;
        }

      // move
      Vector3 moveForceVector = new Vector3(movementInput.x, 0, movementInput.y) * moveForce;
      rigidBody.AddForce(moveForceVector);

      //// dash
      //if (isDashing && dashCooldownCoroutine == null && movementInput != Vector2.zero)
      //{
      //    Vector3 dashForceVector = new Vector3(movementInput.x, 0, movementInput.y) * dashForce;
      //    rigidBody.AddForce(dashForceVector, ForceMode.Impulse);
      //    onDash?.Invoke();
      //    dashCooldownCoroutine = StartCoroutine(DoDashCooldown());
      //}

      //// jump
      //bool isGrounded = Physics.Raycast(transform.position, Vector3.down, floorRayLength, groundLayer);
      //if (isJumping && isGrounded)
      //{
      //    // bouncy material workaround to advoid super jumps.
      //    if (rigidBody.linearVelocity.y / rigidBody.mass < jumpForce)
      //    {
      //        rigidBody.linearVelocity = new Vector3(rigidBody.linearVelocity.x, 0, rigidBody.linearVelocity.z);
      //        rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
      //    }
      //}
   }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawLine(transform.position, transform.position + Vector3.down * floorRayLength);
    //}

    //private IEnumerator DoDashCooldown()
    //{
    //    yield return new WaitForSeconds(dashCooldown);
    //    dashCooldownCoroutine = null;
    //}

    //private IEnumerator DoBufferVelocity()
    //{
    //    Vector3 _currentVelocity = rigidBody.linearVelocity;

    //    while (true)
    //    {
    //        yield return new WaitForFixedUpdate();
    //        bufferedVelocity = _currentVelocity;
    //        _currentVelocity = rigidBody.linearVelocity;
    //    }
    //}
}