﻿using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Proyecto_Web.Entities
{
    public class Usuario
    {

        public int Consecutivo { get; set; }
        public string? Identificacion { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Contrasenna { get; set; }
        public string? ContrasennaConfirmar { get; set; }
        public string? Token { get; set; }
        public string? Descripcion { get; set; }
        public string? Estado { get; set; }
        public int IdRol { get; set; }
        public bool EsTemporal { get; set; }
        public DateTime VigenciaTemporal { get; set; }


    }

}