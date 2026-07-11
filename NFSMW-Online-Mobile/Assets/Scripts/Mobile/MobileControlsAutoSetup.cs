using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Helper script to auto-configure MobileControls children as UI Buttons with EventTriggers.
// This runs in Edit Mode so you can prepare the prefab without manual steps.
#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class MobileControlsAutoSetup : MonoBehaviour
{
    // Names of expected child buttons
    private readonly string[] buttonNames = new[] {
        "SteerLeft",
        "SteerRight",
        "Accelerate",
        "Brake",
        "Handbrake",
        "Nitro"
    };

    void Awake()
    {
#if UNITY_EDITOR
        // Only run automatically in editor to setup prefab children
        SetupChildren();
#endif
    }

    public void SetupChildren()
    {
        foreach (var name in buttonNames)
        {
            Transform child = transform.Find(name);
            if (child == null)
            {
                var go = new GameObject(name);
                go.transform.SetParent(transform, false);
                child = go.transform;

                // Basic RectTransform settings for UI
                var rect = go.AddComponent<RectTransform>();
                rect.sizeDelta = new Vector2(120, 120);
            }

            // Ensure CanvasRenderer
            if (child.GetComponent<CanvasRenderer>() == null)
                child.gameObject.AddComponent<CanvasRenderer>();

            // Ensure Image component (visual)
            var img = child.GetComponent<Image>();
            if (img == null)
            {
                img = child.gameObject.AddComponent<Image>();
                img.color = new Color(1f, 1f, 1f, 0.5f); // translucent by default
            }

            // Ensure Button component
            var btn = child.GetComponent<Button>();
            if (btn == null)
                btn = child.gameObject.AddComponent<Button>();

            // Ensure EventTrigger component (for pointer down/up)
            var trigger = child.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = child.gameObject.AddComponent<EventTrigger>();

            // Add pointer down/up/exit entries if not present
            EnsureEventEntry(trigger, EventTriggerType.PointerDown);
            EnsureEventEntry(trigger, EventTriggerType.PointerUp);
            EnsureEventEntry(trigger, EventTriggerType.PointerExit);
        }

        UnityEditor.EditorUtility.SetDirty(this);
    }

    void EnsureEventEntry(EventTrigger trigger, EventTriggerType type)
    {
        foreach (var entry in trigger.triggers)
        {
            if (entry.eventID == type) return;
        }
        var e = new EventTrigger.Entry { eventID = type };
        e.callback = new EventTrigger.TriggerEvent();
        trigger.triggers.Add(e);
    }
}
