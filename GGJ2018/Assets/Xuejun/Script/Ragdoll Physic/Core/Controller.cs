using UnityEngine;
using Sirenix.OdinInspector;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private float m_movingSpeed = 1.0f; 

    [System.Flags]
    public enum MovementState
    {
        None = 0,
        Left = 1,
        Right = 2,
        Up = 4,
        Down = 8,
        WallJump = 16,
        GroundJump = 32
    }

    private Movement m_movement;

    private Rotation m_rotation;

    private Animations m_animation;

    private CharacterInformation m_characterInfo;

    private Rigidbody m_hip;

    public GrabHandler GrabHandler { get; private set; }

    private PlayerController m_playerController;

    [ShowInInspector, ReadOnly]
    public MovementState TMovementState { get; private set; }

    private void Start()
    {
        m_movement = GetComponent<Movement>();
        m_characterInfo = GetComponent<CharacterInformation>();
        GrabHandler = GetComponent<GrabHandler>();
        m_hip = GetComponentInChildren<Hip>().GetComponent<Rigidbody>();
        m_animation = GetComponent<Animations>();
        m_rotation = GetComponent<Rotation>();
        m_playerController = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        TMovementState = MovementState.None;
        if (m_playerController.Device != null )
        {
            Vector3 movement = Vector3.zero;
            float horizontalInput = m_playerController.Move.x; ;
            if (horizontalInput < -0.05f)
            {
                TMovementState |= MovementState.Left;
                movement.x = horizontalInput;
            }
            else if (horizontalInput > 0.05f)
            {
                TMovementState |= MovementState.Right;
                movement.x = horizontalInput;
            }

            float verticalInput = m_playerController.Move.y;
            if (verticalInput < -0.05f)
            {
                TMovementState |= MovementState.Down;
                movement.y = verticalInput;
            }
            else if (verticalInput > 0.05f)
            {
                TMovementState |= MovementState.Up;
                movement.y = verticalInput;
            }
            m_movement.Move(new Vector3(horizontalInput, 0, verticalInput));

            if (m_playerController.Actions.Forward ||
                m_playerController.Actions.Back ||
                m_playerController.Actions.Left ||
                m_playerController.Actions.Right)
            {
                m_animation.Run();
            }

            if (m_playerController.Actions.Jump)
            {
                Jump(false, false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) )
        {
            Jump(false, false);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) )
        {
            m_animation.Run();
            m_movement.Move(new Vector3(- m_movingSpeed, 0, 0));
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            m_animation.Run();
            m_movement.Move(new Vector3(m_movingSpeed, 0, 0));
        }
    }


    #region Movement

    public void Jump(bool force = false, bool forceWallJump = false)
    {
        if ((m_characterInfo.m_sinceGrounded < 0.2f || m_characterInfo.m_sinceWall < 0.2f || GrabHandler.HoldSomethingAnchored) &&
           m_characterInfo.m_sinceFallen > 0f &&
           m_characterInfo.m_sinceJumped > 0.3f)
        {
            if (m_characterInfo.m_sinceWall > m_characterInfo.m_sinceGrounded)
            {
                m_characterInfo.WallNormal = Vector3.zero;
            }
            if (GrabHandler.HoldSomething)
            {
                GrabHandler.EndGrab();
            }

            m_characterInfo.m_sinceGrounded = 1f;
            m_characterInfo.m_sinceWall = 1f;
            m_characterInfo.m_sinceJumped = 0f;
            TMovementState = m_movement.Jump(force, forceWallJump) ? MovementState.WallJump : MovementState.GroundJump;
        }
        else if (force)
        {
            if (m_characterInfo.m_sinceWall > m_characterInfo.m_sinceGrabbed)
            {
                m_characterInfo.WallNormal = Vector3.zero;
            }
            if (GrabHandler.HoldSomething)
            {
                GrabHandler.EndGrab();
            }

            m_characterInfo.m_sinceGrounded = 1f;
            m_characterInfo.m_sinceWall = 1f;
            m_characterInfo.m_sinceJumped = 0f;
            TMovementState = m_movement.Jump(force, forceWallJump) ? MovementState.WallJump : MovementState.GroundJump;
        }
    }

    #endregion
}
