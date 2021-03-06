﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoFinalAplicada2.Models;
using ProyectoFinalAplicada2.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ProyectoFinalAplicada2.BLL
{
    public class CacaosBLL
    {
        public static bool Guardar(Cacaos cacao)
        {
            if (!Existe(cacao.CacaoId))
                return Insertar(cacao);
            else
                return Modificar(cacao);
        }

        public static bool Insertar(Cacaos cacao)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                if (db.Cacaos.Add(cacao) != null)
                    paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Cacaos cacao)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                db.Entry(cacao).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                /*List<Entradas> entradas = EntradasBLL.GetList(e => e.CacaoId == id);

                foreach (var item in entradas)
                {
                    EntradasBLL.Eliminar(item.EntradaId);
                }*/

                var cacao = db.Cacaos.Find(id);
                db.Cacaos.Remove(cacao);
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }
        public static Cacaos Buscar(int id)
        {
            Cacaos cacao = new Cacaos();
            Contexto db = new Contexto();
            try
            {
                cacao = db.Cacaos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return cacao;
        }

        public static bool Existe(int id)
        {
            bool encontrado = false;
            Contexto db = new Contexto();

            try
            {
                encontrado = db.Cacaos.Any(c => c.CacaoId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return encontrado;
        }

        public static List<Cacaos> GetList(Expression<Func<Cacaos, bool>> cacao)
        {
            List<Cacaos> Lista = new List<Cacaos>();
            Contexto db = new Contexto();
            try
            {
                Lista = db.Cacaos.Where(cacao).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Lista;
        }

        public static bool ExisteCacao()
        {
            List<Cacaos> cacaos = GetList(c => true);

            if (cacaos.Count > 0)
                return true;
            else
                return false;
        }

        public static void ActualizarCacao(int id, decimal cantidad, decimal costo)
        {
            Cacaos cacao = Buscar(id);

            cacao.Cantidad += cantidad;
            cacao.Costo = costo;

            Modificar(cacao);
        }

        public static bool ContratarCacao(int id, decimal cantidad)
        {
            Cacaos cacao = Buscar(id);

            cacao.Cantidad -= cantidad;

            if (cacao.Cantidad >= 0)
            {
                Modificar(cacao);
                return true;
            }
            else
                return false;
        }

        public static void DevolverCacao(int id, decimal cantidad)
        {
            Cacaos cacao = Buscar(id);

            if (cacao == null)
                return;

            cacao.Cantidad += cantidad;

            Modificar(cacao);
        }

        public static bool cacaoDisponible()
        {
            List<Cacaos> cacaos = GetList(c => true);

            foreach (var item in cacaos)
            {
                if (item.Cantidad > 0)
                    return true;
            }

            return false;
        }
    }
}
