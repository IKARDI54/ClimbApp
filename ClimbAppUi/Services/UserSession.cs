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
        return 0; // R
    }

   
}
