const axios = require('axios');

// Tu token de acceso de usuario (debe tener permisos para publicar en la página)
const userAccessToken = 'EAAMgj0cyWxMBO4wXT1UeoDZAIdtBWZAKFVA3dA6IZAoXFACG1pFuj3RsZB4tAGQ83dpYxPewTBYbRzgFVNDg8ClLQqy88HrFfiwLGwOmO30rnod9LCrZC88RZCJsHRiOqk26Da7vVZAOt1YSfOmgpWX7ro0NhVxW8FV3PsnG3ObhNmzLAb8tsAMUUVo2YZA86CaI8uxX4MsM7LR43tCnMCYuA1tZBIA3kHnlPysaiyAJkDPhyCWrjdMZAm43NyBSOp52dKETxXIQZDZD';

// ID de tu página de Facebook
const pageId = 'wepromolink';

// Mensaje que deseas publicar
const message = '¡Hi from API!';

// URL del endpoint de la API de Facebook para publicar en la página
const apiUrl = `https://graph.facebook.com/v13.0/${pageId}/feed`;

// Parámetros de la solicitud POST
const postData = {
  message: message,
  access_token: userAccessToken,
};

// Realiza la solicitud POST para publicar en la página
axios
  .post(apiUrl, postData)
  .then((response) => {
    console.log('Publicación exitosa:', response.data);
  })
  .catch((error) => {
    console.error('Error al publicar:', error.response.data);
  });
