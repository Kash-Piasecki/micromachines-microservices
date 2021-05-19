using System;

namespace MMProducts.Data
{
    public record ProductReadDto(Guid Id, string Name, decimal Price);

    public record ProductUpsertDto(string Name, decimal Price);
}