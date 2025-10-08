namespace assessment.Application.Interfaces.ExternalWS;

public interface IAnalizaSentimientosAPI
{
    Task<T> analizarSentimientosPOST<T>(object request);
}
