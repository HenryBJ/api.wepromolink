const TelegramBot = require('node-telegram-bot-api');

// Token de acceso del bot (obtenido en el paso 1)
const botToken = '6490698871:AAE7gVse3USKpR0yohcEo7SH7lgTYeOhpsM';

// ID del canal al que deseas enviar el mensaje
const channelId = '@wepromolink';

// Mensaje que deseas enviar
const message = '¡Hi from bot';

// Crea una instancia del bot
const bot = new TelegramBot(botToken, { polling: false });

// Envía el mensaje al canal
bot.sendMessage(channelId, message)
  .then(() => {
    console.log('Mensaje enviado con éxito');
  })
  .catch((error) => {
    console.error('Error al enviar el mensaje:', error);
  });
