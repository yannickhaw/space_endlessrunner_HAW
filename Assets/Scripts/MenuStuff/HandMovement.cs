using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Windows.Kinect;
using System.Collections.Generic;

public class KinectHandMovement : MonoBehaviour
{
    public RawImage cursor;         // Referenz auf das RawImage-Objekt, das den Cursor darstellt (Cursor-png)
    public Button playButton;       // Referenz auf den Play-Button (UI)
    public Button quitButton;       // Referenz auf den Quit-Button (UI)

    private KinectSensor _sensor;   // Referenz auf den Kinect-Sensor
    private BodyFrameReader _reader; // Referenz auf den BodyFrameReader, um die Körperdaten zu lesen

    private float offsetX = 1250f;  // X-Offset zur Positionierung des Cursors auf dem Bildschirm
    private float offsetY = 259f;   // Y-Offset zur Positionierung des Cursors auf dem Bildschirm
    private float scale = 1000f;    // Skalierungsfaktor zur Anpassung der Handposition auf dem Bildschirm

    void Start()
    {
        //Initialisiert dn Kinect-Sensor
        _sensor = KinectSensor.GetDefault();

        if (_sensor != null)
        {
            _reader = _sensor.BodyFrameSource.OpenReader();
            _sensor.Open();
        }
    }

    void Update()
    {
        if (_sensor != null && _sensor.IsOpen)
        {
            using (BodyFrame bodyFrame = _reader.AcquireLatestFrame())
            {
                if (bodyFrame != null)
                {
                    Body[] bodies = new Body[_sensor.BodyFrameSource.BodyCount];
                    bodyFrame.GetAndRefreshBodyData(bodies);

                    foreach (var body in bodies)
                    {
                        if (body.IsTracked)
                        {
                            // Erhalte die Position der rechten Hand des Benutzers
                            Windows.Kinect.Joint handJoint = body.Joints[JointType.HandRight];
                            CameraSpacePoint handPosition = handJoint.Position;

                            // Konvertiert die 3D-Handposition in die Unity-Koordinaten
                            Vector3 unityHandPosition = new Vector3((handPosition.X * scale + offsetX), (handPosition.Y * scale + offsetY), 0);

                            // Setzt die Cursor-Position direkt auf die Hand-Position
                            cursor.rectTransform.position = unityHandPosition;

                            // Überprüft, ob der Cursor den Collider des PlayButtons berührt
                            if (playButton != null && IsCursorOverButton(unityHandPosition, playButton))
                            {
                                // Klickt den PlayButton
                                ClickPlayButton();
                            }

                            if (quitButton != null && IsCursorOverButton(unityHandPosition, quitButton))
                            {
                                // Klickt den QuitButton
                                ClickQuitButton();
                            }
                        }
                    }
                }
            }
        }
    }

    void OnApplicationQuit()                    // wird ausgeführt, wenn das das Spiel geschlossen wird
    {
        if (_sensor != null)                    
        {
            _sensor.Close();                    // schaltet die Kinect aus, wenn sie eingeschaltet ist
        }
    }

    // Überprüft, ob der Cursor den Button berührt
    private bool IsCursorOverButton(Vector3 cursorPosition, Button button)
    {
        // Erzeugt einen Raycast von der Cursor-Position in die Szene
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = cursorPosition;

        // Überprüft, ob ein UI-Element unter dem Cursor liegt
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        // Überprüft, ob der Button unter den Ergebnissen ist
        return results.Exists(result => result.gameObject == button.gameObject);
    }


    private void ClickPlayButton()      // Funktion zum Klicken des PlayButtons
    {
        playButton.onClick.Invoke();
    }

    private void ClickQuitButton()      // Methode zum Klicken des Quit-Buttons
    {
        quitButton.onClick.Invoke();
    }
}
