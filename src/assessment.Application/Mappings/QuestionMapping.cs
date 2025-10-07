using assessment.Application.Dtos.Question;
using assessment.Domain.Entities;
using assessment.Utilities.Static;
using Mapster;

namespace assessment.Application.Mappings;

public class QuestionMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Question, QuestionResponseDTO>()
            .Map(dest => dest.State, src => src.Status)
            .Map(dest => dest.StateDescription, src => src.Status == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();
    }
}
