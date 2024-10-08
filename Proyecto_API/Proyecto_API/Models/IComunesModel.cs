﻿using System.Security.Claims;

namespace Proyecto_API.Models
{
    public interface IComunesModel
    {
        bool EsAdministrador(ClaimsPrincipal User);

        string GenerarCodigo();

        string Encrypt(string texto);

        void EnviarCorreo(string destino, string asunto, string contenido);
    }
}
