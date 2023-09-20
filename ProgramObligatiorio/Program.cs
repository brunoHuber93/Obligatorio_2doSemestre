
using ObligatorioP2_C_;

namespace ProgramObligatiorio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hostal hostal = Hostal.Instancia;


            string opcion = "";
            do
            {
                try
                {
                    ImprimirMenuPrincipal();
                    IngresarValor("Ingrese una opción... ", out opcion);

                    switch (opcion)
                    {
                        case "1":
                            ListarActividades();                            
                            break;
                        case "2":
                            ListarProveedoresAlfabeticamente();
                            break;
                        case "3":
                            ListarActividadesXCostoYFecha();
                            break;
                        case "4":
                            EstablecerDescuentoProveedor();
                            break;
                        case "5":
                            AltaHuesped();
                            break;
                        case "X":
                        case "x":
                            break;
                        default:
                            Console.WriteLine("Opción inválida.");
                            break;
                    }
                }
                catch (Exception ex)

                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
            } while (opcion != "X" && opcion != "x");
        }



        private static void ImprimirMenuPrincipal()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("****  Gestión Hostal ****");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1.Lista Actividades");
            Console.WriteLine("2.Listar Proveedores alfabeticamente");
            Console.WriteLine("3.Filtrar Actividades por costo y fecha");
            Console.WriteLine("4.Establecer descuento proveedor.");
            Console.WriteLine("5.Dar alta a huésped.");
            Console.WriteLine("X.Para salir.");
        }



        //--- Opción 1 del Menú ------------------------------------------------
        private static void ListarActividades()
        {
            var listaActividades = Hostal.Instancia.ObtenerActividades();

            foreach (var actividad in listaActividades)
            {
                Console.WriteLine(actividad);
            }
            Console.ReadLine();
        }

        private static void ListarUsuarios()
        {
            var listaUsuarios = Hostal.Instancia.ObtenerUsuarios();

            foreach (var usuario in listaUsuarios)
            {
                Console.WriteLine(usuario);
            }
            Console.ReadLine();
        }

        private static void ListarProveedoresAlfabeticamente()
        {

            Console.WriteLine(Hostal.Instancia.ListarProveedoresAlfabeticamente());
            Console.ReadLine();
        }


        private static void ListarActividadesXCostoYFecha()
        {
            IngresarValorNumericoTipoFecha("Ingrese la fecha inicial: ", out DateTime fechaInicial);
            IngresarValorNumericoTipoFecha("Ingrese la fecha final: ", out DateTime fechaFinal);
            IngresarValorNumericoDecimal("Ingrese el costo: ", out decimal costo);

            Console.WriteLine(Hostal.Instancia.ListarActividadesXCostoYFecha(costo, fechaInicial, fechaFinal));
            Console.ReadLine();
        }



        private static void AltaHuesped()
        {
          
          
            IngresarValor("Ingrese email: ", out string email);
            IngresarValor("Ingrese contraseña: ", out string contraseña);
            IngresarValorNumericoTipoDocumento("Ingrese el tipo de documento (0) CI, (1) Pasaporte, (2) Otros: ", out TipoDocumento tipoDocumento);
            IngresarValor("Ingrese documento: ", out string numeroDocumento);
            IngresarValor("Ingrese nombre: ", out string nombre);
            IngresarValor("Ingrese apellido: ", out string apellido);
            IngresarValorNumericoEntero("Ingrese habitación: ", out int habitacion);
            IngresarValorNumericoTipoFecha("Ingrese la fecha de nacimiento: ", out DateTime fechaNacimiento);
            IngresarValorNumericoEntero("Ingrese el nivel de fidelización: ", out int nivelFidelizacion);

            Hostal.Instancia.AltaUsuario(new Huesped(email, contraseña, tipoDocumento, numeroDocumento, nombre, apellido, habitacion, fechaNacimiento, nivelFidelizacion));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Huesped ingresado con éxito");
            PausaYRefresar();
        }


        private static void IngresarValor(string mensaje, out string valor)
        {
            bool textoValido = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(mensaje);
                string textoIngresado = Console.ReadLine()!;
                if (string.IsNullOrEmpty(textoIngresado))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El valor ingresado no es correcto, intente nuevamente");
                }
                else
                {
                    textoValido = true;
                }
                valor = textoIngresado!;

            } while (!textoValido);

        } // PARA INGRESAR VALORES TIPO STRING

        private static void IngresarValorNumericoDecimal(string mensaje, out decimal valor)
        {
            bool textoValido = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(mensaje);
                string? textoIngresado = Console.ReadLine();

                if (string.IsNullOrEmpty(textoIngresado))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El valor ingresado no es correcto, intente nuevamente");

                }
                if (decimal.TryParse(textoIngresado, out valor))
                {
                    textoValido = true;

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El valor ingresado no es un entero, intente nuevamente");
                }

            } while (!textoValido);
        } // PARA INGRESAR VALORES DECIMALES

        private static void IngresarValorNumericoEntero(string mensaje, out int valor)
        {
            bool textoValido = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(mensaje);
                string? textoIngresado = Console.ReadLine();

                if (string.IsNullOrEmpty(textoIngresado))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El valor ingresado no es correcto, intente nuevamente");

                }
                if (int.TryParse(textoIngresado, out valor))
                {
                    textoValido = true;

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El valor ingresado no es un entero, intente nuevamente");
                }

            } while (!textoValido);
        }// PARA INGRESAR VALORES ENTEROS

        private static void IngresarValorNumericoTipoFecha(string mensaje, out DateTime valor)
        {
            bool textoValido = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(mensaje);
                string? textoIngresado = Console.ReadLine();
                if (string.IsNullOrEmpty(textoIngresado))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El valor ingresado no es correcto, intente nuevamente");
                }
                if (DateTime.TryParse(textoIngresado, out valor))
                {
                    textoValido = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El formato de fecha debe ser AA/MM/DD, intente nuevamente");
                }

            } while (!textoValido);
        }// PARA INGRESAR VALORES TIPO FECHA

        private static void IngresarValorNumericoTipoDocumento(string mensaje, out TipoDocumento valor)
        {
            bool textoValido = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(mensaje);
                string? textoIngresado = Console.ReadLine();
                if (string.IsNullOrEmpty(textoIngresado))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El valor ingresado no es correcto, intente nuevamente");
                }
                if (Enum.TryParse(textoIngresado, out valor))
                {
                    textoValido = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El valor ingresado no es un entero, intente nuevamente");
                }

            } while (!textoValido);
        }// PARA INGRESAR VALORES TIPO ENUM

        private static void PausaYRefresar()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("presione cualquier tecla para continuar");
            Console.ReadLine();
        }

       

        private static void EstablecerDescuentoProveedor()
        {

            var listaProveedores = Hostal.Instancia.ObtenerProveedores();

            IngresarValor("Ingrese nombre del proveedor: ", out string nombreProveedor);
            Proveedor proveedor = listaProveedores.Find(p => p.NombreProveedor == nombreProveedor); // comparo el atributo NombreProveedor del objeto "p" con el nombreProveedor que me pasan por parametro.

            if (proveedor != null)
            {

                IngresarValorNumericoDecimal("Ingrese el descuento: ", out decimal descuento);
                Hostal.Instancia.PromocionXProveedor(proveedor, descuento);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Descuento asignado con éxito");
            }
            else
            {
                throw new Exception("No existe un proveedor con ese nombre, presione cualquier tecla para continuar");
            }
            PausaYRefresar();

        }

       



    }
}