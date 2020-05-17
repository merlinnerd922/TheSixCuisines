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
            this.managedCamera.orthographicSize = Mathf.Lerp(this.managedCamera.orthographicSize, this.targetZoom,
                Time.deltaTime * ZOOM_LERP_SPEED);
        }

    }

}