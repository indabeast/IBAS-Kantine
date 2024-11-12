using Azure.Data.Tables;
using IBAS_kantine.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Azure;

namespace IBAS_kantine.Pages
{
    public class IndexModel : PageModel
    {
        public List<MenuItem> MenuItems { get; set; }

        public void OnGet()
        {
            var tablename = "UgensMenu";
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=ibasbikeproduction51;AccountKey=3mtytlejwLNfvU2DHU+mO1cjlB4MspS3RPi5TonVGYwi0HBXnL9CUzn7jfbdZVA+3iHbKcVev5dh+ASt5ON5TA==;EndpointSuffix=core.windows.net";

            TableClient tableClient = new TableClient(connectionString, tablename);
            Pageable<TableEntity> items = tableClient.Query<TableEntity>();

            MenuItems = new List<MenuItem>();

            foreach (TableEntity item in items)
            {
                var dto = new MenuItem()
                {
                    Dag = item.RowKey,
                    Uge = item.PartitionKey,
                    VarmRet = item.GetString("VarmRet"),
                    KoldRet = item.GetString("KoldRet")
                };
                MenuItems.Add(dto);
            }
        }
    }
}