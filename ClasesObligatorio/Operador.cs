public class Operador : Usuario
{
    private string nombre; public string Nombre { get => nombre; }
    private string apellido; public string Apellido { get => apellido; }
    private DateTime fechaIngresoTrabajo; public DateTime FechaIngresoTrabajo { get => fechaIngresoTrabajo; }




    public Operador(string email, string contraseņa, string nombre, string apellido, DateTime fechaIngresoTrabajo) : base(email, contraseņa)
    {
        this.nombre = nombre;
        this.apellido = apellido;
        this.fechaIngresoTrabajo = fechaIngresoTrabajo;
    }

    

}

