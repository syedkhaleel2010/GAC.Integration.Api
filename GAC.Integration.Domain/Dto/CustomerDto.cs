﻿namespace GAC.Integration.Domain.Dto
{
    public record CustomerDto
    {
        public int CustomerId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Address { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}
