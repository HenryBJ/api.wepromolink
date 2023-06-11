
using Scriban;

namespace WePromoLink.Services.Email;
// Editar plantillas aqui: https://htmlcodeeditor.com/
public static class Templates
{

    static string logo1 = "https://wepromolinkstorage.blob.core.windows.net/asset/logo1.png";
    static string logo2 = "https://wepromolinkstorage.blob.core.windows.net/asset/logo2.png";

    private static string Bind(string body, dynamic model)
    {
        var t = Template.Parse(body);
        var result = t.Render(model);
        return result;
    }

    public static string Welcome(dynamic model)
    {
        return Bind(WelcomeTemplate, model);
    }

    // Templates
//     static string WelcomeTemplate = @"
// <!DOCTYPE html>
// <html>
// <head>
//     <meta charset=""UTF-8"">
//     <title>Welcome to WePromoLink</title>
//     <style>
//         body {
//             background-color: #ffab48;
//         }

//         .container {
//             max-width: 600px;
//             margin: 0 auto;
//             padding: 20px;
//             background-color: #ffffff;
//             text-align: center;
//         }

//         .logo {
//             text-align: center;
//             margin-bottom: 20px;
//         }

//         .logo img {
//             max-width: 200px;
//         }

//         .message {
//             margin-bottom: 20px;
//             font-size: 15px;
//         }

//         .signature {
//             font-style: italic;
//             color: #666666;
//         }
//     </style>
// </head>
// <body>
//     <div class=""container"">
//         <div class=""logo"">
//             <img src=""https://wepromolinkstorage.blob.core.windows.net/asset/logo1.png"" alt=""WePromoLink Logo"">
//         </div>
//         <h1>Welcome !!!</h1>
//         <p class=""message"">Dear {{user}},</p>
//         <p class=""message"">Thank you for joining us. We are excited to have you as part of our community.</p>
//         <p class=""message"">Now you can promote your campaigns and monetize by sharing the campaigns of others, easier than ever.</p>
//         <p class=""message"">If you have any questions or need assistance, please don't hesitate to reach out to our support team.</p>
//         <p class=""message"">Once again, welcome aboard!</p>
//         <p class=""signature"">Best regards,</p>
//         <p class=""signature"">The WePromoLink Team</p>
//     </div>
// </body>
// </html>";


// static string WelcomeTemplate = @"
// <!DOCTYPE html>
// <html>
// <head>
//     <meta charset=""UTF-8"">
//     <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css"">

//     <title>Welcome to WePromoLink</title>
//     <style>
//         body {
//             background-color: #ffab48;
//         }

//         .container {
//             text-align:center;
//             max-width: 600px;
//             margin: 0 auto;
//             padding: 20px;
//             background-color: #ffffff;
//         }
      
//         .container h1 {
//             text-align:center;
//             font-size: 22px;
//         }
      
//         .social-icons {
//             display: flex;
//             align-items: center;
//             justify-content:center;
//             gap:30px;
//             font-size:22px;
//             margin-bottom:20px;
//         }
      
//         .social-icons a {
//             color:#ea7227;
//         }
      
//         .logo {
//             text-align: center;
//             margin-bottom: 10px;
//         }

//         .logo img {
//             max-width: 200px;
//         }

//         .message {
//             text-align:left;
//             margin-bottom: 20px;
//             font-size: 15px;
//         }

//         .signature {
//             font-style: italic;
//             color: #666666;
//             text-align: center;
//         }
      
//         .footer {
//             font-style: italic;
//             color: #666666;
//             text-align: center;
//         }
      
//     </style>
// </head>
// <body>
//     <div class=""container"">
//         <div class=""logo"">
//             <img src=""https://wepromolinkstorage.blob.core.windows.net/asset/logo1.png"" alt=""WePromoLink Logo"">
//         </div>
//         <h1>Welcome !!! &#127881;</h1>
//         <p class=""message"">Dear {{user}},</p>
//         <p class=""message"">Thank you for joining us. We are excited to have you as part of our community.</p>
//         <p class=""message"">Now you can promote your campaigns and monetize by sharing the campaigns of others, easier than ever.</p>
//         <p class=""message"">We also invite you to join us on our social networks to stay in touch.</p>
      
//         <div class=""social-icons"">
//             <a href=""https://t.me/wepromolink_support""><i class=""fab fa-telegram""></i></a>
//             <a href=""https://chat.whatsapp.com/Itdy4Fgx9ld3UYClzUCPU6""><i class=""fab fa-whatsapp""></i></a>
//             <a href=""https://twitter.com/WePromoLink""><i class=""fab fa-twitter""></i></a>
//         </div>
        
//         <p class=""signature"">Best regards,</p>
//         <p class=""signature"">The WePromoLink Team</p>
//         <p class=""footer"">WePromoLink &copy; 2023</p>
//     </div>
// </body>
// </html>
// ";

static string WelcomeTemplate = @"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>Welcome to WePromoLink</title>
    <style>
        body {
            background-color: #ffab48;
        }

        .container {
            text-align:center;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #ffffff;
        }
      
        .container h1 {
            text-align:center;
            font-size: 22px;
        }
      
        .social-icons {
            width:100%;
            text-align:center;
            font-size:22px;
            margin-bottom:20px;
        }
      
        .social-icons a {
            color:#ea7227;
        }
      
        .logo {
            text-align: center;
            margin-bottom: 10px;
        }

        .logo img {
            max-width: 200px;
        }

        .message {
            text-align:left;
            margin-bottom: 20px;
            font-size: 15px;
        }

        .signature {
            font-style: italic;
            color: #666666;
            text-align: center;
        }
      
        .footer {
            font-style: italic;
            color: #666666;
            text-align: center;
        }
      
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""logo"">
            <img src=""https://wepromolinkstorage.blob.core.windows.net/asset/logo1.png"" alt=""WePromoLink Logo"">
        </div>
        <h1>Welcome !!! &#127881;</h1>
        <p class=""message"">Dear {{user}},</p>
        <p class=""message"">Thank you for joining us. We are excited to have you as part of our community.</p>
        <p class=""message"">Now you can promote your campaigns and monetize by sharing the campaigns of others, easier than ever.</p>
        <p class=""message"">We also invite you to join us on our social networks to stay in touch.</p>
      
        <table class=""social-icons"">
        <tr>
        <td>
            <a href=""https://t.me/wepromolink_support"">
                <img width=""30px"" height=""30px"" src=""https://wepromolinkstorage.blob.core.windows.net/asset/telegram-3.png"" alt=""telegram"" />
            </a>
        </td>
        <td>
            <a href=""https://chat.whatsapp.com/Itdy4Fgx9ld3UYClzUCPU6"">
                <img width=""30px"" height=""30px"" src=""https://wepromolinkstorage.blob.core.windows.net/asset/whatsapp-3.png"" alt=""whatsapp"" />
            </a>
        </td>
        <td>
            <a href=""https://twitter.com/WePromoLink"">
                <img src=""https://wepromolinkstorage.blob.core.windows.net/asset/twitter-3.png"" alt=""twitter""  width=""30px"" height=""30px"" />
            </a>
        </td>
        </tr>
        </table>
        <p class=""signature"">Best regards,</p>
        <p class=""signature"">The WePromoLink Team</p>
        <p class=""footer"">WePromoLink &copy; 2023</p>
    </div>
</body>
</html>
";




}