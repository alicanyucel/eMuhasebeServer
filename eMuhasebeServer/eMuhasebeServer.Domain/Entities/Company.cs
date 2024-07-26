﻿
using eMuhasebeServer.Domain.Abstractions;
using eMuhasebeServer.Domain.VaueObjects;

namespace eMuhasebeServer.Domain.Entities;

public sealed class Company : Entity
{
    public string Name { get; set; } = string.Empty;
    public string FullAddress { get; set; } = string.Empty;
    public Database Database { get; set; } = new(string.Empty, string.Empty, string.Empty, string.Empty);
}