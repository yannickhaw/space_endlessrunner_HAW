using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Windows.Kinect;
using System.Collections.Generic;

public class KinectHandMovement : MonoBehaviour
{
    public RawImage cursor;
    public Button playButton; // Reference to your PlayButton
    public Button quitButton;

    private KinectSensor _sensor;
    private BodyFrameReader _reader;

    private float offsetX = 1250f;
    private float offsetY = 259f;
    private float scale = 1000f;

    void Start()
    {
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
                            Windows.Kinect.Joint handJoint = body.Joints[JointType.HandRight];
                            CameraSpacePoint handPosition = handJoint.Position;

                            Vector3 unityHandPosition = new Vector3((handPosition.X * scale + offsetX), (handPosition.Y * scale + offsetY), 0);

                            // Setze die Cursor-Position direkt auf die Hand-Position
                            cursor.rectTransform.position = unityHandPosition;

                            // Überprüfe, ob der Cursor den Collider des PlayButtons berührt
                            if (playButton != null && IsCursorOverButton(unityHandPosition, playButton))
                            {
                                // Klicke den PlayButton
                                ClickPlayButton();
                            }

                            if (quitButton != null && IsCursorOverButton(unityHandPosition, quitButton))
                            {
                                // Klicke den QuitButton
                                ClickQuitButton();
                            }
                        }
                    }
                }
            }
        }
    }

    void OnApplicationQuit()
    {
        if (_sensor != null)
        {
            _sensor.Close();
        }
    }

    // Überprüfe, ob der Cursor den Button berührt
    private bool IsCursorOverButton(Vector3 cursorPosition, Button button)
    {
        // Erzeuge einen Raycast von der Cursor-Position in die Szene
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = cursorPosition;

        // Überprüfe, ob ein UI-Element unter dem Cursor liegt
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        // Überprüfe, ob der Button unter den Ergebnissen ist
        return results.Exists(result => result.gameObject == button.gameObject);
    }

    // Funktion zum Klicken des PlayButtons
    private void ClickPlayButton()
    {
        playButton.onClick.Invoke();
    }

    private void ClickQuitButton()
    {
        quitButton.onClick.Invoke();
    }
}
