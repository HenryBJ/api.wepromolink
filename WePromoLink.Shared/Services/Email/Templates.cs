
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

    public static string Deposit(dynamic model)
    {
        return Bind(DepositTemplate, model);
    }

    // Templates

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


static string DepositTemplate = @"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>Deposit completed</title>
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
        <h1>Deposit completed</h1>
        <p class=""message"">Dear {{user}},</p>
        <p class=""message"">We are writing to inform you that your deposit has been successfully processed. We are pleased to confirm that an amount of {{amount}} USD has been credited to your account.</p>
        <p class=""message"">This notification serves as confirmation that the transfer has been completed and the funds are now available for your use. We understand the importance of timely and accurate transactions, and we are committed to providing you with exceptional service.</p>
        <p class=""message"">If you have any questions or require further assistance, please do not hesitate to contact our customer support team. We are here to assist you.</p>
      
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