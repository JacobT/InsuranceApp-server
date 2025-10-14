using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Data.Models;

/// <summary>
/// Represents an entity with a primary key <see cref="Id"/> of type <see cref="int"/>.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets or sets the primary key for this entity.
    /// </summary>
    int Id { get; set; }
}
