using ClasesObligatorio;
using ObligatorioP2_C_;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ActividadHostal : Actividad, IValidar
{
    private string personaResponsable; public string PersonaResponsable { get => personaResponsable; }

    private string lugarEnHostal; public string LugarEnHostal { get => lugarEnHostal; }
    private ActividadAlAireLibre actividadAlAireLibre; public ActividadAlAireLibre ActividadAlAireLibre { get => actividadAlAireLibre; }



    public ActividadHostal( string nombre, string descripcion, DateTime fecha, int cantidadMaximaDePersonas, int edadMinimaRequerida, decimal costoDeActividad, string personaResponsable, string lugarEnHostal, ActividadAlAireLibre actividadAlAireLibre) :base( nombre, descripcion, fecha, cantidadMaximaDePersonas, edadMinimaRequerida, costoDeActividad)
    {
        this.personaResponsable = personaResponsable;
        this.lugarEnHostal = lugarEnHostal;
        this.actividadAlAireLibre = actividadAlAireLibre;
        
    }

   

    public override void ValidarEstado()
    {
        base.ValidarEstado();
        ValidarPersonaResponsable();
    }
    public void ValidarPersonaResponsable()

    {
        if (string.IsNullOrEmpty(PersonaResponsable))
        {
            throw new Exception("Se debe indicar la persona responsable de la actividad");

        }
    }

    public override decimal CostoFinal()
    {
            int nivelFidelizacion = 0;
            decimal costoFinal= CostoDeActividad;
            decimal descuento = 0;
           
                switch (nivelFidelizacion)
                {
                    case 1:
                        // sin descuento
                        break;
                    case 2:
                        descuento = costoFinal * 0.10m; // 10% de descuento
                        costoFinal -= descuento;
                        break;
                    case 3:
                        descuento = costoFinal * 0.15m; // 15% de descuento
                        costoFinal -= descuento;
                        break;
                    case 4:
                        descuento = costoFinal * 0.20m; // 20% de descuento
                        costoFinal -= descuento;
                        break;

                
            }
            return costoFinal;
           
        
    }




    
}
    

