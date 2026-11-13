using System;

namespace Agenda15ds.Models;

public class Evento
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; } = DateTime.Today;
    public DateTime DataTermino { get; set; } = DateTime.Today;
    public int NumeroParticipantes { get; set; }
    public string Local { get; set; } = string.Empty;
    public decimal CustoPorParticipante { get; set; }

    public TimeSpan DiferencaDatas => DataTermino.Date - DataInicio.Date;
    public int DuracaoDias => DiferencaDatas.Days;
    public decimal CustoTotal => NumeroParticipantes * CustoPorParticipante;
}
