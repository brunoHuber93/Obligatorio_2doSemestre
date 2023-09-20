using ObligatorioP2_C_;
using System;
using System.Drawing;

public class Huesped : Usuario, IValidar
{
    private TipoDocumento tipoDocumento; public TipoDocumento _tipoDocumento {get => tipoDocumento; } // lo pongo con "_" para diferenciarlo del tipo de dato

	private string numeroDocumento; public string NumeroDocumento { get => numeroDocumento; }

    private string nombre; public string Nombre {get => nombre ;}

	private string apellido; public string Apellido {get => apellido; }

	private int habitacion; public int Habitacion{get => habitacion; }

	private DateTime fechaNacimiento; public DateTime FechaNacimiento { get => fechaNacimiento; }

	private int nivelFidelizacion; public int NivelFidelizacion { get => nivelFidelizacion; }

    public Huesped(string email, string contrase�a, TipoDocumento tipoDocumento, string numeroDocumento, string nombre, string apellido, int habitacion, DateTime fechaNacimiento, int nivelFidelizacion) :base( email, contrase�a)
    {
         this.tipoDocumento= tipoDocumento;
         this.numeroDocumento=numeroDocumento;
         this.nombre=nombre;  
         this.apellido=apellido;    
         this.habitacion=habitacion;
         this.fechaNacimiento = fechaNacimiento;
         this.nivelFidelizacion = nivelFidelizacion;   
    }


   


    public override void ValidarEstado()
    {
        base.ValidarEstado();
        ValidarHabitacion();
        ValidarNumeroDocumento(numeroDocumento);
        ValidarFidelizacion();
    }

    public void ValidarHabitacion()
    {
        if (Habitacion < 0)
        {
            throw new Exception("Se debe ingresar el n�mero de habitaci�n y �ste debe ser mayor que 0");
        }
    }

    public void ValidarNumeroDocumento(string numeroDocumento)
    {
        if (tipoDocumento == TipoDocumento.CI)
        {
            string valor = "1234567890";
            for (int i = 0; i < numeroDocumento.Length; i++)
            {
                if (!valor.Contains(numeroDocumento[i].ToString()))
                {
                    throw new Exception("El n�mero de documento debe contener caracteres num�rico del 0 al 9");
                }
            }
            if (NumeroDocumento.Length != 8)
            {
                throw new Exception("El n�mero de documento debe contener 8 d�gitos, incluido el verificador");
            }            
        }
        
    }

    public void ValidarFidelizacion()
    {
        if (NivelFidelizacion <1 || NivelFidelizacion > 4)
        {
            throw new Exception("El nivel de fidelizaci�n debe ser un valor entre 1 y 4");
        }
    }

   




    //public override string ToString()
    //{
    //    return $"{base.ToString()}\n" +
    //            $"Tipo documento: {_tipoDocumento}\n" +
    //            $"N�mero Documento: {NumeroDocumento}\n" +
    //            $"Nombre: {Nombre}\n" +
    //            $"Apellido: {Apellido}\n" +
    //            $"Habitaci�n: {Habitacion}\n" +
    //            $"Fecha de nacimiento: {FechaNacimiento.ToString("d")}\n" +
    //            $"Nivel de fidelizaci�n: {NivelFidelizacion}\n" +
    //            $"----------------------------------\n";
    //}
}

