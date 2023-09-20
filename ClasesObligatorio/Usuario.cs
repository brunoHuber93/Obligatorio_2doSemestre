using ObligatorioP2_C_;
using System.ComponentModel.Design;

public abstract class Usuario : IValidar
{
    private string email; public string Email {get=>email;}

	private string contraseña; public string Contraseña {get=> contraseña;}

    public Usuario(string email, string contraseña)
    {
        this.email = email;
        this.contraseña = contraseña;
    }



    public virtual void ValidarEstado()
    {
        ValidarEmail();
        ValidarContraseña();
    }
    public void ValidarEmail()
    {
        if (!(Email.Contains("@") && Email.IndexOf("@") != 0 && Email.IndexOf("@") != Email.Length - 1)) //VALIDO QUE SE INGRESE EL "@" Y NO ESTÉ NI AL PRINCIPIO NI AL FINAL
        {
            throw new Exception("El email debe contener el caracter @ y éste no puede estar ni al principio ni al final");
        }
    }
    public void ValidarContraseña() { 
        if (Contraseña.Length < 8)
        {
            throw new Exception("La contraseña debe tener al menos 8 caracteres");
        }
    }
    
   
    
}

