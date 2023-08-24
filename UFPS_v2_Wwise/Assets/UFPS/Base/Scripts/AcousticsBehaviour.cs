using UnityEngine;
using UnityEngine.Serialization;

public class AcousticsBehaviour : MonoBehaviour
{
    private LocationStateManager _lStateManager;
    private CharacterController _charController;
    private bool _isMoving = false;
    private Vector3[] _rayDirections;
    private int _frameCounter = 0;
    
    [SerializeField] private float[] _distances, _ceilingHeights;

    public float averageHeight, averageDistance;
    public float playerVelocityThreshold = 0.1f;
    public int rayCount = 8, rotationSteps;
    public float maxDistance = 50f, maxHeight = 100f, ceilingRayOffset = 2f;
    public int framesPerUpdate = 3;
    public Vector3 spaceDimensions;

    private void Start()
    {
        _lStateManager = GameObject.Find("LocationManager").GetComponent<LocationStateManager>();
        _charController = GetComponentInParent<CharacterController>();
        _rayDirections = CalculateRayDirections();
        _distances = new float[_rayDirections.Length];
        _ceilingHeights = new float[_rayDirections.Length];
    }

    private void Update()
    {
        _frameCounter++;

        if (_frameCounter >= framesPerUpdate)
            return;
        else
        {
            _isMoving = _charController.velocity.magnitude > playerVelocityThreshold;

            if (_isMoving)
                CastAllRays();

            CalculateSpaceDimensions();
            _lStateManager.SetLocationSize(spaceDimensions.x * spaceDimensions.z);
            _lStateManager.SetCeilingHeight(spaceDimensions.y);
            _frameCounter = 0;
        }
    }

    private void CastAllRays()
    {
        for (int i = 0; i < _distances.Length; i++)
        {
            CastDistanceRay(i);
            CastCeilingRay(i);
        }
        averageDistance = CalculateAverage(_distances);
        averageHeight = CalculateAverage(_ceilingHeights);
    }

    private void CastDistanceRay(int index)
    {
        RaycastHit hit;
        Vector3 direction = _rayDirections[index];

        Debug.DrawRay(transform.position, direction * maxDistance, Color.red);

        Ray ray = new Ray(transform.position, direction);

        if (Physics.Raycast(ray, out hit, maxDistance))
            _distances[index] = hit.distance;
        else
            _distances[index] = maxDistance;
    }

    private void CastCeilingRay(int index)
    {
        RaycastHit hit;
        Vector3 endPoint = transform.position + _rayDirections[index] * (_distances[index]/ceilingRayOffset);
        Vector3 ceilingDirection = Vector3.up;

        Ray ceilingRay = new Ray(endPoint, ceilingDirection);

        if (Physics.Raycast(ceilingRay, out hit, maxDistance))
        {
            if (hit.point.y - transform.position.y <= maxHeight)
                _ceilingHeights[index] = hit.point.y;
            else
                _ceilingHeights[index] = transform.position.y + maxHeight;
        }
        else
            _ceilingHeights[index] = transform.position.y + maxHeight;
        
        Debug.DrawRay(endPoint, ceilingDirection * maxDistance, Color.blue);
    }

    private Vector3[] CalculateRayDirections()
    {
        Vector3[] directions = new Vector3[rayCount * rotationSteps];
        float angleIncrement = 360f / rayCount;

        for (int step = 0; step < rotationSteps; step++)
        {
            Quaternion rotation = Quaternion.Euler(0f, step * (360f / rotationSteps), 0f);
            int offset = step * rayCount;

            for (int i = 0; i < rayCount; i++)
            {
                float angle = i * angleIncrement;
                Vector3 direction = rotation * Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
                directions[offset + i] = direction;
            }
        }
        return directions;
    }
    
    private void CalculateSpaceDimensions()
    {
        float minX = Mathf.Infinity;
        float maxX = -Mathf.Infinity;
        float minY = Mathf.Infinity;
        float maxY = -Mathf.Infinity;
        float minZ = Mathf.Infinity;
        float maxZ = -Mathf.Infinity;

        for (int i = 0; i < _distances.Length; i++)
        {
            Vector3 rayDirection = _rayDirections[i] * _distances[i];
            Vector3 rayEndPosition = transform.position + rayDirection;

            minX = Mathf.Min(minX, rayEndPosition.x);
            maxX = Mathf.Max(maxX, rayEndPosition.x);
            minZ = Mathf.Min(minZ, rayEndPosition.z);
            maxZ = Mathf.Max(maxZ, rayEndPosition.z);

            minY = Mathf.Min(minY, _ceilingHeights[i]);
            maxY = Mathf.Max(maxY, _ceilingHeights[i]);
        }

        // Calculate the actual dimensions of the cube
        float width = maxX - minX;
        float height = averageHeight/2; // Use the average height here
        float depth = maxZ - minZ;

        // Calculate the center position of the cube
        Vector3 spaceCenter = new Vector3((minX + maxX) * 0.5f, (minY + maxY) * 0.5f, (minZ + maxZ) * 0.5f);
        spaceDimensions = new Vector3(width, height, depth);

        Color spaceColor = Color.green;
        DrawDebugSpace(spaceCenter, spaceDimensions, spaceColor);
    }

    private void DrawDebugSpace(Vector3 center, Vector3 dimensions, Color color)
    {
        Vector3 halfDimensions = dimensions * 0.5f;

        Vector3 frontTopLeft = center + new Vector3(-halfDimensions.x, halfDimensions.y, -halfDimensions.z);
        Vector3 frontTopRight = center + new Vector3(halfDimensions.x, halfDimensions.y, -halfDimensions.z);
        Vector3 frontBottomLeft = center + new Vector3(-halfDimensions.x, -halfDimensions.y, -halfDimensions.z);
        Vector3 frontBottomRight = center + new Vector3(halfDimensions.x, -halfDimensions.y, -halfDimensions.z);

        Vector3 backTopLeft = center + new Vector3(-halfDimensions.x, halfDimensions.y, halfDimensions.z);
        Vector3 backTopRight = center + new Vector3(halfDimensions.x, halfDimensions.y, halfDimensions.z);
        Vector3 backBottomLeft = center + new Vector3(-halfDimensions.x, -halfDimensions.y, halfDimensions.z);
        Vector3 backBottomRight = center + new Vector3(halfDimensions.x, -halfDimensions.y, halfDimensions.z);

        Debug.DrawLine(frontTopLeft, frontTopRight, color);
        Debug.DrawLine(frontTopRight, frontBottomRight, color);
        Debug.DrawLine(frontBottomRight, frontBottomLeft, color);
        Debug.DrawLine(frontBottomLeft, frontTopLeft, color);

        Debug.DrawLine(backTopLeft, backTopRight, color);
        Debug.DrawLine(backTopRight, backBottomRight, color);
        Debug.DrawLine(backBottomRight, backBottomLeft, color);
        Debug.DrawLine(backBottomLeft, backTopLeft, color);

        Debug.DrawLine(frontTopLeft, backTopLeft, color);
        Debug.DrawLine(frontTopRight, backTopRight, color);
        Debug.DrawLine(frontBottomRight, backBottomRight, color);
        Debug.DrawLine(frontBottomLeft, backBottomLeft, color);
    }

    private float CalculateAverage(float[] array)
    {
        if (array.Length == 0)
            return 0;
        
        float sum = 0f;
        
        for (int i = 0; i < array.Length; i++)
            sum += array[i];

        float average = sum / array.Length;
        return average;
    }
}