namespace SmartSchool.WebAPI.Dtos;

public class AlunoRegistrarDto
{
    public int Id { get; set; }
    public int Matricula { get; set; }

    //Informações do aluno
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Telefone { get; set; }
    public DateTime DataNascimento { get; set; }

    public DateTime DataInicio { get; set; } = DateTime.Now;
    public DateTime? DataFim { get; set; } = null;

    public bool Ativo { get; set; } = true;
}
