﻿namespace VPBot.DataObjects.Contracts.Core
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}
