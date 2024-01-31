using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using System.Threading.Tasks; // Hinzugefügt für Parallel

public class PlayerMovement_v3 : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] float LRmovementSpeed = 5f;
    [SerializeField] float VHmovementSpeed = 0f;                     // Vorne / Hinten Movement Speed
    [SerializeField] float jumpForce = 5f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField]
    public GameObject playerObject;

    private CapsuleCollider capsuleCollider;    // Reference to the Capsule Collider component
    
    // Original values of the Capsule Collider properties
    private Vector3 originalCenter;
    private float originalHeight;

    private KinectSensor _sensor;
    private BodyFrameReader _reader;
    private Body[] _Data;

    // Variablen fuer die Gelenkpositionen - speichert immer die vorherigen Positionen
    private float _previousXPosition;
    
    private float startYPositionHead;

    // Schwellen fuer die Positionsaenderung - umso gruesser bzw. kleiner diese sind, desto schwieriger
    private float MovementThreshold = 0.04f;     // Schwelle fuer Seitwaertsbewegung
    private float BendingThreshold = 0.75f;     // Schwelle fuer das Buecken (negative Werte, da Y-Position nach unten zunimmt)
    private float JumpThreshold = 1.2f;          // Schwelle fuer das Erkennen eines Sprungs

    // Variablen fuer den aktuellen Zustand
    public static bool isJumping = false;
    public static bool isMovingLeft = false;
    public static bool isMovingRight = false;
    public static bool isDucking = false; 


    public static bool slideCooldown = false;

    // Definiere die Gelenktypen fuer den SpineBase (Rumpf), Kopf, linken Fuss und rechten Fuss
    JointType spineBase = JointType.SpineBase;
    JointType head = JointType.Head;

    bool headStart = true;

    // Start wird beim ersten Frame aufgerufen
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        capsuleCollider = GetComponent<CapsuleCollider>();

        if (capsuleCollider != null)
        {
            // Store the original capsuleCollider values
            originalCenter = capsuleCollider.center;
            originalHeight = capsuleCollider.height;
        }
        else
        {
            Debug.LogError("Capsule Collider component not found on the player GameObject.");
        }

        _sensor = KinectSensor.GetDefault();

        if (_sensor != null)
        {
            _reader = _sensor.BodyFrameSource.OpenReader();

            if (!_sensor.IsOpen)
            {
                _sensor.Open();
            }
        }

        _Data = new Body[_sensor.BodyFrameSource.BodyCount];

        
    }

    void Update()
    {
        
        float verticalVelocity = rb.velocity.y;

        if (_reader != null)
        {
            var frame = _reader.AcquireLatestFrame();

            if (frame != null)
            {
                frame.GetAndRefreshBodyData(_Data);


                // Parallelisiere die Verarbeitung der Body-Daten
                Parallel.ForEach(_Data, body =>
                {
                    if (body != null && body.IsTracked)
                    {
                        // Horizontale und vertikale Bewegung parallel verarbeiten
                        Parallel.Invoke(() => horizontalMovement(body), () => verticalMovement(body));
                    }
                    if(headStart == true)
                    {
                        CameraSpacePoint headPosition = body.Joints[head].Position;
                        Debug.Log("wird ausgelöst");
                        startYPositionHead = headPosition.Y;
                        Debug.Log(startYPositionHead);
                        headStart = false;
                    }
                });

                frame.Dispose();
            }
        }

        // Reset horizontal velocity when neither left nor right key is pressed
        if ((!Input.GetKey("left") && !Input.GetKey("right")) || (!isMovingRight && !isMovingLeft))
        {
            rb.velocity = new Vector3(rb.velocity.x * 0.9f, verticalVelocity, VHmovementSpeed);
        }

       // rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, VHmovementSpeed);
        
        if((Input.GetKey("left") || isMovingLeft) && GameOverManager.gameOver == false)
        {
            Debug.Log("Moving Left");
            GoLeft();
        }

        if((Input.GetKey("right") || isMovingRight) && GameOverManager.gameOver == false)
        {
            GoRight();
        }

        if ((Input.GetButtonDown("Jump") || isJumping) && IsGrounded() && GameOverManager.gameOver == false)
        {
            Jump();
        }

        if ((Input.GetKey("down") || isDucking) && IsGrounded() && !slideCooldown && GameOverManager.gameOver == false)
        {
            Slide();
        }
    }
    
    void GoLeft()
    {
        rb.velocity = new Vector3(LRmovementSpeed * -1, rb.velocity.y, rb.velocity.z);
        /*
        if (IsGrounded())
        {
            playerObject.GetComponent<Animator>().Play("Right Strafe");     //Adds animation for moving right
        }
        */
        
    }

    void GoRight()
    {
        rb.velocity = new Vector3(LRmovementSpeed, rb.velocity.y, rb.velocity.z);
        /*
        if (IsGrounded())
        {
            playerObject.GetComponent<Animator>().Play("Left Strafe");        //Adds animation for moving right
        }
        */
        
    }
    void Jump()
    {
        FindObjectOfType<SoundManager>().PlaySound("JumpSFX");
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        //playerObject.GetComponent<Animator>().Play("Jump");
        playerObject.GetComponent<Animator>().Play("Jump_start");

        if (capsuleCollider != null)
        {
            // Set the new height and center
            capsuleCollider.height = originalHeight;        //new height
            capsuleCollider.center = new Vector3(-0.0025f, -0.05f, 0f);     //new center

            // Start a coroutine to revert the changes after the specified duration
            StartCoroutine(RevertColliderProperties(1.1f));  
        }
    }
    

    void Slide()
    {
        //FindObjectOfType<SoundManager>().PlaySound("JumpSFX");
        //rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        //playerObject.GetComponent<Animator>().Play("Jump");
        playerObject.GetComponent<Animator>().Play("Flip");

        if (capsuleCollider != null)
        {
            slideCooldown = true;
            // Set the new height and center
            capsuleCollider.height = 1.0f;        //new height
            capsuleCollider.center = new Vector3(-0.0025f, -0.5f, -0.05f);     //new center

            // Start a coroutine to revert the changes after the specified duration
            
            StartCoroutine(RevertColliderProperties(0.75f)); 
            
                  
        }
    }

    private IEnumerator RevertColliderProperties(float duration)
    {
        
        
        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        slideCooldown = false;
        
        capsuleCollider.height = originalHeight;
        capsuleCollider.center = originalCenter;


        
        
    }


    bool IsGrounded()
    {
       return Physics.CheckSphere(groundCheck.position, .1f, ground);
    } 


   // Ueberwacht die links und rechts Bewegung
    void horizontalMovement(Body body)
    {
        // Erhalte die Positionen des Gelenkes des Koerpers aus dem Kinect-Body-Objekt
        CameraSpacePoint basePosition = body.Joints[spineBase].Position;
        // aktuelle X-Position des Gelenks
        float currentXPosition = basePosition.X;
        //Debug.Log(currentXPosition);

         // Ueberpruefen ob die Aenderung der X-Position groeser ist als der Schwellenwert fuer seitliche Bewegungen
        if (Mathf.Abs(currentXPosition - _previousXPosition) > MovementThreshold)
        {
            // Falls die X-Position zunimmt, setze den Bewegungsstatus nach rechts.
            if (currentXPosition - _previousXPosition > 0)
            {
                isMovingRight = true;
                isMovingLeft = false;
                Debug.Log("Bewegt sich nach rechts-----");
            }
            // Falls die X-Position abnimmt, setze den Bewegungsstatus nach links.
            else
            {
                isMovingLeft = true;
                isMovingRight = false;
                Debug.Log("-----Bewegt sich nach links");
            }
        }
        // Wenn die Aenderung der X-Position unter dem Schwellenwert liegt, setze beide Bewegungsstatus zurueck.
        else
        {
            isMovingLeft = false;
            isMovingRight = false;
        }

        _previousXPosition = currentXPosition;
    }

    /// Ueberwacht das Springen und Ducken
    void verticalMovement(Body body)
    {
        // Erhalte die Positionen des Gelenks des Koerpers aus dem Kinect-Body-Objekt
        CameraSpacePoint headPosition = body.Joints[head].Position;

        // aktuelle Y-Positionen des Gelenks
        float currentYPositionHead = headPosition.Y;
        
        // Variable fuer die Differenz der Position
        float headChange = currentYPositionHead - startYPositionHead;
        
        // Ueberprueft ob sich die Y-Position des SpineBase-Gelenks oder der Fuesse ausreichend aendert
        if (headChange > JumpThreshold)
        {
            // Hier wird der Sprungstatus geaendert
            isJumping = true;
            isDucking = false;
            Debug.Log("springt");
        }
        // Ueberpruefe, ob sich die Y-Position des Kopfes nach unten aendert
        else if (headChange < BendingThreshold)
        {
            // Hier wird der Ducken-Status geaendert
            isDucking = true;
            isJumping = false;
            Debug.Log("duckt sich");
        }
        else
        {
            // Wenn keine der Bedingungen erfuellt ist, werden beide Status zurueckgesetzt
            isJumping = false;
            isDucking = false;
        }
    }

    // Wird aufgerufen, wenn die Anwendung beendet wird
    void OnApplicationQuit()
    {
        if (_reader != null)
        {
            _reader.Dispose();
            _reader = null;
        }

        if (_sensor != null)
        {
            if (_sensor.IsOpen)
            {
                _sensor.Close();
            }
        }
    }
}