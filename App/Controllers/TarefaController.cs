using App.DTO;
using AutoMapper;
using Dominio.Core.Models.Tarefas;
using Dominio.Core.Services.Notificador;
using Dominio.Core.Services.TarefasService;
using Dominio.Core.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.Controllers
{
    [Route("api/[controller]")]
    public class TarefaController : MainController
    {
        protected readonly ITarefaService _tarefaService;
        protected readonly IMapper _mapper;
        protected readonly IUser _user;
    

        public TarefaController(ITarefaService tarefaService,
            IMapper mapper, INotificador notificador,
            IUser usuario) : base(notificador, usuario)
        {

            _tarefaService = tarefaService;
            _mapper = mapper;
            _user = usuario;
        }

        // GET: api/<TarefaController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var consulta = await _tarefaService.GetAllAsync();
            var tarefa = _mapper.Map<IEnumerable<TarefaDto>>(consulta);
            return CustomResponse(tarefa);

        }

        // GET api/<TarefaController>/5
        [HttpGet("{id:Guid}")]
        public Task<Tarefa> GetById(Guid id)
        {
            return _tarefaService.GetById(id);
        }



        // POST api/<TarefaController>
        [HttpPost]
        // [ClaimsAuthorize("Gerencia", "Adicionar")]
        public async Task<IActionResult> Post(Tarefa tarefa)
        {
            if (UsuarioLogado())
            {
                await _tarefaService.InsertAsync(tarefa);
                return CustomResponse(tarefa);
            }
            else
            {
                return CustomResponse();
            }

        }

        // PUT api/<TarefaController>/5
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Put(Guid id, TarefaDto tarefa)
        {

            if (tarefa.Id != id) return BadRequest();

            if (UsuarioLogado())
            {
                var tarefaupdate = _mapper.Map<Tarefa>(tarefa);
                await _tarefaService.UpdateAsync(tarefaupdate);
                return CustomResponse();
            }
            else { 

            return CustomResponse();

            }
        }


        // DELETE api/<TarefaController>/5
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id, TarefaDto tarefa)
        {
            if (tarefa.Id != id) return CustomResponse();

            if (UsuarioLogado())
            {
                var tarefaDel = _mapper.Map<Tarefa>(tarefa);
                await _tarefaService.DeleteAsync(tarefaDel);

                return CustomResponse();
            }
            else
            {
                return CustomResponse();

            }
        }


    }
}
