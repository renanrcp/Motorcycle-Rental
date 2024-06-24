using MotorcycleRental.Core.Application.Errors;

namespace MotorcycleRental.Deliverers.Application.Commands.Deliverers.Create;

public static class CreateDelivererErrors
{
    public static readonly UnprocessableEntityError CnhExists = new("CreateDeliverer.Cnh.Duplicated", "Já existe um entregador com essa CNH.");

    public static readonly UnprocessableEntityError CnpjExists = new("CreateDeliverer.Cnpj.Duplicated", "Já existe um entregador com esse CNPJ.");
}
