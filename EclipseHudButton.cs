using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Tombol HUD tekan/lepas (Fire, Aim, Reload, AbilityQ, dst).
// Dipasang otomatis oleh EclipseGameHudBuilder. Memanggil callback(id, true) saat ditekan
// dan callback(id, false) saat dilepas atau jari keluar dari tombol.
[RequireComponent(typeof(CanvasGroup))]
public class EclipseHudButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public string id;
    public Action<string, bool> callback;

    [Tooltip("Skala tombol saat ditekan.")]
    public float pressedScale = 0.92f;
    [Tooltip("Transparansi tombol saat ditekan.")]
    [Range(0f, 1f)] public float pressedAlpha = 0.8f;

    public bool IsPressed { get; private set; }

    CanvasGroup group;
    Vector3 baseScale = Vector3.one;

    void Awake()
    {
        group = GetComponent<CanvasGroup>();
        baseScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData e)
    {
        if (IsPressed) return;
        IsPressed = true;
        Visual(true);
        if (callback != null) callback(id, true);
    }

    public void OnPointerUp(PointerEventData e) { Release(); }

    public void OnPointerExit(PointerEventData e) { Release(); }

    void OnDisable() { Release(); }

    void Release()
    {
        if (!IsPressed) return;
        IsPressed = false;
        Visual(false);
        if (callback != null) callback(id, false);
    }

    void Visual(bool pressed)
    {
        transform.localScale = pressed ? baseScale * pressedScale : baseScale;
        if (group != null) group.alpha = pressed ? pressedAlpha : 1f;
    }
}
