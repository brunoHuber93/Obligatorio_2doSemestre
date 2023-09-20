using ObligatorioP2_C_;
using System.Runtime.CompilerServices;

public abstract class Actividad : IValidar, IComparable<Actividad>
{
    private int id; public int Id { get => id;}

    private string nombre; public string Nombre { get => nombre; }

    private string descripcion; public string Descripcion { get => descripcion; }

    private DateTime fecha; public DateTime Fecha { get => fecha; }
    private int cantidadMaximaDePersonas; public int CantidadMaximaDePersonas { get => cantidadMaximaDePersonas; set => cantidadMaximaDePersonas = value; }

    private int edadMinimaRequerida; public int EdadMinimaRequerida { get => edadMinimaRequerida; }

    private decimal costoDeActividad; public decimal CostoDeActividad { get => costoDeActividad; }

    private int cantidadCuposDisponibles; public int CantidadCuposDisponibles { get => cantidadCuposDisponibles; set => cantidadCuposDisponibles = value; }



    private static int idCreciente = 0; // genero una variable auxiliar para autogenerar id creciente en cada instancia de Actividad.




    public Actividad(string nombre, string descripcion, DateTime fecha, int cantidadMaximaDePersonas, int edadMinimaRequerida, decimal costoDeActividad)
    {

        
        this.id = idCreciente++;       
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.fecha = fecha;
        this.cantidadMaximaDePersonas = cantidadMaximaDePersonas;
        this.cantidadCuposDisponibles = cantidadMaximaDePersonas;
        this.edadMinimaRequerida = edadMinimaRequerida;
        this.costoDeActividad = costoDeActividad;

    }

    public virtual void ValidarEstado() {
        ValidarNombreActividad();
        ValidarDescripcionActividad();
        //ValidarFechaActividad();
    }
    

    public void ValidarNombreActividad()
        
    {
        if (string.IsNullOrEmpty(Nombre)) {
            throw new Exception("El campo Nombre no puede ser vacío");
            
        }

        if (Nombre.Length > 25)
        {
            Console.WriteLine("El Nombre no puede contener mas de 25 caracteres");
        }         
        
    }

    public void ValidarDescripcionActividad()
    {
        if (string.IsNullOrEmpty(Descripcion))
        {
            throw new Exception("El campo Descripción no puede ser vacío");
        }
    }

    
    public override string ToString()
    {
        return  
                $"Id: {id}\n" +
                $"Nombre: {nombre}\n" +
                $"Descripción: {descripcion}\n" +
                $"Fecha: {fecha.ToString("d")}\n" +
                $"Cantidad máxima de personas: {CantidadMaximaDePersonas}\n" +
                $"Edad minima: {edadMinimaRequerida}\n"+                
                $"----------------------------------\n";
                //$"{costoDeActividad}\n" +
    }

    public int CompareTo(Actividad? other)
    {
        return CostoDeActividad.CompareTo(other.CostoDeActividad) * -1;
    }

    public abstract decimal CostoFinal();

    public string ObtenerCosto()
    {
        if (CostoDeActividad > 0)
        {
            return CostoDeActividad.ToString();
        }
        else
        {
            return "actividad gratuita";
        }
    }



}

