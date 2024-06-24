namespace MotorcycleRental.Core.Domain.Entities;

public enum PermissionType
{
    None = 0,

    All = 1,

    CanListMotorcycles = 2,

    CanCreateMotorcycle = 3,

    CanUpdateMotorcycles = 4,

    CanDeleteMotorcycles = 5,
}
