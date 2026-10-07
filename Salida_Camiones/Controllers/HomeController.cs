using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Evaluacion3.Models;

namespace Evaluacion3.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Cod_Usuario")))
        {
            return RedirectToAction("Login", "Account");
        }
        return View();
    }

    public IActionResult Chofer()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Cod_Usuario")))
        {
            return RedirectToAction("Login", "Account");
        }
        return View();
    }

    public IActionResult Camion()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Cod_Usuario")))
        {
            return RedirectToAction("Login", "Account");
        }
        return View();
    }

    public IActionResult Usuarios()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Cod_Usuario")))
        {
            return RedirectToAction("Login", "Account");
        }
        return View();
    }

    public IActionResult Salida()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Cod_Usuario")))
        {
            return RedirectToAction("Login", "Account");
        }
        return View();
    }

    public List<DatosChofer> ObtenerChoferes()
    {
        List<DatosChofer> listado = new List<DatosChofer>();
        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                var query = from c in bd.Choferes
                            join e in bd.Estados on c.Id_Estado equals e.Id_Estado
                            select new
                            {
                                c.Id_Chofer,
                                c.Nom_Chofer,
                                e.Nom_Estado,
                                c.Id_Estado
                            };
                foreach (var item in query)
                {
                    listado.Add(new DatosChofer
                    {
                        id_chofer = item.Id_Chofer,
                        nom_chofer = item.Nom_Chofer,
                        nom_estado = item.Nom_Estado,
                        id_estado = item.Id_Estado
                    });
                }
            }
        }
        catch (Exception ex)
        {
            return new List<DatosChofer> { new DatosChofer { nom_estado = ex.Message } };
        }
        return listado;
    }

    public string IngresarChofer(string nomChofer, int idEstado)
    {
        var respuesta = "";
        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                Chofer insertar = new Chofer();
                insertar.Nom_Chofer = nomChofer;
                insertar.Id_Estado = idEstado;

                bd.Choferes.Add(insertar);
                bd.SaveChanges();
                respuesta = "OK";
            }
        }
        catch (Exception ex)
        {
            respuesta = ex.Message;
        }
        return respuesta;
    }

    public string EditarChofer(int idChofer, string nomChofer, int idEstado)
    {
        var respuesta = "";
        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                var chofer = bd.Choferes.FirstOrDefault(c => c.Id_Chofer == idChofer);

                if (chofer != null)
                {
                    chofer.Nom_Chofer = nomChofer;
                    chofer.Id_Estado = idEstado;
                    bd.SaveChanges();
                    respuesta = "OK";
                }
            }
        }
        catch (Exception ex)
        {
            respuesta = ex.Message;
        }
        return respuesta;
    }

    public List<Estado> ObtenerEstados()
    {
        List<Estado> listado = new List<Estado>();
        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                listado = bd.Estados.ToList();
            }
        }
        catch (Exception ex)
        {
            return new List<Estado> { new Estado { Nom_Estado = ex.Message } };
        }
        return listado;
    }

    public List<DatosCamion> ObtenerCamiones()
    {
        List<DatosCamion> listado = new List<DatosCamion>();

        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                var query = from p in bd.Plantas
                            join c in bd.Camiones on p.Id_Planta equals c.Id_Planta
                            join e in bd.Estados on c.Id_Estado equals e.Id_Estado
                            select new
                            {
                                c.Cod_Camion,
                                p.Nom_Planta,
                                e.Nom_Estado,
                                e.Id_Estado
                            };
                foreach (var item in query)
                {
                    listado.Add(new DatosCamion
                    {
                        cod_camion = item.Cod_Camion,
                        nom_planta = item.Nom_Planta,
                        nom_estado = item.Nom_Estado,
                        id_estado = item.Id_Estado
                    });
                }
            }
        }
        catch (Exception ex)
        {
            return new List<DatosCamion> { new DatosCamion { nom_estado = ex.Message } };
        }
        return listado;
    }

    public string IngresarCamion(string codCamion, int idPlanta, int idEstado)
    {
        var respuesta = "";
        if (string.IsNullOrWhiteSpace(codCamion))
        {
            return "Debe ingresar el codigo del camion.";
        }
        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                var existe = bd.Camiones.Any(c => c.Cod_Camion == codCamion);
                if (existe)
                {
                    return "Codigo de camion ya en uso";
                }
                else
                {
                    Camion insertar = new Camion
                    {
                        Cod_Camion = codCamion,
                        Id_Planta = idPlanta,
                        Id_Estado = idEstado,
                    };

                    bd.Camiones.Add(insertar);
                    bd.SaveChanges();
                    respuesta = "OK";
                }
            }
        }
        catch (Exception ex)
        {
            respuesta = ex.Message;
        }

        return respuesta;

    }

    public string HabilitarDeshabilitar(string codCamion, int idEstado)
    {
        var respuesta = "";

        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                var camion = bd.Camiones.FirstOrDefault(c => c.Cod_Camion == codCamion);
                if (camion != null)
                {
                    if (idEstado == 1)
                    {
                        camion.Id_Estado = 2;
                    }
                    else
                    {
                        camion.Id_Estado = 1;
                    }
                }
                bd.SaveChanges();
                respuesta = "OK";
            }
        }
        catch (Exception ex)
        {
            respuesta = ex.Message;
        }
        return respuesta;
    }

    public List<Planta> ObtenerPlantas()
    {
        List<Planta> listado = new List<Planta>();

        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                listado = bd.Plantas.ToList();
            }
        }
        catch (Exception ex)
        {
            return new List<Planta> { new Planta { Nom_Planta = ex.Message } };
        }
        return listado;
    }

    public List<DatosUsuario> ObtenerUsuarios()
    {
        List<DatosUsuario> listado = new List<DatosUsuario>();

        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                var query = from u in bd.Usuarios
                            join e in bd.Estados on u.Id_Estado equals e.Id_Estado
                            select new
                            {
                                u.Cod_Usuario,
                                u.Nom_Persona,
                                e.Nom_Estado,
                                e.Id_Estado,
                            };
                foreach (var item in query)
                {
                    listado.Add(new DatosUsuario
                    {
                        cod_usuario = item.Cod_Usuario,
                        nom_persona = item.Nom_Persona,
                        nom_estado = item.Nom_Estado,
                        id_estado = item.Id_Estado
                    });
                }
            }
        }
        catch (Exception ex)
        {
            return new List<DatosUsuario> { new DatosUsuario { nom_estado = ex.Message } };
        }

        return listado;
    }

    public string IngresarUsuario(string codUsuario, string nomPersona, string password, int idEstado)
    {
        var respuesta = "";
        if (string.IsNullOrWhiteSpace(codUsuario))
        {
            return "Debe ingresar el Usuario.";
        }
        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                var existe = bd.Usuarios.Any(u => u.Cod_Usuario == codUsuario);
                if (!existe)
                {
                    Usuario insertar = new Usuario
                    {
                        Cod_Usuario = codUsuario,
                        Nom_Persona = nomPersona,
                        Password = password,
                        Id_Estado = idEstado,
                    };

                    bd.Usuarios.Add(insertar);
                    bd.SaveChanges();
                    respuesta = "OK";
                }
                else
                {
                    respuesta = "El Usuario ya existe en el sistema";
                }
            }
        }
        catch (Exception ex)
        {
            respuesta = ex.Message;
        }

        return respuesta;
    }

    public string EditarUsuario(string codUsuario, string nomPersona, int idEstado)
    {
        var respuesta = "";
        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                var usuario = bd.Usuarios.FirstOrDefault(u => u.Cod_Usuario == codUsuario);

                if (usuario != null)
                {
                    usuario.Cod_Usuario = codUsuario;
                    usuario.Nom_Persona = nomPersona;
                    usuario.Id_Estado = idEstado;
                    bd.SaveChanges();
                    respuesta = "OK";
                }
            }
        }
        catch (Exception ex)
        {
            respuesta = ex.Message;
        }
        return respuesta;
    }

    public List<DatosSalida> ObtenerSalidas()
    {
        List<DatosSalida> listado = new List<DatosSalida>();

        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                var query = from s in bd.Salidas
                            join p in bd.Plantas on s.Id_Planta equals p.Id_Planta
                            join ca in bd.Camiones on s.Cod_Camion equals ca.Cod_Camion
                            join ch in bd.Choferes on s.Id_Chofer equals ch.Id_Chofer
                            join e in bd.EstadosSalidas on s.Id_Estado_Salida equals e.Id_Estado_Salida
                            join u in bd.Usuarios on s.Cod_Usuario equals u.Cod_Usuario
                            select new
                            {
                                s.Id_Salida,
                                s.Fecha_Salida,
                                p.Nom_Planta,
                                ca.Cod_Camion,
                                ch.Nom_Chofer,
                                e.Nom_Estado_Salida,
                                u.Nom_Persona,
                                e.Id_Estado_Salida
                            };
                foreach (var item in query)
                {
                    listado.Add(new DatosSalida
                    {
                        id_salida = item.Id_Salida,
                        fecha_salida = item.Fecha_Salida,
                        nom_planta = item.Nom_Planta,
                        cod_camion = item.Cod_Camion,
                        nom_chofer = item.Nom_Chofer,
                        nom_estado_salida = item.Nom_Estado_Salida,
                        nom_persona = item.Nom_Persona,
                        id_estado_salida = item.Id_Estado_Salida
                    });
                }

            }
        }
        catch (Exception ex)
        {
            return new List<DatosSalida> { new DatosSalida { nom_estado_salida = ex.Message } };
        }
        return listado;
    }

    public string IngresarSalida(int idPlanta, string codCamion, int idChofer, string fechaSalida, string codUsuario)
    {
        var respuesta = "";
        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                Salida insertar = new Salida
                {
                    Fecha_Salida = Convert.ToDateTime(fechaSalida),
                    Id_Planta = idPlanta,
                    Cod_Camion = codCamion,
                    Id_Chofer = idChofer,
                    Cod_Usuario = codUsuario,
                    Id_Estado_Salida = 1
                };
                bd.Salidas.Add(insertar);
                bd.SaveChanges();
                respuesta = "OK";
            }
        }
        catch (Exception ex)
        {
            respuesta = ex.Message;
        }
        return respuesta;
    }

    public string RetornarSalida(int idSalida)
    {
        var respuesta = "OK";
        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                var salida = bd.Salidas.FirstOrDefault(s => s.Id_Salida == idSalida);

                if (salida != null)
                {
                    salida.Id_Estado_Salida = 2;
                    bd.SaveChanges();
                    respuesta = "OK";
                }
            }
        }
        catch (Exception ex)
        {
            respuesta = ex.Message;
        }

        return respuesta;
    }

    public string RechazarSalida(int idSalida)
    {
        var respuesta = "OK";
        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                var salida = bd.Salidas.FirstOrDefault(s => s.Id_Salida == idSalida);

                if (salida != null)
                {
                    salida.Id_Estado_Salida = 3;
                    bd.SaveChanges();
                    respuesta = "OK";
                }
            }
        }
        catch (Exception ex)
        {
            respuesta = ex.Message;
        }

        return respuesta;
    }

    public List<DatosCamion> ObtenerCamionesPorPlanta(int idPlanta)
    {
        List<DatosCamion> listado = new List<DatosCamion>();
        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                var query = from p in bd.Plantas
                            join c in bd.Camiones on p.Id_Planta equals c.Id_Planta
                            join e in bd.Estados on c.Id_Estado equals e.Id_Estado
                            where p.Id_Planta == idPlanta && c.Id_Estado == 1
                            select new
                            {
                                c.Cod_Camion,
                                p.Nom_Planta,
                                e.Nom_Estado,
                                e.Id_Estado
                            };
                foreach (var item in query)
                {
                    listado.Add(new DatosCamion
                    {
                        cod_camion = item.Cod_Camion,
                        nom_planta = item.Nom_Planta,
                        nom_estado = item.Nom_Estado,
                        id_estado = item.Id_Estado
                    });
                }
            }
        }
        catch (Exception ex)
        {
            return new List<DatosCamion> { new DatosCamion { nom_estado = ex.Message } };
        }
        return listado;
    }

    public List<DatosChofer> ObtenerChoferesHabilitados()
    {
        List<DatosChofer> listado = new List<DatosChofer>();
        try
        {
            using (EvaluacionContext bd = new EvaluacionContext())
            {
                var query = from c in bd.Choferes
                            join e in bd.Estados on c.Id_Estado equals e.Id_Estado
                            where c.Id_Estado == 1
                            select new
                            {
                                c.Id_Chofer,
                                c.Nom_Chofer,
                                e.Nom_Estado,
                                c.Id_Estado
                            };
                foreach (var item in query)
                {
                    listado.Add(new DatosChofer
                    {
                        id_chofer = item.Id_Chofer,
                        nom_chofer = item.Nom_Chofer,
                        nom_estado = item.Nom_Estado,
                        id_estado = item.Id_Estado
                    });
                }
            }
        }
        catch (Exception ex)
        {
            return new List<DatosChofer> { new DatosChofer { nom_estado = ex.Message } };
        }
        return listado;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

