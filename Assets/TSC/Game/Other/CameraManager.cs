using Cinemachine;
using UnityEngine;

namespace TSC.Game
{

    /// <summary>
    /// The manager for the camera.
    /// </summary>
    public class CameraManager : MonoBehaviour
    {

        /// <summary>
        /// The camera that this manager is managing.
        /// </summary>
        public Camera managedCamera;

        /// <summary>
        /// The target zoom.
        /// </summary>
        internal float targetZoom;

        /// <summary>
        /// The speed at which the camera pans.
        /// </summary>
        private float speed = 12f;

        /// <summary>
        /// The CinemachineVirtualCamera instance that is confining the position of the camera.
        /// </summary>
        public CinemachineVirtualCamera cinemachineCamera;

        /// <summary>
        /// The speed at which linear interpolation occurs when zooming the camera.
        /// </summary>
        private const float ZOOM_LERP_SPEED = 10;

        /// <summary>
        /// The factor by which we will scale the zoom upon the mouse scrolling.
        /// </summary>
        private const float ZOOM_FACTOR = 3f;

        /// <summary>
        /// Update the camera zoom. 
        /// </summary>
        public void UpdateCameraZoom()
        {
            // Get the scroll input info from the mouse.
            float scrollData = Input.GetAxis("Mouse ScrollWheel");

            // Change the zoom by the amount scrolled.
            targetZoom -= scrollData * ZOOM_FACTOR;

            // Prevent the zoom from zooming in too big or too small.
            this.targetZoom = Mathf.Clamp(this.targetZoom, 2f, 10f);

            // Slowly animate the zoom so that it is a smooth animation.
            this.cinemachineCamera.m_Lens.OrthographicSize = 
                Mathf.Lerp(this.managedCamera.orthographicSize, this.targetZoom,
                Time.deltaTime * ZOOM_LERP_SPEED);
        }

        /// <summary>
        /// Update the panning of the camera.
        /// </summary>
        public void UpdateCameraPan()
        {
            // Update the camera being panned to the right, to the left, downwards and upwards, respectively.
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            }
        }

    }

}