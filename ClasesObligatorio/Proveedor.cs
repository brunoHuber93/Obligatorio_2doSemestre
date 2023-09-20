using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObligatorioP2_C_
{
    public class Proveedor : IValidar, IComparable<Proveedor>, IEquatable<Proveedor>
    {
       
        private string nombreProveedor; public string NombreProveedor { get => nombreProveedor; }

        private string telefono; public string Telefono { get => telefono; }

        private string direccion; public string Direccion { get => direccion; }        

        private decimal descuento; public decimal Descuento { get => descuento; set => descuento = value; }
        

        public Proveedor(string nombreProveedor, string telefono, string direccion, decimal descuento)
        {
           
            this.nombreProveedor = nombreProveedor;
            this.telefono = telefono;
            this.direccion = direccion;
            this.Descuento = descuento;
        }

       
        public void ValidarEstado()
        {   
            ValidarNombreProveedor();   
            ValidarTelefono();
            ValidarDireccion();
        }

        public void ValidarNombreProveedor()
        {
            if (string.IsNullOrEmpty(NombreProveedor))
            {
                throw new Exception("El campo Nombre no puede ser vacío");
            }
        }

        public void ValidarTelefono()
        {
            if (string.IsNullOrEmpty(Telefono))
            {
                throw new Exception("El campo Teléfono no puede ser vacío");
            }
        }

        public void ValidarDireccion()
        {
            if (string.IsNullOrEmpty(Direccion))
            {
                throw new Exception("El campo Dirección no puede ser vacío");
            }
        }

        public override string ToString()
        {
            return $"Nombre: {NombreProveedor}\n" +
                   $"Teléfono: {Telefono}\n" +
                   $"Dirección: {Direccion}\n" +
                   $"Descuento: {Descuento}\n" +
                   $"-----------------------\n";

        }

        public int CompareTo(Proveedor other)
        {
            return NombreProveedor.CompareTo(other.NombreProveedor);
        }



      
        public bool Equals(Proveedor other)
        {
            return other != null && NombreProveedor == other.NombreProveedor;
        }
    }
}
