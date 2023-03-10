using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    public readonly IRepository _repo;
    public readonly IMapper _mapper;

    public AlunoController(IRepository repo, IMapper mapper)
    {
        _mapper = mapper;
        _repo = repo;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var alunos = _repo.GetAllAlunos(true);        

        return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var aluno = _repo.GetAlunoById(id);
        if (aluno == null) return BadRequest("O aluno não foi encontrado.");

        var alunoDto = _mapper.Map<AlunoDto>(aluno);

        return Ok(alunoDto);
    }

    //USANDO O REPOSITORY

    [HttpPost]
    public IActionResult Post(AlunoRegistrarDto model)
    {
        var aluno = _mapper.Map<Aluno>(model);

        _repo.Add(aluno);
        if (_repo.SaveChanges())
        {
            return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
        }
        
        return BadRequest("Aluno não cadastrado");
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, AlunoRegistrarDto model)
    {
        var aluno = _repo.GetAlunoById(id);
        if (aluno == null) return BadRequest("Aluno não encontrado");
        if (aluno.Id != model.Id) return BadRequest("O id do aluno incorreto");

        _mapper.Map(model, aluno);

        _repo.Update(aluno);
        if (_repo.SaveChanges())
        {
            return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
        }
        
        return BadRequest("Aluno não alterado");
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, AlunoRegistrarDto model)
    {
        var aluno = _repo.GetAlunoById(id);
        if (aluno == null) return BadRequest("Aluno não encontrado");
        if (aluno.Id != model.Id) return BadRequest("O id do aluno incorreto");

        _mapper.Map(model, aluno);

        _repo.Update(aluno);
        if (_repo.SaveChanges())
        {
            return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
        }
        
        return BadRequest("Aluno não alterado");
    }

    [HttpDelete("{id}")]
    public IActionResult Delet(int id)
    {
        var alunoById = _repo.GetAlunoById(id);
        if (alunoById == null) return BadRequest("Aluno não encontrado");

        _repo.Delete(alunoById);
        if (_repo.SaveChanges())
        {
            return Ok("Aluno Deletado");
        }
        
        return BadRequest("Aluno não deletado");
    }
}