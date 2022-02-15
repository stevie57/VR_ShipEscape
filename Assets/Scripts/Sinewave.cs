using UnityEngine;

public class Sinewave : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _myLineRenderer;
    [Header("Smoothness")]
    [SerializeField]
    private int _points;

    [Header("Solution Values")]
    [Range(0.1f, .4f)]
    [SerializeField]
    private float _amplitude = 0.2f;
    private float _maxAmplitude = 0.4f;
    private float _minAmplitude = 0.1f;
    [Range(1f, 1.3f)]
    [SerializeField]
    private float _frequency = 1f;
    private float _minFrequency = 1f;
    private float _maxFrequency = 1.3f;
    [Range(1f, 1.2f)]
    [SerializeField]
    private float _movementSpeed = 1;
    private float _minMovement = 1f;
    private float _maxMovement = 1.2f;

    [Header("Line Graph Length")]
    [SerializeField]
    private Vector2 _graphLimitX = new Vector2(0, 1);

    [Header("World Position")]
    [SerializeField]
    private Transform _offset;

    [Range(0, 2 * Mathf.PI)]
    [SerializeField]
    private float radians;
    
    private void Start()
    {
        _myLineRenderer = GetComponent<LineRenderer>();
    }

    private void Draw()
    {
        float xStart = _graphLimitX.x;
        float Tau = 2 * Mathf.PI;
        float xFinish = _graphLimitX.y;

        _myLineRenderer.positionCount = _points;

        for (int currentPoint = 0; currentPoint < _points; currentPoint++)
        {
            float progress = (float)currentPoint / (_points - 1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = (_amplitude * Mathf.Sin((Tau * _frequency * x) + (Time.timeSinceLevelLoad * _movementSpeed)));
            Vector3 graphResult = new Vector3(x, y, 0);
            graphResult += _offset.position;
            _myLineRenderer.SetPosition(currentPoint, graphResult);
        }
    }

    private void Update()
    {
        Draw();
    }

    public float[] GetValues()
    {
        float[] values = new float[3] { _amplitude, _frequency, _movementSpeed };
        return values;
    }

    public void AdjustAmplitude(float amplitudeAdjustment)
    {
        float newValue = _amplitude + amplitudeAdjustment;
        _amplitude = CheckValueLimits(_minAmplitude, _maxAmplitude, newValue);
    }

    private float CheckValueLimits(float minValue, float MaxValue, float currentValue)
    {
        if(currentValue > MaxValue ) return MaxValue;
        if (currentValue < minValue) return minValue;
        return currentValue;
    }

    public void AdjustFrequency(float frequencyAdjustment)
    {
        float newValue = _frequency + frequencyAdjustment;
        _frequency = CheckValueLimits(_minFrequency, _maxFrequency, newValue);
    }

    public void AdjustMovement(float movementAdjustment)
    {
        float newValue = _movementSpeed + movementAdjustment;
        _movementSpeed = CheckValueLimits(_minMovement, _maxMovement, newValue);
    }
}