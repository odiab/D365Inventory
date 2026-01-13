namespace D365.Presentation.Device.Core.Records;

public record KeyValuePairRecord(string? Key, string? Value);

public record ResultRecord(bool Succeeded = true, string[]? Errors = null);

public record ResultRecord<TRecord>(bool Succeeded = true, string[]? Errors = null, TRecord? Data = null)
    where TRecord : class;

public record ResultsRecord<TRecord>(bool Succeeded = true, string[]? Errors = null, TRecord[]? Data = null)
    where TRecord : class;

public record ResultRecord<TRecord, TKey>(bool Succeeded = true, string[]? Errors = null, TRecord? Data = null)
    where TRecord : RecordBase<TKey>;

public record ResultsRecord<TRecord, TKey>(bool Succeeded = true,
    string[]? Errors = null,
    IList<TRecord>? Data = null)
    where TRecord : RecordBase<TKey>;

public record DataTablePaginationRecord<TRecord>(string? Draw,
    int RecordsFiltered,
    int RecordsTotal,
    IList<TRecord>? Data);

public record PaginationRecord(string? Draw,
    string? Query,
    string? SortColumn,
    string? SortDirection,
    int? Skip = 0,
    int? Take = 10);

public record RecordBase<T>(T? Id = default);