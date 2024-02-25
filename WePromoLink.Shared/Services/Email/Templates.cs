
using Scriban;

namespace WePromoLink.Services.Email;
// Editar plantillas aqui: https://htmlcodeeditor.com/
public static class Templates
{
    const string TEMPLATES_PATH = "Services/Email/Templates/";
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
        var template = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, TEMPLATES_PATH, "Welcome.scriban-html"));
        return Bind(template, model);
    }

    public static string JoinWaitingList(dynamic model)
    {
        var template = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, TEMPLATES_PATH, "JoinWaitingList.scriban-html"));
        return Bind(template, model);
    }

    public static string Deposit(dynamic model)
    {
        var template = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, TEMPLATES_PATH, "DepositCompleted.scriban-html"));
        return Bind(template, model);
    }

    public static string Withdraw(dynamic model)
    {
        var template = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, TEMPLATES_PATH, "WithdrawCompleted.scriban-html"));
        return Bind(template, model);
    }

    public static string GenerateQR(dynamic model)
    {
        return Bind(QRTemplate, model);
    }

    // Templates
    static string QRTemplate = @"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>WePromoLink 2KF QR</title>
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

        .qr {
            display:flex;
            width: 100%;
            justify-content: center;
            items-align: center;
            margin-bottom: 5px;
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
        <h1>Two key Factor Credentials</h1>
        <p class=""message"">Dear Admin,</p>
        <p class=""message"">You have received a QR code (QRK) attached to this email. This code is crucial for setting up 2FA on your account. It adds an extra layer of security by requiring a unique code in addition to your password during login. To complete the setup, we recommend using Google Authenticator, a trusted 2FA app available on both iOS and Android. Your account's security is of utmost importance to us, and we appreciate your cooperation in safeguarding it.</p>
        <p class=""message""><b>Manual code:</b></p>
        <p class=""message"">{{manual_code}}.</p>
        <p class=""message""><b>QR</b></p>
        
        <table width=""100%"">
        <tr>
            <td align=""center"">
                <img src=""{{qr_code_url}}"" alt=""QR"">
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