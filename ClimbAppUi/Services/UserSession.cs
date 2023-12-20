using System;
using System.Collections.Generic;
using System.Text;

public static class UserSession
{
    public static string Token { get; set; }
    public static int UserId { get; set; }

    public static void SetToken(string token)
    {
        Token = token;
        UserId = ExtractUserIdFromToken(token);
    }
    // La función `ExtractUserIdFromToken` toma una cadena `token` como entrada y realiza lo siguiente:
    // - Divide la cadena en partes usando el carácter '_' como separador.
    // - Verifica si la primera parte es un número (representando el `UserId`).
    // - Si es un número válido, lo devuelve; de lo contrario, devuelve 0.
    public static int ExtractUserIdFromToken(string token)
    {
        var parts = token.Split('_');
        if (parts.Length > 0)
        {
            if (int.TryParse(parts[0], out int userId))
            {
                return userId;
            }
        }
        return 0; 
    }

   
}
