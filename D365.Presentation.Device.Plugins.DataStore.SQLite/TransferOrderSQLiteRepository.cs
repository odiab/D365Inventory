namespace D365.Presentation.Device.Plugins.DataStore.SQLite;

public class TransferOrderSQLiteRepository(LocalDbContext context) : ITransferOrderRepository
{
    #region SELECT

    public async Task<TransferOrderHeaderEntity?> SelectAsync(Guid? id)
        => await context.TransferOrders!
            .Include(i => i.Lines)
            .Where(q => q.Id == id)
            .FirstOrDefaultAsync();

    public async Task<TransferOrderHeaderEntity[]> SelectAsync()
        => await context.TransferOrders!.ToArrayAsync();


    #endregion

    #region INSERT
    public async Task<string?> AddAsync(TransferOrderHeaderEntity? entity)
    {
        if (entity == null)
        {
            return "Transfer order can not be empty or null.";
        }

        try
        {
            await context.TransferOrders!.AddAsync(entity);
            await context.SaveChangesAsync();

            return null;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public async Task<string?> AddAsync(TransferOrderHeaderEntity[]? entities)
    {
        if (entities == null)
        {
            return "Transfer orders can not be empty or null.";
        }

        try
        {
            List<TransferOrderHeaderEntity>? ordersToAdd = null;
            foreach (var entity in entities)
            {
                var isExists = await
                    context.TransferOrders!.AnyAsync(q =>
                        q.TransferOrderNumber == entity.TransferOrderNumber);
                if (isExists)
                {
                    continue;
                }

                ordersToAdd ??= [];
                ordersToAdd.Add(entity);
            }

            if (ordersToAdd is null)
            {
                return null;
            }

            await context.TransferOrders!.AddRangeAsync(ordersToAdd);
            await context.SaveChangesAsync();

            return null;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    #endregion

    #region INSERTORUPDATE

    public async Task<string?> AddOrUpdateAsync(TransferOrderHeaderEntity[]? entities)
    {
        if (entities == null)
        {
            return "Transfer orders can not be empty or null.";
        }

        try
        {
            foreach (var entity in entities)
            {
                var product = await context.TransferOrders!.FirstOrDefaultAsync(q =>
                    q.DataAreaId == entity.DataAreaId && q.TransferOrderNumber == entity.TransferOrderNumber);
                if (product != null)
                {
                    //context.TransferOrders!.Update(entity);
                    continue;
                }

                if (product == null)
                {
                    await context.TransferOrders!.AddAsync(entity);
                }
            }

            await context.SaveChangesAsync();
            return null;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    #endregion

    #region UPDATE

    public async Task<string?> UpdateStatusAsync(string? transferOrderNumber, string orderStatus = "Received")
    {
        if (string.IsNullOrEmpty(transferOrderNumber))
        {
            return "Transfer order number can not be empty or null.";
        }

        try
        {
            var order = await context.TransferOrders!.FirstOrDefaultAsync(q =>
                q.TransferOrderNumber == transferOrderNumber);
            if (order is null)
            {
                return "Transfer order can not be found.";
            }


            order.TransferOrderStatus = orderStatus;
            await context.SaveChangesAsync();

            return null;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    #endregion


}