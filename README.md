# entra-phishing-detector

A Azure Function which returns an SVG image if the referrer is not login.microsoftonline.com.
When added as background image to Entra ID custom CSS, the image will be loaded by the browser and can show the user a AITM-attack.
