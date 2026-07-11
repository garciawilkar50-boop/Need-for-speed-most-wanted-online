# Need for Speed Most Wanted - Online (NFSMW Online)

Proyecto: Versión multijugador online adaptada para móviles.

## 📱 Versión Móvil

Este proyecto está completamente adaptado para **Android e iOS**.

- **Controles táctiles** con botones virtuales (dirección, acelerar, freno, nitro, handbrake).
- **Soporte multijugador** (Photon) hasta 15 jugadores.
- **Chat de voz** usando el micrófono del dispositivo.
- Menús y personalización 100% táctiles.

Para usar en móvil:
1. Abre la escena `Race` y arrastra el prefab `MobileControls` al Canvas.
2. Asegúrate de que el `CarController` tenga asignado el `MobileCarInput`.
3. Construye para Android/iOS como se indica en la documentación del proyecto.

### Haptics & Sensitivity

Se ha añadido soporte básico de vibración (haptics) y ajustes de sensibilidad para controles táctiles.

- MobileCarInput (componente):
  - `useHaptics` (bool): habilita/deshabilita vibración.
  - `hapticCooldown` (float): intervalo mínimo entre pulsos de vibración (segundos) para evitar spam.
  - `steerSensitivity`, `throttleSensitivity`, `brakeSensitivity` (float): multiplicadores para ajustar la sensibilidad de cada control.
  - `deadzone` (float): valores absolutos por debajo del deadzone se consideran 0.

Recomendación rápida:
- Ajusta `steerSensitivity` para cambiar la respuesta del volante en móviles.
- Ajusta `throttleSensitivity` y `brakeSensitivity` si el coche acelera o frena demasiado rápido con botones.
- Desactiva `useHaptics` si no quieres vibración en pruebas en el Editor (puede comportarse diferente en PC).

Notas:
- La vibración usa `Handheld.Vibrate()` para máxima compatibilidad; para controlar duración/intensidad en Android 8+ o iOS se recomienda integrar un plugin nativo que use VibrationEffect o CoreHaptics.
- El prefab `MobileControls` incluido es un placeholder; crea los botones UI en Unity (SteerLeft, SteerRight, Accelerate, Brake, Handbrake, Nitro) y asigna cada Button al componente MobileCarInput.

Notas de rendimiento:
- El rendimiento en móvil puede requerir optimizaciones adicionales (LOD, sombreadores móviles, etc.).
- El chat de voz (Photon Voice) necesita permisos de micrófono, que se deben solicitar en tiempo de ejecución en Android 6+.

