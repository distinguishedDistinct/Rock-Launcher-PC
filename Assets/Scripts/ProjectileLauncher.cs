using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    // === Core Launch Settings ===
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public float launchForce = 10f;
    public float minForce = 5f;
    public float maxForce = 20f;

    // === Trajectory Settings (NEW) ===
    [Header("Trajectory Settings")]
    public LineRenderer lineRenderer;
    public int predictionSteps = 30; // Number of points to draw (length/smoothness)
    public float predictionTimeStep = 0.1f; // Time interval between points

    void Update()
    {
        HandleAiming();
        HandleShooting();
        
        // Call the new drawing function every frame
        DrawTrajectory(); 
    }

    void HandleAiming()
    {
        // Get mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Direction from launcher to mouse
        Vector3 direction = mousePos - transform.position;

        // Calculate angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation to launcher
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        // Scroll wheel adjusts force
        float scroll = Input.mouseScrollDelta.y;
        launchForce = Mathf.Clamp(launchForce + scroll, minForce, maxForce);
    }

    void Shoot()
    {
        if (projectilePrefab == null || launchPoint == null) return;

        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Shoot in the direction launcher is facing
        rb.AddForce(transform.right * launchForce, ForceMode2D.Impulse);
    }

    // === Trajectory Prediction Logic (NEW) ===
    void DrawTrajectory()
    {
        // Safety check: Don't run if LineRenderer isn't assigned
        if (lineRenderer == null) return;

        // Set the number of points for the LineRenderer
        lineRenderer.positionCount = predictionSteps; 
        
        // P0 (Start Position) and V0 (Start Velocity)
        Vector2 startVelocity = transform.right * launchForce;
        Vector2 startPosition = launchPoint.position;
        
        // Get the gravity setting from Unity's Physics 2D settings
        Vector2 gravity = Physics2D.gravity;

        for (int i = 0; i < predictionSteps; i++)
        {
            // Calculate time 't' for the current point
            float t = i * predictionTimeStep;

            // Kinematic equation for position: P(t) = P0 + V0*t + 0.5*g*t^2
            Vector2 position = startPosition + startVelocity * t + 0.5f * gravity * t * t;
            
            // Set the point's position
            lineRenderer.SetPosition(i, position);
        }
    }
}