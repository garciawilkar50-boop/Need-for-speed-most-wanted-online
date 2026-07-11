using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileCarInput : MonoBehaviour
{
    public float steerInput = 0f;
    public float throttleInput = 0f;
    public float brakeInput = 0f;
    public bool handbrake = false;
    public bool nitro = false;

    // Referencias a los botones (asignar desde el Inspector)
    public Button steerLeftBtn, steerRightBtn;
    public Button accelerateBtn, brakeBtn;
    public Button handbrakeBtn, nitroBtn;

    void Start()
    {
        // Configurar eventos para cada botón
        AddButtonListeners(steerLeftBtn,  val => steerInput = -val);
        AddButtonListeners(steerRightBtn, val => steerInput =  val);
        AddButtonListeners(accelerateBtn, val => throttleInput = val);
        AddButtonListeners(brakeBtn,     val => brakeInput = val);

        // Botones que son de un toque (handbrake, nitro) 
        // pueden usar onClick normal o mantener mientras se presiona
        AddToggleListener(handbrakeBtn, val => handbrake = val);
        AddToggleListener(nitroBtn,     val => nitro = val);
    }

    // Listener que mientras se mantiene presionado devuelve 1, al soltar 0
    void AddButtonListeners(Button btn, System.Action<float> setValue)
    {
        if (btn == null) return;
        EventTrigger trigger = btn.gameObject.GetComponent<EventTrigger>();
        if (trigger == null) trigger = btn.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry press = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        press.callback.AddListener((data) => setValue(1f));
        trigger.triggers.Add(press);

        EventTrigger.Entry release = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        release.callback.AddListener((data) => setValue(0f));
        trigger.triggers.Add(release);
    }

    // Botones que cambian estado al presionar y soltar (handbrake, nitro)
    void AddToggleListener(Button btn, System.Action<bool> setState)
    {
        if (btn == null) return;
        EventTrigger trigger = btn.gameObject.GetComponent<EventTrigger>();
        if (trigger == null) trigger = btn.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry press = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        press.callback.AddListener((data) => setState(true));
        trigger.triggers.Add(press);

        EventTrigger.Entry release = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        release.callback.AddListener((data) => setState(false));
        trigger.triggers.Add(release);
    }
}
