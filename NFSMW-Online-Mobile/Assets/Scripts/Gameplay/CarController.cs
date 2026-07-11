using UnityEngine;

[System.Serializable]
public class CarSetup
{
    public float maxTorque = 500f;
    public float brakeTorque = 3000f;
    public float maxSteerAngle = 30f;
    public float suspensionStiffness = 15000f;
}

public class CarController : MonoBehaviour
{
    public WheelCollider[] driveWheels;
    public WheelCollider[] steerWheels;
    public Transform[] wheelMeshes;
    public CarSetup sportsSetup, offroadSetup;

    // Referencia al input táctil
    public MobileCarInput mobileInput;

    private CarSetup activeSetup;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetCarType(false);

        // Si no se asigna en el inspector, busca en la escena
        if (mobileInput == null)
            mobileInput = FindObjectOfType<MobileCarInput>();
    }

    public void SetCarType(bool offroad)
    {
        activeSetup = offroad ? offroadSetup : sportsSetup;
        foreach (var wc in driveWheels)
        {
            var sf = wc.suspensionSpring;
            sf.spring = activeSetup.suspensionStiffness;
            wc.suspensionSpring = sf;
        }
    }

    void FixedUpdate()
    {
        if (mobileInput == null) return;

        // Obtenemos los valores del input móvil
        float torqueInput = mobileInput.throttleInput - mobileInput.brakeInput;
        float steerInput = mobileInput.steerInput;
        float brakeInput = mobileInput.handbrake ? 1f : 0f;
        // El nitro se puede aplicar como torque extra o multiplicador aparte
        float nitroBoost = mobileInput.nitro ? 1.5f : 1f;

        foreach (var wc in driveWheels)
        {
            wc.motorTorque = torqueInput * activeSetup.maxTorque * nitroBoost;
            wc.brakeTorque = brakeInput * activeSetup.brakeTorque;
        }
        foreach (var wc in steerWheels)
        {
            wc.steerAngle = steerInput * activeSetup.maxSteerAngle;
        }

        // Sincronizar mallas (opcional)
        for (int i = 0; i < wheelMeshes.Length; i++)
        {
            Vector3 pos;
            Quaternion rot;
            driveWheels[i].GetWorldPose(out pos, out rot);
            wheelMeshes[i].position = pos;
            wheelMeshes[i].rotation = rot;
        }
    }
}
