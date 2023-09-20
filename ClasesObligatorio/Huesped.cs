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

    public Huesped(string email, string contraseña, TipoDocumento tipoDocumento, string numeroDocumento, string nombre, string apellido, int habitacion, DateTime fechaNacimiento, int nivelFidelizacion) :base( email, contraseña)
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
            throw new Exception("Se debe ingresar el número de habitación y éste debe ser mayor que 0");
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
                    throw new Exception("El número de documento debe contener caracteres numérico del 0 al 9");
                }
            }
            if (NumeroDocumento.Length != 8)
            {
                throw new Exception("El número de documento debe contener 8 dígitos, incluido el verificador");
            }            
        }
        
    }

    public void ValidarFidelizacion()
    {
        if (NivelFidelizacion <1 || NivelFidelizacion > 4)
        {
            throw new Exception("El nivel de fidelización debe ser un valor entre 1 y 4");
        }
    }

   




    //public override string ToString()
    //{
    //    return $"{base.ToString()}\n" +
    //            $"Tipo documento: {_tipoDocumento}\n" +
    //            $"Número Documento: {NumeroDocumento}\n" +
    //            $"Nombre: {Nombre}\n" +
    //            $"Apellido: {Apellido}\n" +
    //            $"Habitación: {Habitacion}\n" +
    //            $"Fecha de nacimiento: {FechaNacimiento.ToString("d")}\n" +
    //            $"Nivel de fidelización: {NivelFidelizacion}\n" +
    //            $"----------------------------------\n";
    //}
}

