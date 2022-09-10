using BeardPhantom.UnityExtended;
using UnityEngine;

public class DampenedValueTest : MonoBehaviour
{
    #region Properties

    [field: SerializeField]
    public DampenedFloat Float { get; private set; }

    [field: SerializeField]
    public DampenedDouble Double { get; private set; }

    [field: SerializeField]
    public DampenedVector2 Vector2 { get; private set; }

    [field: SerializeField]
    public DampenedVector3 Vector3 { get; private set; }

    #endregion

    #region Methods

    private void Awake()
    {
        InvokeRepeating(nameof(ChangeValues), 0f, 3f);
    }

    private void ChangeValues()
    {
        Float.TargetValue = Random.Range(-100f, 100f);
        Double.TargetValue = Random.Range(-100.0f, 100.0f);
        Vector2.TargetValue = Random.insideUnitCircle * 100f;
        Vector3.TargetValue = Random.insideUnitSphere * 100f;
    }

    private void Update()
    {
        Float.Simulate();
        Double.Simulate();
        Vector2.Simulate();
        Vector3.Simulate();
    }

    #endregion
}