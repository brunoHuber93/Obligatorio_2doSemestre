using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP2_C_
{
    public class Agenda : IComparable<Agenda>
    {
        private int id; public int Id { get => id; set => id = value; }
        private Huesped huesped; public Huesped _huesped { get => huesped; set => huesped = value; }     

        private Actividad actividad; public Actividad _actividad { get => actividad; set => actividad = value; }

        private EstadoAgenda estado; public EstadoAgenda Estado { get => estado; set => estado = value;}
        private Proveedor proveedor; public Proveedor Proveedor { get => proveedor; set => proveedor = value; }

        private static int idCreciente = 0; 



        public Agenda(Huesped huesped, Actividad actividad)
        {
            this.id = idCreciente++;
            this.huesped = huesped; 
            this.actividad = actividad;            
            AsignarEstadoAgenda();

        }    

        


        public void AsignarEstadoAgenda()
        {
            if (actividad.CostoDeActividad > 0)
            {
                Estado = EstadoAgenda.PENDIENTE_PAGO;
            }
            else
            {
                Estado = EstadoAgenda.CONFIRMADA;
            }
        }

        public int CompareTo(Agenda? other)
        {
            return actividad.Fecha.CompareTo(other.actividad.Fecha);
        }
       
    }
}
