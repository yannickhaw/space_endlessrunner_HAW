using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;               // Windows.Kinect für Kinect-Integration
using System.Threading.Tasks;       // Fuer parallele Verarbeitungsaufgaben

public class PlayerMovement_v2 : MonoBehaviour
{

    Rigidbody rb;                                       // Rigidbody-Komponente des Spielers
    [SerializeField] float LRmovementSpeed = 5f;        // Seitwärtsbewegungsgeschwindigkeit
    [SerializeField] float jumpForce = 5f;              // Sprungkraft

    [SerializeField] Transform groundCheck;             // Referenz um zu prüfen, ob der Spieler auf dem Boden ist
    [SerializeField] LayerMask ground;                  // Referenz für Layer (hier: Ground) 
    [SerializeField] GameObject playerObject;           // Referenz für Animation des Spielers

    private CapsuleCollider capsuleCollider;            // Referenz für die HitBox des Spielers (Capsule förmig)
    
    // Original Werte der Spieler Hitbox
    private Vector3 originalCenter;
    private float originalHeight;

    private float playerXcoordinate = 0;    // In-Game X-Koordinate

    private KinectSensor _sensor;           // Kinect-Sensorobjekt
    private BodyFrameReader _reader;        // BodyFrameReader für die Verarbeitung von Körperdaten
    private Body[] _Data;                   // Array zur Speicherung von Körperdaten


    // Variablen fuer die Gelenkpositionen - speichert immer die vorherigen Positionen
    private float _previousXPosition;       // Speichert Vorherige X-Position für Seitwärtsbewegung
    private float startYPositionHead;       // Start Y-Position des Kopfes

    // Schwellen fuer die Positionsaenderung - umso groesser bzw. kleiner diese sind, desto schwieriger
    private float MovementThreshold = 0.05f;    // Schwelle fuer Seitwaertsbewegung
    private float BendingThreshold = 0.75f;     // Schwelle fuer das Ducken
    private float JumpThreshold = 1.075f;       // Schwelle fuer das Erkennen eines Sprungs

    // Zustandsvariablen fuer  Bewegungen (Kinect)
    public static bool isJumping = false;
    public static bool isMovingLeft = false;
    public static bool isMovingRight = false;
    public static bool isDucking = false; 


    public static bool slideCooldown = false;       // Cooldown Variable für das Ducken 

    JointType spineBase = JointType.SpineBase;      // Hueft-/Rumpfgelenk
    JointType head = JointType.Head;                // Kopfgelenk

    bool headStart = true;

    void Start()                                // Start wird beim ersten Frame aufgerufen
    {
        rb = GetComponent<Rigidbody>();                         // Initialisierung des Rigidbody

        capsuleCollider = GetComponent<CapsuleCollider>();       // Initialisierung der Spielr Hitbox

        // Speichern der Originalwerte des Hitbox
        if (capsuleCollider != null)
        {
            originalCenter = capsuleCollider.center;
            originalHeight = capsuleCollider.height;
        }
        else
        {
            Debug.LogError("Capsule Collider component not found on the player GameObject.");
        }


        // Initialisierung des Kinect-Sensors
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
        
        float verticalVelocity = rb.velocity.y;                     // Vertikale Geschwindigkeit des Spielers (Springen)

        if (_reader != null)
        {
            var frame = _reader.AcquireLatestFrame();

            if (frame != null)
            {
                frame.GetAndRefreshBodyData(_Data);


                // Parallelisierung der Verarbeitung der Body-Daten
                Parallel.ForEach(_Data, body =>
                {
                    if (body != null && body.IsTracked)
                    {
                        // Horizontale und vertikale Bewegung parallel verarbeiten
                        Parallel.Invoke(() => horizontalMovement(body), () => verticalMovement(body));
                    }
                });

                frame.Dispose();
            }
        }

       // Zurücksetzen der horizontalen Geschwindigkeit, wenn keine Bewegungstasten gedrückt werden bzw. keine links rechts Bewegung stattfindet
        if ((!Input.GetKey("left") && !Input.GetKey("right")) || (!isMovingRight && !isMovingLeft))
        {
            rb.velocity = new Vector3(rb.velocity.x * 0.9f, verticalVelocity, 0);
        }

       // rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        
        if((Input.GetKey("left") || Input.GetKey("a") || isMovingLeft) && GameOverManager.gameOver == false)
        {
            GoLeft();                                                                                                        // Bewegung nach links
        }

        if((Input.GetKey("right") || Input.GetKey("d") || isMovingRight) && GameOverManager.gameOver == false)
        {
            GoRight();                                                                                                       // Bewegung nach links
        }

        if (((Input.GetButtonDown("Jump") || isJumping) && IsGrounded()) && GameOverManager.gameOver == false)
        {
            Jump();                                                                                                          // Springen
        }

        if (((Input.GetKey("down") || Input.GetKey("s") || isDucking) && IsGrounded()) && !slideCooldown && GameOverManager.gameOver == false)
        {
            Slide();                                                                                                        // Ducken/Rutschen
        }
    }
    
    void GoLeft()                                                                           // Funktion für die Bewegung nach links
    {
        rb.velocity = new Vector3(LRmovementSpeed * -1, rb.velocity.y, rb.velocity.z);
    }

    void GoRight()                                                                          // Funktion für die Bewegung nach rechts
    {
        rb.velocity = new Vector3(LRmovementSpeed, rb.velocity.y, rb.velocity.z);
        
    }
    void Jump()                                                                             // Funktion für den Sprung
    {
        FindObjectOfType<SoundManager>().PlaySound("JumpSFX");                              // Soundeffekt für den Sprung abspielen
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);                 // Anwenden der Sprungkraft
        playerObject.GetComponent<Animator>().Play("Jump_start");                           // Abspielen der Sprunganimation

        if (capsuleCollider != null)
        {
            // Setzen der neuen Höhe und Zentrum für die Spieler Hitbox/collider
            capsuleCollider.height = originalHeight;                        // Neue Höhe
            capsuleCollider.center = new Vector3(-0.0025f, -0.05f, 0f);     // Neues Zentrum

            StartCoroutine(RevertColliderProperties(1.1f));                 // Starten einer Coroutine zum Rückgängigmachen der Änderungen nach einer bestimmten Dauer (nach Ende des 1,1s cooldowns) 
        }
    }
    

    void Slide()                                                                // Funktion für das Rutschen
    {
        playerObject.GetComponent<Animator>().Play("Flip");                     // Abspielen der Slide-Animation

        if (capsuleCollider != null)
        {
            slideCooldown = true;                                               // Slide Cooldown wird aktiviert

            // Setzen der neuen Höhe und Zentrum für die Spieler Hitbox/collider
            capsuleCollider.height = 1.0f;                                      // Neue Höhe
            capsuleCollider.center = new Vector3(-0.0025f, -0.5f, -0.05f);      // Neues Zentrum

            StartCoroutine(RevertColliderProperties(0.75f));                    // Starten einer Coroutine zum Rückgängigmachen der Änderungen nach einer bestimmten Dauer (nach Ende des 0,75s cooldowns)
        }
    }

    private IEnumerator RevertColliderProperties(float duration)                // Coroutine zum Rückgängigmachen der Hitbox-Eigenschaften
    {
        yield return new WaitForSeconds(duration);                              // Warten für die angegebene Dauer

        slideCooldown = false;                                                  // Zurücksetzen des Slide-Cooldowns
        
        // Wiederherstellen der Ursprungswerte der Hitbox
        capsuleCollider.height = originalHeight;                                
        capsuleCollider.center = originalCenter;                                
    }


    bool IsGrounded()                                                           // Überprüfung, ob der Spieler den Boden berührt
    {
       return Physics.CheckSphere(groundCheck.position, .1f, ground);          
    } 


   // Überwacht die links und rechts Bewegung
    void horizontalMovement(Body body)
    {
        // Erhalte die Positionen des Gelenkes des Koerpers aus dem Kinect-Body-Objekt
        CameraSpacePoint basePosition = body.Joints[spineBase].Position;
        // aktuelle X-Position des Gelenks
        float currentXPosition = basePosition.X;

        if (currentXPosition < 0.57f && currentXPosition > -0.57f  && GameOverManager.gameOver == false)
        {
            playerXcoordinate = 3.325f * currentXPosition;        //Seitliche Begrenzung in Unity geht von X = -2 bis X = 2 bzw 1.9 bis -1.9
            
            transform.position = new Vector3(playerXcoordinate, transform.position.y, transform.position.z);
        }

        _previousXPosition = currentXPosition;
    }

    // Überwachung der vertikalen Bewegung (also Spriinge und Ducken) basierend auf den Kinect-Daten
    void verticalMovement(Body body)
    {
        // Erhalte die Positionen des Gelenks des Koerpers aus dem Kinect-Body-Objekt
        CameraSpacePoint headPosition = body.Joints[head].Position;

        // aktuelle Y-Positionen des Gelenks
        float currentYPositionHead = headPosition.Y;
        
        if(headStart == true)
        {
            startYPositionHead = headPosition.Y;                // Speichern der Start-Y-Position für den Kopf
            headStart = false;
        }

        // Berechnung der Änderung der Kopfposition
        float headChange = currentYPositionHead - startYPositionHead;
       
        // Überprüfung auf Sprung oder Ducken basierend auf der Kopfposition
        if (currentYPositionHead > startYPositionHead * JumpThreshold)
        {
            // Hier wird der Sprungstatus geaendert
            isJumping = true;
            isDucking = false;
        }
        else if (currentYPositionHead < startYPositionHead * BendingThreshold)
        {
            // Hier wird der Ducken-Status geaendert
            isDucking = true;
            isJumping = false;
        }
        else
        {
            // Wenn keine der Bedingungen erfuellt ist, werden beide Status zurueckgesetzt
            isJumping = false;
            isDucking = false;
        }
    }

    // Wird aufgerufen, wenn die Anwendung beendet wird (Kinect wird beendet)
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