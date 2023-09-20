using ClasesObligatorio;
using ObligatorioP2_C_;

public class ActividadTerciarizada : Actividad
{
	private Proveedor proveedor; public Proveedor _proveedor{ get =>proveedor; } // Utilizo "_" en vez de mayùscula para diferenciarlo de la clase "Proveedor"

	private ConfirmacionDeActividad confirmacionDeActividad; public ConfirmacionDeActividad ConfirmacionDeActividad { get => confirmacionDeActividad; set => confirmacionDeActividad = value; }

    private DateTime? fechaDeConfirmacion; public DateTime? FechaDeConfirmacion { get =>fechaDeConfirmacion; }

    public ActividadTerciarizada( string nombre, string descripcion, DateTime fecha, int cantidadMaximaDePersonas, int edadMinimaRequerida, decimal costoDeActividad, Proveedor proveedor, ConfirmacionDeActividad confirmacionDeActividad, DateTime? fechaDeConfirmacion) : base( nombre, descripcion, fecha, cantidadMaximaDePersonas, edadMinimaRequerida, costoDeActividad)
    {
        this.proveedor = proveedor;
        this.confirmacionDeActividad = confirmacionDeActividad;
        
        if (confirmacionDeActividad == ConfirmacionDeActividad.CONFIRMADA)
        {
            this.fechaDeConfirmacion = fechaDeConfirmacion;
        }
        else
        {
            this.fechaDeConfirmacion = null;
        }

    }

    public override decimal CostoFinal()
    {
        decimal costoFinal = CostoDeActividad;
        if (ConfirmacionDeActividad == 0 && _proveedor != null)
        {
            decimal descuento = proveedor.Descuento;
            costoFinal *= descuento;
        }
        return costoFinal;
    }

    

   

}

