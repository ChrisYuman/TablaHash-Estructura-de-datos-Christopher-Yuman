using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TablaHash__Estructura_de_datos__Christopher_Yuman
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class Empleado
    {
        public string Nombre { get; set; }
        public string NumeroEmpleado { get; set; }
        public string Departamento { get; set; }

        public Empleado(string nombre, string numeroEmpleado, string departamento)
        {
            Nombre = nombre;
            NumeroEmpleado = EncriptarSHA256(numeroEmpleado); // Encriptar el número de empleado
            Departamento = departamento;
        }

        // Función para encriptar el número de empleado usando SHA256
        private string EncriptarSHA256(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertir el string en un array de bytes y calcular el hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convertir el array de bytes a un string hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Convertir a hexadecimal
                }
                return builder.ToString(); // Retornar el string encriptado
            }
        }
    }
}
