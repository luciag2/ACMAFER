using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace AppAcmafer.Logica
{
    public class EmailService
    {
        public bool EnviarCorreoPedidoCompletado(string numeroPedido, string emailCliente, string nombreCliente, int cantidad, int stockRestante)
        {
            try
            {
                string smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
                string smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
                string smtpPass = ConfigurationManager.AppSettings["SmtpPassword"];

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(smtpUser, "Acmafer");
                    mail.To.Add(emailCliente);
                    mail.Subject = $"✅ Pedido #{numeroPedido} Completado - Acmafer";
                    mail.Body = GenerarCuerpoEmail(numeroPedido, nombreCliente, cantidad, stockRestante);
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtp.Credentials = new NetworkCredential(smtpUser, smtpPass);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al enviar email: " + ex.Message);
                return false;
            }
        }

        private string GenerarCuerpoEmail(string numeroPedido,
                                          string nombreCliente,
                                          int cantidad,
                                          int stockRestante)
        {
            return $@"
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); 
                   color: white; padding: 30px; text-align: center; border-radius: 8px 8px 0 0; }}
        .content {{ background: #f9f9f9; padding: 30px; }}
        .pedido-box {{ background: white; border-left: 4px solid #667eea; 
                       padding: 20px; margin: 20px 0; border-radius: 4px; }}
        .estado {{ color: #28a745; font-weight: bold; font-size: 18px; }}
        .detalles {{ margin: 15px 0; }}
        .footer {{ text-align: center; color: #666; padding: 20px; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🎉 ¡Pedido Completado!</h1>
        </div>
        <div class='content'>
            <p>Hola <strong>{nombreCliente}</strong>,</p>
            <p>Tu pedido ha sido procesado exitosamente.</p>
            
            <div class='pedido-box'>
                <h3>📦 Detalles del Pedido</h3>
                <div class='detalles'>
                    <p><strong>Número de Pedido:</strong> #{numeroPedido}</p>
                    <p><strong>Cantidad:</strong> {cantidad} unidades</p>
                    <p><strong>Fecha:</strong> {DateTime.Now:dd/MM/yyyy HH:mm}</p>
                    <p class='estado'>Estado: ✅ COMPLETADO</p>
                </div>
            </div>
            
            <p>Gracias por tu preferencia. Si tienes alguna pregunta, no dudes en contactarnos.</p>
        </div>
        <div class='footer'>
            <p>Este es un correo automático, por favor no responder.</p>
            <p>© {DateTime.Now.Year} Acmafer - Todos los derechos reservados</p>
        </div>
    </div>
</body>
</html>
            ";
        }
    }
}