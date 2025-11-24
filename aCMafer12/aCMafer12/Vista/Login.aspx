<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppAcmafer.Vista.Login" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Iniciar Sesión - ACMAFER</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet">
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #1a1a1a 0%, #2d2d2d 50%, #404040 100%);
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 20px;
            position: relative;
            overflow: hidden;
        }

            body::before {
                content: '';
                position: absolute;
                top: -50%;
                left: -50%;
                width: 200%;
                height: 200%;
                background: radial-gradient(circle, rgba(217, 119, 54, 0.1) 0%, transparent 50%);
                animation: rotate 30s linear infinite;
            }

        @keyframes rotate {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        .login-container {
            background: white;
            border-radius: 16px;
            box-shadow: 0 20px 60px rgba(0, 0, 0, 0.4);
            overflow: hidden;
            max-width: 900px;
            width: 100%;
            display: grid;
            grid-template-columns: 1fr 1fr;
            position: relative;
            z-index: 1;
        }

        .brand-panel {
            background: linear-gradient(135deg, #d97736 0%, #c2631f 100%);
            padding: 60px 40px;
            color: white;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            text-align: center;
            position: relative;
            overflow: hidden;
        }

            .brand-panel::before {
                content: '';
                position: absolute;
                top: -50%;
                right: -50%;
                width: 200%;
                height: 200%;
                background: radial-gradient(circle, rgba(255, 255, 255, 0.1) 0%, transparent 60%);
            }

        .brand-icon {
            font-size: 80px;
            margin-bottom: 30px;
            text-shadow: 0 4px 15px rgba(0, 0, 0, 0.3);
            animation: float 3s ease-in-out infinite;
        }

        @keyframes float {
            0%, 100% {
                transform: translateY(0);
            }

            50% {
                transform: translateY(-10px);
            }
        }

        .brand-panel h1 {
            font-size: 32px;
            font-weight: 700;
            margin-bottom: 15px;
            text-shadow: 0 2px 10px rgba(0, 0, 0, 0.2);
        }

        .brand-panel p {
            font-size: 16px;
            opacity: 0.95;
            line-height: 1.6;
            max-width: 300px;
        }

        .feature-list {
            margin-top: 40px;
            display: flex;
            flex-direction: column;
            gap: 15px;
            align-items: flex-start;
        }

        .feature-item {
            display: flex;
            align-items: center;
            gap: 12px;
            font-size: 14px;
        }

            .feature-item i {
                font-size: 18px;
                opacity: 0.9;
            }

        .form-panel {
            padding: 60px 50px;
            display: flex;
            flex-direction: column;
            justify-content: center;
        }

        .form-header {
            margin-bottom: 40px;
        }

            .form-header h2 {
                font-size: 28px;
                color: #333;
                margin-bottom: 10px;
                display: flex;
                align-items: center;
                gap: 12px;
            }

                .form-header h2 i {
                    color: #d97736;
                    font-size: 32px;
                }

            .form-header p {
                color: #666;
                font-size: 14px;
            }

        .form-group {
            margin-bottom: 25px;
        }

            .form-group label {
                display: block;
                font-weight: 600;
                color: #555;
                margin-bottom: 8px;
                font-size: 14px;
                display: flex;
                align-items: center;
                gap: 8px;
            }

                .form-group label i {
                    color: #d97736;
                    font-size: 16px;
                }

        .input-wrapper {
            position: relative;
        }

            .input-wrapper input {
                width: 100%;
                padding: 14px 45px 14px 16px;
                border: 2px solid #e0e0e0;
                border-radius: 8px;
                font-size: 15px;
                transition: all 0.3s;
                background-color: #fafafa;
            }

                .input-wrapper input:focus {
                    outline: none;
                    border-color: #d97736;
                    background-color: white;
                    box-shadow: 0 0 0 4px rgba(217, 119, 54, 0.1);
                }

        .input-icon {
            position: absolute;
            right: 15px;
            top: 50%;
            transform: translateY(-50%);
            color: #999;
            font-size: 16px;
            pointer-events: none;
        }

        .remember-forgot {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 30px;
            font-size: 14px;
        }

        .remember-me {
            display: flex;
            align-items: center;
            gap: 8px;
            color: #666;
        }

            .remember-me input[type="checkbox"] {
                width: 18px;
                height: 18px;
                cursor: pointer;
                accent-color: #d97736;
            }

        .forgot-password {
            color: #d97736;
            text-decoration: none;
            font-weight: 600;
            transition: color 0.3s;
        }

            .forgot-password:hover {
                color: #c2631f;
                text-decoration: underline;
            }

        .btn-login {
            width: 100%;
            padding: 16px;
            background: linear-gradient(135deg, #d97736 0%, #c2631f 100%);
            color: white;
            border: none;
            border-radius: 8px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s;
            box-shadow: 0 4px 15px rgba(217, 119, 54, 0.3);
        }

            .btn-login:hover {
                transform: translateY(-2px);
                box-shadow: 0 6px 20px rgba(217, 119, 54, 0.4);
            }

            .btn-login:active {
                transform: translateY(0);
            }

        .divider {
            text-align: center;
            margin: 30px 0;
            position: relative;
            color: #999;
            font-size: 14px;
        }

            .divider::before,
            .divider::after {
                content: '';
                position: absolute;
                top: 50%;
                width: 40%;
                height: 1px;
                background-color: #e0e0e0;
            }

            .divider::before {
                left: 0;
            }

            .divider::after {
                right: 0;
            }

        .register-link {
            text-align: center;
            color: #666;
            font-size: 14px;
        }

            .register-link a {
                color: #d97736;
                font-weight: 600;
                text-decoration: none;
                transition: color 0.3s;
            }

                .register-link a:hover {
                    color: #c2631f;
                    text-decoration: underline;
                }

        .alert {
            padding: 12px 16px;
            border-radius: 8px;
            margin-bottom: 20px;
            display: flex;
            align-items: center;
            gap: 10px;
            font-size: 14px;
        }

        .alert-error {
            background-color: #fff3cd;
            border-left: 4px solid #ffc107;
            color: #856404;
        }

        @media (max-width: 768px) {
            .login-container {
                grid-template-columns: 1fr;
                max-width: 450px;
            }

            .brand-panel {
                padding: 40px 30px;
            }

            .brand-icon {
                font-size: 60px;
            }

            .brand-panel h1 {
                font-size: 24px;
            }

            .feature-list {
                display: none;
            }

            .form-panel {
                padding: 40px 30px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <!-- Panel Izquierdo - Branding -->
            <div class="brand-panel">
                <div class="brand-icon">
                    <i class="fas fa-industry"></i>
                </div>
                <h1>ACMAFER</h1>
                <p>Sistema de Gestión de Fundición Industrial</p>

                <div class="feature-list">
                    <div class="feature-item">
                        <i class="fas fa-check-circle"></i>
                        <span>Control de Producción</span>
                    </div>
                    <div class="feature-item">
                        <i class="fas fa-check-circle"></i>
                        <span>Gestión de Inventario</span>
                    </div>
                    <div class="feature-item">
                        <i class="fas fa-check-circle"></i>
                        <span>Reportes en Tiempo Real</span>
                    </div>
                </div>
            </div>

            <!-- Panel Derecho - Formulario -->
            <div class="form-panel">
                <div class="form-header">
                    <h2>
                        <i class="fas fa-user-circle"></i>
                        Iniciar Sesión
                    </h2>
                    <p>Ingresa tus credenciales para acceder al sistema</p>
                </div>

                <!-- Mensaje de error -->
                <asp:Panel ID="pnlError" runat="server" CssClass="alert alert-error" Visible="false">
                    <i class="fas fa-exclamation-triangle"></i>
                    <asp:Label ID="lblError" runat="server"></asp:Label>
                </asp:Panel>

                <div class="form-group">
                    <label>
                        <i class="fas fa-envelope"></i>
                        Usuario / Correo Electrónico
                    </label>
                    <div class="input-wrapper">
                        <asp:TextBox
                            ID="txtUsuario"
                            runat="server"
                            placeholder="laura.hernandez@cliente.com">
                        </asp:TextBox>
                        <i class="fas fa-user input-icon"></i>
                    </div>
                </div>

                <div class="form-group">
                    <label>
                        <i class="fas fa-lock"></i>
                        Contraseña
                    </label>
                    <div class="input-wrapper">
                        <asp:TextBox
                            ID="txtPassword"
                            runat="server"
                            TextMode="Password"
                            placeholder="Digite contraseña">
                        </asp:TextBox>
                        <i class="fas fa-key input-icon"></i>
                    </div>
                </div>

                <div class="remember-forgot">
                    <label class="remember-me">
                        <asp:CheckBox ID="chkRecordar" runat="server" />
                        <span>Recordarme</span>
                    </label>
                    <a href="#" class="forgot-password">¿Olvidaste tu contraseña?</a>
                   
                </div>

                <asp:Button
                    ID="btnIngresar"
                    runat="server"
                    Text="🔓 Ingresar al Sistema"
                    CssClass="btn-login"
                    OnClick="BtnIngresar_Click" />

                <div class="divider">O</div>

                <div class="register-link">
                    ¿No tienes una cuenta? <a href="#">Solicitar acceso</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
