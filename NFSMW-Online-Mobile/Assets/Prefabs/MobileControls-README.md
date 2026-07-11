# MobileControls prefab usage

El prefab `MobileControls` incluye los GameObjects hijos:
- SteerLeft
- SteerRight
- Accelerate
- Brake
- Handbrake
- Nitro

También añadí el script `MobileControlsAutoSetup.cs` que, en el Editor, crea los hijos que falten, añade `RectTransform`, `CanvasRenderer`, `Image`, `Button` y `EventTrigger` con entradas PointerDown/PointerUp/PointerExit.

Cómo usar:
1. En Unity, abre la carpeta `NFSMW-Online-Mobile/Assets/Prefabs` y arrastra `MobileControls.prefab` al Canvas.
2. Selecciona el prefab instanciado y, en la ventana del Inspector, haz clic en "Add Component" y añade el script `MobileControlsAutoSetup` (si no aparece automáticamente).
3. Con el objeto seleccionado, en el menú del script ejecuta `SetupChildren` (el script corre en modo edición y también en Awake en Editor) para asegurar que los botones y EventTriggers se creen.
4. Finalmente, asigna cada uno de los botones al componente `MobileCarInput` en el Inspector (arrastrando los hijos correspondientes).

Si quieres, puedo también:
- Subir sprites por defecto para los botones (flechas / pedales).
- Ajustar posiciones y tamaños para diferentes resoluciones.
