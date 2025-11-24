<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccesoDenegado.aspx.cs" Inherits="AppAcmafer.Vista.AccesoDenegado" %>

<!DOCTYPE html>
<html>
<head>
    <title>Acceso Denegado</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f8d7da;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        .error-container {
            background: white;
            padding: 40px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
            text-align: center;
        }
        h1 {
            color: #721c24;
            margin-bottom: 20px;
        }
        p {
            color: #666;
            margin-bottom: 30px;
        }
        .btn {
            background-color: #d97736;
            color: white;
            padding: 12px 30px;
            border: none;
            border-radius: 4px;
            text-decoration: none;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <div class="error-container">
        <h1>⛔ Acceso Denegado</h1>
        <p>No tienes permisos para acceder a esta página.</p>
        <a href="Login.aspx" class="btn">Volver al Login</a>
    </div>
</body>
</html>