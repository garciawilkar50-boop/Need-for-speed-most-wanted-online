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

Notas:
- El rendimiento en móvil puede requerir optimizaciones adicionales (LOD, sombreadores móviles, etc.).
- El chat de voz (Photon Voice) necesita permisos de micrófono, que se deben solicitar en tiempo de ejecución en Android 6+.
- El prefab `MobileControls` es un placeholder — reemplázalo por el prefab real desde Unity si tienes uno.
