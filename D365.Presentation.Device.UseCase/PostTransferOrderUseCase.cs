namespace D365.Presentation.Device.UseCase;

public class PostTransferOrderUseCase(
    IMapper mapper,
    ID365Repository repository,
    ITransferOrderRepository transferOrderRepository) : IPostTransferOrderUseCase
{
    public async Task<string?> ExecuteAsync(string baseUrl,
        D365TransferOrderHeaderRecord record)
    {
        var headerResult = await repository.OnPostTransferOrderHeaderAsync(baseUrl, record);
        if (headerResult is not { Succeeded: true })
        {
            if (headerResult.Errors != null) return string.Join(Environment.NewLine, headerResult.Errors);
        }

        var lineResult = await repository.OnPostTransferOrderLinesAsync(baseUrl,
            headerResult.Data?.TransferOrderNumber,
            record.Lines);

        if (lineResult is not { Succeeded: true })
        {
            if (lineResult.Errors != null) return string.Join(Environment.NewLine, lineResult.Errors);
        }

        if (headerResult.Data == null)
        {
            return "Unknown error!";
        }


        var data = headerResult.Data with { Lines = lineResult.Data };
        var order = mapper.Map<TransferOrderHeaderEntity>(data);


        return await transferOrderRepository.AddAsync(order);
    }
}