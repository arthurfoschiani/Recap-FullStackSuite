﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Quiron.WebApi.Domains;
using Senai.Quiron.WebApi.Interfaces;
using Senai.Quiron.WebApi.Repositories;

namespace Senai.Quiron.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private IPacienteRepository PacienteRepository { get; set; }

        public PacientesController()
        {
            PacienteRepository = new PacienteRepository();
        }

        [HttpGet("{idDoutor}")]
        public IActionResult FiltrarPorDoutor(int idDoutor)
        {
            return Ok(PacienteRepository.FiltrarPorDoutor(idDoutor));
        }

        [HttpPut]
        public IActionResult Atualizar(Pacientes pacientes)
        {
            try
            {
                Pacientes PacienteBuscado = PacienteRepository.BuscarPorId(pacientes.IdPaciente);
                if (PacienteBuscado == null)
                    return NotFound();

                PacienteRepository.Atualizar(pacientes);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(PacienteRepository.Listar());
        }

        [HttpPost]
        public IActionResult Cadastrar(Pacientes pacientes)
        {
            try
            {
                PacienteRepository.Cadastrar(pacientes);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ocorreu um erro: " + ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            PacienteRepository.Deletar(id);
            return Ok();
        }
    }
}