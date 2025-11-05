# ASP.NET MVC + PayPal Checkout SDK Integration

Proyecto de ejemplo que implementa **pagos con PayPal** usando el **SDK oficial de PayPal (`PayPalCheckoutSdk`)** en un entorno **ASP.NET MVC (.NET 9)**.

Este proyecto muestra el flujo completo de pago con PayPal:

1. Crear una orden de pago (`CreateOrder`)
2. Aprobar la orden desde el frontend con el SDK de PayPal (botón)
3. Capturar el pago (`CaptureOrder`)
4. Consultar detalles de una orden (`GetOrder`)

---

## Requisitos previos

- [.NET SDK 9](https://dotnet.microsoft.com/download/dotnet)
- Visual Studio 2022 o VS Code
- Cuenta de [PayPal Developer](https://developer.paypal.com/)
- Claves **Client ID** y **Secret** del entorno **Sandbox**

---

## Configuración inicial

Abre el archivo **`appsettings.json`** y localiza la sección `PayPal`:

```json
"PayPal": {
  "ClientId": "TU_CLIENT_ID_AQUI",
  "Secret": "TU_SECRET_AQUI",
  "Environment": "sandbox",
  "Currency": "MXN",
  "Locale": "es_MX"
}

