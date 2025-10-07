using assessment.Application.Dtos.Professor;
using assessment.Domain.Entities;
using assessment.Utilities.Static;
using Mapster;

namespace assessment.Application.Mappings;

public class ProffessorMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Professor, ProfessorResponseDTO>()
            .Map(dest => dest.State, src => src.Status)
            .Map(dest => dest.StateDescription, src => src.Status == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();
    }
}
