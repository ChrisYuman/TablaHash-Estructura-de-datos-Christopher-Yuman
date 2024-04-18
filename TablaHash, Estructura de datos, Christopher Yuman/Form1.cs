using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TablaHash__Estructura_de_datos__Christopher_Yuman
{
    public partial class Form1 : Form
    {
        private Hashtable empleados = new Hashtable();
        private Hashtable empleadosPorNumero = new Hashtable();


        public Form1()
        {
            InitializeComponent();
            // Crear las columnas del DataGridView
            Data.Columns.Add("Nombre", "Nombre");
            Data.Columns.Add("NumeroEmpleado", "Número de Empleado");
            Data.Columns.Add("Departamento", "Departamento");
            // Agregar los departamentos al ComboBox
            comboBox1.Items.AddRange(new string[] { "Ventas", "Finanzas", "TI" });


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        // Método para agregar un nuevo empleado
        private void AgregarEmpleado(string nombre, string numeroEmpleado, string departamento)
        {
            // Crear un nuevo objeto Empleado
            Empleado empleado = new Empleado(nombre, numeroEmpleado, departamento);
            empleados.Add(empleado.NumeroEmpleado, empleado);
            empleadosPorNumero.Add(numeroEmpleado, empleado);
        }

        // Método para buscar un empleado por su número de empleado
        private Empleado BuscarEmpleado(string numeroEmpleado)
        {
            // Verificar si el empleado existe
            if (empleadosPorNumero.ContainsKey(numeroEmpleado))
            {
                return (Empleado)empleadosPorNumero[numeroEmpleado]; // Retornar el empleado encontrado
            }
            else
            {
                return null;
            }
        }

        // Método para mostrar la información de todos los empleados en el DataGridView
        private void MostrarEmpleados()
        {
            // Limpiar el DataGridView
            Data.Rows.Clear();
            // Recorrer la lista de empleados
            foreach (DictionaryEntry empleado in empleados)
            {
                Empleado emp = (Empleado)empleado.Value;
                Data.Rows.Add(emp.Nombre, emp.NumeroEmpleado, emp.Departamento); // Agregar el empleado al DataGridView
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Obtener los datos ingresados por el usuario
            string nombre = textBox1.Text;
            // Encriptar el número de empleado
            string numeroEmpleado = textBox2.Text;
            // Obtener el departamento seleccionado
            string departamento = comboBox1.SelectedItem.ToString();

            // Agregar el empleado a la tabla hash
            AgregarEmpleado(nombre, numeroEmpleado, departamento);
            // Mostrar los empleados en el DataGridView
            MostrarEmpleados();
            // Limpiar los campos de texto
            LimpiarTextBox();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtener el número de empleado ingresado por el usuario
            string numeroEmpleado = textBox2.Text;
            // Buscar el empleado en la tabla hash
            Empleado empleado = BuscarEmpleado(numeroEmpleado);
            // Verificar si el empleado fue encontrado
            if (empleado != null)
            {
                // Limpiar el DataGridView
                Data.Rows.Clear();

                // Agregar el empleado encontrado al DataGridView
                Data.Rows.Add(empleado.Nombre, empleado.NumeroEmpleado, empleado.Departamento);

                // Mostrar el nombre y el departamento del empleado en los campos de texto
                textBox1.Text = empleado.Nombre;
                // Seleccionar el departamento del empleado en el ComboBox
                comboBox1.SelectedItem = empleado.Departamento;
                // Mostrar un mensaje de éxito
                MessageBox.Show("Empleado encontrado.");
            }
            // Si el empleado no fue encontrado
            else
            {
                // Mostrar un mensaje de error
                MessageBox.Show("Empleado no encontrado.");
            }
            // Limpiar los campos de texto
            LimpiarTextBox();

        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            MostrarEmpleados();
            LimpiarTextBox();


        }

        private string EncriptarSHA256(string input)
        {
            // Crear una instancia de SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertir el string en un array de bytes y calcular el hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convertir el array de bytes a un string hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    // Convertir a hexadecimal
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString(); // Retornar el string encriptado
            }
        }
        private void LimpiarTextBox()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

    }
}
