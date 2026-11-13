using System.Globalization;
using Agenda15ds.Models;
using Agenda15ds.Views;

namespace Agenda15ds;

public partial class MainPage : ContentPage
{
    private readonly Evento _evento = new();

    public MainPage()
    {
        InitializeComponent();
        BindingContext = _evento;
    }

    private async void CadastrarEvento_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_evento.Nome) ||
            string.IsNullOrWhiteSpace(_evento.Local))
        {
            await DisplayAlert("Atenção", "Preencha o nome e o local do evento.", "OK");
            return;
        }

        if (_evento.DataTermino.Date < _evento.DataInicio.Date)
        {
            await DisplayAlert("Atenção", "A data de término não pode ser anterior à data de início.", "OK");
            return;
        }

        if (!int.TryParse(ParticipantesEntry.Text?.Trim(), out int participantes) || participantes <= 0)
        {
            await DisplayAlert("Atenção", "Informe um número de participantes maior que zero.", "OK");
            return;
        }

        var cultura = CultureInfo.GetCultureInfo("pt-BR");
        if (!decimal.TryParse(CustoEntry.Text?.Trim(), NumberStyles.Number, cultura, out decimal custo) || custo < 0)
        {
            await DisplayAlert("Atenção", "Informe um custo válido, como 50,00.", "OK");
            return;
        }

        _evento.NumeroParticipantes = participantes;
        _evento.CustoPorParticipante = custo;

        var eventoCadastrado = new Evento
        {
            Nome = _evento.Nome.Trim(),
            DataInicio = _evento.DataInicio,
            DataTermino = _evento.DataTermino,
            NumeroParticipantes = _evento.NumeroParticipantes,
            Local = _evento.Local.Trim(),
            CustoPorParticipante = _evento.CustoPorParticipante
        };

        await Navigation.PushAsync(new ResumoPage
        {
            BindingContext = eventoCadastrado
        });
    }
}
