using ClasesObligatorio;
using ObligatorioP2_C_;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading;

public class Hostal
{

    private static Hostal instancia;

    // Aplico patrón singleton
    public static Hostal Instancia
    {
        get
        {
            if (instancia == null)
            {
                instancia = new Hostal();
                instancia.PrecargaUsuario();
                instancia.PrecargaProveedor();
                instancia.PrecargaActividad();
                //instancia.PrecargaAgenda();


            }
            return instancia;
        }
    }


    //inicio las listas	

    private List<Actividad> actividades;
    private List<Agenda> agendas;
    private List<Usuario> usuarios;
    private List<Proveedor> proveedores;

    private Hostal()
    {
        actividades = new List<Actividad>();
        agendas = new List<Agenda>();
        usuarios = new List<Usuario>();
        proveedores = new List<Proveedor>();
    }

    // ---------------- Metodos para acceder a las listas --------------
    public List<Usuario> ObtenerUsuarios() { return usuarios; }
    public List<Actividad> ObtenerActividades() { return actividades; }

    public List<Agenda> ObtenerAgendas() { return agendas; }

    public List<Proveedor> ObtenerProveedores() { return proveedores; }


    // --- Metodo para dar de alta actividades ---------------------
    public void AltaActividad(Actividad actividad)
    {
        if (actividad != null) // Verifico que la actividad que se quiere ingresar no sea nula
        {
            actividad.CostoFinal();
            actividad.ObtenerCosto();
            actividad.ValidarEstado(); //Valido si se cumple con los requisitos antes de agregar una Actividad
            actividades.Add(actividad);
            actividad.ToString();

        }
        else
        {
            throw new Exception("No existe la actividad");
        }
    }
    public bool ValidarTipoYNumeroDocumento(TipoDocumento tipoDocumento, string numeroDocumento) //metodo para validar que la combinación entre el numero de documento y el tipo de documento del Huesped sea unica en el sistema
    {
        ObtenerUsuarios();
        foreach (Usuario usuario in usuarios) // recorro la lista de usuarios
        {
            if (usuario is Huesped huesped)// Corroboro que el usuario que estoy buscado sea de tipo Huesped
            {
                if (huesped._tipoDocumento == tipoDocumento && huesped.NumeroDocumento == numeroDocumento)
                {
                    return false; // la combinaciòn ya existe en la lista de usuarios
                }
            }
        }
        return true; // se puede agregar el usuario
    }


    // --- Metodo para dar de alta Usuarios ---------------------

    public void AltaUsuario(Usuario usuario)
    {
        ObtenerUsuarios();
        if (usuario != null)
        {

            if (usuario is Huesped huesped)
            {
                if (ValidarTipoYNumeroDocumento(huesped._tipoDocumento, huesped.NumeroDocumento))// Si la validacion devuelve true, se sigue adelante
                {
                    huesped.ValidarEstado(); //Valido si se cumple con los requisitos antes de agregar un Usuario
                    usuarios.Add(usuario);
                    usuario.ToString();
                }
            }

            else if (usuario is Operador operador)
            {
                operador.ValidarEstado();
                usuarios.Add(usuario);
            }

            else
            {
                throw new Exception("Usuario ya registrado");
            }

        }
        else
        {
            throw new Exception("El usuario no existe");
        }

    }

    // --- Metodo para dar de alta Proveedores ---------------------

    public void AltaProveedor(Proveedor proveedor)
    {
        if (ExisteProveedor(proveedor)) // verifico que el proveedor no exista en el sistema antes de agregarlo
        {
            throw new Exception("El proveedor ya existe en la lista");
        }
        else
        {
            proveedor.ValidarEstado();
            proveedores.Add(proveedor);
            proveedor.ToString();

        }

    }

    // ---- Metodo para corroborar que no se repite el nombre del proveedor ------------------
    public bool ExisteProveedor(Proveedor proveedor)
    {
        bool existeProveedor = false;

        foreach (var p in proveedores)
        {
            if (p.NombreProveedor == proveedor.NombreProveedor)
            {
                existeProveedor = true;
                break;
            }
        }
        return existeProveedor;
    }




    /// // -------------- Para dar de alta una agenda --------------------------------------------------------------------


    public bool ValidarEdadMinima(Huesped huesped, Actividad actividad)
    {
        bool esApto = true;
        DateTime fechaActual = DateTime.Now;

        int edad = fechaActual.Year - huesped.FechaNacimiento.Year;

        if (fechaActual.Month < huesped.FechaNacimiento.Month ||
            (fechaActual.Month == huesped.FechaNacimiento.Month && fechaActual.Day < huesped.FechaNacimiento.Day))
        {
            edad--; // Ajuste si aún no ha cumplido años en el año actual
        }

        if (actividad.EdadMinimaRequerida > edad)
        {
            esApto = false;
        }

        return esApto;
    }
   

    
    public void ActualizarCantidadDisponible(Actividad actividad)
    {
        foreach (Actividad item in actividades)
        {
            if (item.Id == actividad.Id)
            {
                item.CantidadCuposDisponibles--;
            }
        }
    }

    public void AltaAgenda(Agenda agenda)
    {
        if (agenda != null)
        {           
            ValidarEdadMinima(agenda._huesped, agenda._actividad);
            if (agenda._actividad.CantidadCuposDisponibles > 0) {                
            
                    agendas.Add(agenda);
                    ActualizarCantidadDisponible(agenda._actividad);
                    agenda.AsignarEstadoAgenda();
            }                    
        }
        else
        {
            throw new Exception("La agenda no existe");
        }
    }






    //------------- Precarga de datos ------------------------------------------------------------------
    private void PrecargaUsuario()
    {

        AltaUsuario(new Huesped("jorgeRodriguez@gmail.com", "jorgeRodriguez123", TipoDocumento.CI, "43681573", "Jorge", "Rodríguez", 102, new DateTime(1990, 06, 04), 2));
        AltaUsuario(new Huesped("martin_delcampo@hotmail.com", "del_campo555", TipoDocumento.PASAPORTE, "52613194", "Martin", "Del Campo", 25, new DateTime(1998, 02, 27), 2));
        AltaUsuario(new Operador("ricardo_operador@gmail.com", "ricardo12345", "Ricardo", "López", new DateTime(2006, 03, 13)));
        AltaUsuario(new Operador("federico_operador@gmail.com", "fede12345", "Federico", "Roque", new DateTime(2016, 10, 22)));

    }

    private void PrecargaProveedor()
    {
        AltaProveedor(new Proveedor("DreamWorks S.R.L.", "23048549", "Suarez 3380 Apto 304", 10));
        AltaProveedor(new Proveedor("Estela Umpierrez S.A.", "33459678", "Lima 2456", 7));
        AltaProveedor(new Proveedor("TravelFun", "29152020", "Misiones 1140", 9));
        AltaProveedor(new Proveedor("Rekreation S.A.", "29162019", "Bacacay 1211", 11));
        AltaProveedor(new Proveedor("Alonso & Umpierrez", "24051920", "18 de Julio 1956 Apto 4", 10));
        AltaProveedor(new Proveedor("Electric Blue", "26018945", "Cooper 678", 5));
        AltaProveedor(new Proveedor("Lúdica S.A.", "26142967", "Dublin 560", 4));
        AltaProveedor(new Proveedor("Gimenez S.R.L.", "29001010", "Andes 1190", 7));
        AltaProveedor(new Proveedor("papaNoel", "22041120", "Agraciada 2512 Apto. 1", 8));  // AL MOMENTO DE HACER LAS PRUEBAS LE ASIGNÉ UN NOMBRE, LO VUELVO A DEJAR VACÍO
        AltaProveedor(new Proveedor("Norberto Molina", "22001189", "Paraguay 2100", 9));
    }
    

    private void PrecargaActividad()
    {
        // ----------------- De Hostal ------------------------------------------------------
        AltaActividad(new ActividadHostal("Fútbol", "Partido entre huespedes", DateTime.Today, 22, 18, 250, "Alberto Martínez", "Cancha Fútbol", ActividadAlAireLibre.Si));
        AltaActividad(new ActividadHostal("Yoga", "Ejercicio relajación", new DateTime(2024, 07, 23), 16, 21, 300, "Mónica Dominguez", "Sala 3", ActividadAlAireLibre.No));
        AltaActividad(new ActividadHostal("Baile", "Clases de baile", new DateTime(2023, 11, 06), 10, 16, 450, "Manuel Ortega", "Salón de baile", ActividadAlAireLibre.No));
        AltaActividad(new ActividadHostal("kayak", "Paseo en Kayak", new DateTime(2023, 06, 15), 10, 18, 500, "Daniela Molina", "Río Conde", ActividadAlAireLibre.Si));
        AltaActividad(new ActividadHostal("Tennis", "Clases particulares de tennis", DateTime.Today, 4, 12, 680, "Mariano Pernía", "Canchas de Tennis", ActividadAlAireLibre.Si));
        AltaActividad(new ActividadHostal("Natación", "Espacio de recreación en piscina", new DateTime(2024, 03, 30), 15, 12, 850, "Victor Hugo Morales", "Piscinas del hostal", ActividadAlAireLibre.No));
        AltaActividad(new ActividadHostal("Spinning", "Sesión de spinning con profesor", new DateTime(2022, 09, 14), 9, 16, 200, "Alfredo Montes de Oca", "Gimnasio del hostal", ActividadAlAireLibre.No));
        AltaActividad(new ActividadHostal("Clases de Zumba", "Sesión de zumba con profesora", new DateTime(2024, 04, 30), 13, 18, 350, "Rosa Luna", "Gimnasio del hostal", ActividadAlAireLibre.No));
        AltaActividad(new ActividadHostal("Cine", "Noche de cine", new DateTime(2023, 12, 20), 25, 5, 500, "Carlos Reyes", "Salón de espectaculos", ActividadAlAireLibre.No));
        AltaActividad(new ActividadHostal("Paseo a Caballo", "Paseo por las sierras a caballo", new DateTime(2023, 08, 12), 6, 16, 135, "Marcos Pérez", "Predio del hostal", ActividadAlAireLibre.Si));
        AltaActividad(new ActividadHostal("Paseuuo a Caballo", "Paseo por las sierras a caballo", new DateTime(2023, 08, 12), 6, 16, 0, "Marcos Pérez", "Predio del hostal", ActividadAlAireLibre.Si));

        // ----------------- Terciarizadas ------------------------------------------------------
        AltaActividad(new ActividadTerciarizada("Alpinismo", "Escalar las sierras", new DateTime(2022, 10, 12), 5, 18, 2135, proveedores[0], ConfirmacionDeActividad.CONFIRMADA, new DateTime(2023, 10, 10)));
        AltaActividad(new ActividadTerciarizada("Paracaidismo", "Tirarse en paracaidas", new DateTime(2022, 06, 22), 3, 21, 1950, proveedores[0], ConfirmacionDeActividad.CONFIRMADA, new DateTime(2023, 06, 10)));
        AltaActividad(new ActividadTerciarizada("Pesca en bote", "Salida de pesca por el río", new DateTime(2023, 11, 02), 10, 15, 980, proveedores[0], ConfirmacionDeActividad.CONFIRMADA, new DateTime(2023, 10, 29)));
        AltaActividad(new ActividadTerciarizada("Paintball", "Contienda de Paintball", new DateTime(2023, 07, 15), 20, 18, 3200, proveedores[1], ConfirmacionDeActividad.CONFIRMADA, new DateTime(2023, 07, 10)));
        AltaActividad(new ActividadTerciarizada("Paseo por el bosque", "Un paseo por el interior del bosque", new DateTime(2023, 12, 03), 25, 8, 650, proveedores[1], ConfirmacionDeActividad.CONFIRMADA, new DateTime(2023, 11, 28)));
        AltaActividad(new ActividadTerciarizada("Concierto de rock", "Salida a evento de rock", new DateTime(2023, 09, 15), 30, 18, 2200, proveedores[1], ConfirmacionDeActividad.CONFIRMADA, new DateTime(2023, 08, 10)));
        AltaActividad(new ActividadTerciarizada("Parque de diversiones", "Salida a parque de diversiones", new DateTime(2023, 10, 10), 20, 5, 1000, proveedores[2], ConfirmacionDeActividad.CONFIRMADA, new DateTime(2023, 08, 15)));
        AltaActividad(new ActividadTerciarizada("Carrera en moto", "Evento de carreras en moto", new DateTime(2023, 11, 14), 12, 18, 3000, proveedores[2], ConfirmacionDeActividad.CONFIRMADA, new DateTime(2023, 11, 03)));
        AltaActividad(new ActividadTerciarizada("Parque acuático", "Salida al parque acuático", new DateTime(2024, 01, 12), 30, 10, 4500, proveedores[2], ConfirmacionDeActividad.CONFIRMADA, new DateTime(2023, 12, 26)));
        AltaActividad(new ActividadTerciarizada("Visita a Iglesia Matriz", "Salida a la Iglesia Matriz", new DateTime(2023, 10, 04), 25, 5, 1500, proveedores[3], ConfirmacionDeActividad.CONFIRMADA, new DateTime(2023, 09, 26)));
        AltaActividad(new ActividadTerciarizada("Salida a la playa", "Una salida en familia a la playa", new DateTime(2024, 02, 05), 35, 5, 1200, proveedores[3], ConfirmacionDeActividad.CONFIRMADA, new DateTime(2023, 12, 22)));
        AltaActividad(new ActividadTerciarizada("Casamiento Falso", "Evento en casamiento simulado", new DateTime(2023, 07, 07), 40, 18, 3500, proveedores[3], ConfirmacionDeActividad.CONFIRMADA, new DateTime(2023, 04, 30)));
        AltaActividad(new ActividadTerciarizada("Paseo en helicóptero", "Recorrido en helicóptero por la ciudad", new DateTime(2023, 08, 09), 4, 18, 6500, proveedores[4], ConfirmacionDeActividad.CONFIRMADA, new DateTime(2023, 07, 11)));
        AltaActividad(new ActividadTerciarizada("Teatro", "Salida a evento de teatro", new DateTime(2023, 11, 04), 15, 18, 2650, proveedores[4], ConfirmacionDeActividad.CONFIRMADA, new DateTime(2023, 09, 01)));
        AltaActividad(new ActividadTerciarizada("Salida al Estadio", "Salida a partido de fútbol profesional", new DateTime(2023, 10, 22), 15, 12, 5300, proveedores[4], ConfirmacionDeActividad.CONFIRMADA, new DateTime(2023, 09, 13)));
        AltaActividad(new ActividadTerciarizada("Salida al Circo", "Salida a circo de barrio", new DateTime(2023, 10, 12), 15, 12, 5300, proveedores[4], ConfirmacionDeActividad.NO_COFIRMADA, null));


    }

    
    







    //// -- Listar Detalles para Menú------------------------------------------------------------

    //// -- Opción 2 Menú---------------------------------
    //public string ListarProveedoresAlfabeticamente()
    //{
    //    ObtenerProveedores();
    //    proveedores.Sort();                  // LOS ORDENO ALFABETICAMENTE LLAMANDO AL CompareTo()
    //    string listaProveedoresOrdenada = string.Empty;
    //    foreach (var proveedor in proveedores)
    //    {
    //        listaProveedoresOrdenada += proveedor;
    //    }
    //    return listaProveedoresOrdenada.ToString();

    //}

    ////-----Opción 3 Menú --------------------------------

    //public string ListarActividadesXCostoYFecha(decimal costo, DateTime fechaInicial, DateTime fechaFinal)
    //{
    //    ObtenerActividades();
    //    actividades.Sort();
    //    string nuevaListaAct = string.Empty;
    //    foreach (var actividad in actividades)
    //    {
    //        if (fechaInicial < fechaFinal)
    //        {
    //            if (actividad.CostoDeActividad > costo && actividad.Fecha >= fechaInicial && actividad.Fecha <= fechaFinal)
    //            {
    //                nuevaListaAct += actividad; 
    //            }                
    //        }
    //        else { throw new Exception("la fecha inicial debe ser anterior a la fecha final"); }
    //    }

    //    if (string.IsNullOrEmpty(nuevaListaAct))
    //    {
    //        throw new Exception("No hay actividades que cumplan los requisitos dados");
    //    }
    //    return nuevaListaAct;
    //}

    ////-----Opción 4 Menú --------------------------------


    //public void PromocionXProveedor(Proveedor unProveedor, decimal descuento)
    //{
    //    ObtenerProveedores();

    //    bool proveedorEncontrado = false;
    //    foreach (var proveedor in proveedores)
    //    {
    //        if (proveedor.NombreProveedor == unProveedor.NombreProveedor)
    //        {
    //            if (descuento >= 0 && descuento <= 100)
    //            {
    //                proveedor.Descuento = descuento;
    //                proveedorEncontrado = true;
    //                break;
    //            }
    //            else
    //            {
    //                throw new Exception("El monto de descuento debe ser un valor entre 0 y 100 inclusive");
    //            }
    //        }

    //    }
    //    if (!proveedorEncontrado)
    //    {
    //        throw new Exception("El nombre de proveedor ingresado no coincide con ninguno de la lista");
    //    }
    //}







    //---------------------------------------------------------------------------------------------------------------

    public string ObtenerRol(string email)
    {
        var usuario = ObtenerUsuarioPorEmail(email);

            if (usuario is Huesped)
            {
                return "huesped";
            }
            else if (usuario is Operador)
            {
                return "operador";
            }
       
        return "ninguno";
    }

    public Usuario ObtenerUsuarioPorEmail(string email)
    {
        
        foreach (Usuario usuario in usuarios)
        {
            if (usuario.Email == email)
            {
                return usuario;
                
            }

        }
        return null;

    }






    public List<Actividad> ListarActividadesPorFecha(DateTime fecha)
    {
        List<Actividad> listaActividades = new List<Actividad>();
        foreach (var actividad in listaActividades)
        {
            if (actividad.Fecha == fecha)
            {
                listaActividades.Add(actividad);
            }
        }
        return listaActividades;
    }

    public Huesped ObtenerHuespedPorEmail(string email)
    {
        Huesped huesped = null;

        foreach (Usuario usuario in usuarios)
        {
            if (usuario is Huesped huespedActual && huespedActual.Email == email)
            {
                huesped = huespedActual;
                break;
            }
        }

        return huesped;
    }

 

    public Huesped ObtenerHuespedPorNombre(string nombre)
    {
        Huesped huesped = null;

        foreach (Usuario usuario in usuarios)
        {
            if (usuario is Huesped huespedActual && huespedActual.Nombre == nombre)
            {
                huesped = huespedActual;
                break;
            }
        }

        return huesped;
    }

    public Huesped ObtenerHuespedPorApellido(string apellido)
    {
        Huesped huesped = null;

        foreach (Usuario usuario in usuarios)
        {
            if (usuario is Huesped huespedActual && huespedActual.Apellido == apellido)
            {
                huesped = huespedActual;
                break;
            }
        }

        return huesped;
    }

    public List<Usuario> ObtenerHuespedes()
    {
        List<Usuario> huespedes = new List<Usuario>();

        foreach (Usuario usuario in usuarios)
        {
            if (usuario is Huesped huesped)
            {
                huespedes.Add(huesped);
            }
        }

        return huespedes;
    }

    public Actividad ObtenerActividad(int id)
    {
        Actividad actividad = null;
        foreach (Actividad item in actividades)
        {
            if (item.Id == id)
            {
                return item;

            }
        }
        return actividad;
    }

   

    public Proveedor ObtenerProveedor(string nombre)
    {
        foreach (Proveedor item in proveedores)
        {
            if (item.NombreProveedor == nombre)
            {
                return item;

            }
        }
        return null;
    }

    public Operador ObtenerOperadorPorNombre(string nombre)
    {
        Operador operador = null;

        foreach (Usuario usuario in usuarios)
        {
            if (usuario is Operador operadorActual && operadorActual.Nombre == nombre)
            {
                operador = operadorActual;
                break;
            }
        }

        return operador;
    }

    public Operador ObtenerOperadorPorEmail(string email)
    {
        Operador operador = null;

        foreach (Usuario usuario in usuarios)
        {
            if (usuario is Operador operadorActual && operadorActual.Email == email)
            {
                operador = operadorActual;
                break;
            }
        }

        return operador;
    }



    public void ActualizarDescuentoProveedor(Proveedor proveedor, decimal nuevoDescuento)
    {
        var proveedorEntidad = proveedores.Find(x => x.NombreProveedor == proveedor.NombreProveedor);
        if (proveedorEntidad is null)
        {
            throw new Exception("Proveedor no encontrado");
        }


        proveedorEntidad.Descuento = nuevoDescuento;
    }

    public List<Agenda> ListarAgendasOrdenadasPorFecha()
    {
        ObtenerAgendas();

        agendas.Sort();

        return agendas;
    }

    public List<Agenda> AgendasPorHuesped(TipoDocumento tipoDoc, string numeroDoc)
    {
        List<Agenda> listaAgendas = new List<Agenda>();

        foreach (var agenda in agendas)
        {
            if (agenda != null && agenda._huesped != null && agenda._huesped._tipoDocumento == tipoDoc && agenda._huesped.NumeroDocumento == numeroDoc)
            {
                listaAgendas.Add(agenda);
            }
        }
        return listaAgendas;
    }

    public void ConfirmarPagoAgenda(int id)
    {
        foreach (var agenda in agendas)
        {
            if (agenda.Id == id && agenda.Estado == EstadoAgenda.PENDIENTE_PAGO)
            {
                agenda.Estado = EstadoAgenda.CONFIRMADA;
            }
            
        }

    }

    public Agenda ObtenerAgendaPorId(int id)
    {
        foreach (var agenda in agendas)
        {
            if (agenda.Id == id)
            {
                return agenda;
            }
        }
        return null; // Si no se encuentra la agenda, se devuelve null
    }

    public void ConfirmarEstadoAgenda(int id)
    {
        var agenda = ObtenerAgendaPorId(id);

        if (agenda == null)
        {
            
            throw new Exception("No se encontró la agenda.");
        }

        if (agenda.Estado != EstadoAgenda.PENDIENTE_PAGO)
        {
            
            throw new Exception("La agenda no se puede confirmar porque no está en estado pendiente de pago.");
        }

        agenda.Estado = EstadoAgenda.CONFIRMADA;
        

       
    }






}
