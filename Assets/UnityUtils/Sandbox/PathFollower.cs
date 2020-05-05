using UnityEngine;
/// <summary>
/// An object that follows a specific path.
/// </summary>
public class PathFollower : MonoBehaviour
{

    public void Update()
    {
        // Set the position of this path follower so it gradually moves towards the desired point.
        transform.position = Vector3.Lerp(transform.position, pathToFollow.GetIthNode(currentPoint).position, Time.deltaTime * speed);

        // Consider this object to have arrived at its next point when it has arrived within a specific distance threshold (i.e. within REACH_DISTANCE_THRESHOLD).
        float dist = Vector3.Distance(pathToFollow.GetIthNode(currentPoint).position, transform.position);
        if (dist <= REACH_DISTANCE_THRESHOLD)
        {
            currentPoint++;
        }

        // If the follower has iterated over all of its points, go back to the first point.
        if (currentPoint >= pathToFollow.GetLength())
        {
            currentPoint = 0;
        }
    }


    /// <summary>
    /// The path that this follower is currently following.
    /// </summary>
    public MapPath pathToFollow;

    /// <summary>
    /// The speed at which this follower moves.
    /// </summary>
    public float speed = 5f;

    /// <summary>
    /// The distance within which this follower has to be in order to consider to have reached one of its target.
    /// </summary>
    public static float REACH_DISTANCE_THRESHOLD = 1f;

    /// <summary>
    /// The index of the point of the path this follower is currently on.
    /// </summary>
    public int currentPoint = 0;

    public void OnDrawGizmos()
    {
        // Do nothing for zero-length paths.
        if (pathToFollow.GetLength() <= 0)
        {
            return;
        }

        // Otherwise, draw each of the path's nodes.
        for (int i = 0; i < pathToFollow.GetLength(); i++)
        {
            if (pathToFollow.GetIthNode(i) != null)
            {
                Gizmos.DrawSphere(pathToFollow.GetIthNode(i).position, REACH_DISTANCE_THRESHOLD);
            }
        }
    }

}