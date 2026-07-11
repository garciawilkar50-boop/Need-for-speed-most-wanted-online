using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileCarInput : MonoBehaviour
{
    [Header("Output")]
    public float steerInput = 0f;
    public float throttleInput = 0f;
    public float brakeInput = 0f;
    public bool handbrake = false;
    public bool nitro = false;

    [Header("Buttons (assign in Inspector)")]
    public Button steerLeftBtn, steerRightBtn;
    public Button accelerateBtn, brakeBtn;
    public Button handbrakeBtn, nitroBtn;

    [Header("Sensitivity & Deadzone")]
    [Tooltip("Multiplier applied to steering input (1 = unchanged)")]
    public float steerSensitivity = 1f;
    [Tooltip("Multiplier applied to throttle input (1 = unchanged)")]
    public float throttleSensitivity = 1f;
    [Tooltip("Multiplier applied to brake input (1 = unchanged)")]
    public float brakeSensitivity = 1f;
    [Tooltip("Inputs whose absolute value is below this are treated as 0")]
    public float deadzone = 0.05f;

    [Header("Haptics")]
    public bool useHaptics = true;
    [Tooltip("Minimum time (seconds) between haptic pulses to avoid spamming")]
    public float hapticCooldown = 0.08f;

    private float lastHapticTime = -10f;

    void Start()
    {
        // Configurar eventos para cada botón. Las lambdas aplican sensibilidad y deadzone.
        AddButtonListeners(steerLeftBtn,  val => steerInput = ApplyDeadzone(-val * steerSensitivity));
        AddButtonListeners(steerRightBtn, val => steerInput = ApplyDeadzone(val * steerSensitivity));
        AddButtonListeners(accelerateBtn, val => throttleInput = ApplyDeadzone(val * throttleSensitivity));
        AddButtonListeners(brakeBtn,     val => brakeInput = ApplyDeadzone(val * brakeSensitivity));

        // Botones que actúan como toggle mientras se mantienen presionados (handbrake, nitro)
        AddToggleListener(handbrakeBtn, val => handbrake = val);
        AddToggleListener(nitroBtn,     val => nitro = val);
    }

    // Aplicar deadzone simple
    float ApplyDeadzone(float value)
    {
        if (Mathf.Abs(value) < deadzone) return 0f;
        return value;
    }

    // Añade listeners que mientras se mantiene presionado llaman setValue(1) y al soltar setValue(0)
    void AddButtonListeners(Button btn, System.Action<float> setValue)
    {
        if (btn == null) return;
        EventTrigger trigger = btn.gameObject.GetComponent<EventTrigger>();
        if (trigger == null) trigger = btn.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry press = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        press.callback.AddListener((data) => { setValue(1f); TryHaptic(); });
        trigger.triggers.Add(press);

        EventTrigger.Entry release = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        release.callback.AddListener((data) => setValue(0f));
        trigger.triggers.Add(release);

        // También soportar PointerExit (por si arrastran el dedo fuera del botón)
        EventTrigger.Entry exit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        exit.callback.AddListener((data) => setValue(0f));
        trigger.triggers.Add(exit);
    }

    // Botones que cambian estado al presionar y soltar (handbrake, nitro)
    void AddToggleListener(Button btn, System.Action<bool> setState)
    {
        if (btn == null) return;
        EventTrigger trigger = btn.gameObject.GetComponent<EventTrigger>();
        if (trigger == null) trigger = btn.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry press = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        press.callback.AddListener((data) => { setState(true); TryHaptic(); });
        trigger.triggers.Add(press);

        EventTrigger.Entry release = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        release.callback.AddListener((data) => setState(false));
        trigger.triggers.Add(release);

        EventTrigger.Entry exit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        exit.callback.AddListener((data) => setState(false));
        trigger.triggers.Add(exit);
    }

    // Intenta disparar una vibración si ha pasado suficiente tiempo
    void TryHaptic()
    {
        if (!useHaptics) return;
        if (Time.unscaledTime - lastHapticTime < hapticCooldown) return;

        // Método simple compatible con la mayoría de plataformas Unity: Handheld.Vibrate()
        // Para control avanzado de duración/intensidad se necesitaría plugin nativo.
        try
        {
            Handheld.Vibrate();
        }
        catch
        {
            // Ignorar si la plataforma no soporta vibración en este contexto
        }

        lastHapticTime = Time.unscaledTime;
    }
}
