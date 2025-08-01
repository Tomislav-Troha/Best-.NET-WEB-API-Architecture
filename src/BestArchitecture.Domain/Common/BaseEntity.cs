﻿namespace BestArchitecture.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
